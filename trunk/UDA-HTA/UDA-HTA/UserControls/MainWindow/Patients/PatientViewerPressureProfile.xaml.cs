using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Entities;
using Gateway;
using UDA_HTA.Helpers;

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

            var valid = r.Measures.Where(m => m.Valid && m.IsEnabled
                                              && m.Asleep.HasValue && m.Middle.HasValue
                                              && m.Systolic.HasValue && m.Diastolic.HasValue
                                              && m.HeartRate.HasValue && m.Time.HasValue)
                .OrderBy(m => m.Time.Value).ToList();

            if (valid.Any() && 
                Math.Abs((valid.Last().Time.Value - valid.First().Time.Value).TotalDays) > 7)
            {
                MessageBox.Show("Se ha encontrado un error en la extensión del " +
                                "reporte y no puede ser graficado. Verifique " +
                                "las fechas de las mediciones tomadas.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

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

            ((LineSeries) PressureProfile.Series[0]).ItemsSource = systolic;
            ((LineSeries) PressureProfile.Series[1]).ItemsSource = middle;
            ((LineSeries) PressureProfile.Series[2]).ItemsSource = diastolic;
            ((LineSeries) PressureProfile.Series[3]).ItemsSource = hr;

            // Limits
            var max = new List<KeyValuePair<DateTime, int>>();
            var min = new List<KeyValuePair<DateTime, int>>();

            // Day period
            max.Add(new KeyValuePair<DateTime, int>(r.BeginDate.Value, _limits.HiSysDay));
            max.Add(new KeyValuePair<DateTime, int>(r.Carnet.SleepTimeStart.Value, _limits.HiSysDay));
            min.Add(new KeyValuePair<DateTime, int>(r.BeginDate.Value, _limits.HiDiasDay));
            min.Add(new KeyValuePair<DateTime, int>(r.Carnet.SleepTimeStart.Value, _limits.HiDiasDay));

            // Night period
            max.Add(new KeyValuePair<DateTime, int>(r.Carnet.SleepTimeStart.Value.AddMilliseconds(1),
                _limits.HiSysNight));
            max.Add(new KeyValuePair<DateTime, int>(r.Carnet.SleepTimeEnd.Value, _limits.HiSysNight));
            min.Add(new KeyValuePair<DateTime, int>(r.Carnet.SleepTimeStart.Value.AddMilliseconds(1),
                _limits.HiDiasNight));
            min.Add(new KeyValuePair<DateTime, int>(r.Carnet.SleepTimeEnd.Value, _limits.HiDiasNight));

            // Day period 2
            max.Add(new KeyValuePair<DateTime, int>(r.Carnet.SleepTimeEnd.Value.AddMilliseconds(1), _limits.HiSysDay));
            max.Add(new KeyValuePair<DateTime, int>(r.EndDate.Value, _limits.HiSysDay));
            min.Add(new KeyValuePair<DateTime, int>(r.Carnet.SleepTimeEnd.Value.AddMilliseconds(1),
                _limits.HiDiasDay));
            min.Add(new KeyValuePair<DateTime, int>(r.EndDate.Value, _limits.HiDiasDay));

            ((LineSeries) PressureProfile.Series[4]).ItemsSource = max;
            ((LineSeries) PressureProfile.Series[5]).ItemsSource = min;

            var xAxis = new DateTimeAxis
            {
                Minimum = r.BeginDate, //valid.Min(v => v.Time.Value),
                Maximum = r.EndDate, //valid.Max(v => v.Time.Value),
                IntervalType = DateTimeIntervalType.Hours,
                Interval = 2,
                Orientation = AxisOrientation.X,
                Location = AxisLocation.Bottom,
                ShowGridLines = true,
            };

            ((LineSeries) PressureProfile.Series[0]).IndependentAxis = xAxis;
            ((LineSeries) PressureProfile.Series[1]).IndependentAxis = xAxis;
            ((LineSeries) PressureProfile.Series[2]).IndependentAxis = xAxis;
            ((LineSeries) PressureProfile.Series[3]).IndependentAxis = xAxis;
            ((LineSeries) PressureProfile.Series[4]).IndependentAxis = xAxis;
            ((LineSeries) PressureProfile.Series[5]).IndependentAxis = xAxis;
        }

        public void GetChartImage()
        {
            string path = ConfigurationManager.AppSettings["GraphicPressurePrfl"];
            var dir = path.Substring(0, path.LastIndexOf('\\'));
            GatewayController.GetInstance().CreateFolderIfNotExists(dir);

            ImageHelper.CreateImageA4(GraphicGrid, path);
        }
    }
}
