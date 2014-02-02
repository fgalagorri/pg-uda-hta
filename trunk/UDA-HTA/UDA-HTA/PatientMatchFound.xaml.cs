using System.Windows;
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
