﻿using System.Threading.Tasks;
using CPMobile.Models;
using CPMobile.Views;
using Xamarin.Forms;
using System;

namespace CPMobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        readonly ICPFeeds cpFeeds;


        public LoginViewModel(Page page)
            : base(page)
        {
            this.cpFeeds = DependencyService.Get<ICPFeeds>();
        }

        public const string LoginCommandPropertyName = "LoginCommand";
        public const string RegistroCommandPropertyName = "RegistroCommand";

        Command loginCommand;
        Command registroCommand;
        public Command LoginCommand
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new Command(async () => await ExecuteLoginCommand()));
            }
        }
        public Command RegistroCommand
        {
            get
            {
                return registroCommand ??
                    (registroCommand = new Command(async () => await ExecuteRegistroCommand()));
            }
        }

        private async Task ExecuteLoginCommand()
        {
          
            bool isLoginSuccess = false;
            if (IsBusy)
                return;

            IsBusy = true;
            loginCommand.ChangeCanExecute();

            try
            {

                isLoginSuccess = await cpFeeds.GetAccessToken(this.UserName, this.Password);
            }
            catch(Exception ex)
            {
                isLoginSuccess = false;
            }

            finally
            {
                IsBusy = false;
                loginCommand.ChangeCanExecute();
            }

            if (isLoginSuccess)
            {
                await page.Navigation.PushModalAsync(new RootPage());
                if(Device.OS == TargetPlatform.Android)
                Application.Current.MainPage = new RootPage();
            }
            else
            {
                await page.DisplayAlert("Login Erro", "Login Erro! por favor tente novamente", "Ok");
            }
        }


        private async Task ExecuteRegistroCommand()
        {
            await page.Navigation.PushModalAsync(new RegistroPage());
            if (Device.OS == TargetPlatform.Android)
                Application.Current.MainPage = new RegistroPage();
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
    }
}
