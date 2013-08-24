﻿using System;
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
        private PatientInformation patientInfo;

        private bool _crear;

        public NewPatient(Patient patient)
        {
            //editar
            patientInfo = new PatientInformation(patient);
            _crear = false;

            InitializeComponent();

            CurrentControl.Content = patientInfo;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (patientInfo.IsValid())
            {
                var patient = patientInfo.GetPatient();
                if (_crear)
                {
                    //Crear paciente
                    GatewayController.GetInstance().CreatePatient(patient);                    
                }
                else
                {
                    //Editar paciente
                    GatewayController.GetInstance().EditPatient(patient);
                }
                Close();                
            }
            else
            {
                MessageBox.Show("Alguno de los datos no es correcto"); //TODO indicar que datos no son correctos
            }
        }
    }
}