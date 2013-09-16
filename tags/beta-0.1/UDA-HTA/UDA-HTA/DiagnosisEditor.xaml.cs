using System;
using System.Windows;
using Entities;
using Gateway;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for DiagnosisEditor.xaml
    /// </summary>
    public partial class DiagnosisEditor : Window
    {
        private long _reportId;
        private bool _saveChanges = false;

        public DiagnosisEditor()
        {
            InitializeComponent();
        }

        public void SetReport(Report r)
        {
            _reportId = r.UdaId.Value;
            txtDiagnosis.Text = r.Diagnosis;
        }

        public bool ChangesCommited(out long reportId, out string diagnosis)
        {
            reportId = _reportId;
            diagnosis = txtDiagnosis.Text;

            return _saveChanges;
        }


        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            _saveChanges = !String.IsNullOrWhiteSpace(txtDiagnosis.Text);
            this.Close();
        }
    }
}
