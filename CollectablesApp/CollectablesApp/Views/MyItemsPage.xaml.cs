using System.Collections.ObjectModel;
using System.Linq;
using CollectablesApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectablesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyItemsPage : ContentPage
    {

        public ObservableCollection<CollectableItem> Items { get; set; }

        public MyItemsPage()
        {
            InitializeComponent();
            Items = new ObservableCollection<CollectableItem>();

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var user = App.GetInstance().CurrentUser;
            if (user == null) return;

            var allItems = user.CollectableItems;
            if (allItems == null)
            {
                await DisplayAlert("Error", "Failed to load collectable items!", "Ok");
                return;
            }

            foreach (var item in allItems.Where(item => Items.All(arg => arg.Id != item.Id)))
            {
                Items.Add(item);
            }
        }

        private void SearchBar1_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchResult = Items.Where(item => item.Name.ToLower().Contains(SearchBar1.Text.ToLower()));
            CollectionView1.ItemsSource = searchResult;
        }
    }
}