﻿using CPMobile.ViewModels;
using Xamarin.Forms;

namespace CPMobile.Views
{
    public class LoginPage : ContentPage
    {
        LoginViewModel loginViewModel;
        public LoginPage()
        {
            BindingContext = loginViewModel = new LoginViewModel(this);
            var activityIndicator = new ActivityIndicator
            {
                Color = Color.FromHex("#8BC34A"),
            };
            activityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            //BackgroundColor = Color.White;
            BackgroundImage = "LoginScreen.png";
            var layout = new StackLayout { Padding = 20 };
            layout.Children.Add(activityIndicator);
            var label = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Center,

            };


            var backgroundImage = new Image()
            {
                Aspect = Aspect.Fill,
                BackgroundColor = Color.White,//Color.FromHex("#00a3f5")
                // Source = "" //FileImageSource.FromFile("header1.png")
                 Source = FileImageSource.FromFile("LoginScreen.png")
            };


            layout.Children.Add(label);

            var username = new Entry { Placeholder = "Nome Usuário", TextColor=Color.Black };
            username.SetBinding(Entry.TextProperty, LoginViewModel.UsernamePropertyName);
            layout.Children.Add(username);

            var password = new Entry { Placeholder = "Senha", IsPassword = true, TextColor = Color.Black };
            password.SetBinding(Entry.TextProperty, RegistroViewModel.PasswordPropertyName);
            layout.Children.Add(password);

            var relativelayout = new RelativeLayout();

            var button = new Button { Text = "Logar", TextColor = Color.White, BackgroundColor = Color.FromHex("#8BC34A") };
            button.SetBinding(Button.CommandProperty, LoginViewModel.LoginCommandPropertyName);

            var btnRegistro = new Button { Text = "Registrar", TextColor = Color.White, BackgroundColor = Color.FromHex("#8BC34A") };
            btnRegistro.SetBinding(Button.CommandProperty, LoginViewModel.RegistroCommandPropertyName);

            layout.Children.Add(button);
            layout.Children.Add(btnRegistro);
            relativelayout.Children.Add(backgroundImage,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.RelativeToParent((parent) => { return parent.Height; }));

            relativelayout.Children.Add(layout,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.RelativeToParent((parent) => { return parent.Height; }));


            ////button.Clicked += (sender, e) => {
            ////    if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text))
            ////    {
            ////        DisplayAlert("Validation Error", "Username and Password are required", "Re-try");
            ////    } else {
            ////        // REMEMBER LOGIN STATUS!
            ////        App.Current.Properties["IsLoggedIn"] = true;


            ////        //ilm.ShowRootPage();

            ////    }
            ////};

            Content = new ScrollView { Content = relativelayout };




        }
    }
}
