using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Entities;
using Gateway;

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    /// <summary>
    /// Interaction logic for PatientViewerData.xaml
    /// </summary>
    public partial class PatientViewerData : UserControl
    {
        private ICollection<Measurement> _measures;
        private Limits lim;

        public PatientViewerData()
        {
            InitializeComponent();
            colDate.Binding.StringFormat = ConfigurationManager.AppSettings["ShortDateString"];
            colTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
            var controller = GatewayController.GetInstance();
            lim = controller.GetLimits();
        }


        
        public void SetReport(Report r)
        {
            _measures = r.Measures.OrderBy(m => m.Time.Value).ToList();
            grid.DataContext = _measures;
        }
        
        public ICollection<Measurement> GetMeasurements()
        {
            return _measures;
        } 

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            //actualizar base
            Measurement m = (Measurement) grid.SelectedItem;
            var controller = GatewayController.GetInstance();
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                controller.UpdateMeasure(m.Id, m.IsEnabled, m.Comment);
                Mouse.OverrideCursor = null;
            }
            catch (Exception exception)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Comment_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CheckBox_Click(sender, e);
        }

        private void EventSetter_OnHandler(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush();

            var s = sender as DataGridCell;
            var colName = s.Column.SortMemberPath;            

            var cell = e.Source as DataGridCell;
            var cellText = cell.Content as TextBlock;
            int cellValue;

            if (colName == "Systolic")
            {
                int.TryParse(cellText.Text,out cellValue);
                if (cellValue > lim.MaxSysDay)
                {
                    brush = new SolidColorBrush(Colors.IndianRed);
                    cell.Background = brush;
                }                
            }
            else
            {
                if (colName == "Diastolic")
                {
                    int.TryParse(cellText.Text, out cellValue);
                    if (cellValue > lim.MaxDiasDay)
                    {
                        brush = new SolidColorBrush(Colors.IndianRed);
                        cell.Background = brush;
                    }                                    
                }
            }

            cell.Background = brush;
        }

    }
}
