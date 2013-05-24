using System.Windows.Controls;
using Entities;

namespace UDA_HTA.UserControls.ReportCreation
{
    /// <summary>
    /// Interaction logic for AdmissionForm.xaml
    /// </summary>
    public partial class AdmissionForm : UserControl
    {
        public AdmissionForm(Report report)
        {
            InitializeComponent();

            // Initialize with report information
            dtEstudioIni.SelectedDate = report.BeginDate;
        }

        private void btnAddIllness_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void btnDelIllness_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void grBackground_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
