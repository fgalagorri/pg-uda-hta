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

        public PatientViewerData()
        {
            InitializeComponent();
            colDate.Binding.StringFormat = ConfigurationManager.AppSettings["ShortDateString"];
            colTime.Binding.StringFormat = ConfigurationManager.AppSettings["ShortTimeString"];
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
                controller.UpdateMeasure(m.Id, m.IsEnabled, m.Comment);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Comment_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CheckBox_Click(sender, e);
        }

    }
}
