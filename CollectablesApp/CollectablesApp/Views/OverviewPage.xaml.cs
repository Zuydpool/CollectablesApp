using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CollectablesApp.Models;
using CollectablesApp.Views.Modals;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectablesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewPage : ContentPage
    {

        public ObservableCollection<CollectableItem> Items { get; set; }
        
        public OverviewPage()
        {
            InitializeComponent();
            Items = new ObservableCollection<CollectableItem>();

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var allItems = await App.GetInstance().Storage.Dao.CollectableItemsDao.GetAll();
            if (allItems == null)
            {
                await DisplayAlert("Error", "Failed to load collectable items!", "Ok");
                return;
            }

            foreach (var item in allItems.Where(item => Items.All(arg => arg.Id != item.Id)))
            {
                Items.Add(item);
            }

            if (allItems.Any()) return;
            var newItems = new List<CollectableItem>
            {
                new CollectableItem
                {
                    Name = "Tomato",
                    Description = "It is a vegetable", 
                    Quantity = 1, 
                    Price = 0.50, 
                    Seller = "admin",
                    ImageUrl = "tomato.png"
                },
                new CollectableItem
                {
                    Name = "Banana", 
                    Description = "It is a fruit", 
                    Quantity = 1, 
                    Price = 0.30,
                    Seller = "admin",
                    ImageUrl = "banana.png"
                },
            };

            await App.GetInstance().Storage.Dao.CollectableItemsDao.Add(newItems);

            newItems.ForEach(item => Items.Add(item));
        }

        private void SearchBar1_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchResult = Items.Where(item => item.Name.ToLower().Contains(SearchBar1.Text.ToLower()));
            CollectionView1.ItemsSource = searchResult;
        }

        private async void AddItemButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddItemModal());
        }

        private async void CollectionView1_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var collectableItem = e.CurrentSelection.FirstOrDefault() as CollectableItem;
            //((CollectionView)sender).SelectedItem = null;
            await Navigation.PushModalAsync(new DetailsItemModal(collectableItem));
        }
    }
}