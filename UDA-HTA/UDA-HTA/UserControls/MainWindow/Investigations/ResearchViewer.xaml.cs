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
        private Report _report ;

        public ResearchViewer(InvestigationSearch investigation)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            _investigation = GatewayController.GetInstance().GetInvestigation(investigation.IdInvestigation);

            InitializeComponent();
            
            TabInvestigation.SetInformationInfo(_investigation);
            TabReports.SetReportList(_investigation.LReports);

            PopulateTree();

            Mouse.OverrideCursor = null;
        }

        private void PopulateTree(long? reportId = null)
        {
            treeInvestigation.Items.Clear();
            lblTreeName.Text = _investigation.Name;

            foreach (var r in _investigation.LReports.OrderByDescending(r => r.BeginDate))
            {
                TreeViewItem child = new TreeViewItem();
                StackPanel sp = new StackPanel { Orientation = Orientation.Horizontal };

                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri("/Images/tree_study24.png", UriKind.Relative);
                src.EndInit();
                Image img = new Image { Source = src };
                sp.Children.Add(img);

                Label lbl = new Label { Content = r.BeginDate.Value.ToShortDateString() };
                sp.Children.Add(lbl);

                child.Header = sp;
                if (reportId.HasValue && r.UdaId.Equals(reportId))
                    child.IsSelected = true;
                treeInvestigation.Items.Add(child);
            }
        }



        private void treeInvestigation_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            int index = treeInvestigation.Items.IndexOf(e.NewValue);
            if (index >= 0)
            {
                _report = _investigation.LReports
                                  .OrderByDescending(r => r.BeginDate)
                                  .ElementAt(index);

                TabReports.SetReportList(_investigation.LReports);
                TabReportInfo.SetReport(_report);
                ReportInfo.Visibility = Visibility.Visible;
            }
            else
            {
                _report = null;
                ReportInfo.Visibility = Visibility.Collapsed;
            }
            Mouse.OverrideCursor = null;
          
        }
    }
}
