using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
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
        private string _addingRoomName = "";
        private int _addingRoomCapacity;
        private string _addingRoomTypeName = "";
        private RoomType _selectedRoomType = new()
        {
            Name = "None"
        };

        ContentControl OuterContentControl;
        UserControl PreviousUserControl;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();
        public ObservableCollection<RoomType> RoomTypes { get; set; } 
            = new ObservableCollection<RoomType>();
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
        public string AddingRoomTypeName
        {
            get { return _addingRoomTypeName; }
            set
            {
                if (!_addingRoomTypeName.Equals(value))
                {
                    _addingRoomTypeName = value;
                    NotifyPropertyChanged("AddingRoomTypeName");
                }
            }
        }
        public RoomType SelectedRoomType
        {
            get { return _selectedRoomType; }
            set
            {
                if (_selectedRoomType == null || !_selectedRoomType.Equals(value))
                {
                    _selectedRoomType = value;
                    NotifyPropertyChanged("SelectedRoomType");
                }
            }
        }

        public ManageRoomsUserControl(ContentControl outerContentControl, UserControl previousUserControl)
        {
            InitializeComponent();
            OuterContentControl = outerContentControl;
            PreviousUserControl = previousUserControl;
            InitRoomTypesCollection();
            InitRoomsCollection();
            this.DataContext = this;
        }

        private void InitRoomsCollection()
        {
            var rooms = from room in App.DBContext.Rooms
                        select room;
            Rooms = new ObservableCollection<Room>(rooms.ToList());
        }
        private void InitRoomTypesCollection()
        {
            var types = from type in App.DBContext.RoomTypes
                        select type;
            RoomTypes = new ObservableCollection<RoomType>(types.ToList());
        }

        private void ButtonAddRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string roomName = AddingRoomName.Trim();
                int roomCapacity = AddingRoomCapacity;
                if (string.IsNullOrWhiteSpace(roomName))
                {
                    throw new Exception("Name must not be empty or white space");
                }
                var context = App.DBContext;
                Room room = new()
                {
                    Name = roomName,
                    Capacity = roomCapacity,
                    RoomTypeId = SelectedRoomType.RoomTypeId,
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

        private void dataGridRoom_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }

        private void dataGridRoom_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            DeleteRowFromDataGrid(sender, e, App.DBContext.Rooms);
        }

        private void dataGridRoomType_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            DeleteRowFromDataGrid(sender, e, App.DBContext.RoomTypes);
        }

        private void DeleteRowFromDataGrid<T> (object sender, CanExecuteRoutedEventArgs e, DbSet<T> contextTable) where T : class
        {
            if (e.Command != DataGrid.DeleteCommand)
            {
                return;
            }
            var answer = MessageBox.Show(
                    "Are you sure you want to delete selected rows?",
                    "Warning",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
            if (answer == MessageBoxResult.Yes)
            {
                var selectedItems = ((DataGrid)e.Source).SelectedItems;
                foreach (T item in selectedItems)
                {
                    contextTable.Remove(item);
                }
                App.DBContext.SaveChanges();
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) && !string.IsNullOrWhiteSpace(e.Text);
        }

        private void NotifyPropertyChanged(string propName)
        {
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void ButtonAddRoomType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string typeName = AddingRoomTypeName.Trim();
                if (string.IsNullOrWhiteSpace(typeName))
                {
                    throw new Exception("Name must not be empty or white space");
                }
                var context = App.DBContext;
                RoomType type = new()
                {
                    Name = typeName
                };
                context.RoomTypes.Add(type);
                context.SaveChanges();

                RoomTypes.Add(type);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                "Failed to add room type to database. Reason:\n" + ex.Message,
                "Fail",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
                return;
            }
            MessageBox.Show(
                "Room type successfully added",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void dataGridRoomType_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
    }
}
