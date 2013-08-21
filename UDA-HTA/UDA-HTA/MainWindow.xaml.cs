using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Entities;
using Gateway;
using Microsoft.Win32;
using UDA_HTA.UserControls;
using UDA_HTA.UserControls.MainWindow;
using UDA_HTA.UserManagement;
using UDA_HTA.UserControls.MainWindow.Investigations;
using UDA_HTA.UserControls.MainWindow.Administration;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(User usr)
        {
            InitializeComponent();
            Container.Content = new PatientFinder(this);

            btnAddStudyResearch.IsEnabled = false;
            btnEditResearch.IsEnabled = false;
            btnExportXLS.IsEnabled = false;
            //btnExportCSV.IsEnabled = false;
        }

    #region Ribbon Buttons

        private void CreateNewReport(object sender, RoutedEventArgs e)
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

        private void EditDiagnosis(object sender, RoutedEventArgs e)
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

        private void FindPatient(object sender, RoutedEventArgs e)
        {
            Container.Content = new PatientFinder(this); ;
        }


        private void ExportPdf(object sender, RoutedEventArgs e)
        {
            var pv = Container.Content as PatientViewer;
            if (pv != null && pv.GetSelectedReport() != null)
            {
                var report = pv.GetSelectedReport();

                SaveFileDialog saveAs = new SaveFileDialog
                {
                    FileName = "Registro " + report.Patient.Names + " " + report.Patient.Surnames,
                    DefaultExt = ".pdf",
                    Filter = "PDF Document|*.pdf"
                };
                if (report.BeginDate.HasValue)
                {
                    saveAs.FileName += " " +
                                       report.BeginDate.Value
                                             .ToString(ConfigurationManager.AppSettings["ShortDateString"])
                                             .Replace('/', '-');
                }
                var result = saveAs.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    GatewayController.GetInstance().ExportToPdf(report, saveAs.FileName);
                    // Abro el archivo exportado
                    Process.Start(saveAs.FileName);
                    Mouse.OverrideCursor = null;
                }
            }
        }

        private void ExportDoc(object sender, RoutedEventArgs e)
        {
            var pv = Container.Content as PatientViewer;
            if (pv != null && pv.GetSelectedReport() != null)
            {
                var report = pv.GetSelectedReport();

                SaveFileDialog saveAs = new SaveFileDialog
                    {
                        FileName = "Registro " + report.Patient.Names + " " + report.Patient.Surnames,
                        DefaultExt = ".docx",
                        Filter = "Microsoft Word Document|*.docx"
                    };
                if (report.BeginDate.HasValue)
                {
                    saveAs.FileName += " " +
                                       report.BeginDate.Value
                                             .ToString(ConfigurationManager.AppSettings["ShortDateString"])
                                             .Replace('/', '-');
                }
                var result = saveAs.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    GatewayController.GetInstance().ExportToDocx(report, saveAs.FileName);
                    // Abro el archivo exportado
                    Process.Start(saveAs.FileName);
                    Mouse.OverrideCursor = null;
                }
            }
        }

    #endregion


        private void MenuRibbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.tabPaciente.IsSelected)
            {
                Container.Content = new PatientFinder(this);
            }
            if (this.tabInvestigacion.IsSelected)
            {
                Container.Content = new ResearchFinder(this);
            }
            if (this.tabAdministration.IsSelected)
            {
                Container.Content = null;
            }

        }

        public void PatientSelected(PatientSearch patient)
        {
            if (patient.UdaId.HasValue)
                Container.Content = new PatientViewer(patient);
        }

    #region Usuario

        private void NewUser(object sender, RoutedEventArgs e)
        {
            var newUserWindow = new NewUser();
            newUserWindow.ShowDialog();
        }

        private void FindUser(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ChangePassword(object sender, RoutedEventArgs e)
        {
            var changePswdWindow = new ChangePassword();
            changePswdWindow.ShowDialog();
        }

    #endregion

    #region Investigaciones

        public void InvestigationSelected(InvestigationSearch investigation)
        {
            btnAddStudyResearch.IsEnabled = true;
            btnEditResearch.IsEnabled = true;
            btnExportXLS.IsEnabled = true;
            //btnExportCSV.IsEnabled = true;
            Container.Content = new ResearchViewer(investigation.IdInvestigation);

        }

        private void BtnNewResearch_OnClick(object sender, RoutedEventArgs e)
        {
            var newResearchWindow = new NewResearch(this,null);
            newResearchWindow.ShowDialog();
        }


        private void BtnAddReport_OnClick(object sender, RoutedEventArgs e)
        {
            var rv = Container.Content as ResearchViewer;
            if (rv != null && rv.GetSelectedInvestigation() != null)
            {
                var i = rv.GetSelectedInvestigation();
                var addReportWindow = new AddReportsToResearch(i, this);
                addReportWindow.Show();
                
            }
        }

        private void BtnFindResearch_OnClick(object sender, RoutedEventArgs e)
        {
            btnAddStudyResearch.IsEnabled = false;
            btnEditResearch.IsEnabled = false;
            btnExportXLS.IsEnabled = false;
            //btnExportCSV.IsEnabled = false;
            Container.Content = new ResearchFinder(this);
        }

        private void BtnEditResearch_OnClick(object sender, RoutedEventArgs e)
        {
            var rv = Container.Content as ResearchViewer;
            if (rv != null && rv.GetSelectedInvestigation() != null)
            {
                var i = rv.GetSelectedInvestigation();

                var newResearchWindow = new NewResearch(this, i);
                newResearchWindow.Show();
            }
        }

        private void ExportXLS(object sender, RoutedEventArgs e)
        {
            var pv = Container.Content as ResearchViewer;
            if (pv != null && pv.GetSelectedInvestigation() != null)
            {
                var investigation = pv.GetSelectedInvestigation();

                SaveFileDialog saveAs = new SaveFileDialog
                {
                    FileName = "Investigacion " + investigation.Name,
                    DefaultExt = ".xlsm"
//                    Filter = "*.xlsm"
                };
                saveAs.FileName += " " + investigation.CreationDate
                                             .ToString(ConfigurationManager.AppSettings["ShortDateString"])
                                             .Replace('/', '-');

                var result = saveAs.ShowDialog();
                if (result.Value)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    GatewayController.GetInstance().ExportInvestigationXLS(investigation, saveAs.FileName);
                    // Abro el archivo exportado
                    Process.Start(saveAs.FileName);
                    Mouse.OverrideCursor = null;
                }
            }

        }

        private void ExportCSV(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    #endregion

    #region Drogas
        private void BtnAddDrugs_OnClick(object sender, RoutedEventArgs e)
        {
            Container.Content = new AddDrugs();
        }
    #endregion

    }
}
