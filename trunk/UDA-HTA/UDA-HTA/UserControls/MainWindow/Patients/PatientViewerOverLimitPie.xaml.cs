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
            PieSysTot.DataContext = null;
            PieSysDay.DataContext = null;
            PieSysNight.DataContext = null;
            PieDiasTot.DataContext = null;
            PieDiasDay.DataContext = null;
            PieDiasNight.DataContext = null;

            _limits = GatewayController.GetInstance().GetLimits();

            var valid = r.Measures.Where(m => m.Valid && m.IsEnabled
                                              && m.Asleep.HasValue
                                              && m.Systolic.HasValue
                                              && m.Diastolic.HasValue).ToList();

            var hiTotalCount = 0;
            var okTotalCount = 0;
            var totalCount = valid.Count;
            var dayCount = valid.Count(m => !m.Asleep.Value);
            var nightCount = valid.Count(m => m.Asleep.Value);
            var hiCount = valid.Count(m => !m.Asleep.Value && m.Systolic.Value >= _limits.HiSysDay);
            var okCount = valid.Count(m => !m.Asleep.Value && m.Systolic.Value < _limits.HiSysDay);
            
            PieSysDay.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>(GetLegendText(hiCount, dayCount), hiCount),
                    new KeyValuePair<string, int>(GetLegendText(okCount, dayCount), okCount),
                };

            hiTotalCount += hiCount;
            okTotalCount += okCount;


            hiCount = valid.Count(m => m.Asleep.Value && m.Systolic.Value >= _limits.HiSysNight);
            okCount = valid.Count(m => m.Asleep.Value && m.Systolic.Value < _limits.HiSysNight);
            
            PieSysNight.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>(GetLegendText(hiCount, nightCount), hiCount),
                    new KeyValuePair<string, int>(GetLegendText(okCount, nightCount), okCount),
                };

            hiTotalCount += hiCount;
            okTotalCount += okCount;


            PieSysTot.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>(GetLegendText(hiTotalCount, totalCount), hiTotalCount),
                    new KeyValuePair<string, int>(GetLegendText(okTotalCount, totalCount), okTotalCount),
                };



            // Diastolic measures
            hiTotalCount = 0;
            okTotalCount = 0;
            hiCount = valid.Count(m => !m.Asleep.Value && m.Diastolic.Value >= _limits.HiDiasDay);
            okCount = valid.Count(m => !m.Asleep.Value && m.Diastolic.Value < _limits.HiDiasDay);
            
            PieDiasDay.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>(GetLegendText(hiCount, dayCount), hiCount),
                    new KeyValuePair<string, int>(GetLegendText(okCount, dayCount), okCount),
                };

            hiTotalCount += hiCount;
            okTotalCount += okCount;


            hiCount = valid.Count(m => m.Asleep.Value && m.Diastolic.Value >= _limits.HiDiasNight);
            okCount = valid.Count(m => m.Asleep.Value && m.Diastolic.Value < _limits.HiDiasNight);

            PieDiasNight.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>(GetLegendText(hiCount, nightCount), hiCount),
                    new KeyValuePair<string, int>(GetLegendText(okCount, nightCount), okCount),
                };

            hiTotalCount += hiCount;
            okTotalCount += okCount;


            PieDiasTot.DataContext = new List<KeyValuePair<string, int>>
                {
                    new KeyValuePair<string, int>(GetLegendText(hiTotalCount, totalCount), hiTotalCount),
                    new KeyValuePair<string, int>(GetLegendText(okTotalCount, totalCount), okTotalCount),
                };
        }


        private string GetLegendText(int count, int total)
        {
            double perc;
            if (total > 0)
                perc = (count/(double) total)*100;
            else
                perc = 0;

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
