using System;
using System.Configuration;
using System.Linq;
using System.Windows.Controls;
using Entities;

namespace UDA_HTA.UserControls.MainWindow.Patients
{
    /// <summary>
    /// Interaction logic for PatientViewerSummary.xaml
    /// </summary>
    public partial class PatientViewerSummary : UserControl
    {
        // TODO: Ver estos valores
        int sysMaxDay = 120;
        int sysMaxNight = 120;
        int diasMaxDay = 80;
        int diasMaxNight = 80;


        public PatientViewerSummary()
        {
            InitializeComponent();
        }

        public void SetReport(Report r)
        {
            int aux, validTotal, validDay, validNight;
            int total = r.Measures.Count(m => !m.Retry);
            int totalDay = r.Measures.Count(m => m.Asleep.HasValue && !m.Asleep.Value && !m.Retry);
            int totalNight = r.Measures.Count(m => m.Asleep.HasValue && m.Asleep.Value && !m.Retry);

            // Totales
            lblTotalMeasuresT.Text = total.ToString();
            lblTotalMeasuresD.Text = totalDay.ToString();
            lblTotalMeasuresN.Text = totalNight.ToString();

            validTotal = r.Measures.Count(m => m.Valid);
            lblValidMeasuresT.Text = validTotal.ToString();
            lblValidMeasuresTP.Text = PercentageUI(validTotal, total);

            validDay = r.Measures.Count(m => m.Valid && m.Asleep.HasValue && !m.Asleep.Value);
            lblValidMeasuresD.Text = validDay.ToString();
            lblValidMeasuresDP.Text = PercentageUI(validDay, totalDay);

            validNight = r.Measures.Count(m => m.Valid && m.Asleep.HasValue && m.Asleep.Value);
            lblValidMeasuresN.Text = validNight.ToString();
            lblValidMeasuresNP.Text = PercentageUI(validNight, totalNight);


            var valid = r.Measures.Where(m => m.Valid).ToList();
            if (valid.Any())
            {
                // PROMEDIOS
                // Systolic 
                lblSystolicAvgT.Text = r.SystolicTotalAvg.HasValue
                                           ? r.SystolicTotalAvg.ToString()
                                           : Math.Round(valid.Average(m => m.Systolic.Value)).ToString();
                lblSystolicAvgD.Text = r.SystolicDayAvg.HasValue
                                           ? r.SystolicDayAvg.Value.ToString()
                                           : Math.Round(valid.Where(m => !m.Asleep.Value)
                                                             .Average(m => m.Systolic.Value)).ToString();
                lblSystolicAvgN.Text = r.SystolicNightAvg.HasValue
                                           ? r.SystolicNightAvg.Value.ToString()
                                           : Math.Round(valid.Where(m => m.Asleep.Value)
                                                             .Average(m => m.Systolic.Value)).ToString();
                // Diastolic 
                lblDiastolicAvgT.Text = r.DiastolicTotalAvg.HasValue
                                            ? r.DiastolicTotalAvg.Value.ToString()
                                            : Math.Round(valid.Average(m => m.Diastolic.Value)).ToString();
                lblDiastolicAvgD.Text = r.DiastolicDayAvg.HasValue
                                            ? r.DiastolicDayAvg.Value.ToString()
                                            : Math.Round(valid.Where(m => !m.Asleep.Value)
                                                              .Average(m => m.Diastolic.Value)).ToString();
                lblDiastolicAvgN.Text = r.DiastolicNightAvg.HasValue
                                            ? r.DiastolicNightAvg.Value.ToString()
                                            : Math.Round(valid.Where(m => m.Asleep.Value)
                                                              .Average(m => m.Diastolic.Value)).ToString();
                // Middle
                lblTamAvgT.Text = r.MiddleTotalAvg.HasValue
                                      ? r.MiddleTotalAvg.Value.ToString()
                                      : Math.Round(valid.Average(m => m.Middle.Value)).ToString();
                lblTamAvgD.Text = r.MiddleDayAvg.HasValue
                                      ? r.MiddleDayAvg.Value.ToString()
                                      : Math.Round(valid.Where(m => !m.Asleep.Value)
                                                        .Average(m => m.Middle.Value)).ToString();
                lblTamAvgN.Text = r.MiddleNightAvg.HasValue
                                      ? r.MiddleNightAvg.Value.ToString()
                                      : Math.Round(valid.Where(m => m.Asleep.Value)
                                                        .Average(m => m.Middle.Value)).ToString();
                // Heart Rate
                lblHRAvgT.Text = r.HeartRateTotalAvg.HasValue
                                     ? r.HeartRateTotalAvg.Value.ToString()
                                     : Math.Round(valid.Average(m => m.HeartRate.Value)).ToString();
                lblHRAvgD.Text = r.HeartRateDayAvg.HasValue
                                     ? r.HeartRateDayAvg.Value.ToString()
                                     : Math.Round(valid.Where(m => !m.Asleep.Value)
                                                       .Average(m => m.HeartRate.Value)).ToString();
                lblHRAvgN.Text = r.HeartRateNightAvg.HasValue
                                     ? r.HeartRateNightAvg.Value.ToString()
                                     : Math.Round(valid.Where(m => m.Asleep.Value)
                                                       .Average(m => m.HeartRate.Value)).ToString();


                // Valores sobre el límite
                aux = valid.Where(m => !m.Asleep.Value).Count(m => m.Systolic > sysMaxDay);
                lblSystolicOverLimD.Text = aux.ToString();
                lblSystolicOverLimDP.Text = PercentageUI(aux, validDay);
                aux = valid.Where(m => m.Asleep.Value).Count(m => m.Systolic > sysMaxNight);
                lblSystolicOverLimN.Text = aux.ToString();
                lblSystolicOverLimNP.Text = PercentageUI(aux, validNight);

                aux = valid.Where(m => !m.Asleep.Value).Count(m => m.Diastolic > diasMaxDay);
                lblDiastolicOverLimD.Text = aux.ToString();
                lblDiastolicOverLimDP.Text = PercentageUI(aux, validDay);
                aux = valid.Where(m => m.Asleep.Value).Count(m => m.Diastolic > diasMaxNight);
                lblDiastolicOverLimN.Text = aux.ToString();
                lblDiastolicOverLimNP.Text = PercentageUI(aux, validNight);


                // MAXIMOS
                // Sistolic
                var max = r.SystolicDayMax.HasValue
                              ? r.SystolicDayMax.Value
                              : valid.Where(m => !m.Asleep.Value).Max(m => m.Systolic.Value);
                lblSystolicMaxD.Text = max.ToString();
                lblSystolicMaxDT.Text = valid.First(m => !m.Asleep.Value && m.Systolic == max).Time.Value
                                             .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                max = r.SystolicNightMax.HasValue
                          ? r.SystolicNightMax.Value
                          : valid.Where(m => m.Asleep.Value).Max(m => m.Systolic.Value);
                lblSystolicMaxN.Text = max.ToString();
                lblSystolicMaxNT.Text = valid.First(m => m.Asleep.Value && m.Systolic.Value == max).Time.Value
                                             .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                // Diastolic
                max = r.DiastolicDayMax.HasValue
                          ? r.DiastolicDayMax.Value
                          : valid.Where(m => !m.Asleep.Value).Max(m => m.Diastolic.Value);
                lblDiastolicMaxD.Text = max.ToString();
                lblDiastolicMaxDT.Text = valid.First(m => !m.Asleep.Value && m.Diastolic == max).Time.Value
                                              .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                max = r.DiastolicNightMax.HasValue
                          ? r.DiastolicNightMax.Value
                          : valid.Where(m => m.Asleep.Value).Max(m => m.Diastolic.Value);
                lblDiastolicMaxN.Text = max.ToString();
                lblDiastolicMaxNT.Text = valid.First(m => m.Asleep.Value && m.Diastolic == max).Time.Value
                                              .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                // Heart Rate
                max = r.HeartRateDayMax.HasValue
                          ? r.HeartRateDayMax.Value
                          : valid.Where(m => !m.Asleep.Value).Max(m => m.HeartRate.Value);
                lblHRMaxD.Text = max.ToString();
                lblHRMaxDT.Text = valid.First(m => !m.Asleep.Value && m.HeartRate == max).Time.Value
                                       .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                max = r.HeartRateNightMax.HasValue
                          ? r.HeartRateNightMax.Value
                          : valid.Where(m => m.Asleep.Value).Max(m => m.HeartRate.Value);
                lblHRMaxN.Text = max.ToString();
                lblHRMaxNT.Text = valid.First(m => m.Asleep.Value && m.HeartRate == max).Time.Value
                                       .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);


                // MINIMOS
                // Sistolic
                var min = r.SystolicDayMin.HasValue
                              ? r.SystolicDayMin.Value
                              : valid.Where(m => !m.Asleep.Value).Min(m => m.Systolic.Value);
                lblSystolicMinD.Text = min.ToString();
                lblSystolicMinDT.Text = valid.First(m => !m.Asleep.Value && m.Systolic == min).Time.Value
                                             .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                min = r.SystolicNightMin.HasValue
                          ? r.SystolicNightMin.Value
                          : valid.Where(m => m.Asleep.Value).Min(m => m.Systolic.Value);
                lblSystolicMinN.Text = min.ToString();
                lblSystolicMinNT.Text = valid.First(m => m.Asleep.Value && m.Systolic == min).Time.Value
                                             .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                // Diastolic
                min = r.DiastolicDayMin.HasValue
                          ? r.DiastolicDayMin.Value
                          : valid.Where(m => !m.Asleep.Value).Min(m => m.Diastolic.Value);
                lblDiastolicMinD.Text = min.ToString();
                lblDiastolicMinDT.Text = valid.First(m => !m.Asleep.Value && m.Diastolic == min).Time.Value
                                              .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                min = r.DiastolicNightMin.HasValue
                          ? r.DiastolicNightMin.Value
                          : valid.Where(m => m.Asleep.Value).Min(m => m.Diastolic.Value);
                lblDiastolicMinN.Text = min.ToString();
                lblDiastolicMinNT.Text = valid.First(m => m.Asleep.Value && m.Diastolic == min).Time.Value
                                              .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                // Heart Rate
                min = r.HeartRateDayMin.HasValue
                          ? r.HeartRateDayMin.Value
                          : valid.Where(m => !m.Asleep.Value).Min(m => m.HeartRate.Value);
                lblHRMinD.Text = min.ToString();
                lblHRMinDT.Text = valid.First(m => !m.Asleep.Value && m.HeartRate == min).Time.Value
                                       .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                min = r.HeartRateNightMin.HasValue
                          ? r.HeartRateNightMin.Value
                          : valid.Where(m => m.Asleep.Value).Min(m => m.HeartRate.Value);
                lblHRMinN.Text = min.ToString();
                lblHRMinNT.Text = valid.First(m => m.Asleep.Value && m.HeartRate == min).Time.Value
                                       .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);

