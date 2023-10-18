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

namespace Marketplace.Pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
            var roles = App.Connection.Role.ToList();
            RoleComboBox.ItemsSource = roles;
        }

        private void RegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            string login;
            string password;
            string name;
            string surname;
            DateTime birhdate;
            double balance;
            
            try
            {
                login = LoginTextBox.Text;
                password = PasswordTextBox.Password;
                name = NameTextBox.Text;
                surname = SurNameTextBox.Text;
                birhdate = BirhDateDatePicker.SelectedDate.Value;
                balance = Double.Parse(BalanceTextBox.Text);
                
                if (String.IsNullOrEmpty(login) || 
                    String.IsNullOrEmpty(password) || 
                    String.IsNullOrEmpty(name) || 
                    String.IsNullOrEmpty(surname) ||
                    balance == null || 
                    birhdate == null)
                    
                    throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("Корректно заполните все поля!");
                return;
            }
            
            User newUser = new User();
            newUser.Name = name;
            newUser.Surname = surname;
            newUser.BirthDate = birhdate;
            newUser.Balance = (decimal)balance;

            var isUserExist = App.Connection.User.Where(z => 
                                                        z.Name.Equals(name) && 
                                                        z.Surname.Equals(surname) && 
                                                        newUser.BirthDate.Equals(birhdate)
                                                        ).FirstOrDefault() != null ? true : false;

            if(isUserExist)
            {
                MessageBox.Show("Такой пользователь уже существует");
            }
            else
            {
                var newAuth = new Authorization();
                
                newAuth.Login = login;
                newAuth.Password = password;

                App.Connection.Authorization.Add(newAuth);
                App.Connection.SaveChanges(); 

                newUser.Authorization = newAuth;
                newUser.Role = (Role)RoleComboBox.SelectedItem;

                App.Connection.User.Add(newUser);

                App.Connection.SaveChanges();

                MessageBox.Show("Регистрация успешна");

                NavigationService.Navigate(new AuthorizationPage());
            }

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
