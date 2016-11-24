using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CPMobile.Helper;
using CPMobile.Models;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading;
using System.Text;
using CPMobile.Service;
using Xamarin.Forms;
using Akavache;
using Moments.Models;
using Newtonsoft.Json;


[assembly: Dependency(typeof(CPFeedService))]
namespace CPMobile.Service
{
    public class CPFeedService :ICPFeeds
    {
        bool initialized = false;

        static string clientId = "WRymObjweyg9fj78Z5FV3R-uHeoVt_Oh";
        static string clientSecret = "NQyjvo7N7eN06Xu9nTHm4jRt0X7IZThNwPAKVnp9GBcOZKm89xIOhbeOIQrOXVJj";
        static string baseUrl = "https://aspnetidentitywebapi20161025090604.azurewebsites.net/";

        private static string azure =
            "Endpoint=sb://mobilex.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=KI/QBg3Jw2HqXZtMvDa+U19QeEQj73HTStxu111AsEk=";

        private static string azurefull =
            "Endpoint=sb://mobilex.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=B37fOl6lMSDjWY/bbwbyPX78jXso4vLo+E8lbYoaevs=";
        public CPFeedService()
        {

        }


        //public async Task<> IncluirUsuario(string email, string username, string firstname, string lastname, string password, string confirmpassword)
        //{

        //    var client =
        //        new RestClient("https://aspnetidentitywebapi20161025090604.azurewebsites.net/");

        //    var request = new RestRequest("api/account/create", Method.POST);



        //    request.RequestFormat = DataFormat.Json;

        //    request.AddParameter("Email", email);
        //    request.AddParameter("Username", username);
        //    request.AddParameter("FirstName", firstname);
        //    request.AddParameter("LastName", lastname);
        //    request.AddParameter("Password", password);
        //    request.AddParameter("ConfirmPassword", confirmpassword);


        //    try
        //    {
        //        var response = client.Execute<Usuario>(request);
        //    }
        //    catch (Exception ex)
        //    {

        //        initialized = false;
        //        return;
        //    }
        //    return null;
        //}

        public async Task<bool> PostIncluirUsuario(string email, string username, string firstname, string lastname, string password, string confirmpassword)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                // We want the response to be JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                // Build up the data to POST.
                // List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();

               string usuariopostData;


               // List<Usuario> usuario = new List<Usuario>();

                var usuario = new Usuario
                {
                    Email    = email,
                    UserName = username,
                    FirstName = firstname,
                    LastName = lastname,
                    Password = password,
                    ConfirmPassword = confirmpassword

                };

                
                //postData.Add(new KeyValuePair<string, string>("Email", email));
                //postData.Add(new KeyValuePair<string, string>("Username", username));
                //postData.Add(new KeyValuePair<string, string>("FirstName", firstname));
                //postData.Add(new KeyValuePair<string, string>("LastName", lastname));
                //postData.Add(new KeyValuePair<string, string>("Password", password));
                //postData.Add(new KeyValuePair<string, string>("ConfirmPassword", confirmpassword));

                

               //  FormUrlEncodedContent content = new FormUrlEncodedContent(postData);


                // Post to the Server and parse the response.



