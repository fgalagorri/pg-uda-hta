using System.Linq;
using System.Windows.Controls;
using Entities;

namespace UDA_HTA.UserControls.MainWindow.Investigations
{
    /// <summary>
    /// Interaction logic for ResearchViewerInformation.xaml
    /// </summary>
    public partial class ResearchViewerInformation : UserControl
    {
        public ResearchViewerInformation()
        {
            InitializeComponent();
        }

        public void SetInformationInfo(Investigation investigation)
        {
            lblName.Content = investigation.Name;
            lblCant.Content = investigation.LReports.LongCount().ToString();
            lblDate.Content = investigation.CreationDate;
            txtComment.Text = investigation.Comment;
        }
    }
}
