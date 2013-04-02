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
using UDA_HTA.UserControls;
using UDA_HTA.UserControls.ReportCreation;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for ReportCreate.xaml
    /// </summary>
    public partial class ReportCreate : Window
    {
        private static int state = 0;
        private PatientInformation patientinfo;
        private AdmissionForm admissionForm;
        private ManualInput manualInput;
        private OtherInformation otherInfo;

        public ReportCreate()
        {
            InitializeComponent();
            
            patientinfo = new PatientInformation();
            admissionForm = new AdmissionForm();
            manualInput = new ManualInput();
            otherInfo = new OtherInformation();

            CurrentControl.Content = patientinfo;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            switch (state)
            {
                case 0:
                    CurrentControl.Content = admissionForm;
                    btnBack.IsEnabled = true;
                    state++;
                    break;
                case 1:
                    CurrentControl.Content = manualInput;
                    btnBack.IsEnabled = true;
                    state++;
                    break;
                case 2:
                    CurrentControl.Content = otherInfo;
                    btnBack.IsEnabled = true;
                    btnNext.Content = "Finalizar >>";
                    state++;
                    break;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            switch (state)
            {
                case 1:
                    CurrentControl.Content = patientinfo;
                    btnBack.IsEnabled = false;
                    state--;
                    break;
                case 2:             
                    CurrentControl.Content = admissionForm;
                    btnBack.IsEnabled = true;
                    state--;
                    break;
                case 3:
                    CurrentControl.Content = manualInput;
                    btnBack.IsEnabled = true;
                    state--;
                    break;
            }
        }
    }
}
