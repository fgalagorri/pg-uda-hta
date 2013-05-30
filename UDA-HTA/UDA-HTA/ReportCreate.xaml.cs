using System.Windows;
using Entities;
using Gateway;
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
        private PatientInformation patientInfo;
        private AdmissionForm admissionForm;
        private OtherInformation otherInfo;
        public Report Report { get; private set; }

        public ReportCreate(Report report)
        {
            InitializeComponent();
            Report = report;
            _state = 0;
            patientInfo = new PatientInformation();
            admissionForm = new AdmissionForm();
            otherInfo = new OtherInformation();

            patientInfo.Report = report;
            CurrentControl.Content = patientInfo;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            switch (_state)
            {
                case 0:
                    Report = patientInfo.Report;
                    admissionForm.Report = Report;
                    CurrentControl.Content = admissionForm;
                    btnBack.IsEnabled = true;
                    _state++;
                    break;
                case 1:
                    Report = admissionForm.Report;
                    otherInfo.Report = Report;
                    CurrentControl.Content = otherInfo;
                    btnBack.IsEnabled = true;
                    btnNext.Content = Finalizar;
                    _state++;
                    break;
                case 2:
                    Report = otherInfo.Report;
                    DialogResult = true;
                    //GatewayController.GetInstance()

                    Close();
                    break;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            switch (_state)
            {
                case 1:
                    Report = admissionForm.Report;
                    patientInfo.Report = Report;
                    CurrentControl.Content = patientInfo;
                    btnBack.IsEnabled = false;
                    _state--;
                    break;
                case 2:
                    Report = otherInfo.Report;
                    admissionForm.Report = Report;
                    CurrentControl.Content = admissionForm;
                    btnBack.IsEnabled = true;
                    btnNext.Content = Siguiente;
                    _state--;
                    break;
            }
        }
    }
}
