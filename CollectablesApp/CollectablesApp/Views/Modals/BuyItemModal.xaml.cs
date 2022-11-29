using System;
using System.Globalization;
using CollectablesApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectablesApp.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyItemModal : ContentPage
    {

        private readonly CollectableItem _collectableItem;
        private int _selectedQuantity = 1;
        private double _selectedPrice;

        public BuyItemModal(CollectableItem collectableItem)
        {
            InitializeComponent();
            _collectableItem = collectableItem;
            _selectedPrice = collectableItem.Price;
            BindingContext = _collectableItem;

            PageTitleLabel.Text = _collectableItem.OpenForTrading ? "Trade " + _collectableItem.Name : "Buy " + _collectableItem.Name;
            PriceLabel.Text = "€" + _selectedPrice.ToString(CultureInfo.CurrentCulture);
        }

        private void TapGestureRecognizer_OnQuantityDecrease(object sender, EventArgs e)
        {
            if (_selectedQuantity <= 1) return;
            _selectedQuantity--;
            _selectedPrice -= _collectableItem.Price;
            QuantityLabel.Text = _selectedQuantity.ToString();
            PriceLabel.Text = "€" + _selectedPrice.ToString(CultureInfo.CurrentCulture);

        }

        private void TapGestureRecognizer_OnQuantityIncrease(object sender, EventArgs e)
        {
            if (_selectedQuantity < 1) return;
            if (_selectedQuantity == _collectableItem.Quantity) return;
            _selectedQuantity++;
            _selectedPrice += _collectableItem.Price;
            QuantityLabel.Text = _selectedQuantity.ToString();
            PriceLabel.Text = "€" + _selectedPrice.ToString(CultureInfo.CurrentCulture);
        }

        private void Button_OnConfirm(object sender, EventArgs e)
        {
            var currentUser = App.GetInstance().CurrentUser;
            if (currentUser == null) return;

            // Decrease the quantity on purchase
            _collectableItem.Quantity -= _selectedQuantity;

            var placeholder = _collectableItem.OpenForTrading ? "traded" : "bought";
            DisplayAlert("Success", $"You've successfully {placeholder} {_selectedQuantity} {_collectableItem.Name} from {_collectableItem.Seller}.",
                "Ok");

            currentUser.Transactions.Add(new Models.Transaction
            {
                CollectableItemId = _collectableItem.Id,
                Buyer = currentUser.Username,
                Seller = _collectableItem.Seller,
                Quantity = _selectedQuantity,
                Price = _collectableItem.Price * _selectedQuantity,
                CollectableItem = _collectableItem
            });

            App.GetInstance().Storage.Dao.UsersDao.Update(currentUser);
            App.GetInstance().Storage.Dao.CollectableItemsDao.Update(_collectableItem);
        }
    }
}