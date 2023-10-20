using Marketplace.ADOModel;
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

            List<ViewProduct> viewProductList = new List<ViewProduct>();

            var productList = App.Connection.Product.Where(z => z.idUser.Equals(App.CurrentUser.idUser)).ToList();

            foreach (Product product in productList)
                viewProductList.Add(new ViewProduct(product));

            ProductList.ItemsSource = viewProductList;
        }

        private void LoginHyperlinkClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddNewProductButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddNewProductPage());
        }

        private void LikeButtonClick(object sender, RoutedEventArgs e)
        {
            var newLike = new Like();

            var currentProduct = ProductList.SelectedItem as Product;

            if (currentProduct == null)
                return;

            newLike.Product = currentProduct;
            newLike.User = App.CurrentUser;

            var oldLike = App.Connection.Like.
                Where(z => z.idProduct.Equals(currentProduct.idProduct) &&
                           z.idUser.Equals(App.CurrentUser.idUser)).
                FirstOrDefault();

            if (oldLike != null)
                return;
            else
            {
                App.Connection.Like.Add(newLike);
                App.Connection.SaveChanges();
            }
        }

        private void LikedProductButtonClick(object sender, RoutedEventArgs e)
        {
            var newLike = new Like();

            var currentProduct = ProductList.SelectedItem as Product;

            if (currentProduct == null)
                return;

            newLike.Product = currentProduct;
            newLike.User = App.CurrentUser;

            var oldLike = App.Connection.Like.
                Where(z => z.idProduct.Equals(currentProduct.idProduct) &&
                           z.idUser.Equals(App.CurrentUser.idUser)).
                FirstOrDefault();

            if (oldLike != null)
                return;
            else
            {
                App.Connection.Like.Add(newLike);
                App.Connection.SaveChanges();
            }
        }

        private void ProductListMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(ProductList.SelectedItem != null)
            {
                NavigationService.Navigate(new EditSellerProductPage(((Product)ProductList.SelectedItem).idProduct));
            }
        }

        private void LogoButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SellerHomePage());
        }
    }
}
