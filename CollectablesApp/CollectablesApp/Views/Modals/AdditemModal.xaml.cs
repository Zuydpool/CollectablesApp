using System;
using System.Linq;
using CollectablesApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectablesApp.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemModal : ContentPage
    {
        public AddItemModal()
        {
            InitializeComponent();
        }

        /*private async void SelectImageButton_OnClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "Upload not supported on this device", "Ok");
                //Toast.MakeText(this, "Upload not supported on this device", ToastLength.Short).Show();
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                CompressionQuality = 40

            });

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
        }*/

        private async void SaveItemButton_OnClicked(object sender, EventArgs e)
        {
            var productName = ProductNameEntry.Text;
            var productDescription = ProductDescriptionEntry.Text;
            var quantity = ProductQuantityEntry.Text;
            var price = ProductPriceEntry.Text;
            if (price == "") price = "0";
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

            var user = App.GetInstance().CurrentUser;
            if (user == null)
            {
                await DisplayAlert("Error", "Invalid user. Please login with a valid user!", "Ok!");
                return;
            }


            var collectableItem = new CollectableItem
                {
                    Name = productName,
                    Description = productDescription,
                    Quantity = int.Parse(quantity),
                    Price = double.Parse(price),
                    ImageUrl = imageUrl,
                    Seller = user.Username
                };

            await App.GetInstance().Storage.Dao.CollectableItemsDao.Add(collectableItem);

            await DisplayAlert("Success", "Successfully saved item!", "Ok");
            
            await Navigation.PopModalAsync();
        }

        private void ProductOpenForTrade_OnToggled(object sender, ToggledEventArgs e)
        {
            ProductPriceEntry.IsVisible = !ProductOpenForTrade.IsToggled;
        }
    }
}