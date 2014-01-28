using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UDA_HTA.UserControls.ReportCreation;
using Gateway;
using Entities;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for NewPatient.xaml
    /// </summary>
    public partial class NewPatient : Window
    {
        public Patient Patient { get { return _patient; } }

        private const string ErrorMessage = "Algunos datos obligatorios no fueron" +
                                            " ingresados o el valor ingresado es incorrecto.";
        private const string Siguiente = "Siguiente >";
        private const string Finalizar = "Finalizar >>";
        private readonly PatientInformation patientInfo;
        private readonly BackgroundEdit backgroundEdit;
        private Patient _patient;
        private bool _crear;
        private int _state = 0;

        public NewPatient(Patient patient)
        {
            InitializeComponent();

            patientInfo = new PatientInformation(patient);
            backgroundEdit = new BackgroundEdit(patient);
            _crear = patient == null;
            _state = 0;

            if (!_crear)
            {
                this.Title = "Editar Paciente";
                _patient = patient;
            }

            CurrentControl.Content = patientInfo;
        }


        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            switch (_state)
            {
                case 0:
                    if (patientInfo.IsValid())
                    {
                        CurrentControl.Content = backgroundEdit;
                        btnBack.IsEnabled = true;
                        btnNext.Content = Finalizar;
                        _state++;
                    }
                    else
                    {
                        MessageBox.Show(ErrorMessage, "Datos faltantes",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case 1:
                    SavePatient();
                    break;
            }
        }

        private void SavePatient()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                _patient = patientInfo.GetPatient();
                _patient = backgroundEdit.GetPatient(_patient);
                if (_crear)
                {
                    //Crear paciente
                    GatewayController.GetInstance().CreatePatient(_patient);
                }
                else
                {
                    //Editar paciente
                    _patient.ModifiedDate = DateTime.Now;
                    GatewayController.GetInstance().EditPatient(_patient);
                    _patient.EmergencyContactList = _patient.EmergencyContactList.Where(e => !e.DeleteContact).ToList();
                    MessageBox.Show("El paciente se ha actualizado correctamente", "Aviso", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    DialogResult = true;
                }

                Close();
                Mouse.OverrideCursor = null;
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            switch (_state)
            {
                case 1:
                    CurrentControl.Content = patientInfo;
                    btnBack.IsEnabled = false;
                    btnNext.Content = Siguiente;
                    _state--;
                    break;
            }
        }
    }
}
