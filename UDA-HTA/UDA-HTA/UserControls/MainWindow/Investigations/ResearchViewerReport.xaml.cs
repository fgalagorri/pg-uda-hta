using System.Configuration;
using System.Linq;
using System.Windows.Controls;
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
