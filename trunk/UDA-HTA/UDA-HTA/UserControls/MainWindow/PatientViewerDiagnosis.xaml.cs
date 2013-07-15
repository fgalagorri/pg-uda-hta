using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Entities;

namespace UDA_HTA.UserControls.MainWindow
{
    /// <summary>
    /// Interaction logic for PatientViewerDiagnosis.xaml
    /// </summary>
    public partial class PatientViewerDiagnosis : UserControl
    {
        public PatientViewerDiagnosis()
        {
            InitializeComponent();
        }

        public void SetReport(Report r)
        {
            txtDiagnosis.Text = r.Diagnosis;
            lblDoctor.Text = r.Doctor.Name;
            lblDate.Text = r.DiagnosisDate.HasValue
                               ? r.DiagnosisDate.Value.ToString(ConfigurationManager.AppSettings["LongDateTimeString"])
                               : "";
        }
        public void UpdateDiagnosis(DiagnosisEdited d)
        {
            txtDiagnosis.Text = d.Diagnosis;
            lblDoctor.Text = d.Doctor.Name;
            lblDate.Text = d.DiagnosisDate.ToString(ConfigurationManager.AppSettings["LongDateTimeString"]);
        }
    }
}
