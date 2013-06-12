using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gateway;

namespace UDA_HTA.UserControls
{
    /// <summary>
    /// Interaction logic for PatientFinder.xaml
    /// </summary>
    public partial class PatientFinder : UserControl
    {
        public PatientFinder()
        {
            InitializeComponent();
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var controller = GatewayController.GetInstance();
            var patients = controller.ListPatients(txtDocument.Text, txtName.Text, txtSurname.Text,
                                                   dtBirthDate.SelectedDate, long.Parse(txtRegistry.Text));

            grPatients.DataContext = patients;
        }


        private void grPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Open patient viewer for the selected patient...
        }
    }
}
