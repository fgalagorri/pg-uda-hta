using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Entities;
using Gateway;

namespace UDA_HTA.UserControls.MainWindow
{
    /// <summary>
    /// Interaction logic for PatientViewer.xaml
    /// </summary>
    public partial class PatientViewer : UserControl
    {
        private Patient _patient;
        private Report _report;

        public PatientViewer(PatientSearch patient)
        {
            _patient = GatewayController.GetInstance().GetPatientFullView(patient.UdaId.Value);

            InitializeComponent();

            treePatient.Items.Clear();
            TabPaciente.SetPatientInfo(_patient);
            lblTreeName.Text = _patient.Names + " " + _patient.Surnames;

            foreach (var r in _patient.ReportList.OrderByDescending(r=>r.BeginDate))
            {
                TreeViewItem child = new TreeViewItem();
                StackPanel sp = new StackPanel {Orientation = Orientation.Horizontal};

                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri("/Images/tree_study24.png", UriKind.Relative);
                src.EndInit();
                Image img = new Image{Source = src};
                sp.Children.Add(img);

                Label lbl = new Label{Content = r.BeginDate.Value.ToShortDateString()};
                sp.Children.Add(lbl);

                child.Header = sp;
                treePatient.Items.Add(child);
            }
        }


        private void treePatient_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            int index = treePatient.Items.IndexOf(e.NewValue);
            if (index >= 0)
            {
                _report = _patient.ReportList
                                  .OrderByDescending(r => r.BeginDate)
                                  .ElementAt(index);

                TabReportInfo.SetReport(_report);
                ReportInfo.Visibility = Visibility.Visible;
                TabReportSummary.SetReport(_report);
                ReportSummary.Visibility = Visibility.Visible;
                ReportDiagnosis.Visibility = Visibility.Visible;
                ReportData.Visibility = Visibility.Visible;
                ReportCharts.Visibility = Visibility.Visible;
            }
            else
            {
                ReportInfo.Visibility = Visibility.Collapsed;
                ReportSummary.Visibility = Visibility.Collapsed;
                ReportDiagnosis.Visibility = Visibility.Collapsed;
                ReportData.Visibility = Visibility.Collapsed;
                ReportCharts.Visibility = Visibility.Collapsed;
            }
        }


    }
}
