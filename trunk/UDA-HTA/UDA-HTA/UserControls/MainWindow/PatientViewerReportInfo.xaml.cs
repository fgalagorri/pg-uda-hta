using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
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

namespace UDA_HTA.UserControls.MainWindow
{
    /// <summary>
    /// Interaction logic for PatientViewerReportInfo.xaml
    /// </summary>
    public partial class PatientViewerReportInfo : UserControl
    {
        public PatientViewerReportInfo()
        {
            InitializeComponent();
        }

        public void SetReport(Report report)
        {
            var carnet = report.Carnet;
            
            // General
            lblTechnician.Text = carnet.Technician.Name;
            lblBeginDate.Text = report.BeginDate.Value.ToString(ConfigurationManager.AppSettings["LongDateTimeString"]);
            lblSleepStart.Text = carnet.SleepTimeStart.Value.ToString(ConfigurationManager.AppSettings["LongDateTimeString"]);
            lblSleepEnd.Text = carnet.SleepTimeEnd.Value.ToString(ConfigurationManager.AppSettings["LongDateTimeString"]);
            lblSleepQty.Text = carnet.SleepQuality;
            lblSleepDesc.Text = carnet.SleepQualityDescription;

            // Mediciones Iniciales
            lblInitSystolic1.Text = carnet.InitSystolic1.ToString();
            lblInitSystolic2.Text = carnet.InitSystolic2.ToString();
            lblInitSystolic3.Text = carnet.InitSystolic3.ToString();
            var avg = (decimal) ((carnet.InitSystolic1 + carnet.InitSystolic2 + carnet.InitSystolic3)/(decimal) 3);
            lblInitSystolicP.Text = avg.ToString("0.##", CultureInfo.InvariantCulture);
            lblInitDiastolic1.Text = carnet.InitDiastolic1.ToString();
            lblInitDiastolic2.Text = carnet.InitDiastolic2.ToString();
            lblInitDiastolic3.Text = carnet.InitDiastolic3.ToString();
            avg = (decimal) ((carnet.InitDiastolic1 + carnet.InitDiastolic2 + carnet.InitDiastolic3)/(decimal) 3);
            lblInitDiastolicP.Text = avg.ToString("0.##", CultureInfo.InvariantCulture);
            lblInitHR1.Text = carnet.InitHeartRate1.ToString();
            lblInitHR2.Text = carnet.InitHeartRate2.ToString();
            lblInitHR3.Text = carnet.InitHeartRate3.ToString();
            avg = (decimal)((carnet.InitHeartRate1 + carnet.InitHeartRate2 + carnet.InitHeartRate3) / (decimal)3);
            lblInitHRP.Text = avg.ToString("0.##", CultureInfo.InvariantCulture);

            // Mediciones finales
            lblFinalSystolic1.Text = carnet.FinalSystolic1.ToString();
            lblFinalSystolic2.Text = carnet.FinalSystolic2.ToString();
            lblFinalSystolic3.Text = carnet.FinalSystolic3.ToString();
            avg = (decimal)((carnet.FinalSystolic1 + carnet.FinalSystolic2 + carnet.FinalSystolic3) / (decimal)3);
            lblFinalSystolicP.Text = avg.ToString("0.##", CultureInfo.InvariantCulture);
            lblFinalDiastolic1.Text = carnet.FinalDiastolic1.ToString();
            lblFinalDiastolic2.Text = carnet.FinalDiastolic2.ToString();
            lblFinalDiastolic3.Text = carnet.FinalDiastolic3.ToString();
            avg = (decimal)((carnet.FinalDiastolic1 + carnet.FinalDiastolic2 + carnet.FinalDiastolic3) / (decimal)3);
            lblFinalDiastolicP.Text = avg.ToString("0.##", CultureInfo.InvariantCulture);
            lblFinalHR1.Text = carnet.FinalHeartRate1.ToString();
            lblFinalHR2.Text = carnet.FinalHeartRate2.ToString();
            lblFinalHR3.Text = carnet.FinalHeartRate3.ToString();
            avg = (decimal)((carnet.FinalHeartRate1 + carnet.FinalHeartRate2 + carnet.FinalHeartRate3) / (decimal)3);
            lblFinalHRP.Text = avg.ToString("0.##", CultureInfo.InvariantCulture);
        }
    }
}
