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
using UDA_HTA.Helpers;

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
            if (report.BeginDate != null)
                lblBeginDate.Text = report.BeginDate.Value.ToString(ConfigurationManager.AppSettings["LongDateTimeString"]);
            lblBeginAge.Text = report.TemporaryData.Age.ToString();
            if (carnet.SleepTimeStart != null)
                lblSleepStart.Text = carnet.SleepTimeStart.Value.ToString(ConfigurationManager.AppSettings["LongDateTimeString"]);
            if (carnet.SleepTimeEnd != null)
                lblSleepEnd.Text = carnet.SleepTimeEnd.Value.ToString(ConfigurationManager.AppSettings["LongDateTimeString"]);
            lblSleepQty.Text = carnet.SleepQuality;
            lblSleepDesc.Text = carnet.SleepQualityDescription;
            if (carnet.MealTime != null)
                lblMealTime.Text = carnet.MealTime.Value.ToString(ConfigurationManager.AppSettings["ShortTimeString"]);

            double avg;
            // Mediciones Iniciales
            if ((carnet.InitSystolic1 != null) && (carnet.InitSystolic2 != null) && (carnet.InitSystolic3 != null))
            {
                lblInitSystolic1.Text = carnet.InitSystolic1.ToString();
                lblInitSystolic2.Text = carnet.InitSystolic2.ToString();
                lblInitSystolic3.Text = carnet.InitSystolic3.ToString();
                avg = Math.Round((carnet.InitSystolic1.Value + carnet.InitSystolic2.Value
                                      + carnet.InitSystolic3.Value)/(double) 3);
                lblInitSystolicP.Text = avg.ToString();
                lblInitDiastolic1.Text = carnet.InitDiastolic1.ToString();
                lblInitDiastolic2.Text = carnet.InitDiastolic2.ToString();
                lblInitDiastolic3.Text = carnet.InitDiastolic3.ToString();
            }
            if ((carnet.InitDiastolic1 != null) && (carnet.InitDiastolic2 != null) && (carnet.InitDiastolic3 != null))
            {
                avg = Math.Round((carnet.InitDiastolic1.Value + carnet.InitDiastolic2.Value
                                      + carnet.InitDiastolic3.Value)/(double) 3);
                lblInitDiastolicP.Text = avg.ToString();
                lblInitHR1.Text = carnet.InitHeartRate1.ToString();
                lblInitHR2.Text = carnet.InitHeartRate2.ToString();
                lblInitHR3.Text = carnet.InitHeartRate3.ToString();
            }

            if ((carnet.InitHeartRate1 != null) && (carnet.InitHeartRate2 != null) && (carnet.InitHeartRate3 != null))
            {
                avg = Math.Round((carnet.InitHeartRate1.Value + carnet.InitHeartRate2.Value
                                  + carnet.InitHeartRate3.Value) / (double)3);
                lblInitHRP.Text = avg.ToString();                
            }

                // Mediciones finales
            lblFinalSystolic1.Text = carnet.FinalSystolic1.ToString();
            lblFinalSystolic2.Text = carnet.FinalSystolic2.ToString();
            lblFinalSystolic3.Text = carnet.FinalSystolic3.ToString();
            if ((carnet.FinalSystolic1 != null) && (carnet.FinalSystolic2 != null) && (carnet.FinalSystolic3 != null))
            {
                avg = Math.Round((carnet.FinalSystolic1.Value + carnet.FinalSystolic2.Value
                                  + carnet.FinalSystolic3.Value) / (double)3);
                lblFinalSystolicP.Text = avg.ToString();
            }

            lblFinalDiastolic1.Text = carnet.FinalDiastolic1.ToString();
            lblFinalDiastolic2.Text = carnet.FinalDiastolic2.ToString();
            lblFinalDiastolic3.Text = carnet.FinalDiastolic3.ToString();

            if ((carnet.FinalDiastolic1 != null) && (carnet.FinalDiastolic2 != null) && (carnet.FinalDiastolic3 != null))
            {
                avg = Math.Round((carnet.FinalDiastolic1.Value + carnet.FinalDiastolic2.Value
                                    + carnet.FinalDiastolic3.Value)/(double) 3);
                lblFinalDiastolicP.Text = avg.ToString();                
            }

            lblFinalHR1.Text = carnet.FinalHeartRate1.ToString();
            lblFinalHR2.Text = carnet.FinalHeartRate2.ToString();
            lblFinalHR3.Text = carnet.FinalHeartRate3.ToString();
            if ((carnet.FinalHeartRate1 != null) && (carnet.FinalHeartRate2 != null) && (carnet.FinalHeartRate3 != null))
            {
                avg = Math.Round((carnet.FinalHeartRate1.Value + carnet.FinalHeartRate2.Value
                                    + carnet.FinalHeartRate3.Value)/(double) 3);
                lblFinalHRP.Text = avg.ToString();                
            }
            
        }
    }
}