                try
                {
                    var json = JsonConvert.SerializeObject(usuario);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("api/account/create", content);
                    response.EnsureSuccessStatusCode();
                    //var mes = response.RequestMessage.ToString();

                    string jsonString = response.Content.ReadAsStringAsync().Result;

                    
                    Usuario responseData = JsonHelper.Deserialize<Usuario>(jsonString);

                    return true;
                    // return the Access Token.
                    //return responseData.ToString();
                }
                catch (Exception ex)
                {

                    initialized = false;
                    return false;
                }
                return false;

            }
        }

        public async Task<bool> GetAccessToken(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                // We want the response to be JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               
                // Build up the data to POST.
                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
                
                postData.Add(new KeyValuePair<string, string>("grant_type", "password"));
                postData.Add(new KeyValuePair<string, string>("username", username));
                postData.Add(new KeyValuePair<string, string>("password", password));
                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
                // Post to the Server and parse the response.
                try
                {
                    var response = await client.PostAsync("oauth/token", content);
                    response.EnsureSuccessStatusCode();
                    string jsonString = response.Content.ReadAsStringAsync().Result;

                    //object responseData = JsonConvert.DeserializeObject(jsonString);
                    Login responseData = JsonHelper.Deserialize<Login>(jsonString);

                    Settings.AuthLoginToken = responseData.access_token;


                    return true;
                    // return the Access Token.
                    //return responseData.ToString();
                }
                catch (Exception ex)
                {

                    initialized = false;
                    return false;
                }
                return false;

            }
        }

        public async Task<MyProfile> GetMyProfile(string username, string password)
        {
            var loginToken = Settings.AuthLoginToken;

            if (!string.IsNullOrEmpty(loginToken))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();

                    postData.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + loginToken));
                    postData.Add(new KeyValuePair<string, string>("username", username));
                    postData.Add(new KeyValuePair<string, string>("password", password));
                    FormUrlEncodedContent content = new FormUrlEncodedContent(postData);

                    HttpResponseMessage response = await client.GetAsync("api/account/user/" + username);

                    string jsonString = await response.Content.ReadAsStringAsync();
                    MyProfile responseData = JsonHelper.Deserialize<MyProfile>(jsonString);

                    return responseData;
                }
            }
            else
            {
                return null;
            }
            //throw new NotImplementedException();
        }

        //public async Task<MyProfile> GetMyProfile(string username, string password)
        //{
        //    var loginToken = Settings.AuthLoginToken;

        //    if (!string.IsNullOrEmpty(loginToken))
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(baseUrl);
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            // Add the Authorization header with the AccessToken.
        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + loginToken);
        //            client.DefaultRequestHeaders.Add("username", username);
        //            client.DefaultRequestHeaders.Add("password", password);

        //            // create the URL string.
        //            string url = string.Format("api/account/user/" + username);

        //            // make the request
        //            HttpResponseMessage response = await client.GetAsync(url);
        //            // make the request


        //            // parse the response and return the data.
        //            string jsonString = await response.Content.ReadAsStringAsync();
        //            MyProfile responseData = JsonHelper.Deserialize<MyProfile>(jsonString);


        //            //await BlobCache.LocalMachine.InsertObject<MyProfile>("DefaultArticle", responseData, DateTimeOffset.Now.AddDays(1));
        //            // await dataStorageService.Save_Value("MyProfile", responseData);

        //            return responseData;
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //    //throw new NotImplementedException();
        //}

        public async Task Init()
        {
            initialized = true;
            if (!String.IsNullOrEmpty(Settings.AuthToken))
            {
                GetArticleAsync(1);
                return;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                // We want the response to be JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Build up the data to POST.
                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                postData.Add(new KeyValuePair<string, string>("client_id", clientId));
                postData.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
                // Post to the Server and parse the response.
                try
                {
                    var response = await client.PostAsync("Token", content);
                    response.EnsureSuccessStatusCode();
                    string jsonString = response.Content.ReadAsStringAsync().Result;
                    CPAuthToken responseData = JsonHelper.Deserialize<CPAuthToken>(jsonString);

                    Settings.AuthToken = responseData.access_token;
                    
                    GetArticleAsync(1);
                    // return the Access Token.
                    //return responseData.ToString();
                }
                catch (Exception ex)
                {

                    initialized = false;
                }

            }
        }

        public async Task<CPFeed> GetArticleAsync(int page)
        {
            if (!initialized)
                await Init();
            var accessToken = Settings.AuthToken;

            if (!string.IsNullOrEmpty(accessToken))
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken.
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    // create the URL string.
                    string url = string.Format("v1/Articles?page={0}", page);

                    // make the request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // parse the response and return the data.
                    string jsonString = await response.Content.ReadAsStringAsync();
                    CPFeed responseData = JsonHelper.Deserialize<CPFeed>(jsonString);
                    await BlobCache.LocalMachine.InsertObject<CPFeed>("DefaultArticle", responseData, DateTimeOffset.Now.AddDays(1));
                    return responseData;
                }
            }
            else
            {
                Init();
                return null;
            }
        }

        public async Task<CPFeed> GetForumAsync(int tag)
        {
            
            var accessToken = Settings.AuthToken;

            if (!string.IsNullOrEmpty(accessToken))
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken.
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    // create the URL string.
                    string url = string.Format("v1/Forum/{0}/Messages?page=1", tag);

                    // make the request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // parse the response and return the data.
                    string jsonString = await response.Content.ReadAsStringAsync();
                    CPFeed responseData = JsonHelper.Deserialize<CPFeed>(jsonString);
                    //await BlobCache.LocalMachine.InsertObject<CPFeed>("DefaultForum", responseData, DateTimeOffset.Now.AddDays(1));
                    return responseData;
                }
            }
            else
            {
                
                return null;
            }
        }

        public Task<CPFeed> GetForumAsync()
        {
            throw new NotImplementedException();
        }


       

       

        /// <summary>
        /// Gets the page of Articles.
        /// </summary>
        /// <param name="page">The page to get.</param>
        /// <returns>The page of articles.</returns>
        public async Task<CPFeed> MyArticles(int page)
        {
            var loginToken = Settings.AuthLoginToken;
            if (!string.IsNullOrEmpty(loginToken))
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken.
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + loginToken);

                    // create the URL string.
                    string url = string.Format("v1/My/Articles?page={0}", page);

                    // make the request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // parse the response and return the data.
                    string jsonString = await response.Content.ReadAsStringAsync();
                    CPFeed responseData = JsonHelper.Deserialize<CPFeed>(jsonString);
                    await BlobCache.LocalMachine.InsertObject<CPFeed>("MyArticle", responseData, DateTimeOffset.Now.AddDays(3));
                    
                    return responseData;
                }
            }
            else
            {

                return null;
            }
        }

        public async Task<CPFeed> MyMessage(int page)
        {
            var loginToken = Settings.AuthLoginToken;
            if (!string.IsNullOrEmpty(loginToken))
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken.
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + loginToken);

                    // create the URL string.
                    string url = string.Format("v1/My/Messages?page={0}", page);

                    // make the request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // parse the response and return the data.
                    string jsonString = await response.Content.ReadAsStringAsync();
                    CPFeed responseData = JsonHelper.Deserialize<CPFeed>(jsonString);
                    await BlobCache.LocalMachine.InsertObject<CPFeed>("MyMessage", responseData, DateTimeOffset.Now.AddDays(3));

                    return responseData;
                }
            }
            else
            {

                return null;
            }
        }

        public async Task<CPFeed> MyTips(int page)
        {
            var loginToken = Settings.AuthLoginToken;
            if (!string.IsNullOrEmpty(loginToken))
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken.
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + loginToken);

                    // create the URL string.
                    string url = string.Format("v1/My/tips?page={0}", page);

                    // make the request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // parse the response and return the data.
                    string jsonString = await response.Content.ReadAsStringAsync();

                    CPFeed responseData = JsonHelper.Deserialize<CPFeed>(jsonString);
                    await BlobCache.LocalMachine.InsertObject<CPFeed>("MyTips", responseData, DateTimeOffset.Now.AddDays(3));

                    return responseData;
                }
            }
            else
            {

                return null;
            }
        }

        public async Task<CPFeed> MyBlogs(int page)
        {
            var loginToken = Settings.AuthLoginToken;
            if (!string.IsNullOrEmpty(loginToken))
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken.
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + loginToken);

                    // create the URL string.
                    string url = string.Format("v1/My/blogposts?page={0}", page);

                    // make the request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // parse the response and return the data.
                    string jsonString = await response.Content.ReadAsStringAsync();
                    CPFeed responseData = JsonHelper.Deserialize<CPFeed>(jsonString);
                    await BlobCache.LocalMachine.InsertObject<CPFeed>("MyBlogs", responseData, DateTimeOffset.Now.AddDays(3));

                    return responseData;
                }
            }
            else
            {

                return null;
            }
        }

        public async Task<CPFeed> MyComments(int page)
        {
            var loginToken = Settings.AuthLoginToken;
            if (!string.IsNullOrEmpty(loginToken))
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken.
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + loginToken);

                    // create the URL string.
                    string url = string.Format("v1/My/questions?page={0}", page);

                    // make the request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // parse the response and return the data.
                    string jsonString = await response.Content.ReadAsStringAsync();
                    CPFeed responseData = JsonHelper.Deserialize<CPFeed>(jsonString);
                    await BlobCache.LocalMachine.InsertObject<CPFeed>("MyComments", responseData, DateTimeOffset.Now.AddDays(3));

                    return responseData;
                }
            }
            else
            {

                return null;
            }
        }

    }
}

