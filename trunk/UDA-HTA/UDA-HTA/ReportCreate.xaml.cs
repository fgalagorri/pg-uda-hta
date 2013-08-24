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
        private PatientCondition patientCondition;
        private AdmissionForm admissionForm;
        private OtherInformation otherInfo;

        public ReportCreate(Report report)
        {
            InitializeComponent();
            _report = report;
            _state = 0;
            patientInfo = new PatientInformation(report.Patient);
            patientCondition = new PatientCondition(report);
            admissionForm = new AdmissionForm(report);
            otherInfo = new OtherInformation(report);

            CurrentControl.Content = patientInfo;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            switch (_state)
            {
                case 0:
                    if (patientInfo.IsValid())
                    {
                        CurrentControl.Content = patientCondition;
                        btnBack.IsEnabled = true;
                        _state++;
                    }
                    else
                    {
                        //display message error
                    }
                    break;
                case 1:
                    if (patientCondition.IsValid())
                    {
                        CurrentControl.Content = admissionForm;
                        _state++;
                    }
                    else
                    {
                        //display message error
                    }
                    break;
                case 2:
                    if (admissionForm.IsValid())
                    {
                        CurrentControl.Content = otherInfo;
                        btnNext.Content = Finalizar;
                        _state++;
                    }
                    else
                    {
                        //display message error
                    }
                    break;
                case 3:
                    _report = patientInfo.GetReport(_report);
                    _report = patientCondition.GetReport(_report);
                    _report = admissionForm.GetReport(_report);
                    _report = otherInfo.GetReport(_report);
                    DialogResult = true;
                    GatewayController.GetInstance().AddImportedData(_report, true); 

                    Close();
                    break;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            switch (_state)
            {
                case 1:
                    CurrentControl.Content = patientInfo;
                    btnBack.IsEnabled = false;
                    _state--;
                    break;
                case 2:
                    CurrentControl.Content = patientCondition;
                    _state--;
                    break;
                case 3:
                    CurrentControl.Content = admissionForm;
                    btnNext.Content = Siguiente;
                    _state--;
                    break;
            }
        }
    }
}
