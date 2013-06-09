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
        private Report _report;
        private PatientInformation patientInfo;
        private AdmissionForm admissionForm;
        private OtherInformation otherInfo;

        public ReportCreate(Report report)
        {
            InitializeComponent();
            _report = report;
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
                    _report = patientInfo.Report;
                    admissionForm.Report = _report;
                    CurrentControl.Content = admissionForm;
                    btnBack.IsEnabled = true;
                    _state++;
                    break;
                case 1:
                    _report = admissionForm.Report;
                    otherInfo.Report = _report;
                    CurrentControl.Content = otherInfo;
                    btnBack.IsEnabled = true;
                    btnNext.Content = Finalizar;
                    _state++;
                    break;
                case 2:
                    _report = otherInfo.Report;
                    DialogResult = true;
                    GatewayController.GetInstance().AddImportedData(_report, true); // TODO ver modified

                    Close();
                    break;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            switch (_state)
            {
                case 1:
                    _report = admissionForm.Report;
                    patientInfo.Report = _report;
                    CurrentControl.Content = patientInfo;
                    btnBack.IsEnabled = false;
                    _state--;
                    break;
                case 2:
                    _report = otherInfo.Report;
                    admissionForm.Report = _report;
                    CurrentControl.Content = admissionForm;
                    btnBack.IsEnabled = true;
                    btnNext.Content = Siguiente;
                    _state--;
                    break;
            }
        }
    }
}
