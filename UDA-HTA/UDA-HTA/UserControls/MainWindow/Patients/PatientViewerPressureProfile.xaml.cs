using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Entities;
using Gateway;

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    /// <summary>
    /// Interaction logic for PatientViewerPressureProfile.xaml
    /// </summary>
    public partial class PatientViewerPressureProfile : UserControl
    {
        private Limits _limits; 

        public PatientViewerPressureProfile()
        {
            InitializeComponent();
        }

        public void SetReport(Report r)
        {
            _limits = GatewayController.GetInstance().GetLimits();

            var valid = r.Measures.Where(m => m.Valid && m.Asleep.HasValue && m.Middle.HasValue
                                              && m.Systolic.HasValue && m.Diastolic.HasValue
                                              && m.HeartRate.HasValue && m.Time.HasValue).ToList();

            var systolic = new List<KeyValuePair<DateTime, int>>();
            var diastolic = new List<KeyValuePair<DateTime, int>>();
            var middle = new List<KeyValuePair<DateTime, int>>();
            var hr = new List<KeyValuePair<DateTime, int>>();

            foreach (var m in valid)
            {
                systolic.Add(new KeyValuePair<DateTime, int>(m.Time.Value, m.Systolic.Value));
                diastolic.Add(new KeyValuePair<DateTime, int>(m.Time.Value, m.Diastolic.Value));
                middle.Add(new KeyValuePair<DateTime, int>(m.Time.Value, m.Middle.Value));
                hr.Add(new KeyValuePair<DateTime, int>(m.Time.Value, m.HeartRate.Value));
            }

            ((LineSeries)PressureProfile.Series[0]).ItemsSource = systolic;
            ((LineSeries)PressureProfile.Series[1]).ItemsSource = middle;
            ((LineSeries)PressureProfile.Series[2]).ItemsSource = diastolic;
            ((LineSeries)PressureProfile.Series[3]).ItemsSource = hr;
        }
    }
}
