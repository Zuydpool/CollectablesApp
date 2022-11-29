using System;
using CollectablesApp.DBStorage;
using CollectablesApp.Models;
using CollectablesApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace CollectablesApp
{
    public partial class App : Application
    {
        private static App _instance;

        public IStorage Storage { get; } = new StorageFactory().GetInstance();

        public User? CurrentUser { get; set; }

        public static string PasswordHash = BCrypt.Net.BCrypt.GenerateSalt(12);

        public App()
        {
            _instance = this;
            InitializeComponent();

            MainPage = new NavigationPage(new MainFlyoutPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static App GetInstance()
        {
            return _instance;
        }
    }
}
