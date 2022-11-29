using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CollectablesApp.Views.Account;
using CollectablesApp.Views.Transaction;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectablesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainFlyoutPageFlyout : ContentPage
    {
        public ListView ListView;

        public MainFlyoutPageFlyout()
        {
            InitializeComponent();

            BindingContext = new MainFlyoutPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class MainFlyoutPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainFlyoutPageFlyoutMenuItem> MenuItems { get; set; }

            public MainFlyoutPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MainFlyoutPageFlyoutMenuItem>(new[]
                {
                    new MainFlyoutPageFlyoutMenuItem { Id = 0, Title = "Overview", TargetType = typeof(OverviewPage), IconSource = "dashboard.png" },
                    new MainFlyoutPageFlyoutMenuItem { Id = 1, Title = "My Transactions", TargetType = typeof(MyTransactionsOverview), IconSource = "receipt.png" },
                    new MainFlyoutPageFlyoutMenuItem { Id = 2, Title = "My Items", TargetType = typeof(MyItemsPage), IconSource = "category.png" },
                    new MainFlyoutPageFlyoutMenuItem { Id = 3, Title = "Profiel", TargetType = typeof(ProfilePage), IconSource = "account.png" },
                    new MainFlyoutPageFlyoutMenuItem { Id = 4, Title = "Inloggen", TargetType = typeof(LoginPage), IconSource = "login.png" },
                    new MainFlyoutPageFlyoutMenuItem { Id = 5, Title = "Registreren", TargetType = typeof(RegisterPage), IconSource = "edit.png" },
                    new MainFlyoutPageFlyoutMenuItem { Id = 6, Title = "Uitloggen", StyleClass = "MenuItemLayoutStyle", CustomAction = Logout, IconSource = "logout.png" },
                });
            }

            private void Logout()
            {
                Application.Current.Properties["Token"] = "";
                //await Navigation.PushAsync(new LoginPage());
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}