using System;
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
using UDA_HTA.UserControls.MainWindow.Administration.UserManagement;
using UDA_HTA.UserControls.MainWindow.Administration.Drugs;
using UDA_HTA.UserControls.MainWindow.Patients;
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

            if (usr.Role.Equals("Administrador"))
            {
                ContainerPatient.Content = new PatientFinder(this);
                ContainerPatient.Visibility = Visibility.Visible;
                ContainerInvestigation.Content = new ResearchFinder(this);
                ContainerInvestigation.Visibility = Visibility.Hidden;
                ContainerAdministration.Content = null;
                ContainerAdministration.Visibility = Visibility.Hidden;
            }
            else
            {
                if (usr.Role.Equals("Clinico"))
                {
                    this.tabAdministration.Visibility = Visibility.Hidden;
                    this.itemNewUsr.Visibility = Visibility.Collapsed;
                    this.itemFindUsr.Visibility = Visibility.Collapsed;
                    ContainerPatient.Content = new PatientFinder(this);
                    ContainerPatient.Visibility = Visibility.Visible;
                    ContainerInvestigation.Content = new ResearchFinder(this);
                    ContainerInvestigation.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (usr.Role.Equals("Tecnico"))
                    {
                        this.tabInvestigacion.Visibility = Visibility.Hidden;
                        this.tabAdministration.Visibility = Visibility.Hidden;
                        this.itemNewUsr.Visibility = Visibility.Collapsed;
                        this.itemFindUsr.Visibility = Visibility.Collapsed;
                        ContainerPatient.Content = new PatientFinder(this);
                        ContainerPatient.Visibility = Visibility.Visible;
                    }
                }
            }

            btnEditPatient.IsEnabled = false;
            btnEditDiagnosis.IsEnabled = false;
            btnEditDiagnosis.IsEnabled = false;
            btnExportReport.IsEnabled = false;

            btnAddStudyResearch.IsEnabled = false;
            btnEditResearch.IsEnabled = false;
            btnExportXLS.IsEnabled = false;
            //btnExportCSV.IsEnabled = false;

            btnEditDrugs.IsEnabled = false;

            btnEditDoctor.IsEnabled = false;
        }

        private void MenuRibbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.tabPaciente.IsSelected)
            {
                ContainerPatient.Visibility = Visibility.Visible;
                ContainerAdministration.Visibility = Visibility.Hidden;
                ContainerInvestigation.Visibility = Visibility.Hidden;
            }


            if (this.tabInvestigacion.IsSelected)
            {
                ContainerPatient.Visibility = Visibility.Hidden;
                ContainerAdministration.Visibility = Visibility.Hidden;
                ContainerInvestigation.Visibility = Visibility.Visible;
            }


            if (this.tabAdministration.IsSelected)
            {
                ContainerPatient.Visibility = Visibility.Hidden;
                ContainerAdministration.Visibility = Visibility.Visible;
                ContainerInvestigation.Visibility = Visibility.Hidden;
            }
        }


        #region Report

        private void CreateNewReport(object sender, RoutedEventArgs e)
        {
            try
            {
                btnEditPatient.IsEnabled = false;
                btnEditDiagnosis.IsEnabled = false;
                btnExportReport.IsEnabled = false;

                var newReportPopup = new NewReportFinder {Owner = this};
                var imported = newReportPopup.ShowDialog();

                if (imported.HasValue && imported.Value)
                {
                    long patientId, reportId;
                    // Despliego el nuevo informe

                    GatewayController.GetInstance().GetLastInsertedReport(out patientId, out reportId);
                    ContainerPatient.Content = new PatientViewer(patientId, reportId, this);
                }
            }

            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditDiagnosis(object sender, RoutedEventArgs e)
        {
            var pv = ContainerPatient.Content as PatientViewer;
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
            try
            {
                long reportId;
                string diagnosis;

                var de = (DiagnosisEditor) sender;
                var saveChanges = de.ChangesCommited(out reportId, out diagnosis);

                if (saveChanges)
                {

                    Mouse.OverrideCursor = Cursors.Wait;
                    var d = GatewayController.GetInstance().UpdateDiagnosis(reportId, diagnosis);
                    var pv = ContainerPatient.Content as PatientViewer;
                    if (pv != null && pv.GetSelectedReport() != null)
                    {
                        pv.UpdateDiagnosis(d);
                    }
                }
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportReport(object sender, RoutedEventArgs e)
        {
            var pv = ContainerPatient.Content as PatientViewer;
            if (pv != null && pv.GetSelectedReport() != null)
            {
                var er = new ExportReport(this, pv.GetSelectedReport());
                er.ShowDialog();
            }
        }


        #endregion

        #region Patient

        private void FindPatient(object sender, RoutedEventArgs e)
        {
            btnEditPatient.IsEnabled = false;
            btnEditDiagnosis.IsEnabled = false;
            btnExportReport.IsEnabled = false;

            ContainerPatient.Content = new PatientFinder(this);
            ;
        }

        public void PatientSelected(PatientSearch patient)
        {
            if (patient.UdaId.HasValue)
                ContainerPatient.Content = new PatientViewer(patient, this);
        }

        private void BtnNewPatient_OnClick(object sender, RoutedEventArgs e)
        {
            var newPatientWindow = new NewPatient(null) {Owner = this};
            newPatientWindow.ShowDialog();
        }

        private void BtnEditPatient_OnClick(object sender, RoutedEventArgs e)
        {
            var pv = ContainerPatient.Content as PatientViewer;
            if (pv != null && pv.GetSelectedPatient() != null)
            {
                var patient = pv.GetSelectedPatient();
                var editPatientWindow = new NewPatient(patient);
                var edition = editPatientWindow.ShowDialog();

                if(edition.HasValue && edition.Value)
                    pv.UpdatePatient(editPatientWindow.Patient);
            }
        }

        #endregion

        #region Investigaciones

        public void InvestigationSelected(InvestigationSearch investigation)
        {
            btnAddStudyResearch.IsEnabled = true;
            btnEditResearch.IsEnabled = true;
            btnExportXLS.IsEnabled = true;
            //btnExportCSV.IsEnabled = true;
            ContainerInvestigation.Content = new ResearchViewer(investigation.IdInvestigation);

        }

        private void BtnNewResearch_OnClick(object sender, RoutedEventArgs e)
        {
            var newResearchWindow = new NewResearch(this, null);
            newResearchWindow.ShowDialog();
        }


        private void BtnAddReport_OnClick(object sender, RoutedEventArgs e)
        {
            var rv = ContainerInvestigation.Content as ResearchViewer;
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
            ContainerInvestigation.Content = new ResearchFinder(this);
        }

        private void BtnEditResearch_OnClick(object sender, RoutedEventArgs e)
        {
            var rv = ContainerInvestigation.Content as ResearchViewer;
            if (rv != null && rv.GetSelectedInvestigation() != null)
            {
                var i = rv.GetSelectedInvestigation();

                var newResearchWindow = new NewResearch(this, i);
                newResearchWindow.Show();
            }
        }

        private void ExportXLS(object sender, RoutedEventArgs e)
        {
            var pv = ContainerInvestigation.Content as ResearchViewer;
            if (pv != null && pv.GetSelectedInvestigation() != null)
            {
                var investigation = pv.GetSelectedInvestigation();

                SaveFileDialog saveAs = new SaveFileDialog
                    {
                        FileName = "Investigacion " + investigation.Name,
                        DefaultExt = ".xlsx"
                        //                    Filter = "*.xlsm"
                    };
                saveAs.FileName += " " + investigation.CreationDate
                                                      .ToString(ConfigurationManager.AppSettings["ShortDateString"])
                                                      .Replace('/', '-');

                var result = saveAs.ShowDialog();
                if (result.Value)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    try
                    {
                        GatewayController.GetInstance().ExportInvestigationXLS(investigation, saveAs.FileName);
                        // Abro el archivo exportado
                        Process.Start(saveAs.FileName);
                        Mouse.OverrideCursor = null;
                    }
                    catch (Exception exception)
                    {
                        Mouse.OverrideCursor = null;
                        MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);                        
                    }
                }
            }

        }

        private void ExportCSV(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Usuario

        private void NewUser(object sender, RoutedEventArgs e)
        {
            btnEditDoctor.IsEnabled = false;
            var newUserWindow = new NewUser();
            newUserWindow.ShowDialog();
        }

        private void FindUser(object sender, RoutedEventArgs e)
        {
            btnEditDoctor.IsEnabled = false;
            tabAdministration.IsSelected = true;
            ContainerInvestigation.Visibility = Visibility.Hidden;
            ContainerPatient.Visibility = Visibility.Hidden;
            ContainerAdministration.Visibility = Visibility.Visible;
            ContainerAdministration.Content = new UserFinder(this);

        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            var uf = ContainerAdministration.Content as UserFinder;
            if (uf != null && uf.GetSelectedUser() != null)
            {
                var u = uf.GetSelectedUser();

                ContainerAdministration.Content = new EditUser(u);
            }
        }

        private void ChangePassword(object sender, RoutedEventArgs e)
        {
            btnEditDoctor.IsEnabled = false;
            var changePswdWindow = new ChangePassword();
            changePswdWindow.ShowDialog();
        }

        #endregion

        #region Drogas

        private void BtnAddDrugs_OnClick(object sender, RoutedEventArgs e)
        {
            btnEditDrugs.IsEnabled = false;
            ContainerAdministration.Content = new AddDrugs(null);
        }

        private void BtnSearchDrugs_OnClick(object sender, RoutedEventArgs e)
        {
            btnEditDrugs.IsEnabled = false;
            ContainerAdministration.Content = new DrugFinder(this);
        }

        private void BtnEditDrugs_OnClick(object sender, RoutedEventArgs e)
        {
            var df = ContainerAdministration.Content as DrugFinder;
            if (df != null && df.GetSelectedDrug() != null)
            {
                var d = df.GetSelectedDrug();

                ContainerAdministration.Content = new AddDrugs(d);
            }

        }

        #endregion

    }
}
