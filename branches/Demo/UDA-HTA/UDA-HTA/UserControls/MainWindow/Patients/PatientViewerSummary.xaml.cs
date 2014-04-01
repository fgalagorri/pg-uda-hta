using System.Configuration;
using System.Linq;
using System.Windows.Controls;
using Entities;
using Gateway;

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    /// <summary>
    /// Interaction logic for PatientViewerSummary.xaml
    /// </summary>
    public partial class PatientViewerSummary : UserControl
    {
        private Limits _limits;

        public PatientViewerSummary()
        {
            var controller = GatewayController.GetInstance();
            _limits = controller.GetLimits();
            InitializeComponent();
        }

        public void SetReport(Report r)
        {
            int aux, validTotal, validDay, validNight, max, min;
            int total = r.Measures.Count(m => !m.Retry);
            int totalDay = r.Measures.Count(m => m.Asleep.HasValue && !m.Asleep.Value && !m.Retry);
            int totalNight = r.Measures.Count(m => m.Asleep.HasValue && m.Asleep.Value && !m.Retry);

            // TOTAL DE MEDICIONES
            lblTotalMeasuresT.Text = total.ToString();
            lblTotalMeasuresD.Text = totalDay.ToString();
            lblTotalMeasuresN.Text = totalNight.ToString();

            validTotal = r.Measures.Count(m => m.Valid && m.IsEnabled);
            lblValidMeasuresT.Text = validTotal.ToString();
            lblValidMeasuresTP.Text = PercentageUI(validTotal, total);

            validDay = r.Measures.Count(m => m.Valid && m.IsEnabled && m.Asleep.HasValue && !m.Asleep.Value);
            lblValidMeasuresD.Text = validDay.ToString();
            lblValidMeasuresDP.Text = PercentageUI(validDay, totalDay);

            validNight = r.Measures.Count(m => m.Valid && m.IsEnabled && m.Asleep.HasValue && m.Asleep.Value);
            lblValidMeasuresN.Text = validNight.ToString();
            lblValidMeasuresNP.Text = PercentageUI(validNight, totalNight);

            var valid = r.Measures.Where(m => m.Valid && m.IsEnabled).ToList();
            var validD = valid.Where(v => v.Asleep.HasValue && !v.Asleep.Value).ToList();
            var validN = valid.Where(v => v.Asleep.HasValue && v.Asleep.Value).ToList();


            // PROMEDIOS - TOTALES
            lblSystolicAvgT.Text = r.SystolicTotalAvg.HasValue ? r.SystolicTotalAvg.ToString() : "-";
            //: Math.Round(valid.Average(m => m.Systolic.Value)).ToString();
            lblDiastolicAvgT.Text = r.DiastolicTotalAvg.HasValue ? r.DiastolicTotalAvg.Value.ToString() : "-";
            //: Math.Round(valid.Average(m => m.Diastolic.Value)).ToString();
            lblTamAvgT.Text = r.MiddleTotalAvg.HasValue ? r.MiddleTotalAvg.Value.ToString() : "-";
            //: Math.Round(valid.Average(m => m.Middle.Value)).ToString();
            lblHRAvgT.Text = r.HeartRateTotalAvg.HasValue ? r.HeartRateTotalAvg.Value.ToString() : "-";
            //: Math.Round(valid.Average(m => m.HeartRate.Value)).ToString();

            // PROMEDIOS - VIGILIA
            lblSystolicAvgD.Text = r.SystolicDayAvg.HasValue ? r.SystolicDayAvg.Value.ToString() : "-";
            //: Math.Round(validD.Average(m => m.Systolic.Value)).ToString();
            lblDiastolicAvgD.Text = r.DiastolicDayAvg.HasValue ? r.DiastolicDayAvg.Value.ToString() : "-";
            //: Math.Round(validD.Average(m => m.Diastolic.Value)).ToString();
            lblTamAvgD.Text = r.MiddleDayAvg.HasValue ? r.MiddleDayAvg.Value.ToString() : "-";
            //: Math.Round(validD.Average(m => m.Middle.Value)).ToString();
            lblHRAvgD.Text = r.HeartRateDayAvg.HasValue ? r.HeartRateDayAvg.Value.ToString() : "-";
            //: Math.Round(validD.Average(m => m.HeartRate.Value)).ToString();

            // VALORES SOBRE EL LÍMITE - VIGILIA
            if (_limits != null)
            {
                aux = validD.Count(m => m.Systolic >= _limits.HiSysDay);
                lblSystolicOverLimD.Text = aux.ToString();
                lblSystolicOverLimDP.Text = PercentageUI(aux, validDay);
                aux = validD.Count(m => m.Diastolic >= _limits.HiDiasDay);
                lblDiastolicOverLimD.Text = aux.ToString();
                lblDiastolicOverLimDP.Text = PercentageUI(aux, validDay);
            }

            // MÁXIMOS - VIGILIA
            if (r.SystolicDayMax.HasValue)
            {
                max = r.SystolicDayMax.Value;
                //: validD.Max(m => m.Systolic.Value);
                lblSystolicMaxD.Text = max.ToString();
                lblSystolicMaxDT.Text = validD.Any()
                    ? validD.First(m => m.Systolic.Value == max).Time.Value
                        .ToString(ConfigurationManager.AppSettings["ShortTimeString"])
                    : "-";
            }
            else
            {
                lblSystolicMaxD.Text = "-";
                lblSystolicMaxDT.Text = "-";
            }

            if (r.DiastolicDayMax.HasValue)
            {
                max = r.DiastolicDayMax.Value;
                //: validD.Max(m => m.Diastolic.Value);
                lblDiastolicMaxD.Text = max.ToString();
                lblDiastolicMaxDT.Text = validD.Any()
                    ? validD.First(m => m.Diastolic.Value == max).Time.Value
                        .ToString(ConfigurationManager.AppSettings["ShortTimeString"])
                    : "-";
            }
            else
            {
                lblDiastolicMaxD.Text = "-";
                lblDiastolicMaxDT.Text = "-";
            }

            if (r.HeartRateDayMax.HasValue)
            {
                max = r.HeartRateDayMax.Value;
                //: validD.Max(m => m.HeartRate.Value);
                lblHRMaxD.Text = max.ToString();
                lblHRMaxDT.Text = validD.Any()
                    ? validD.First(m => m.HeartRate.Value == max).Time.Value
                        .ToString(ConfigurationManager.AppSettings["ShortTimeString"])
                    : "-";
            }
            else
            {
                lblHRMaxD.Text = "-";
                lblHRMaxDT.Text = "-";
            }


            // MINIMOS - VIGILIA
            if (r.SystolicDayMin.HasValue)
            {
                min = r.SystolicDayMin.Value;
                //: validD.Min(m => m.Systolic.Value);
                lblSystolicMinD.Text = min.ToString();
                lblSystolicMinDT.Text = validD.Any()
                    ? validD.First(m => m.Systolic == min).Time.Value
                        .ToString(ConfigurationManager.AppSettings["ShortTimeString"])
                    : "-";
            }
            else
            {
                lblSystolicMinD.Text = "-";
                lblSystolicMinDT.Text = "-";
            }
            if (r.DiastolicDayMin.HasValue)
            {
                min = r.DiastolicDayMin.Value;
                //: validD.Min(m => m.Diastolic.Value);
                lblDiastolicMinD.Text = min.ToString();
                lblDiastolicMinDT.Text = validD.Any()
                    ? validD.First(m => m.Diastolic == min).Time.Value
                        .ToString(ConfigurationManager.AppSettings["ShortTimeString"])
                    : "-";
            }
            else
            {
                lblDiastolicMinD.Text = "-";
                lblDiastolicMinDT.Text = "-";
            }
            if (r.HeartRateDayMin.HasValue)
            {
                min = r.HeartRateDayMin.Value;
                //: validD.Min(m => m.HeartRate.Value);
                lblHRMinD.Text = min.ToString();
                lblHRMinDT.Text = validD.Any()
                    ? validD.First(m => m.HeartRate == min).Time.Value
                        .ToString(ConfigurationManager.AppSettings["ShortTimeString"])
                    : "-";
            }
            else
            {
                lblHRMinD.Text = "-";
                lblHRMinDT.Text = "-";
            }


            // PROMEDIOS - SUEÑO 
            lblSystolicAvgN.Text = r.SystolicNightAvg.HasValue ? r.SystolicNightAvg.Value.ToString() : "-";
            //: Math.Round(validN.Average(m => m.Systolic.Value)).ToString();
            lblDiastolicAvgN.Text = r.DiastolicNightAvg.HasValue ? r.DiastolicNightAvg.Value.ToString() : "-";
            //: Math.Round(validN.Average(m => m.Diastolic.Value)).ToString();
            lblTamAvgN.Text = r.MiddleNightAvg.HasValue ? r.MiddleNightAvg.Value.ToString() : "-";
            //: Math.Round(validN.Average(m => m.Middle.Value)).ToString();
            lblHRAvgN.Text = r.HeartRateNightAvg.HasValue ? r.HeartRateNightAvg.Value.ToString() : "-";
            //: Math.Round(validN.Average(m => m.HeartRate.Value)).ToString();

            // VALORES SOBRE EL LIMITE - SUEÑO
            if (_limits != null)
            {
                aux = validN.Count(m => m.Systolic >= _limits.HiSysNight);
                lblSystolicOverLimN.Text = aux.ToString();
                lblSystolicOverLimNP.Text = PercentageUI(aux, validNight);
                aux = validN.Count(m => m.Diastolic >= _limits.HiDiasNight);
                lblDiastolicOverLimN.Text = aux.ToString();
                lblDiastolicOverLimNP.Text = PercentageUI(aux, validNight);
            }

            // MAXIMOS - SUEÑO
            if (r.SystolicNightMax.HasValue)
            {
                max = r.SystolicNightMax.Value;
                //: validN.Max(m => m.Systolic.Value);
                lblSystolicMaxN.Text = max.ToString();
                lblSystolicMaxNT.Text = validN.Any()
                    ? validN.First(m => m.Systolic.Value == max).Time.Value
                        .ToString(ConfigurationManager.AppSettings["ShortTimeString"])
                    : "-";
            }
            else
            {
                lblSystolicMaxN.Text = "-";
                lblSystolicMaxNT.Text = "-";
            }

            if (r.DiastolicNightMax.HasValue)
            {
                max = r.DiastolicNightMax.Value;
                //: validN.Max(m => m.Diastolic.Value);
                lblDiastolicMaxN.Text = max.ToString();
                lblDiastolicMaxNT.Text = validN.Any()
                    ? validN.First(m => m.Diastolic.Value == max).Time.Value
                        .ToString(ConfigurationManager.AppSettings["ShortTimeString"])
                    : "-";
            }
            else
            {
                lblDiastolicMaxN.Text = "-";
                lblDiastolicMaxNT.Text = "-";
            }

            if (r.HeartRateNightMax.HasValue)
            {
                max = r.HeartRateNightMax.Value;
                //: validN.Max(m => m.HeartRate.Value);
                lblHRMaxN.Text = max.ToString();
                lblHRMaxNT.Text = validN.Any()
                    ? validN.First(m => m.HeartRate.Value == max).Time.Value
                        .ToString(ConfigurationManager.AppSettings["ShortTimeString"])
                    : "-";
            }
            else
            {
                lblHRMaxN.Text = "-";
                lblHRMaxNT.Text = "-";
            }

            // MINIMOS - SUEÑO
            if (r.SystolicNightMin.HasValue)
            {

                min = r.SystolicNightMin.Value;
                //: validN.Min(m => m.Systolic.Value);
                lblSystolicMinN.Text = min.ToString();
                lblSystolicMinNT.Text = validN.Any()
                    ? validN.First(m => m.Systolic == min).Time.Value
                        .ToString(ConfigurationManager.AppSettings["ShortTimeString"])
                    : "-";
            }
            else
            {
                lblSystolicMinN.Text = "-";
                lblSystolicMinNT.Text = "-";
            }


            if (r.DiastolicNightMin.HasValue)
            {
                min = r.DiastolicNightMin.Value;
                //: validN.Min(m => m.Diastolic.Value);
                lblDiastolicMinN.Text = min.ToString();
                lblDiastolicMinNT.Text = validN.Any()
                    ? validN.First(m => m.Diastolic == min).Time.Value
                        .ToString(ConfigurationManager.AppSettings["ShortTimeString"])
                    : "-";
            }
            else
            {
                lblDiastolicMinN.Text = "-";
                lblDiastolicMinNT.Text = "-";
            }

            if (r.HeartRateNightMin.HasValue)
            {
                min = r.HeartRateNightMin.Value;
                //: validN.Min(m => m.HeartRate.Value);
                lblHRMinN.Text = min.ToString();
                lblHRMinNT.Text = validN.Any()
                    ? validN.First(m => m.HeartRate == min).Time.Value
                        .ToString(ConfigurationManager.AppSettings["ShortTimeString"])
                    : "-";
            }
            else
            {
                lblHRMinN.Text = "-";
                lblHRMinNT.Text = "-";
            }


            // DESVIACIÓN ESTÁNDARD - SISTÓLICO
            lblSDSysT.Text = r.StandardDeviationSysTotal.HasValue
                ? r.StandardDeviationSysTotal.Value.ToString("F1")
                : "-";
            lblSDSysD.Text = r.StandardDeviationSysDay.HasValue
                ? r.StandardDeviationSysDay.Value.ToString("F1")
                : "-";
            lblSDSysN.Text = r.StandardDeviationSysNight.HasValue
                ? r.StandardDeviationSysNight.Value.ToString("F1")
                : "-";

            // DESVIACIÓN ESTÁNDARD - DIASTÓLICO
            lblSDDiasT.Text = r.StandardDeviationDiasTotal.HasValue
                ? r.StandardDeviationDiasTotal.Value.ToString("F1")
                : "-";
            lblSDDiasD.Text = r.StandardDeviationDiasDay.HasValue
                ? r.StandardDeviationDiasDay.Value.ToString("F1")
                : "-";
            lblSDDiasN.Text = r.StandardDeviationDiasNight.HasValue
                ? r.StandardDeviationDiasNight.Value.ToString("F1")
                : "-";

            // DESVIACIÓN ESTÁNDARD - TAM
            lblSDTAMT.Text = r.StandardDeviationTamTotal.HasValue
                ? r.StandardDeviationTamTotal.Value.ToString("F1")
                : "-";
            lblSDTAMD.Text = r.StandardDeviationTamDay.HasValue
                ? r.StandardDeviationTamDay.Value.ToString("F1")
                : "-";
            lblSDTAMN.Text = r.StandardDeviationTamNight.HasValue
                ? r.StandardDeviationTamNight.Value.ToString("F1")
                : "-";

            // DESVIACIÓN ESTÁNDARD - HR
            lblSDFCT.Text = r.StandardDeviationHeartRateTotal.HasValue
                ? r.StandardDeviationHeartRateTotal.Value.ToString("F1")
                : "-";
            lblSDFCD.Text = r.StandardDeviationHeartRateDay.HasValue
                ? r.StandardDeviationHeartRateDay.Value.ToString("F1")
                : "-";
            lblSDFCN.Text = r.StandardDeviationHeartRateNight.HasValue
                ? r.StandardDeviationHeartRateNight.Value.ToString("F1")
                : "-";


            lblDippingSysT.Text = r.SystolicDipping.HasValue ? r.SystolicDipping.Value.ToString("P2") : "-";
            lblDippingDiasT.Text = r.DiastolicDipping.HasValue ? r.DiastolicDipping.Value.ToString("P2") : "-";
        }

        private string PercentageUI(int qty, int total)
        {
            if (total == 0)
                return "(" + 0 + "%)";

            return "(" + (qty / (double)total).ToString("P1") + ")";
        }
    }
}
