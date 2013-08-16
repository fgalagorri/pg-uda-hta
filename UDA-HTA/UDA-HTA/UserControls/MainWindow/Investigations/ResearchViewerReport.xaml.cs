using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Entities;

namespace UDA_HTA.UserControls.MainWindow.Investigations
{
    /// <summary>
    /// Interaction logic for ResearchViewerReport.xaml
    /// </summary>
    public partial class ResearchViewerReport : UserControl
    {
        public ResearchViewerReport()
        {
            InitializeComponent();
            colDate.Binding.StringFormat = ConfigurationManager.AppSettings["ShortDateString"];
            colTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
        }

        public void SetReport(Report r)
        {
            grid.DataContext = r.Measures.OrderBy(m => m.Time.Value);
        }

    }
}
