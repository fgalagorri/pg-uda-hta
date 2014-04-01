using System;
using System.Windows;
using System.Windows.Input;
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
        private const string ErrorMessage = "Algunos datos obligatorios no fueron" +
                                            " ingresados o el valor ingresado es incorrecto.";
        private const string Siguiente = "Siguiente >";
        private const string Finalizar = "Finalizar >>";
        private bool _isEdition;
        private int _state;
        private Report _report;
        private PatientInformation patientInfo;
        private PatientCondition patientCondition;
        private AdmissionForm admissionForm;
        private OtherInformation otherInfo;

        public ReportCreate(Report report, bool isEdition = false)
        {
            InitializeComponent();
            _report = report;
            _isEdition = isEdition;

            patientCondition = new PatientCondition(report, _isEdition);
            admissionForm = new AdmissionForm(report);
            otherInfo = new OtherInformation(report);

            if (!_isEdition)
            {
                _state = 0;
                patientInfo = new PatientInformation(report.Patient);
                CurrentControl.Content = patientInfo;
            }
            else
            {
                _state = 1;
                CurrentControl.Content = patientCondition;
            }
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
                        MessageBox.Show(ErrorMessage, "Datos faltantes",
                                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case 1:
                    if (patientCondition.IsValid())
                    {
                        CurrentControl.Content = admissionForm;
                        btnBack.IsEnabled = true;
                        _state++;
                    }
                    else
                    {
                        MessageBox.Show(ErrorMessage, "Datos faltantes",
                                        MessageBoxButton.OK, MessageBoxImage.Warning);
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
                        MessageBox.Show(ErrorMessage, "Datos faltantes",
                                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case 3:
                    if (!_isEdition)
                        _report = patientInfo.GetReport(_report);
                    _report = patientCondition.GetReport(_report);
                    _report = admissionForm.GetReport(_report);
                    _report = otherInfo.GetReport(_report);
                    DialogResult = true;
                    try
                    {
                        var controller = GatewayController.GetInstance();
                        Mouse.OverrideCursor = Cursors.Wait;
                        if (!_isEdition)
                        {
                            // Ver si existe un paciente similar
                            if (!_report.Patient.UdaId.HasValue)
                            {
                                // Chequeo que no exista un paciente en la BD
                                var p = controller.FindSimilarPatient(_report.Patient.DocumentId,
                                                                      _report.Patient.RegisterNumber);
                                if (p != null)
                                {
                                    var pmf = new PatientMatchFound(p);
                                    bool? usePatient = pmf.ShowDialog();
                                    if (usePatient.HasValue && usePatient.Value)
                                        _report.Patient.UdaId = p.UdaId;
                                }
                            }
                            GatewayController.GetInstance().AddImportedData(_report, true);
                        }
                        else
                        {
                            _report = GatewayController.GetInstance().UpdateReport(_report);
                        }
                        Mouse.OverrideCursor = null;
                    }
                    catch (Exception exception)
                    {
                        Mouse.OverrideCursor = null;
                        MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

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
                    if (_isEdition)
                        btnBack.IsEnabled = false;
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
