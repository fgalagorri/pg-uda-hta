using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Gateway;
using UDA_HTA.UserControls.MainWindow.Patients;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for ProgressExportHC.xaml
    /// </summary>
    public partial class ProgressExportHC : Window
    {
        private BackgroundWorker _bw;
        private PatientViewer _pv;
        private bool _isRunning = false;

        public ProgressExportHC(PatientViewer pv)
        {
            InitializeComponent();
            _pv = pv;

            _bw = new BackgroundWorker {WorkerSupportsCancellation = true};
            _bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        private void VisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!_isRunning)
            {
                _bw.RunWorkerAsync();
                _isRunning = true;
            }
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            _bw.CancelAsync();
        }


        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            var report = _pv.GetSelectedReport();
            report = GatewayHelper.UpdateMeasureSummary(report);

            string pathOverLimit = ConfigurationManager.AppSettings["GraphicOverLimit"];
            string pathPressPrfl = ConfigurationManager.AppSettings["GraphicPressurePrfl"];
            string pathReport = ConfigurationManager.AppSettings["PathExportHC"];

            if (report != null && !String.IsNullOrWhiteSpace(pathOverLimit) &&
                        !String.IsNullOrWhiteSpace(pathPressPrfl) && !String.IsNullOrWhiteSpace(pathReport))
            {
                var controller = GatewayController.GetInstance();
                var sepatator = pathReport.ElementAt(pathReport.Length - 1) == '\\' ? "" : "\\";
                pathReport += sepatator + report.BeginDate.Value.Year + "\\" + report.Patient.DocumentId + "\\";
                controller.CreateFolderIfNotExists(pathReport);
                pathReport += report.Patient.Surnames + "_" + report.Patient.Names + "_" +
                              report.BeginDate.Value.Year + report.BeginDate.Value.Month +
                              report.BeginDate.Value.Day + ".pdf";

                Dispatcher.Invoke((Action) (() =>
                    {
                        _pv.GetChartImage(7);
                        _pv.GetChartImage(8);
                    }));
                
                // Chequeo que no se haya cancelado
                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    Dispatcher.Invoke((Action) (() =>
                        {
                            btnCancel.IsEnabled = false;
                        }));

                    // Creo y guardo el reporte
                    controller.ExportToPdf(report, true, true, true, true,
                                           pathOverLimit, pathPressPrfl, false, pathReport);

                    controller.SetPathReportHC(report.UdaId.Value, pathReport);
                }
            }
        }


        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Mouse.OverrideCursor = null;

            if ((e.Cancelled))
            {
                MessageBox.Show("La publicación del informe ha sido cancelada.", "Publicación cancelada",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                MessageBox.Show("Se ha publicado el informe con éxito.", "Publicación finalizada",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Close();
        }
    }
}
