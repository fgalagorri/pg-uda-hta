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
    /// Interaction logic for ResearchViewerReportSummary.xaml
    /// </summary>
    public partial class ResearchViewerReportSummary : UserControl
    {
        public ResearchViewerReportSummary()
        {
            InitializeComponent();
            colDate.Binding.StringFormat = ConfigurationManager.AppSettings["ShortDateString"];
        }

        public void SetReportList(ICollection<Report> lReports)
        {
            grid.DataContext = lReports.OrderBy(r => r.BeginDate.Value);
        }
    }
}
