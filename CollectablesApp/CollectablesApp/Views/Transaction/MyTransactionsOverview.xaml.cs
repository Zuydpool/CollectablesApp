using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace CollectablesApp.Views.Transaction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTransactionsOverview : ContentPage
    {

        public ObservableCollection<Models.Transaction> Items { get; set; }

        public MyTransactionsOverview()
        {
            InitializeComponent();

            Items = new ObservableCollection<Models.Transaction>();

            BindingContext = this;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            var user = App.GetInstance().CurrentUser;

            user?.Transactions.Reverse().ForEach(transaction => Items.Add(transaction));
        }


        private void MyTransactionsSearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchResult = Items.Where(item => item.CollectableItem.Name.ToLower().Contains(MyTransactionsSearchBar.Text.ToLower()));
            MyTransactionsCollectionView.ItemsSource = searchResult.Reverse();
        }

        private void MyTransactionsCollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}