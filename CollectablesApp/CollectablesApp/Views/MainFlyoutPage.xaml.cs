using System;
using CollectablesApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectablesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainFlyoutPage : FlyoutPage
    {
        private readonly WeatherApiService _weatherApiService = new WeatherApiService();

        public MainFlyoutPage()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var currentWeatherCondition = await _weatherApiService.GetCurrentWeatherCondition();

            ToolbarWeatherIcon.IconImageSource = ImageSource.FromUri(new Uri(currentWeatherCondition.IconUrl, UriKind.Absolute));
            ToolbarWeatherTemperature.Text = "°" + currentWeatherCondition.Temperature;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is MainFlyoutPageFlyoutMenuItem item))
                return;


            if (item.TargetType != null)
            {
                var page = (Page) Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail = new NavigationPage(page);
                IsPresented = false;

                FlyoutPage.ListView.SelectedItem = null;
            }
            else if (item.CustomAction != null)
            {
                item.CustomAction.Invoke();
                FlyoutPage.ListView.SelectedItem = null;
            }

            // 
        }
    }
}