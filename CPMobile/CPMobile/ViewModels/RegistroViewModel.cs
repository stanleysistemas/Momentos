using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMobile.Models;
using CPMobile.Views;
using Xamarin.Forms;

namespace CPMobile.ViewModels
{
    public class RegistroViewModel : BaseViewModel
    {
        readonly ICPFeeds cpFeeds;


        public RegistroViewModel(Page page)
            : base(page)
        {
            this.cpFeeds = DependencyService.Get<ICPFeeds>();
        }

        public const string RegistroCommandPropertyName = "RegistroCommand";
        public const string LoginCommandPropertyName = "LoginCommand";

        Command loginCommand;
        Command registroCommand;

        public Command RegistroCommand
        {
            get
            {
                return registroCommand ??
                    (registroCommand = new Command(async () => await ExecuteRegistroCommand()));
            }
        }

        public Command LoginCommand
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new Command(async () => await ExecuteLoginCommand()));
            }
        }

        private async Task ExecuteRegistroCommand()
        {

            bool isRegistroSuccess = false;
            if (IsBusy)
                return;

            IsBusy = true;
            registroCommand.ChangeCanExecute();

            try
            {

              //  isRegistroSuccess = await cpFeeds.GetAccessToken(this.UserName, this.Password);

                isRegistroSuccess =
                    await
                        cpFeeds.PostIncluirUsuario(this.Email, this.UserName, this.FirstName, this.LastName,
                            this.Password, this.ConfirmPassword);


            }
            catch (Exception ex)
            {
                isRegistroSuccess = false;
            }

            finally
            {
                IsBusy = false;
                registroCommand.ChangeCanExecute();
            }

            if (isRegistroSuccess)
            {
                await page.Navigation.PushModalAsync(new RootPage());
                if (Device.OS == TargetPlatform.Android)
                    Application.Current.MainPage = new RootPage();
            }
            else
            {
                await page.DisplayAlert("Erro no Registro", "Erro ao tentar registrar! por favor tente novamente", "Ok");
            }
        }

        private async Task ExecuteLoginCommand()
        {
            await page.Navigation.PushModalAsync(new LoginPage());
            if (Device.OS == TargetPlatform.Android)
                Application.Current.MainPage = new LoginPage();
        }

        string username = string.Empty;
        public const string UsernamePropertyName = "UserName";
        public string UserName
        {
            get { return username; }
            set { SetProperty(ref username, value, UsernamePropertyName); }
        }

        string password = string.Empty;
        public const string PasswordPropertyName = "Password";
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value, PasswordPropertyName); }
        }

        string confirmpassword = string.Empty;
        public const string ConfirmPasswordPropertyName = "ConfirmPassword";
        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set { SetProperty(ref confirmpassword, value, ConfirmPasswordPropertyName); }
        }

        string email = string.Empty;
        public const string EmailPropertyName = "Email";
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value, EmailPropertyName); }
        }

        string firstname = string.Empty;
        public const string FirstnamePropertyName = "FirstName";
        public string FirstName
        {
            get { return username; }
            set { SetProperty(ref firstname, value, FirstnamePropertyName); }
        }

        string lastname = string.Empty;
        public const string LastnamePropertyName = "LastName";
        public string LastName
        {
            get { return lastname; }
            set { SetProperty(ref lastname, value, LastnamePropertyName); }
        }


    }
}
