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
using Gateway;

namespace UDA_HTA.UserControls.MainWindow.Investigations
{
    /// <summary>
    /// Interaction logic for ResearchViewer.xaml
    /// </summary>
    public partial class ResearchViewer : UserControl
    {
        private Investigation _investigation;

        public ResearchViewer(InvestigationSearch investigation)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            _investigation = GatewayController.GetInstance().GetInvestigation(investigation.IdInvestigation);

            InitializeComponent();
            
            TabInvestigation.SetInformationInfo(_investigation);
            TabReports.SetReportList(_investigation.LReports);
        }


        private void treeInvestigation_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            throw new NotImplementedException();
        }
    }
}
