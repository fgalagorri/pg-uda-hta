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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
