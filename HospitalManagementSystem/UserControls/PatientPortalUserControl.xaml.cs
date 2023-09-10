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
    /// Логика взаимодействия для PatientUserControl.xaml
    /// </summary>
    public partial class PatientPortalUserControl : UserControl
    {
        ContentControl OuterContentControl;
        UserControl PreviousUserControl;
        public PatientPortalUserControl(ContentControl outerContentControl, UserControl previousUserControl)
        {
            InitializeComponent();
            OuterContentControl = outerContentControl;
            PreviousUserControl = previousUserControl;
        }
    }
}
