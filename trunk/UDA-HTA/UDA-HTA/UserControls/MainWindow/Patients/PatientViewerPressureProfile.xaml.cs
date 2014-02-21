using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
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

            var reportLength = (valid.Last().Time.Value - valid.First().Time.Value);

            if (valid.Any() && Math.Abs(reportLength.TotalDays) > 3)
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
            var sys = new List<KeyValuePair<DateTime, int>>();
            var dias = new List<KeyValuePair<DateTime, int>>();

            bool isSleep = false;
            var date = r.BeginDate.Value;

            bool isSleep1 = true;
            int sys1 = _limits.HiSysNight;
            int dias1 = _limits.HiDiasNight;
            var time1 = new DateTime(date.Year, date.Month, date.Day, r.Carnet.SleepTimeStart.Value.Hour,
                r.Carnet.SleepTimeStart.Value.Minute, r.Carnet.SleepTimeStart.Value.Second);

            bool isSleep2 = false;
            int sys2 = _limits.HiSysDay;
            int dias2 = _limits.HiDiasDay;
            var time2 = new DateTime(date.Year, date.Month, date.Day, r.Carnet.SleepTimeEnd.Value.Hour,
                    r.Carnet.SleepTimeEnd.Value.Minute, r.Carnet.SleepTimeEnd.Value.Second);

            // Me aseguro que time1 sea menor que time2
            if (time2 < time1)
            {
                var auxTime = time1;
                var auxSys = sys1;
                var auxDias = dias1;
                var auxSleep = isSleep1;

                time1 = time2;
                sys1 = sys2;
                dias1 = dias2;
                isSleep1 = isSleep2;

                time2 = auxTime;
                sys2 = auxSys;
                dias2 = auxDias;
                isSleep2 = auxSleep;
            }


            // AGREGO LA PRIMER MARCA
            if (GatewayHelper.IsAsleep(r.Carnet.SleepTimeStart.Value.TimeOfDay, 
                                       r.Carnet.SleepTimeEnd.Value.TimeOfDay,
                                       r.BeginDate.Value))
            {
                // Inicia en Sueño
                sys.Add(new KeyValuePair<DateTime, int>(r.BeginDate.Value, _limits.HiSysNight));
                dias.Add(new KeyValuePair<DateTime, int>(r.BeginDate.Value, _limits.HiDiasNight));
            }
            else
            {
                // Inicia en Vigilia
                sys.Add(new KeyValuePair<DateTime, int>(r.BeginDate.Value, _limits.HiSysDay));
                dias.Add(new KeyValuePair<DateTime, int>(r.BeginDate.Value, _limits.HiDiasDay));
            }

            // Agrego períodos de sueño
            while (time1 <= r.EndDate.Value || time2 <= r.EndDate.Value)
            {
                if (r.BeginDate.Value <= time1 && time1 <= r.EndDate.Value)
                {
                    // Agrego el final del período anterior
                    sys.Add(new KeyValuePair<DateTime, int>(time1, sys2));
                    dias.Add(new KeyValuePair<DateTime, int>(time1, dias2));

                    // Agrego el inicio del período actual
                    sys.Add(new KeyValuePair<DateTime, int>(time1.AddMilliseconds(1), sys1));
                    dias.Add(new KeyValuePair<DateTime, int>(time1.AddMilliseconds(1), dias1));

                    isSleep = isSleep1;
                }
                time1 = time1.AddDays(1);

                if (r.BeginDate.Value <= time2 && time2 <= r.EndDate.Value)
                {
                    // Agrego el final del período anterior
                    sys.Add(new KeyValuePair<DateTime, int>(time2, sys1));
                    dias.Add(new KeyValuePair<DateTime, int>(time2, dias1));

                    // Agrego el inicio del período actual
                    sys.Add(new KeyValuePair<DateTime, int>(time2.AddMilliseconds(1), sys2));
                    dias.Add(new KeyValuePair<DateTime, int>(time2.AddMilliseconds(1), dias2));

                    isSleep = isSleep2;
                }
                time2 = time2.AddDays(1);
            }

            // Agrego la última marca
            if (isSleep)
            {
                // Finaliza en sueño
                sys.Add(new KeyValuePair<DateTime, int>(r.EndDate.Value, _limits.HiSysNight));
                dias.Add(new KeyValuePair<DateTime, int>(r.EndDate.Value, _limits.HiDiasNight));
            }
            else
            {
                // Finaliza en vigilia
                sys.Add(new KeyValuePair<DateTime, int>(r.EndDate.Value, _limits.HiSysDay));
                dias.Add(new KeyValuePair<DateTime, int>(r.EndDate.Value, _limits.HiDiasDay));
            }


            ((LineSeries) PressureProfile.Series[4]).ItemsSource = sys;
            ((LineSeries) PressureProfile.Series[5]).ItemsSource = dias;

            bloodAxis.Minimum = Math.Floor(valid.Min(m => m.HeartRate.Value)/(double) 10)*10;

            int timeInterval = reportLength.TotalHours < 36 ? 2 : 4;

            var xAxis = new DateTimeAxis
            {
                Minimum = r.BeginDate, //valid.Min(v => v.Time.Value),
                Maximum = r.EndDate, //valid.Max(v => v.Time.Value),
                IntervalType = DateTimeIntervalType.Hours,
                Interval = timeInterval,
                Orientation = AxisOrientation.X,
                Location = AxisLocation.Bottom,
                ShowGridLines = true
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
