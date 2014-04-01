using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Controls;
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
            grid.DataContext = lReports.OrderBy(r => r.BeginDate != null ? r.BeginDate.Value : new DateTime());
        }
    }
}
