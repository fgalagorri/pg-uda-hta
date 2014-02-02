using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Controls;
using Entities;
using Gateway;
using UDA_HTA.Helpers;

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    /// <summary>
    /// Interaction logic for PatientViewerOverLimitPie.xaml
    /// </summary>
    public partial class PatientViewerOverLimitPie : UserControl
    {
        private Limits _limits;

        public PatientViewerOverLimitPie()
        {
            InitializeComponent();
        }

        public void SetReport(Report r)
        {
            _limits = GatewayController.GetInstance().GetLimits();

            var valid = r.Measures.Where(m => m.Valid && m.IsEnabled
                                              && m.Asleep.HasValue
                                              && m.Systolic.HasValue
                                              && m.Diastolic.HasValue).ToList();

            var totalCount = valid.Count;
            var dayCount = valid.Count(m => !m.Asleep.Value);
            var nightCount = valid.Count(m => m.Asleep.Value);
            var hiCount = valid.Count(m => m.Systolic.Value >= _limits.HiSysTotal);
            var okCount = valid.Count(m => m.Systolic.Value < _limits.HiSysTotal);

            PieSysTot.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>(GetLegendText(hiCount, totalCount), hiCount),
                    new KeyValuePair<string, int>(GetLegendText(okCount, totalCount), okCount),
                };


            hiCount = valid.Count(m => !m.Asleep.Value && m.Systolic.Value >= _limits.HiSysDay);
            okCount = valid.Count(m => !m.Asleep.Value && m.Systolic.Value < _limits.HiSysDay);
            
            PieSysDay.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>(GetLegendText(hiCount, dayCount), hiCount),
                    new KeyValuePair<string, int>(GetLegendText(okCount, dayCount), okCount),
                };


            hiCount = valid.Count(m => m.Asleep.Value && m.Systolic.Value >= _limits.HiSysNight);
            okCount = valid.Count(m => m.Asleep.Value && m.Systolic.Value < _limits.HiSysNight);
            
            PieSysNight.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>(GetLegendText(hiCount, nightCount), hiCount),
                    new KeyValuePair<string, int>(GetLegendText(okCount, nightCount), okCount),
                };


            // Diastolic measures
            hiCount = valid.Count(m => m.Diastolic.Value >= _limits.HiDiasTotal);
            okCount = valid.Count(m => m.Diastolic.Value < _limits.HiDiasTotal);
            
            PieDiasTot.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>(GetLegendText(hiCount, totalCount), hiCount),
                    new KeyValuePair<string, int>(GetLegendText(okCount, totalCount), okCount),
                };


            hiCount = valid.Count(m => !m.Asleep.Value && m.Diastolic.Value >= _limits.HiDiasDay);
            okCount = valid.Count(m => !m.Asleep.Value && m.Diastolic.Value < _limits.HiDiasDay);
            
            PieDiasDay.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>(GetLegendText(hiCount, dayCount), hiCount),
                    new KeyValuePair<string, int>(GetLegendText(okCount, dayCount), okCount),
                };


            hiCount = valid.Count(m => m.Asleep.Value && m.Diastolic.Value >= _limits.HiDiasDay);
            okCount = valid.Count(m => m.Asleep.Value && m.Diastolic.Value < _limits.HiDiasDay);

            PieDiasNight.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>(GetLegendText(hiCount, nightCount), hiCount),
                    new KeyValuePair<string, int>(GetLegendText(okCount, nightCount), okCount),
                };
           
        }


        private string GetLegendText(int count, int total)
        {
            var perc = (count/(double) total) * 100;
            return perc.ToString("N0") + "% (" + count + ")";
                //count + " (" + perc.ToString("N0") + "%)";
        }


        public void GetChartImage()
        {
            string path = ConfigurationManager.AppSettings["GraphicOverLimit"];
            var dir = path.Substring(0, path.LastIndexOf('\\'));
            GatewayController.GetInstance().CreateFolderIfNotExists(dir);

            ImageHelper.CreateImageA4(GraphicGrid, path);
        }
    }
}
