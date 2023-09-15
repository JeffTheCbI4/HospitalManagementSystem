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
using System.Collections.ObjectModel;

namespace HospitalManagementSystem.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MedicineUserControl.xaml
    /// </summary>
    public partial class MedicineUserControl : UserControl
    {
        ContentControl OuterContentControl;
        UserControl PreviousUserControl;
        public ObservableCollection<Medicine> MedicinesColl { get; set; } = new ObservableCollection<Medicine>();
        public MedicineUserControl(ContentControl outerContentControl, UserControl previousUserControl)
        {
            InitializeComponent();
            OuterContentControl = outerContentControl;
            PreviousUserControl = previousUserControl;
            InitMedicinesColl();
            this.DataContext = this;
        }

        private void ButtonAddMedicine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string medName = textBoxMedicineName.Text.Trim();
                if (String.IsNullOrWhiteSpace(medName))
                {
                    throw new Exception("Name must not be empty or white space");
                }
                var context = App.DBContext;
                Medicine med = new Medicine();
                med.Name = medName;
                context.Medicines.Add(med);
                context.SaveChanges();

                MedicinesColl.Add(med);
            } catch (Exception ex)
            {
                MessageBox.Show(
                "Failed to add medicine to database. Reason:\n" + ex.Message,
                "Fail",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
                return;
            }
            MessageBox.Show(
                "Medicine successfully added", 
                "Success", 
                MessageBoxButton.OK, 
                MessageBoxImage.Information);
        }

        private void InitMedicinesColl()
        {
            var context = App.DBContext;
            var meds = from med in context.Medicines
                       select med;
            MedicinesColl = new ObservableCollection<Medicine>(meds.ToList());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            OuterContentControl.Content = PreviousUserControl;
        }

        private void dataGridMedicine_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
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
                var context = App.DBContext;
                var selectedMeds = dataGridMedicine.SelectedItems;
                foreach (Medicine med in selectedMeds)
                {
                    context.Medicines.Remove(med);
                }
                context.SaveChanges();
            }
            else
            {
                e.Handled = true;
            }
        }

        private void dataGridMedicine_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit)
            {
                e.Cancel = true;
                return;
            }
            try
            {                
                var context = App.DBContext;
                Medicine? editedMedicine = e.Row.DataContext as Medicine ?? throw new Exception("Couldn't find edited row");
                string trimmedName = editedMedicine.Name.Trim();
                if (string.IsNullOrWhiteSpace(trimmedName)) throw new Exception("New name must not be empty");

                editedMedicine.Name = trimmedName;
                context.SaveChanges();
            } catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to edit information. Reason:" + ex.Message, 
                    "Fail",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                e.Cancel = true;
                return;
            }
        }
    }
}
