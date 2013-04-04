    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        private static List<ExampleReportList> list;

        public NewReportFinder()
        {
            InitializeComponent();

            list = new List<ExampleReportList>();
            list.Add(new ExampleReportList{
                Date = DateTime.Now,
                Patient = "Juan Alberto Pérez Manzanares",
                Device = "Spacelabs"
            });
            list.Add(new ExampleReportList
            {
                Date = DateTime.Now.AddDays(-3).AddMinutes(-2480),
                Patient = "Pedro Pereyra",
                Device = "HMS"
            });
            list.Add(new ExampleReportList
            {
                Date = DateTime.Now.AddDays(-15).AddMinutes(-1080),
                Patient = "Matías Alvez Correa",
                Device = "Spacelabs"
            });
            list.Add(new ExampleReportList
            {
                Date = DateTime.Now.AddDays(-7).AddMinutes(-520),
                Patient = "Alberto Molina",
                Device = "HMS"
            });

            grReports.DataContext = list;
        }

        private void onRowSelect(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            List<ExampleReportList> filter = list;

            if (dtStart.SelectedDate.HasValue)
                filter = list.Where(d => d.Date >= dtStart.SelectedDate.Value).ToList();
            if (dtEnd.SelectedDate.HasValue)
                filter = list.Where(d => d.Date <= dtEnd.SelectedDate.Value).ToList();
            if (!String.IsNullOrWhiteSpace(patientName.Text))
                filter = list.Where(d => d.Patient.ToLower().Contains(patientName.Text.ToLower())).ToList();

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
                string s = ((ExampleReportList)e.AddedItems[0]).Patient;
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

    public class ExampleReportList
    {
        /* ID para identificarlo */
        public DateTime Date { get; set; }
        public string Patient { get; set; }
        public string Device { get; set; }
    }
}
