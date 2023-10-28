using Marketplace.ADOModel;
using Marketplace.Pages.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Marketplace.Pages
{
    /// <summary>
    /// Interaction logic for PurchaseHistoryPage.xaml
    /// </summary>
    public partial class PurchaseHistoryPage : Page
    {
        public PurchaseHistoryPage()
        {
            InitializeComponent();

            UserNameTextBlock.Text = App.CurrentUser.Surname + " " + App.CurrentUser.Name.ElementAt(0) + ".";
            MoneyTextBlock.Text = App.CurrentUser.Balance.ToString();

            var list = new List<ViewHistoryBasket>();

            var list1 = App.Connection.Basket.Where(z => !z.PurchaseDate.Equals(null)).ToList();

            foreach(var item in list1)
            {
                list.Add(new ViewHistoryBasket(item));
            }

            HistoryBusketList.ItemsSource = list;
        }

        private void HistoryBusketList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HistoryBusketList.SelectedItem == null)
                return;

            NavigationService.Navigate(new OldBusketPage((HistoryBusketList.SelectedItem as ViewHistoryBasket).idBasket));
        }

        private void PlusButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QRPage());
        }

        private void NameHyperlinkClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SellerProductsPage());
        }

        private void LogoButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SellerHomePage());
        }

        private void BalanceHyperlinkClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QRPage());
        }
    }
}
