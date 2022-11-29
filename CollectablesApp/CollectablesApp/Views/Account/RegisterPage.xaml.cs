using System;
using CollectablesApp.DBStorage;
using CollectablesApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectablesApp.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterProcedure(object sender, EventArgs e)
        {
            var username = EntryUsername.Text;
            var password = EntryPassword.Text;
            if (string.IsNullOrEmpty(username))
            {
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                return;
            }

            //if (password.Length )
            if (await App.GetInstance().Storage.Dao.UsersDao.CheckIfUsernameExists(username))
            {
                return;
            }

            await App.GetInstance().Storage.Dao.UsersDao
                .Add(new User(username, BCrypt.Net.BCrypt.HashPassword(password, App.PasswordHash)));
            await DisplayAlert("Success", "Successfully created a user account!", "Ok");

            await Navigation.PushAsync(new LoginPage());
        }

        private void ImageButtonShowHidePassword_OnClicked(object sender, EventArgs e)
        {
            if (EntryPassword.IsPassword)
            {
                EntryPassword.IsPassword = false;
                ImageButtonShowHidePassword.Source = ImageSource.FromFile("visibility.png");
            }
            else
            {
                EntryPassword.IsPassword = true;
                ImageButtonShowHidePassword.Source = ImageSource.FromFile("visibility_off.png");
            }
        }
    }
}