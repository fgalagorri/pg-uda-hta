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
using System.Windows.Shapes;
using Entities;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for PatientFoundHC.xaml
    /// </summary>
    public partial class PatientFoundHC : Window
    {
        public bool UseAllData = false;

        public PatientFoundHC(Patient patient)
        {
            InitializeComponent();
            PatientInformation.SetPatientInfo(patient);
            PatientInformation.CollapseEmergencyContacts();
            UseAllData = false;
        }

        public void HideRegisterButton()
        {
            btnUseRegister.Visibility = Visibility.Collapsed;
        }

        private void btnUseAll_Click(object sender, RoutedEventArgs e)
        {
            UseAllData = true;
            DialogResult = true;
        }

        private void btnUseRegister_Click(object sender, RoutedEventArgs e)
        {
            UseAllData = false;
            DialogResult = true;
        }
    }
}
