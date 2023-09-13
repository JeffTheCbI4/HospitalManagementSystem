using HospitalManagementSystem.Models;
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
    /// Логика взаимодействия для ManageStaffUserControl.xaml
    /// </summary>
    public partial class ManageStaffUserControl : UserControl
    {
        ContentControl OuterContentControl;
        UserControl PreviousUserControl;
        User? ChosenUser;
        public ManageStaffUserControl(ContentControl outerContentControl, UserControl previousUserControl)
        {
            InitializeComponent();
            OuterContentControl = outerContentControl;
            PreviousUserControl = previousUserControl;

            var context = App.DBContext;
            var staff = from userRole in context.UsersRoles
                        join user in context.Users on userRole.UserId equals user.UserId
                        join role in context.Roles on userRole.RoleId equals role.RoleId
                        where role.Name.Equals("Doctor")
                        select user;
            DataGridStaff.ItemsSource = staff.ToList();
        }

        private void DataGridStaff_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid source = (DataGrid) e.Source;
            User user = (User) source.SelectedItem;
            textBlockFullName.Text = user.FirstName + " " + user.LastName;

            var context = App.DBContext;
            if (ChosenUser != null)
            {
                var specialties = from userSpec in context.UsersSpecs
                                  join specialty in context.Specialties
                                  on userSpec.SpecialtyId equals specialty.SpecialtyId
                                  where userSpec.UserId == ChosenUser.UserId
                                  select new { specialty.Name };
                DataGridSpecialties.ItemsSource = specialties.ToList();

                var rooms = from roomOccupation in context.RoomOccupations
                            join room in context.Rooms on roomOccupation.RoomId equals room.RoomId
                            where roomOccupation.UserId == ChosenUser.UserId
                            select new { room.Name };
                DataGridRooms.ItemsSource = rooms.ToList();
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            OuterContentControl.Content = PreviousUserControl;
        }
    }
}
