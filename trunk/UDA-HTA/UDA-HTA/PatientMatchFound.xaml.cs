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
    /// Interaction logic for PatientMatchFound.xaml
    /// </summary>
    public partial class PatientMatchFound : Window
    {
        public PatientMatchFound(Patient patient)
        {
            InitializeComponent();

            MatchInformation.SetPatientInfo(patient);
            MatchInformation.CollapseEmergencyContacts();
        }

        private void btnUsePatientData_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
