using HospitalManagementSystem.Models;
using Microsoft.Data.SqlClient;
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
        ContentControl OuterContentControl;
        UserControl PreviousUserControl;
        public RegisterUserControl(ContentControl outerContentControl, UserControl previousUserControl)
        {
            InitializeComponent();
            OuterContentControl = outerContentControl;
            PreviousUserControl = previousUserControl;
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
                MessageBox.Show(
                    "One of the mandatory fields is empty",
                    "Empty field",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            } else if (CheckNoSameLogin(login))
            {
                MessageBox.Show(
                    "This login is already taken",
                    "Login taken",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            } else if (!password.Equals(repeatPassword))
            {
                MessageBox.Show(
                    "Password and repeated password don't match",
                    "Password mismatch",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            string salt = PasswordMaster.GenerateSalt();
            string passwordHash = PasswordMaster.GetSaltHashedPassword(password, salt);

            HospitalDBContext context = App.DBContext;

            User user = new User();
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Login = login;
            if (!String.IsNullOrEmpty(email)) user.Email = email;
            if (!String.IsNullOrEmpty(phone)) user.Phone = phone;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = salt;

            List<UsersRole> usersRoles = GetCheckedRoles();
            usersRoles.ForEach(role => { role.User = user; user.UsersRoles.Add(role); });

            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            } catch (SqlException ex)
            {
                MessageBox.Show(
                    "Registration failed. Reason: \n" + ex.Message, 
                    "Registration failed", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                return;
            }

            MessageBox.Show(
                    "User successfully registrated",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
        }

        private bool CheckNoSameLogin(string login)
        {
            var context = App.DBContext;
            var sameLogins = from user in context.Users
                             where user.Login.Equals(login)
                             select user;
            return sameLogins.Count() > 0;
        }

        private List<UsersRole> GetCheckedRoles()
        {
            HospitalDBContext context = App.DBContext;
            List<UsersRole> usersRoles = new List<UsersRole>();
            var roles = from r in context.Roles
                        select r;

            foreach (var child in rolesStackPanel.Children)
            {
                if (child is CheckBox)
                {
                    CheckBox cbChild = (CheckBox) child;
                    string cbRoleName = cbChild.Content.ToString();
                    Role role = roles.First((role) => role.Name.Equals(cbRoleName));
                    if ((bool)cbChild.IsChecked && role != null)
                    {
                        UsersRole usersRole = new UsersRole();
                        usersRole.Role = role;
                        usersRoles.Add(usersRole);
                    }
                }
            }
            return usersRoles;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            OuterContentControl.Content = PreviousUserControl;
        }
    }
}
