﻿using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Entities;
using Gateway;
using UDA_HTA.Helpers;

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    /// <summary>
    /// Interaction logic for PatientViewer.xaml
    /// </summary>
    public partial class PatientViewer : UserControl
    {
        private Patient _patient;
        private Report _report;
        private UDA_HTA.MainWindow container;

        public PatientViewer()
        {
            InitializeComponent();
        }

        public PatientViewer(PatientSearch patient, UDA_HTA.MainWindow w)
        {
            container = w;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                _patient = GatewayController.GetInstance().GetPatientFullView(patient.UdaId.Value);

                InitializeComponent();

                TabPatient.SetPatientInfo(_patient);
                TabCondition.SetInfo(_patient.LastTempData(), _patient.Background);

                PopulateTree();

                Mouse.OverrideCursor = null;
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public PatientViewer(long patientId, long reportId, UDA_HTA.MainWindow w)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            container = w;
            try
            {
                _patient = GatewayController.GetInstance().GetPatientFullView(patientId);

                InitializeComponent();

                PopulateTree(reportId);
                _report = _patient.ReportList.First(r => r.UdaId.Value.Equals(reportId));

                TabPatient.SetPatientInfo(_patient);
                TabCondition.SetInfo(_report.TemporaryData, _patient.Background);
                TabReportInfo.SetReport(_report);
                ReportInfo.Visibility = Visibility.Visible;
                TabEvents.SetInfo(_report.Carnet.Efforts, _report.Carnet.Complications);
                ReportEvents.Visibility = Visibility.Visible;
                TabReportSummary.SetReport(_report);
                ReportSummary.Visibility = Visibility.Visible;
                TabReportDiagnosis.SetReport(_report);
                ReportDiagnosis.Visibility = Visibility.Visible;
                TabReportData.SetReport(_report);
                ReportData.Visibility = Visibility.Visible;
                TabOverLimit.SetReport(_report);
                ReportOverLimit.Visibility = Visibility.Visible;
                TabPressureProfile.SetReport(_report);
                ReportPressureProfile.Visibility = Visibility.Visible;

                Mouse.OverrideCursor = null;
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void PopulateTree(long? reportId = null)
        {
            treePatient.Items.Clear();
            lblTreeName.Text = _patient.Names + " " + _patient.Surnames;

            foreach (var r in _patient.ReportList.OrderByDescending(r => r.BeginDate))
            {
                TreeViewItem child = new TreeViewItem();
                StackPanel sp = new StackPanel {Orientation = Orientation.Horizontal};

                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri("/Images/tree_study24.png", UriKind.Relative);
                src.EndInit();
                Image img = new Image {Source = src};
                sp.Children.Add(img);

                Label lbl = new Label {Content = r.BeginDate.Value.ToShortDateString()};
                sp.Children.Add(lbl);

                child.Header = sp;
                if (reportId.HasValue && r.UdaId.Equals(reportId))
                    child.IsSelected = true;
                treePatient.Items.Add(child);
            }
        }


        private void treePatient_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            int index = treePatient.Items.IndexOf(e.NewValue);
            if (index >= 0)
            {
                container.EnableDiagnosis();
                container.btnExportReport.IsEnabled = true;
                container.btnEditReport.IsEnabled = true;
                container.EnablePublish();

                _report = _patient.ReportList
                    .OrderByDescending(r => r.BeginDate)
                    .ElementAt(index);
                _report.Patient = _patient;

                TabCondition.SetInfo(_report.TemporaryData, _patient.Background);
                TabReportInfo.SetReport(_report);
                ReportInfo.Visibility = Visibility.Visible;
                TabEvents.SetInfo(_report.Carnet.Efforts, _report.Carnet.Complications);
                ReportEvents.Visibility = Visibility.Visible;
                TabReportSummary.SetReport(_report);
                ReportSummary.Visibility = Visibility.Visible;
                TabReportDiagnosis.SetReport(_report);
                ReportDiagnosis.Visibility = Visibility.Visible;
                TabReportData.SetParent(this);
                TabReportData.SetReport(_report);
                ReportData.Visibility = Visibility.Visible;
                TabOverLimit.SetReport(_report);
                ReportOverLimit.Visibility = Visibility.Visible;
                TabPressureProfile.SetReport(_report);
                ReportPressureProfile.Visibility = Visibility.Visible;
            }
            else
            {
                container.DisableDiagnosis();
                container.btnExportReport.IsEnabled = false;
                container.btnEditReport.IsEnabled = false;
                container.DisablePublish();

                TabCondition.SetInfo(_patient.LastTempData(), _patient.Background);
                if (!ReportPatient.IsSelected && !ReportCondition.IsSelected)
                    ReportPatient.IsSelected = true;

                _report = null;
                ReportInfo.Visibility = Visibility.Collapsed;
                ReportEvents.Visibility = Visibility.Collapsed;
                ReportSummary.Visibility = Visibility.Collapsed;
                ReportDiagnosis.Visibility = Visibility.Collapsed;
                ReportData.Visibility = Visibility.Collapsed;
                ReportOverLimit.Visibility = Visibility.Collapsed;
                ReportPressureProfile.Visibility = Visibility.Collapsed;
            }
            Mouse.OverrideCursor = null;
        }

        public void SelectTab(int tab)
        {
            PatientTabControl.SelectedIndex = tab;  
            PatientTabControl.UpdateLayout();
        }

        public void GetChartImage(int tab)
        {
            switch (tab)
            {
                case 7:
                    TabOverLimit.GetChartImage();
                    break;
                case 8:
                    TabPressureProfile.GetChartImage();
                    break;
            }
        }

        public Report GetSelectedReport()
        {
            return _report;
        }

        public Patient GetSelectedPatient()
        {
            return _patient;
        }

        public void UpdateDiagnosis(DiagnosisEdited d)
        {
            var report = _patient.ReportList.First(r => r.UdaId == d.ReportId);
            report.Diagnosis = d.Diagnosis;
            report.DiagnosisDate = d.DiagnosisDate;
            report.Doctor = d.Doctor;

            _report.Diagnosis = d.Diagnosis;
            _report.DiagnosisDate = d.DiagnosisDate;
            _report.Doctor = d.Doctor;

            TabReportDiagnosis.UpdateDiagnosis(d);
        }

        public void UpdateMeasure(Measurement m)
        {
            // Actualizar valores del reporte
            _report = UpdateReportHelper.UpdateMeasure(_report, m);
            GatewayController.GetInstance().UpdateMeasureSummary(_report);

            // Actualizar las vistas actuales
            TabReportSummary.SetReport(_report);
            TabReportData.SetReport(_report);
            TabOverLimit.SetReport(_report);
            TabPressureProfile.SetReport(_report);
        }

        public void UpdatePatient(Patient p)
        {
            TabPatient.SetPatientInfo(p);
            if(_report == null)
                TabCondition.SetInfo(p.LastTempData(), p.Background);
        }

        public void ReportUpdated(Report report)
        {
            _report = report;
            _patient.ReportList.Remove(_patient.ReportList.First(r => r.UdaId == report.UdaId));
            _patient.ReportList.Add(report);

            // Update tabs
            TabCondition.SetInfo(_report.TemporaryData, _patient.Background);
            TabReportInfo.SetReport(_report);
            TabEvents.SetInfo(_report.Carnet.Efforts, _report.Carnet.Complications);
            TabReportSummary.SetReport(_report);
            TabReportData.SetReport(_report);
            TabOverLimit.SetReport(_report);
            TabPressureProfile.SetReport(_report);
        }
    }
}
