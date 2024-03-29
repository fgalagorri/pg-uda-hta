﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Entities;
using Gateway;
using Cursors = System.Windows.Input.Cursors;
using Label = System.Windows.Controls.Label;
using Orientation = System.Windows.Controls.Orientation;
using UserControl = System.Windows.Controls.UserControl;

namespace UDA_HTA.UserControls.MainWindow.Investigations
{
    /// <summary>
    /// Interaction logic for ResearchViewer.xaml
    /// </summary>
    public partial class ResearchViewer : UserControl
    {
        private Investigation _investigation;
        private Report _report ;

        public ResearchViewer(long idInvestigation)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                _investigation = GatewayController.GetInstance().GetInvestigation(idInvestigation);

                InitializeComponent();
            
                TabInvestigation.SetInformationInfo(_investigation);
                TabReports.SetReportList(_investigation.LReports);

                PopulateTree();

                Mouse.OverrideCursor = null;
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public Investigation GetSelectedInvestigation()
        {
            return _investigation;
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

                if (r.BeginDate != null)
                {
                    Label lbl = new Label { Content = r.BeginDate.Value.ToShortDateString() };
                    sp.Children.Add(lbl);
                }

                child.Header = sp;
                if (reportId.HasValue && r.UdaId.Equals(reportId))
                    child.IsSelected = true;
                treeInvestigation.Items.Add(child);

            }

        }



        private void treeInvestigation_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            Mouse.OverrideCursor = Cursors.Wait;
            if (_investigation.LReports.Count > 0)
            {
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
                
            }
            Mouse.OverrideCursor = null;
          
        }


        private void DeleteReportFromResearch_OnClick(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait; 
            if (_investigation.LReports.Count > 0)
            {
                TreeViewItem selected = (TreeViewItem)TreeReports.SelectedValue;
                int index = treeInvestigation.Items.IndexOf(selected);
                ICollection<Report> lstReports = _investigation.LReports.OrderByDescending(r => r.BeginDate).ToList();

                _report = lstReports.ElementAt(index);
                try
                {
                    GatewayController.GetInstance().DeleteReportFromResearch(_report, _investigation);

                    lstReports.Remove(_report);
                    _investigation.LReports = lstReports;
                    treeInvestigation.Items.Remove(selected);
                    TabInvestigation.SetInformationInfo(_investigation);
                    TabReports.SetReportList(_investigation.LReports);
                }
                catch (Exception exception)
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            Mouse.OverrideCursor = null;
        }


        private static DependencyObject SearchTreeView<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof (T))
            {
                source = VisualTreeHelper.GetParent(source);
            }
            return source;
        }

        private void TreeReports_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = (TreeViewItem) SearchTreeView<TreeViewItem>((DependencyObject) e.OriginalSource);
            if (treeViewItem != null)
            {
                treeViewItem.IsSelected = true;
                e.Handled = true;
            }
        }

    }
}
