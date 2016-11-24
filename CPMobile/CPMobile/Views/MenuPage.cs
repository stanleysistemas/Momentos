using Xamarin.Forms;
using CPMobile.Helper;
using Moments.Views;

namespace CPMobile.Views
{
    public class MenuTableView :TableView
    {

    }
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }
        RootPage rootPage;
        TableView tableView;

        public MenuPage(RootPage rootPage)
        {
            Icon = "menu.png";
            Title = "menu"; // The Title property must be set.

            this.rootPage = rootPage;

            var logoutButton = new Button { Text = "Logout", TextColor=Color.White };
            logoutButton.Clicked += (sender, e) =>
            {
                Settings.AuthLoginToken = "";
                
                Navigation.PushModalAsync(new LoginPage());

                //Special Handel for Android Back button
                if (Device.OS == TargetPlatform.Android)
                Application.Current.MainPage = new LoginPage();
            };
            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#FF9800"),
            };
            var section = new TableSection()
            {
                new MenuCell {Text = "Mapa",Host= this,ImageSrc="menu_venue.png"},
                new MenuCell {Text = "Lugares",Host= this,ImageSrc="menu_current_trip.png"},
                new MenuCell {Text = "Conversa",Host= this,ImageSrc="menu_sponsors.png"},
                new MenuCell {Text = "Configurações",Host= this,ImageSrc="menu_settings.png"},
                new MenuCell {Text = "Sobre",Host= this,ImageSrc="menu_profile.png"},
            };
            
            var root = new TableRoot() { section };

            tableView = new MenuTableView()
            {
                Root = root,
                Intent = TableIntent.Data,
                //BackgroundColor = Color.FromHex("2C3E50"),
                BackgroundColor = Color.White,

            };

            var settingView = new SettingsUserView();

            //settingView.tapped += (object sender, TapViewEventHandler e) =>
            //{

            //    Navigation.PushAsync(new Profile());
            //    // var home = new NavigationPage(new Profile());
            //    // rootPage.Detail = home;
            //};

            layout.Children.Add(settingView);
            //layout.Children.Add(new BoxView()
            //{
            //    HeightRequest = 1,
            //    BackgroundColor = AppStyle.DarkLabelColor,
            //});
            layout.Children.Add(tableView);
            layout.Children.Add(logoutButton);

            Content = layout;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped +=
                (sender, e) =>
                {
                    NavigationPage profile = new NavigationPage(new Profile(settingView.profileViewModel.myProfile)) { BarBackgroundColor = App.BrandColor,BarTextColor = Color.White };
                    rootPage.Detail = profile;
                    rootPage.IsPresented = false;
                };
            settingView.GestureRecognizers.Add(tapGestureRecognizer);
 
        }

        //NavigationPage home, About, favorites;

        NavigationPage mapa, lugares, conversa,configuracoes,sobre;
        public void Selected(string item)
        {

            switch (item)
            {
                case "Mapa":

                    sobre = new NavigationPage(new MapaPage()) { BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                    rootPage.Detail = mapa;
                    break;

                case "Lugares":


                    break;
                case "Conversa":


                    break;
                case "Configuração":


                    break;

               


                    //case "Home":
                    //    if (home == null)
                    //        home = new NavigationPage(new MainListPage()) { BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                    //    rootPage.Detail = home;
                    //    break;
                    //case "Favorites":
                    //    favorites = new NavigationPage(new FavoriteListPage()) { BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                    //    rootPage.Detail = favorites;
                    //    break;

                    case "Sobre":
                    sobre = new NavigationPage(new AboutPage()) { BarBackgroundColor = App.BrandColor, BarTextColor = Color.White };
                        rootPage.Detail = sobre;
                        break;
            };
            rootPage.IsPresented = false;  // close the slide-out
        }

    }

    
}
