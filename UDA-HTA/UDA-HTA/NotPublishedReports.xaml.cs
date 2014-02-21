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
    /// Interaction logic for NotPublishedReports.xaml
    /// </summary>
    public partial class NotPublishedReports : Window
    {
        public long? PatientId { get; set; }
        public long? ReportId { get; set; }
        private ICollection<NotPublishedReport> _list;

        public NotPublishedReports()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            InitializeComponent();

            try
            {
                _list = GatewayController.GetInstance().GetNotPublishedReports();

                grReports.DataContext = _list.OrderBy(r => r.ReportDate);
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void patientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnFilter_Click(sender, e);
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            ICollection<NotPublishedReport> filter = _list;

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

        private void grReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((DataGrid) sender).SelectedIndex != -1)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                var npr = (NotPublishedReport) e.AddedItems[0];
                PatientId = npr.PatientId;
                ReportId = npr.ReportId;
                DialogResult = true;
                Close();
            }
        }
    }
}
