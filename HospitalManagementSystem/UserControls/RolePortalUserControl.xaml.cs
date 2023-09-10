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

namespace HospitalManagementSystem.UserControls
{
    /// <summary>
    /// Логика взаимодействия для RolePortalUserControl.xaml
    /// </summary>
    public partial class RolePortalUserControl : UserControl
    {
        public RolePortalUserControl()
        {
            InitializeComponent();
            if(App.LoggedInUser == null)
            {
                MessageBox.Show("User is not logged", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var user = App.LoggedInUser;
            var context = App.DBContext;

            var userRoles = from userRole in context.UsersRoles
                            where userRole.UserId == user.UserId
                            select userRole;

            if (userRoles.FirstOrDefault(userRole => userRole.Role.Name.Equals("Admin")) != null)
            {
                RoleTabControl.SelectedItem = AdminTab;
                AdminTab.Visibility = Visibility.Visible;
                AdminTab.Content = new AdminPortalUserControl(AdminTab, this);
            }

            if (userRoles.FirstOrDefault(userRole => userRole.Role.Name.Equals("Doctor")) != null)
            {
                RoleTabControl.SelectedItem = DoctorTab;
                DoctorTab.Visibility = Visibility.Visible;
                DoctorTab.Content = new DoctorPortalUserControl(DoctorTab, this);
            }

            if (userRoles.FirstOrDefault(userRole => userRole.Role.Name.Equals("Patient")) != null)
            {
                RoleTabControl.SelectedItem = PatientTab;
                PatientTab.Visibility = Visibility.Visible;
                PatientTab.Content = new PatientPortalUserControl(PatientTab, this);
            }
        }
    }
}
