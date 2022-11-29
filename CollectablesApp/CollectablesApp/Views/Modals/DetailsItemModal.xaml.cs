using System;
using CollectablesApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectablesApp.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsItemModal : ContentPage
    {
        private readonly CollectableItem _collectableItem;

        public DetailsItemModal(CollectableItem collectableItem)
        {
            InitializeComponent();

            this._collectableItem = collectableItem;
            BindingContext = collectableItem;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ProductPriceGrid.IsVisible = !_collectableItem.OpenForTrading;

            var currentUser = App.GetInstance().CurrentUser;
            // Hide buy button if no user is logged in or
            // if the logged in user is the seller of the item or
            // if the quantity of the item is below 1
            if (currentUser == null || currentUser.Username == _collectableItem.Seller || _collectableItem.Quantity < 1)
            {
                BuyItemButton.IsVisible = false;
            }

            if (_collectableItem.OpenForTrading)
            {
                BuyItemButton.Text = "Trade";
            }
        }

        private async void DetailsItemButton_OnClicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new EditItemModal(_collectableItem));
        }

        private async void DeleteItemButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void BuyItemButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new BuyItemModal(_collectableItem));
        }
    }
}