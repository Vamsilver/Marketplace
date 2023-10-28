﻿using Marketplace.ADOModel;
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

namespace Marketplace.Pages.Byer
{
    /// <summary>
    /// Interaction logic for ByerHomePage.xaml
    /// </summary>
    public partial class ByerHomePage : Page
    {
        List<ViewProduct> products;

        public ByerHomePage()
        {
            InitializeComponent();

            UserNameTextBlock.Text = App.CurrentUser.Surname + " " + App.CurrentUser.Name.ElementAt(0) + ".";
            MoneyTextBlock.Text = App.CurrentUser.Balance.ToString();

            products = Converter.ConvertToListViewProducts(App.Connection.Product.ToList());

            products.OrderBy(z => z.AmountOfSales);

            ProductList.ItemsSource = products;

            var categoryList = App.Connection.ProductCategory.ToList();
            var allProductCategory = new ProductCategory()
            {
                Title = "Все"
            };
            categoryList.Add(allProductCategory);
            CategorySortComboBox.ItemsSource = categoryList;
            CategorySortComboBox.SelectedItem = allProductCategory;
        }

        private void NameHyperlinkClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BuyerLikedProductsPage());
        }

        private void LikeButtonClick(object sender, RoutedEventArgs e)
        {
            var newLike = new Like();

            if (ProductList.SelectedItem == null)
                return;

            var currentProduct = Converter.ConvertToProduct(ProductList.SelectedItem as ViewProduct);

            newLike.idProduct = currentProduct.idProduct;
            newLike.idUser = App.CurrentUser.idUser;

            var oldLike = App.Connection.Like.
                Where(z => z.idProduct.Equals(currentProduct.idProduct) &&
                           z.idUser.Equals(App.CurrentUser.idUser)).
                FirstOrDefault();

            if (oldLike != null)
            {
                MessageBox.Show("Уже у вас в изрбранном", "Упс");
                return;
            }
            else
            {
                App.Connection.Like.Add(newLike);
                App.Connection.SaveChanges();

                this.NavigationService.Refresh();

                MessageBox.Show("Успешно добавлено в избранное!)");
            }
        }

        private void NavigateToBasketPageButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BusketPage());
        }

        private void SortComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategorySortComboBox == null)
                return;

            products = OrderProductList(products);

            ProductList.ItemsSource = products;
            ProductList.Items.Refresh();
        }

        private List<ViewProduct> OrderProductList(List<ViewProduct> list)
        {

            var categorySortComboBoxSelectedItem = CategorySortComboBox.SelectedItem;


            if (categorySortComboBoxSelectedItem != null)
                if ((categorySortComboBoxSelectedItem as ProductCategory).Title.Equals("Все"))
                    list = Converter.ConvertToListViewProducts(App.Connection.Product.ToList());

            switch ((SortComboBox.SelectedItem as ComboBoxItem).Content.ToString())
            {
                case "Популярное":
                    list = list.OrderBy(z => z.AmountOfSales).ToList();
                    list.Reverse();
                    break;
                case "Избранное":
                    list = list.OrderBy(z => z.GetAmountOfLikes).ToList();
                    list.Reverse();
                    break;
                case "Сначала дешевые":
                    list = list.OrderBy(z => z.Cost).ToList();
                    break;
                case "Сначала дорогие":
                    list = list.OrderBy(z => z.Cost).ToList();
                    list.Reverse();
                    break;
                case "По размеру скидки":
                    list = list.OrderBy(z => z.OldCost - z.Cost).ToList();
                    list.Reverse();
                    break;
            }

            return list;
        }

        private void CategorySortComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            products = Converter.ConvertToListViewProducts(App.Connection.Product.Where(z => z.onSell && z.isApproved).ToList());

            var categorySortComboBoxSelectedItem = CategorySortComboBox.SelectedItem as ProductCategory;

            if (categorySortComboBoxSelectedItem.Title.Equals("Все"))
                products = Converter.ConvertToListViewProducts(App.Connection.Product.Where(z => z.onSell && z.isApproved).ToList());
            else
                products = products.Where(z => z.ProductCategory.Equals(categorySortComboBoxSelectedItem)).ToList();

            var newList = OrderProductList(products);

            if (newList != null)
                products = newList;

            ProductList.ItemsSource = products;
            ProductList.Items.Refresh();
        }

        private void PlusButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QRPage());
        }

        private void AddProductToBasketButtonClick(object sender, RoutedEventArgs e)
        {
            if (ProductList.SelectedItem == null)
                return;

            var newProductInBasket = new BasketProduct()
            {
                idBasket = App.Connection.Basket.Where(z => z.idUser.Equals(App.CurrentUser.idUser) && z.PurchaseDate.Equals(null)).FirstOrDefault().idBasket,
                idProduct = Converter.ConvertToProduct(ProductList.SelectedItem as ViewProduct).idProduct,
                Count = 1
            };

            var idBasket = App.Connection.Basket.Where(z => z.idUser.Equals(App.CurrentUser.idUser) && z.PurchaseDate.Equals(null)).FirstOrDefault().idBasket;
            var oldBasketProductInBasket = App.Connection.BasketProduct.Where(z => z.idBasket.Equals(idBasket) && z.idProduct.Equals(newProductInBasket.idProduct)).FirstOrDefault();

            if (oldBasketProductInBasket != null)
            {
                MessageBox.Show("Товар уже у вас в корзине!)", "Упс");
                return;
            }

            App.Connection.BasketProduct.Add(newProductInBasket);
            App.Connection.SaveChanges();

            MessageBox.Show("Товар успешно добавлен в корзину!)");
        }

        private void BalanceHyperlinkClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QRPage());
        }
    }
}
