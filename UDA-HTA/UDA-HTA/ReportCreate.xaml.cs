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
        private int _state;
        private PatientInformation patientinfo;
        private AdmissionForm admissionForm;
        private ManualInput manualInput;
        private OtherInformation otherInfo;

        public ReportCreate()
        {
            InitializeComponent();
            _state = 0;
            patientinfo = new PatientInformation();
            admissionForm = new AdmissionForm();
            manualInput = new ManualInput();
            otherInfo = new OtherInformation();

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
                    CurrentControl.Content = manualInput;
                    btnBack.IsEnabled = true;
                    _state++;
                    break;
                case 2:
                    CurrentControl.Content = otherInfo;
                    btnBack.IsEnabled = true;
                    btnNext.Content = "Finalizar >>";
                    _state++;
                    break;
                case 3:
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
                    _state--;
                    break;
                case 3:
                    CurrentControl.Content = manualInput;
                    btnBack.IsEnabled = true;
                    _state--;
                    break;
            }
        }
    }
}
