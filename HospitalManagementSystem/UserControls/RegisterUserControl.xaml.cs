using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace HospitalManagementSystem
{
    /// <summary>
    /// Логика взаимодействия для RegisterUserControl.xaml
    /// </summary>
    public partial class RegisterUserControl : UserControl
    {
        MainWindow Window;
        public RegisterUserControl(MainWindow window)
        {
            InitializeComponent();
            Window = window;
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            string firstName = textBoxFirstName.Text.Trim();
            string lastName = textBoxLastName.Text.Trim();
            string login = textBoxLogin.Text.Trim();
            string email = textBoxEmail.Text.Trim();
            string phone = textBoxPhone.Text.Trim();
            string password = textBoxPassword.Text.Trim();
            string repeatPassword = textBoxRepeatPassword.Text.Trim();

            if (String.IsNullOrEmpty(firstName) ||
                String.IsNullOrEmpty(lastName) ||
                String.IsNullOrEmpty(login) ||
                String.IsNullOrEmpty(password) ||
                String.IsNullOrEmpty(repeatPassword))
            {
                MessageBox.Show("One of the mandatory fields is empty", "Empty field", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } else if (!password.Equals(repeatPassword))
            {
                MessageBox.Show("Password and repeated password don't match", "Password mismatch", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string salt = PasswordMaster.GenerateSalt();
            string passwordHash = PasswordMaster.GetSaltHashedPassword(password, salt);

            User user = new User();
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Login = login;
            if (!String.IsNullOrEmpty(email)) user.Email = email;
            if (!String.IsNullOrEmpty(phone)) user.Phone = phone;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = salt;

            HospitalDBContext context = new HospitalDBContext();
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
