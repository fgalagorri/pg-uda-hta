using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Entities;
using Gateway;

namespace UDA_HTA
{
    /// <summary>
    /// Interaction logic for NewReportFinder.xaml
    /// </summary>
    public partial class NewReportFinder : Window
    {
        private static ICollection<PatientReport> _list;

        public NewReportFinder()
        {
            InitializeComponent();

            var controller = GatewayController.GetInstance();
            _list = controller.GetNewReports();

            grReports.DataContext = _list;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            ICollection<PatientReport> filter = _list;

            if (dtStart.SelectedDate.HasValue)
                filter = filter.Where(d => d.ReportDate >= dtStart.SelectedDate.Value).ToList();

            if (dtEnd.SelectedDate.HasValue)
                filter = filter.Where(d => d.ReportDate <= dtEnd.SelectedDate.Value).ToList();
            
            if (!String.IsNullOrWhiteSpace(patientName.Text))
                filter = filter.Where(d => d.PatientName.ToLower().Contains(patientName.Text.ToLower())
                                    || d.PatientLastName.ToLower().Contains(patientName.Text.ToLower())
                                    || d.PatientDocument.Contains(patientName.Text)).ToList();

            grReports.DataContext = filter;
        }

        private void patientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnFilter_Click(sender, e);
        }

        private void grReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((DataGrid) sender).SelectedIndex != -1)
            {
                var pr = (PatientReport) e.AddedItems[0];
                var report = GatewayController.GetInstance().ImportReport(pr.ReportId, pr.ReportDevice);
                var rc = new ReportCreate(report) {Owner = this};
                var cancelled = rc.ShowDialog();

                if (cancelled.HasValue && !cancelled.Value)
                {
                    //grReports.UnselectAll();
                    // Hack to disable selected row style
                    grReports.IsEnabled = false;
                    grReports.IsEnabled = true;
                }

                Close();
            }
        }
    }
}
