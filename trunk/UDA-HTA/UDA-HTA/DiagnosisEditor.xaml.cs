using System;
using System.Configuration;
using System.Windows;
using System.IO;
using Entities;

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
            if (!string.IsNullOrEmpty(r.Diagnosis))
            {
                txtDiagnosis.Text = r.Diagnosis;
            }
            else
            {
                StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["DiagnosisTemplate"], System.Text.Encoding.GetEncoding("iso-8859-1"));
                var s = sr.ReadToEnd();
                txtDiagnosis.Text = s;                
            }
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
