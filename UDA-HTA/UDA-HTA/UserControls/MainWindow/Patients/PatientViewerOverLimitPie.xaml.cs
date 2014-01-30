using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Entities;
using Gateway;

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


        /* Retorna el path donde guardo la imagen */
        public void GetChartImage()
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int) GraphicGrid.ActualWidth,
                (int) GraphicGrid.ActualHeight,
                96d,
                96d,
                PixelFormats.Pbgra32);
            
            renderBitmap.Render(GraphicGrid);

            // Create a file stream for saving image
            string path = ConfigurationManager.AppSettings["GraphicOverLimit"];
            using (FileStream outStream = new FileStream(path, FileMode.Create))
            {
                // Use png encoder for our data
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                // push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);
            }
        }
    }
}
