using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectablesApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectablesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            var user = App.GetInstance().CurrentUser ?? new User
            {
                Username = "Friet van Piet"
            };

            BindingContext = user;
        }
    }
}