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
        public ByerHomePage()
        {
            InitializeComponent();

            //UserNameTextBlock.Text = App.CurrentUser.Surname + " " + App.CurrentUser.Name.ElementAt(0) + ".";

            //ProductList.ItemsSource = App.Connection.Product.ToList();
        }
    }
}
