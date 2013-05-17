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
        /* La idea es que la consulta a la BD xa obtener la lista 
         * se haga una sola vez, después se envía el filtro al gateway 
         * que guarda la lista de pedidos en su memoria y devuelve la lista filtrada */
        private static ICollection<PatientReport> _list;

        public NewReportFinder()
        {
            InitializeComponent();

            var controller = GatewayController.GetInstance();
            _list = controller.GetNewReports();

            /*_list = new List<ExampleReportList>();
            _list.Add(new ExampleReportList{
                Date = DateTime.Now,
                Patient = "Juan Alberto Pérez Manzanares",
                Device = "Spacelabs"
            });
            _list.Add(new ExampleReportList
            {
                Date = DateTime.Now.AddDays(-3).AddMinutes(-2480),
                Patient = "Pedro Pereyra",
                Device = "HMS"
            });
            _list.Add(new ExampleReportList
            {
                Date = DateTime.Now.AddDays(-15).AddMinutes(-1080),
                Patient = "Matías Alvez Correa",
                Device = "Spacelabs"
            });
            _list.Add(new ExampleReportList
            {
                Date = DateTime.Now.AddDays(-7).AddMinutes(-520),
                Patient = "Alberto Molina",
                Device = "HMS"
            });*/

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
                var rc = new ReportCreate {Owner = this};
                var cancelled = rc.ShowDialog();

                if (cancelled.HasValue && !cancelled.Value)
                {
                    //grReports.UnselectAll();
                    // Hack to disable selected row style
                    grReports.IsEnabled = false;
                    grReports.IsEnabled = true;
                }
                else
                    Close();
            }
        }
    }
}
