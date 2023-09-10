using HospitalManagementSystem.Models;
using HospitalManagementSystem.UserControls;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        private MainWindow Window;
        public LoginUserControl(MainWindow window)
        {
            InitializeComponent();
            Window = window;
        }

        private void uriEmailAddress_Click(object sender, RoutedEventArgs e)
        {
            Window.contentControl1.Content = new RegisterUserControl(Window.contentControl1, this);
        }

        private void buttonLogIn_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = passwordBoxLogin.Password;
            bool check = VerifyLoginPassword(login, password);
            if (check)
            {
                var user = from u in App.DBContext.Users
                           where u.Login.Equals(login)
                           select u;
                App.LoggedInUser = user.First();
                Window.contentControl1.Content = new RolePortalUserControl();
            }
            else
            {
                MessageBox.Show("You entered wrong Login or Password. Please try again.", "Wrong Login or Password", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool VerifyLoginPassword(string login, string password)
        {
            HospitalDBContext context = App.DBContext;
            var users = from user in context.Users
                        where user.Login.Equals(login)
                        select user;
            if (!users.Any())
            {
                return false;
            }

            User _user = users.First();
            string passwordHash = _user.PasswordHash;
            string salt = _user.PasswordSalt;
            bool passwordCheck = PasswordMaster.VerifyPassword(password, passwordHash, salt);

            return passwordCheck;
        }
    }
}
