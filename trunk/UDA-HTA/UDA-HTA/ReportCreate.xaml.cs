using System.Windows;
using Entities;
using UDA_HTA.UserControls.ReportCreation;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for ReportCreate.xaml
    /// </summary>
    public partial class ReportCreate : Window
    {
        private const string Siguiente = "Siguiente >";
        private const string Finalizar = "Finalizar >>";
        private int _state;
        private PatientInformation patientinfo;
        private AdmissionForm admissionForm;
        private OtherInformation otherInfo;

        public ReportCreate(Report report)
        {
            InitializeComponent();
            _state = 0;
            patientinfo = new PatientInformation(report);
            admissionForm = new AdmissionForm(report);
            otherInfo = new OtherInformation(report);

            CurrentControl.Content = patientinfo;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            switch (_state)
            {
                case 0:
                    CurrentControl.Content = admissionForm;
                    btnBack.IsEnabled = true;
                    _state++;
                    break;
                case 1:
                    CurrentControl.Content = otherInfo;
                    btnBack.IsEnabled = true;
                    btnNext.Content = Finalizar;
                    _state++;
                    break;
                case 2:
                    DialogResult = true;
                    Close();
                    break;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            switch (_state)
            {
                case 1:
                    CurrentControl.Content = patientinfo;
                    btnBack.IsEnabled = false;
                    _state--;
                    break;
                case 2:             
                    CurrentControl.Content = admissionForm;
                    btnBack.IsEnabled = true;
                    btnNext.Content = Siguiente;
                    _state--;
                    break;
            }
        }
    }
}
