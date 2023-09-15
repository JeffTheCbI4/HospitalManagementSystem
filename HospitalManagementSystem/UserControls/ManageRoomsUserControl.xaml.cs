using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для ManageRoomsUserControl.xaml
    /// </summary>
    public partial class ManageRoomsUserControl : UserControl, INotifyPropertyChanged
    {
        ContentControl OuterContentControl;
        UserControl PreviousUserControl;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();
        private string _addingRoomName = "";
        public string AddingRoomName { 
            get { return _addingRoomName; } 
            set 
            {
                if (!_addingRoomName.Equals(value))
                {
                    _addingRoomName = value;
                    NotifyPropertyChanged("AddingRoomName");
                }
            } }
        private int _addingRoomCapacity { get; set; }
        public int AddingRoomCapacity {
            get { return _addingRoomCapacity; }
            set
            {
                if (_addingRoomCapacity != value)
                {
                    _addingRoomCapacity = value;
                    NotifyPropertyChanged("AddingRoomCapacity");
                }
            }
        }

        public ManageRoomsUserControl(ContentControl outerContentControl, UserControl previousUserControl)
        {
            InitializeComponent();
            OuterContentControl = outerContentControl;
            PreviousUserControl = previousUserControl;
            this.DataContext = this;
        }

        private void ButtonAddRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string roomName = AddingRoomName.Trim();
                int roomCapacity = AddingRoomCapacity;
                if (String.IsNullOrWhiteSpace(roomName))
                {
                    throw new Exception("Name must not be empty or white space");
                }
                var context = App.DBContext;
                Room room = new()
                {
                    Name = roomName,
                    Capacity = roomCapacity
                };
                context.Rooms.Add(room);
                context.SaveChanges();

                Rooms.Add(room);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                "Failed to add room to database. Reason:\n" + ex.Message,
                "Fail",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
                return;
            }
            MessageBox.Show(
                "Room successfully added",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            OuterContentControl.Content = PreviousUserControl;
        }

        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }

        private void dataGrid_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NotifyPropertyChanged(string propName)
        {
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
