using Marketplace.Pages.Seller;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
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

namespace Marketplace.Pages
{
    /// <summary>
    /// Interaction logic for QRPage.xaml
    /// </summary>
    public partial class QRPage : Page
    {
        public QRPage()
        {
            InitializeComponent();

            UserNameTextBlock.Text = App.CurrentUser.Surname + " " + App.CurrentUser.Name.ElementAt(0) + ".";
            MoneyTextBlock.Text = App.CurrentUser.Balance.ToString();

            App.CurrentUser.Balance += (decimal)5000.00;
            App.Connection.User.AddOrUpdate(App.CurrentUser);
            App.Connection.SaveChanges();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            // Ссылка на XL таблицу
            string soucer_xl = "https://docs.google.com/spreadsheets/d/1uquqqre6lcUmxVvHEfLXEQN_5f9dHf0VkODR0A6vc9g/edit?usp=sharing";
            string soucer_Pay = "https://qiwi.com/";
            // Создание переменной библиотеки QRCoder
            QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
            // Присваеваем значиения
            QRCoder.QRCodeData dataXl = qr.CreateQrCode(soucer_xl, QRCoder.QRCodeGenerator.ECCLevel.L);
            QRCoder.QRCodeData dataPay = qr.CreateQrCode(soucer_Pay, QRCoder.QRCodeGenerator.ECCLevel.L);
            // переводим в Qr
            QRCoder.QRCode codeXl = new QRCoder.QRCode(dataXl);
            QRCoder.QRCode codePay = new QRCoder.QRCode(dataPay);
            Bitmap bitmapXl = codeXl.GetGraphic(100);
            Bitmap bitmapPay = codePay.GetGraphic(100);
            /// Создание картинки
            using (MemoryStream memory = new MemoryStream())
            {
                bitmapXl.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                ExcelImage.Source = bitmapimage;
            }

            using (MemoryStream memory = new MemoryStream())
            {
                bitmapPay.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                AddmoneyImage.Source = bitmapimage;
            }
        }

        private void BusketButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BusketPage());
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
        }
    }
}
