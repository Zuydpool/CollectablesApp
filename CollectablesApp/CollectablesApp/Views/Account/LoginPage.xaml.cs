using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectablesApp.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            EntryUsername.Completed += (s, e) => EntryPassword.Focus();
            EntryPassword.Completed += (s, e) => EntryUsername.Focus();
        }

        private async void SignInProcedure(object sender, EventArgs e)
        {
            var username = EntryUsername.Text;
            var password = EntryPassword.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Login", "Invalid Credentials", "Ok");
                return;
            }
            var user = await App.GetInstance().Storage.Dao.UsersDao.GetByUsername(username);
            if (user == null)
            {
                await DisplayAlert("Login", "Invalid Credentials", "Ok");
                return;
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (isPasswordValid)
            {
                App.GetInstance().CurrentUser = user;
                await DisplayAlert("Login", "Login Success", "Ok");
                //await Navigation.PushAsync(new MainFlyoutPage());
            } else
            {
                await DisplayAlert("Login", "Invalid Credentials", "Ok");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void LogOut()
        {
            Application.Current.Properties["Token"] = "";
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
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