                //DESVIACION ESTANDAR
                //Presion sistolica
                lblSDSysT.Text = r.StandardDeviationSysTotal.ToString();
                lblSDSysD.Text = r.StandardDeviationSysDay.ToString();
                lblSDSysN.Text = r.StandardDeviationSysNight.ToString();

                //Presion diastolica
                lblSDDiasT.Text = r.StandardDeviationDiasTotal.ToString();
                lblSDDiasD.Text = r.StandardDeviationDiasDay.ToString();
                lblSDDiasN.Text = r.StandardDeviationDiasNight.ToString();

                //TAM
                lblSDTAMT.Text = r.StandardDeviationTamTotal.ToString();
                lblSDTAMD.Text = r.StandardDeviationTamDay.ToString();
                lblSDTAMN.Text = r.StandarDeviationTamNight.ToString();

                //Frecuencia cardiaca
                lblSDFCT.Text = r.StandardDeviationHeartRateTotal.ToString();
                lblSDFCD.Text = r.StandardDeviationHeartRateDay.ToString();
                lblSDFCN.Text = r.StandardDeviationHeartRateNight.ToString();

                //DIPPING
                decimal dippingSys = Math.Round(((decimal)(r.SystolicDayAvg.Value - r.SystolicNightAvg.Value) / r.SystolicDayAvg.Value) * 100, 2);
                decimal dippingDias = Math.Round(((decimal)(r.DiastolicDayAvg.Value - r.DiastolicNightAvg.Value) / r.DiastolicDayAvg.Value) * 100, 2);

                lblDippingSysT.Text = dippingSys.ToString();
                lblDippingDiasT.Text = dippingDias.ToString();
            }
        }

        private string PercentageUI(int qty, int total)
        {
            if (total == 0)
                return "(" + 0 + "%)";

            return "(" + ((qty * 100) / (double)total).ToString("N1") + "%)";
        }
    }
}
