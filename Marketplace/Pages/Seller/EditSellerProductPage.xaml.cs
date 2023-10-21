using Marketplace.ADOModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for EditSellerProductPage.xaml
    /// </summary>
    public partial class EditSellerProductPage : Page
    {
        Product product = new Product();
        byte[] imageBytes;

        public EditSellerProductPage(int productId)
        {
            InitializeComponent();

            product = App.Connection.Product.Where(z => z.idProduct.Equals(productId)).FirstOrDefault();

            DataContext = product;
            BirthRateComboBox.ItemsSource = App.Connection.ProductBirthRate.ToList();
            CategoryComboBox.ItemsSource = App.Connection.ProductCategory.ToList();
            TitleTextBox.Text = product.Title;
            CostTextBox.Text = product.Cost.ToString();
            CategoryComboBox.SelectedItem = product.ProductCategory;
            BirthRateComboBox.SelectedItem = product.ProductBirthRate;
            DesriptionTextBox.Text = product.Description;
            imageBytes = product.image;
        }

        private void SelectImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;";
            openFileDialog.Title = "Выберите изображение для товара.";
            openFileDialog.ShowDialog();
            imageBytes = File.ReadAllBytes(openFileDialog.FileName);
            if (String.IsNullOrEmpty(openFileDialog.FileName))
            {
                MessageBox.Show("Пустое изображение");
                return;
            }

            product.image = imageBytes;

            DataContext = product;
            BindingOperations.GetBindingExpressionBase(ProductImage, Image.SourceProperty).UpdateTarget();
            DataContext = product;
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            SelectImage();

        }

        private void EditProductButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                product.Title = TitleTextBox.Text;
                product.Description = DesriptionTextBox.Text;
                product.User = App.CurrentUser;
                product.ProductCategory = (ProductCategory)CategoryComboBox.SelectedItem;
                product.ProductBirthRate = (ProductBirthRate)BirthRateComboBox.SelectedItem;
                product.onSell = false;

                var newCost = Decimal.Parse(CostTextBox.Text.Replace('.', ','));

                if (product.Cost != newCost)
                    product.OldCost = product.Cost;
                product.Cost = newCost;
                product.isApproved = false;
                product.image = imageBytes;
                product.AmountOfSales = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так..");
                return;
            }

            App.Connection.SaveChanges();

            MessageBox.Show("Успешное изменение товара товара!");
            NavigationService.Navigate(new SellerProductsPage());
        }

        private void LogoButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SellerHomePage());
        }

        private void BusketButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
