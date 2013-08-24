using System.Configuration;
using System.Linq;
using System.Windows.Controls;
using Entities;

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    /// <summary>
    /// Interaction logic for PatientViewerData.xaml
    /// </summary>
    public partial class PatientViewerData : UserControl
    {
        public PatientViewerData()
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
