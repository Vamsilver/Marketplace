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

namespace Marketplace.Pages.Seller
{
    /// <summary>
    /// Interaction logic for SellerProductsPage.xaml
    /// </summary>
    public partial class SellerProductsPage : Page
    {
        public SellerProductsPage()
        {
            InitializeComponent();
        }

        private void LoginHyperlinkClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddNewProductButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddNewProductPage());
        }
    }
}
