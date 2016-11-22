using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMobile.ViewModels;
using Xamarin.Forms;

namespace CPMobile.Views
{
    public class RegistroPage : ContentPage
    {
        RegistroViewModel registroViewModel;

        public RegistroPage()
        {
            BindingContext = registroViewModel = new RegistroViewModel(this);
            var activityIndicator = new ActivityIndicator
            {
                Color = Color.FromHex("#8BC34A"),
            };
            activityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
           // BackgroundColor = Color.White;
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
                //BackgroundColor = Color.FromHex("#00a3f5")
                Source =  FileImageSource.FromFile("LoginScreen.png")
            };

            layout.Children.Add(label);


            var username = new Entry { Placeholder = "Nome Usuário", TextColor = Color.Black };
            username.SetBinding(Entry.TextProperty, LoginViewModel.UsernamePropertyName);
            layout.Children.Add(username);

            var password = new Entry { Placeholder = "Senha", IsPassword = true, TextColor = Color.Black };
            password.SetBinding(Entry.TextProperty, RegistroViewModel.PasswordPropertyName);
            layout.Children.Add(password);

            var confirmpassword = new Entry { Placeholder = "Confirme Senha", IsPassword = true, TextColor = Color.Black };
            confirmpassword.SetBinding(Entry.TextProperty, RegistroViewModel.ConfirmPasswordPropertyName);
            layout.Children.Add(confirmpassword);


            var email = new Entry { Placeholder = "Email", TextColor = Color.Black };
            email.SetBinding(Entry.TextProperty, RegistroViewModel.EmailPropertyName);
            layout.Children.Add(email);


            var firstname = new Entry { Placeholder = "Nome", TextColor = Color.Black };
            firstname.SetBinding(Entry.TextProperty, RegistroViewModel.FirstnamePropertyName);
            layout.Children.Add(firstname);

            var lastname = new Entry { Placeholder = "SobreNome", TextColor = Color.Black };
            lastname.SetBinding(Entry.TextProperty, RegistroViewModel.LastnamePropertyName);
            layout.Children.Add(lastname);




            var relativelayout = new RelativeLayout();

            //var button = new Button { Text = "Logar", TextColor = Color.White, BackgroundColor = Color.FromHex("#8BC34A") };
            //button.SetBinding(Button.CommandProperty, LoginViewModel.LoginCommandPropertyName);

            var btnRegistro = new Button { Text = "Registrar", TextColor = Color.White, BackgroundColor = Color.FromHex("#8BC34A") };
            btnRegistro.SetBinding(Button.CommandProperty, RegistroViewModel.RegistroCommandPropertyName);

            var button = new Button { Text = "Logar", TextColor = Color.White, BackgroundColor = Color.FromHex("#8BC34A") };
            button.SetBinding(Button.CommandProperty, LoginViewModel.LoginCommandPropertyName);

            // layout.Children.Add(button);
            layout.Children.Add(btnRegistro);
            layout.Children.Add(button);

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
