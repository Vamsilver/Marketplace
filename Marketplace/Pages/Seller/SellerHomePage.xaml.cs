using Marketplace.ADOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for SellerHomePage.xaml
    /// </summary>
    public partial class SellerHomePage : Page
    {
        public SellerHomePage()
        {
            InitializeComponent();
            UserNameTextBlock.Text = App.CurrentUser.Surname + " " + App.CurrentUser.Name.ElementAt(0) + ".";

            List<ViewProduct> viewProductList = new List<ViewProduct>();

            var productList = App.Connection.Product.ToList();

            foreach (Product product in productList)
                viewProductList.Add(new ViewProduct(product));

            ProductList.ItemsSource = viewProductList;
        }

        private void NameHyperlinkClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SellerProductsPage());
        }

        private void LikeButtonClick(object sender, RoutedEventArgs e)
        {
            var newLike = new Like();
            
            var currentProduct = ProductList.SelectedItem as Product;

            if(currentProduct == null)
                return;

            newLike.Product = currentProduct;
            newLike.User = App.CurrentUser;

            var oldLike = App.Connection.Like.
                Where(z => z.idProduct.Equals(currentProduct.idProduct) && 
                           z.idUser.Equals(App.CurrentUser.idUser)).
                FirstOrDefault();

            if ( oldLike != null)
                return;
            else
            {
                App.Connection.Like.Add(newLike);
                App.Connection.SaveChanges();
            }
        }

        private void BusketButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
