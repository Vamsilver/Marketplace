using Marketplace.Pages.Admin;
using Marketplace.Pages.Byer;
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
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void AuthorizationButtonClick(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Password;

            var authorization = App.Connection.Authorization.Where(z => z.Login.Equals(login) && z.Password.Equals(password)).FirstOrDefault();
            if(authorization != null)
            {
                App.CurrentUser = authorization.User.FirstOrDefault();

                Page Page = new Page(); 
                
                switch (App.CurrentUser.idRole)
                {
                    case 1:
                        Page = new AdminHomePage();
                        break;
                    case 2:
                        Page = new SellerHomePage();
                        break;
                    case 3:
                        Page = new ByerHomePage();
                        break;
                }

                NavigationService.Navigate(Page);
            }
            else
            {
                MessageBox.Show("Неправильные данные");
            }
        }

        private void RegistrationHyperlinkClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
