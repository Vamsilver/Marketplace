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
    /// Interaction logic for AddNewProductPage.xaml
    /// </summary>
    public partial class AddNewProductPage : Page
    {
        Product product = new Product();
        byte[] imageBytes;

        public AddNewProductPage()
        {
            InitializeComponent();
            DataContext = product;
            BirthRateComboBox.ItemsSource = App.Connection.ProductBirthRate.ToList();
            CategoryComboBox.ItemsSource = App.Connection.ProductCategory.ToList();
        }
        private void SelectImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;";
            openFileDialog.Title = "Выберите изображение для товара.";
            openFileDialog.ShowDialog();
            imageBytes = File.ReadAllBytes(openFileDialog.FileName);
            if(String.IsNullOrEmpty(openFileDialog.FileName))
            {
                MessageBox.Show("Пустое изображение");
                return;
            }
            product.image = imageBytes;
            BindingOperations.GetBindingExpressionBase(ProductImage, Image.SourceProperty).UpdateTarget();
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            SelectImage();
           
        }

        private void AddnewProductButtonClick(object sender, RoutedEventArgs e)
        {
            Product newProduct = new Product();
            try
            {
                newProduct.Title = TitleTextBox.Text;
                newProduct.Description = DesriptionTextBox.Text;
                newProduct.User = App.CurrentUser;
                newProduct.ProductCategory = (ProductCategory)CategoryComboBox.SelectedItem;
                newProduct.ProductBirthRate = (ProductBirthRate)BirthRateComboBox.SelectedItem;
                newProduct.onSell = false;
                newProduct.Cost = Decimal.Parse(CostTextBox.Text.Replace('.', ','));
                newProduct.isApproved = false;
                newProduct.image = imageBytes;
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так..");
                return;
            }

            App.Connection.Product.Add(newProduct);
            App.Connection.SaveChanges();

            MessageBox.Show("Успешное добавление товара!");
            NavigationService.GoBack();
        }
    }

}
