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
using UDA_HTA.UserControls.ReportCreation;
using Gateway;
using Entities;

namespace UDA_HTA.UserControls.MainWindow.Patients
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
        private int _state = 0;

        private PatientInformation patientInfo;
        private PatientCondition patientCondition;
        
        private Patient _patient;
        private bool _crear;

        public NewPatient(Patient patient)
        {
            InitializeComponent();

            patientInfo = new PatientInformation(patient);
            patientCondition = new PatientCondition(patient);
            _crear = patient == null;
            _state = 0;
            
            if (!_crear)
                _patient = patient;

            CurrentControl.Content = patientInfo;
        }


        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (_state == 0)
            {
                if (patientInfo.IsValid())
                {
                    CurrentControl.Content = patientCondition;
                    btnBack.IsEnabled = true;
                    btnNext.Content = Finalizar;
                    _state++;
                }
                else
                {
                    MessageBox.Show(ErrorMessage, "Datos faltantes", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                try
                {
                    if (patientCondition.IsValid())
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        _patient = patientInfo.GetPatient();
                        _patient = patientCondition.GetPatient(_patient);
                        if (_crear)
                        {
                            //Crear paciente
                            GatewayController.GetInstance().CreatePatient(_patient);
                        }
                        else
                        {
                            //Editar paciente
                            _patient.ModifiedDate = DateTime.Today;
                            GatewayController.GetInstance().EditPatient(_patient);
                            MessageBox.Show("El paciente se ha actualizado correctamente", "Aviso", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                            DialogResult = true;
                        }
                        Close();
                        Mouse.OverrideCursor = null;
                    }
                    else
                    {
                        MessageBox.Show(ErrorMessage, "Datos faltantes", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                }
                catch (Exception exception)
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (_state == 1)
            {
                CurrentControl.Content = patientInfo;
                btnBack.IsEnabled = false;
                btnNext.Content = Siguiente;
                _state--;
            }
        }
    }
}
