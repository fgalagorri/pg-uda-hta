using System.Collections.Generic;
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
        }

        public void SetInfo(ICollection<Effort> effort, ICollection<Complication> sintoms)
        {
            grEffort.DataContext = effort;
            grSintoms.DataContext = sintoms;
        }
    }
}
