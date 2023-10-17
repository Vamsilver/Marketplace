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
            string password = PasswordTextBox.Text;

            Authorization authorization = App.Connection.Authorization.Where(z => z.Login.Equals(login) && z.Password.Equals(password)).FirstOrDefault();
            if(authorization != null)
            {
                App.CurrentUser = authorization.User.FirstOrDefault();
                //if(App.CurrentUser.Role == )
                //{
                //
                //}
                //NavigationService.Navigate();
            }
            else
            {
                MessageBox.Show("Неправильные данные");
            }
        }
    }
}
