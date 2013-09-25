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
        private const string ErrorMessage = "Algunos datos obligatorios no fueron" +
                                    " ingresados o el valor ingresado es incorrecto.";

        private PatientInformation patientInfo;
        private PatientCondition patientCondition;

        private Patient _patient;

        private bool _crear;

        public NewPatient(Patient patient)
        {
            patientInfo = new PatientInformation(patient);
            patientCondition = new PatientCondition(patient);
            _crear = patient == null;
            
            InitializeComponent();

            if (!_crear)
            {
                //editar
                _patient = patient;
                btnAdd.Content = "Editar Paciente";
                this.Title = "Editar Paciente";
            }

            CurrentControl.Content = patientInfo;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (patientInfo.IsValid())
            {
                try
                {
                    var patient = patientInfo.GetPatient();
                    patient = patientCondition.GetPatient(patient);
                    if (_crear)
                    {
                        //Crear paciente
                        GatewayController.GetInstance().CreatePatient(patient);
                    }
                    else
                    {
                        //Editar paciente
                        patient.UdaId = _patient.UdaId;
                        patient.ModifiedDate = DateTime.Today;
                        GatewayController.GetInstance().EditPatient(patient);
                        MessageBox.Show("El paciente se ha actualizado correctamente", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    Close();                

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Alguno de los datos no es correcto", "Error", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (patientInfo.IsValid())
            {
                CurrentControl.Content = patientCondition;
                btnAdd.IsEnabled = true;
                btnNext.IsEnabled = false;                
            }
            else
            {
                MessageBox.Show(ErrorMessage, "Datos faltantes", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
