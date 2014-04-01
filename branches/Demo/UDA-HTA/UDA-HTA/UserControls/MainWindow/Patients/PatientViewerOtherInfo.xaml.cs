using System.Collections.Generic;
using System.Configuration;
using System.Windows.Controls;
using Entities;

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    /// <summary>
    /// Interaction logic for PatientViewerOtherInfo.xaml
    /// </summary>
    public partial class PatientViewerOtherInfo : UserControl
    {
        public PatientViewerOtherInfo()
        {
            InitializeComponent();

            colCompTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
            colEffortTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
        }

        public void SetInfo(ICollection<Effort> effort, ICollection<Complication> sintoms)
        {
            grEffort.DataContext = null;
            grEffort.DataContext = effort;

            grSintoms.DataContext = null;
            grSintoms.DataContext = sintoms;
        }
    }
}
