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
    /// Логика взаимодействия для AdminPortalUserControl.xaml
    /// </summary>
    public partial class AdminPortalUserControl : UserControl
    {
        ContentControl OuterContentControl;
        UserControl PreviousUserControl;
        public AdminPortalUserControl(ContentControl outerContentControl, UserControl previousUserControl)
        {
            InitializeComponent();
            OuterContentControl = outerContentControl;
            PreviousUserControl = previousUserControl;
        }

        private void buttonRegisterUser_Click(object sender, RoutedEventArgs e)
        {
            OuterContentControl.Content = new RegisterUserControl(OuterContentControl, this);
        }

        private void buttonManageStaff_Click(object sender, RoutedEventArgs e)
        {
            OuterContentControl.Content = new ManageStaffUserControl(OuterContentControl, this);
        }

        private void buttonManageMedicine_Click(object sender, RoutedEventArgs e)
        {
            OuterContentControl.Content = new MedicineUserControl(OuterContentControl, this);
        }
    }
}
