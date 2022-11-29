using System;
using CollectablesApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectablesApp.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditItemModal : ContentPage
    {
        private readonly CollectableItem _collectableItem;

        public EditItemModal(CollectableItem collectableItem)
        {
            InitializeComponent();

            this._collectableItem = collectableItem;
            BindingContext = collectableItem;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ProductPriceEntry.IsVisible = !_collectableItem.OpenForTrading;
        }

        private async void SaveItemButton_OnClicked(object sender, EventArgs e)
        {
            var productName = ProductNameEntry.Text;
            var productDescription = ProductDescriptionEntry.Text;
            var quantity = ProductQuantityEntry.Text;
            var price = ProductPriceEntry.Text;
            var imageUrl = ImageUrlEntry.Text;

            if (string.IsNullOrEmpty(productName) ||
                string.IsNullOrEmpty(productDescription) ||
                string.IsNullOrEmpty(quantity) ||
                string.IsNullOrEmpty(price))
            {
                await DisplayAlert("Error", "Product Name, Product Description, Quantity and Price need a valid value",
                    "Ok!");
                return;
            }

            _collectableItem.Name = productName;
            _collectableItem.Description = productDescription;
            _collectableItem.Quantity = int.Parse(quantity);
            _collectableItem.Price = double.Parse(price);
            _collectableItem.ImageUrl = imageUrl;

            await App.GetInstance().Storage.Dao.CollectableItemsDao.Update(_collectableItem);

            await DisplayAlert("Success", "Successfully saved item!", "Ok");

            await Navigation.PopModalAsync();
        }

        private void ProductOpenForTrade_OnToggled(object sender, ToggledEventArgs e)
        {
            ProductPriceEntry.IsVisible = !ProductOpenForTrade.IsToggled;
        }
    }
}