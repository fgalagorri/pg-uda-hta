using System.Configuration;
using System.Windows.Controls;
using Entities;

namespace UDA_HTA.UserControls.MainWindow.Patients
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
            lblDoctor.Text = r.Doctor;
            lblDate.Text = r.DiagnosisDate.HasValue
                               ? r.DiagnosisDate.Value.ToString(ConfigurationManager.AppSettings["LongDateTimeString"])
                               : "";
        }
        public void UpdateDiagnosis(DiagnosisEdited d)
        {
            txtDiagnosis.Text = d.Diagnosis;
            lblDoctor.Text = d.Doctor;
            lblDate.Text = d.DiagnosisDate.ToString(ConfigurationManager.AppSettings["LongDateTimeString"]);
        }
    }
}
