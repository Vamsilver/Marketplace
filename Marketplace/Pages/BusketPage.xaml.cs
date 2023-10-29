using Marketplace.ADOModel;
using Marketplace.Pages.Seller;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Interaction logic for BusketPage.xaml
    /// </summary>
    public partial class BusketPage : Page
    {
        int idBasket;

        List<BusketProduct> busketProducts= new List<BusketProduct>();  

        public BusketPage()
        {
            InitializeComponent();

            UserNameTextBlock.Text = App.CurrentUser.Surname + " " + App.CurrentUser.Name.ElementAt(0) + ".";
            MoneyTextBlock.Text = App.CurrentUser.Balance.ToString();

            idBasket = App.Connection.Basket.Where(z => z.idUser.Equals(App.CurrentUser.idUser) && z.PurchaseDate.Equals(null)).FirstOrDefault().idBasket;

            var listBasketProductsFromDb = App.Connection.BasketProduct.Where(z => z.idBasket.Equals(idBasket)).ToList();
            
            foreach(var basketProduct in listBasketProductsFromDb)
            {
                busketProducts.Add(new BusketProduct(App.Connection.Product.Where(z => z.idProduct.Equals(basketProduct.idProduct)).FirstOrDefault()));
            }

            BusketList.ItemsSource = busketProducts;

            RefreshList();
        }

        private void LikeProductButtonClick(object sender, RoutedEventArgs e)
        {
            var newLike = new Like();

            if (BusketList.SelectedItem == null)
                return;

            var currentProduct = Converter.ConvertToProduct(BusketList.SelectedItem as BusketProduct);

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

        private bool IsSelectedItemInBusketListNotNull()
        {
            return BusketList.SelectedItem != null;
        }

        private BusketProduct GetSelectedItemInBusketList()
        {
            if (IsSelectedItemInBusketListNotNull())
                return BusketList.SelectedItem as BusketProduct;
            else
                return null;
        }

        private void DeleteProductFromBasketButtonClick(object sender, RoutedEventArgs e)
        {
            DeleteSelectedProductFromBasket();
         
            RefreshList();
        }

        private void DeleteSelectedProductFromBasket()
        {
            if (!IsSelectedItemInBusketListNotNull())
                return;

            var currentBusketProduct = GetSelectedItemInBusketList();
            var basketProduct = App.Connection.BasketProduct.Where(z => z.idBasket.Equals(idBasket) &&
                                                                   z.idProduct.Equals(currentBusketProduct.idProduct))
                                                            .FirstOrDefault();

            App.Connection.BasketProduct.Remove(basketProduct);
            App.Connection.SaveChanges();
        }

        private void AddCountOfProductButtonClick(object sender, RoutedEventArgs e)
        {
            if(!IsSelectedItemInBusketListNotNull())
                return;

            var currentBusketProduct = GetSelectedItemInBusketList();

            currentBusketProduct.GetCountInBasket += 1;

            var basketProduct = App.Connection.BasketProduct.Where(z => z.idProduct.Equals(currentBusketProduct.idProduct) && z.idBasket.Equals(idBasket)).FirstOrDefault();
            basketProduct.Count = currentBusketProduct.GetCountInBasket;

            App.Connection.BasketProduct.AddOrUpdate(basketProduct);
            App.Connection.SaveChanges();

            RefreshList();
        }

        private void RefreshList()
        {
            busketProducts.Clear();

            var listBasketProductsFromDb = App.Connection.BasketProduct.Where(z => z.idBasket.Equals(idBasket)).ToList();

            foreach (var basketProduct in listBasketProductsFromDb)
            {
                busketProducts.Add(new BusketProduct(App.Connection.Product.Where(z => z.idProduct.Equals(basketProduct.idProduct)).FirstOrDefault()));
            }

            BusketList.ItemsSource = busketProducts;
            BusketList.Items.Refresh();


            decimal totalCost = 0;
            decimal? totalDiscount = 0;
            int totalAmountOfProducts = 0;

            foreach(var busketProduct in busketProducts)
            {
                totalCost += busketProduct.Cost * busketProduct.GetCountInBasket;
                if(busketProduct.OldCost != null)
                    totalDiscount += (busketProduct.OldCost - busketProduct.Cost) * busketProduct.GetCountInBasket;
                totalAmountOfProducts += busketProduct.GetCountInBasket;
            }

            AmountOfProductsTextBlock.Text = "Товаров:  " + totalAmountOfProducts + " шт.";

            TotalCostTextBlock.Text = "Общая сумма:  " + totalCost.ToString() + " ₽"; 
            TotalDiscountTextBlock.Text = "Общая скидка:  " + totalDiscount.ToString() + " ₽";
        }

        private void ReduceCountOfProductButtonClick(object sender, RoutedEventArgs e)
        {
            if (!IsSelectedItemInBusketListNotNull())
                return;

            var currentBusketProduct = GetSelectedItemInBusketList();

            if (currentBusketProduct.GetCountInBasket.Equals(1))
            {
                var messageResult = MessageBox.Show("Вы уверены, что хотите удалить товар из корзины?", "Подвтердите удаление", MessageBoxButton.YesNo);

                if (messageResult.Equals(MessageBoxResult.Yes))
                {
                    DeleteSelectedProductFromBasket();
                    RefreshList();
                    return;
                }
            }

            currentBusketProduct.GetCountInBasket -= 1;

            var basketProduct = App.Connection.BasketProduct.Where(z => z.idProduct.Equals(currentBusketProduct.idProduct) && z.idBasket.Equals(idBasket)).FirstOrDefault();
            basketProduct.Count = currentBusketProduct.GetCountInBasket;

            App.Connection.BasketProduct.AddOrUpdate(basketProduct);
            App.Connection.SaveChanges();

            RefreshList();
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

        private void BuyButtonClick(object sender, RoutedEventArgs e)
        {
            if(BusketList.Items.Count == 0)
            {
                MessageBox.Show("Ваша корзина пуста(");
                return;
            }

            if(App.CurrentUser.Balance < Decimal.Parse(TotalCostTextBlock.Text.Split(' ')[3]))
            {
                MessageBox.Show("На счету недостаточно средств(", "Вы можете пополнить баланс нажав на \"+\"");
                return;
            }

            var basket = App.Connection.Basket.FirstOrDefault(z => z.idBasket.Equals(idBasket));

            basket.PurchaseDate = DateTime.Now;

            App.Connection.Basket.AddOrUpdate(basket);

            App.Connection.Basket.Add(new Basket()
            {
                idUser = basket.idUser,
            });

            foreach(var item in busketProducts)
            {
                var product = App.Connection.Product.FirstOrDefault(z => z.idProduct.Equals(item.idProduct));
                product.AmountOfSales += item.GetCountInBasket;

                App.Connection.Product.AddOrUpdate(product);
            }

            App.Connection.SaveChanges();

            MessageBox.Show("Товары успешно куплены!");
            NavigationService.Navigate(new BusketPage());
        }

        private void ShowHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PurchaseHistoryPage());
        }
    }
}
