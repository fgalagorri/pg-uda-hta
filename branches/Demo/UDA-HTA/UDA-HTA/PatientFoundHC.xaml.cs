using System.Windows;
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
