using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Gateway;
using UDA_HTA.UserControls;
using UDA_HTA.UserControls.MainWindow;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Container.Content = new PatientFinder(this);
        }


        #region Ribbon Buttons

        private void btnNewReport_Click(object sender, RoutedEventArgs e)
        {
            var newReportPopup = new NewReportFinder { Owner = this };
            var imported = newReportPopup.ShowDialog();

            if (imported.HasValue && imported.Value)
            {
                long patientId, reportId;
                // Despliego el nuevo informe
                GatewayController.GetInstance().GetLastInsertedReport(out patientId, out reportId);
                Container.Content = new PatientViewer(patientId, reportId);
            }
        }

        private void btnEditDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            var pv = Container.Content as PatientViewer;
            if (pv != null && pv.GetSelectedReport() != null)
            {
                var de = new DiagnosisEditor {Owner = this};
                de.Closing += DiagnosisEditorClosed;
                de.SetReport(pv.GetSelectedReport());
                de.Show();
            }
        }
        private void DiagnosisEditorClosed(object sender, CancelEventArgs e)
        {
            long reportId;
            string diagnosis;

            var de = (DiagnosisEditor) sender;
            var saveChanges = de.ChangesCommited(out reportId, out diagnosis);

            if (saveChanges)
            {
                var d = GatewayController.GetInstance().UpdateDiagnosis(reportId, diagnosis);

                var pv = Container.Content as PatientViewer;
                if (pv != null && pv.GetSelectedReport() != null)
                {
                    pv.UpdateDiagnosis(d);
                }
            }
        }


        private void btnFindPatient_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new PatientFinder(this); ;
        }

        #endregion


        private void MenuRibbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void PatientSelected(PatientSearch patient)
        {
            if (patient.UdaId.HasValue)
                Container.Content = new PatientViewer(patient);
        }
    }
}
