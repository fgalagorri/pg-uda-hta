﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Entities;
using Gateway;
using Microsoft.Win32;
using UDA_HTA.UserControls.MainWindow.Patients;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for ExportReport.xaml
    /// </summary>
    public partial class ExportReport : Window
    {
        private UDA_HTA.MainWindow container;
        private Report _report;

        public ExportReport(UDA_HTA.MainWindow w)
        {
            container = w;
//            _report = report;

            InitializeComponent();
        }

        
        private void ExportDoc(object sender, RoutedEventArgs e)
        {
            try
            {
                var pv = container.ContainerPatient.Content as PatientViewer;
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
                        var includePatientData = cbPatientData.IsChecked != null && cbPatientData.IsChecked.Value;
                        var includeDiagnostic = cbDiagnostic.IsChecked != null && cbDiagnostic.IsChecked.Value;
                        var includeProfile = cbProfile.IsChecked != null && cbProfile.IsChecked.Value;
                        var includeGraphic = cbGraphic.IsChecked != null && cbGraphic.IsChecked.Value;
                        var includeMeasures = cbRegisterValues.IsChecked != null && cbRegisterValues.IsChecked.Value;

                        string pathOverLimit = ConfigurationManager.AppSettings["GraphicOverLimit"];
                        string pathPressPrfl = ConfigurationManager.AppSettings["GraphicPressurePrfl"];

                        if (includeGraphic)
                        {
                            pv.GetChartImage(7);
                            pv.GetChartImage(8);
                        }

                        Mouse.OverrideCursor = Cursors.Wait;

                        GatewayController.GetInstance().ExportToDocx(report, includePatientData,
                                                                     includeDiagnostic, includeProfile,
                                                                     includeGraphic, pathOverLimit, pathPressPrfl, 
                                                                     includeMeasures, saveAs.FileName);

                        // Abro el archivo exportado
                        Process.Start(saveAs.FileName);
                        Mouse.OverrideCursor = null;
                    }
                }
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void ExportPdf(object sender, RoutedEventArgs e)
        {
            try
            {
                var pv = container.ContainerPatient.Content as PatientViewer;
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
                        var includePatientData = cbPatientData.IsChecked != null && cbPatientData.IsChecked.Value;
                        var includeDiagnostic = cbDiagnostic.IsChecked != null && cbDiagnostic.IsChecked.Value;
                        var includeProfile = cbProfile.IsChecked != null && cbProfile.IsChecked.Value;
                        var includeGraphic = cbGraphic.IsChecked != null && cbGraphic.IsChecked.Value;
                        var includeMeasures = cbRegisterValues.IsChecked != null && cbRegisterValues.IsChecked.Value;

                        Mouse.OverrideCursor = Cursors.Wait;

                        string pathOverLimit = ConfigurationManager.AppSettings["GraphicOverLimit"];
                        string pathPressPrfl = ConfigurationManager.AppSettings["GraphicPressurePrfl"];

                        if (includeGraphic)
                        {
                            pv.GetChartImage(7);
                            pv.GetChartImage(8);
                        }

                        GatewayController.GetInstance()
                                         .ExportToPdf(report, includePatientData, includeDiagnostic, includeProfile,
                                                      includeGraphic, pathOverLimit, pathPressPrfl, includeMeasures, saveAs.FileName);
                        // Abro el archivo exportado
                        Process.Start(saveAs.FileName);
                        Mouse.OverrideCursor = null;
                    }
                }
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
