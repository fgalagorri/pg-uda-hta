using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using Entities;

namespace UDA_HTA.UserControls.MainWindow
{
    /// <summary>
    /// Interaction logic for PatientViewerSummary.xaml
    /// </summary>
    public partial class PatientViewerSummary : UserControl
    {
        // TODO: Ver estos valores
        int sysMaxDay = 140;
        int sysMaxNight = 125;
        int diasMaxDay = 90;
        int diasMaxNight = 80;


        public PatientViewerSummary()
        {
            InitializeComponent();
        }

        public void SetReport(Report r)
        {
            int aux;
            int total = r.Measures.Count;
            int totalDay = r.Measures.Count(m => m.Asleep.HasValue && !m.Asleep.Value);
            int totalNight = r.Measures.Count(m => m.Asleep.HasValue && m.Asleep.Value);

            // Totales
            lblTotalMeasuresT.Text = total.ToString();
            lblTotalMeasuresD.Text = totalDay.ToString();
            lblTotalMeasuresN.Text = totalNight.ToString();

            aux = r.Measures.Count(m => m.Valid);
            lblValidMeasuresT.Text = aux.ToString();
            lblValidMeasuresTP.Text = "(" + ((total != 0) ? aux/total : 0) + "%)";

            aux = r.Measures.Count(m => m.Valid && m.Asleep.HasValue && !m.Asleep.Value);
            lblValidMeasuresD.Text = aux.ToString();
            lblValidMeasuresDP.Text = "(" + ((totalDay != 0) ? aux/totalDay : 0) + "%)";

            aux = r.Measures.Count(m => m.Valid && m.Asleep.HasValue && m.Asleep.Value);
            lblValidMeasuresN.Text = aux.ToString();
            lblValidMeasuresNP.Text = "(" + ((totalNight != 0) ? aux/totalNight : 0) + "%)";


            // Promedios
            if (r.Measures.Any())
            {
                var valid = r.Measures.Where(m => m.Valid).ToList();
                lblSystolicAvgT.Text = Math.Round(valid.Average(m => m.Systolic.Value)).ToString();
                lblSystolicAvgD.Text = Math.Round(valid.Where(m => !m.Asleep.Value)
                                                   .Average(m => m.Systolic.Value)).ToString();
                lblSystolicAvgN.Text = Math.Round(valid.Where(m => m.Asleep.Value)
                                                   .Average(m => m.Systolic.Value)).ToString();

                lblDiastolicAvgT.Text = Math.Round(valid.Average(m => m.Diastolic.Value)).ToString();
                lblDiastolicAvgD.Text = Math.Round(valid.Where(m => !m.Asleep.Value)
                                                    .Average(m => m.Diastolic.Value)).ToString();
                lblDiastolicAvgN.Text = Math.Round(valid.Where(m => m.Asleep.Value)
                                                    .Average(m => m.Diastolic.Value)).ToString();

                lblTamAvgT.Text = Math.Round(valid.Average(m => m.Middle.Value)).ToString();
                lblTamAvgD.Text = Math.Round(valid.Where(m => !m.Asleep.Value)
                                              .Average(m => m.Middle.Value)).ToString();
                lblTamAvgN.Text = Math.Round(valid.Where(m => m.Asleep.Value)
                                              .Average(m => m.Middle.Value)).ToString();

                lblHRAvgT.Text = Math.Round(valid.Average(m => m.Middle.Value)).ToString();
                lblHRAvgD.Text = Math.Round(valid.Where(m => !m.Asleep.Value)
                                             .Average(m => m.Middle.Value)).ToString();
                lblHRAvgN.Text = Math.Round(valid.Where(m => m.Asleep.Value)
                                             .Average(m => m.Middle.Value)).ToString();


                // Valores sobre el límite
                aux = valid.Where(m => !m.Asleep.Value).Count(m => m.Systolic > sysMaxDay);
                lblSystolicOverLimD.Text = aux.ToString();
                lblSystolicOverLimDP.Text = "(" + ((totalDay != 0) ? aux/totalDay : 0) + "%)";
                aux = valid.Where(m => m.Asleep.Value).Count(m => m.Systolic > sysMaxNight);
                lblSystolicOverLimN.Text = aux.ToString();
                lblSystolicOverLimNP.Text = "(" + ((totalNight != 0) ? aux/totalNight : 0) + "%)";

                aux = valid.Where(m => !m.Asleep.Value).Count(m => m.Diastolic > diasMaxDay);
                lblDiastolicOverLimD.Text = aux.ToString();
                lblDiastolicOverLimDP.Text = "(" + ((totalDay != 0) ? aux/totalDay : 0) + "%)";
                aux = valid.Where(m => m.Asleep.Value).Count(m => m.Systolic > diasMaxNight);
                lblDiastolicOverLimN.Text = aux.ToString();
                lblDiastolicOverLimNP.Text = "(" + ((totalNight != 0) ? aux/totalNight : 0) + "%)";


                // Maximos
                aux = valid.Where(m => !m.Asleep.Value).Max(m => m.Systolic.Value);
                lblSystolicMaxD.Text = aux.ToString();
                lblSystolicMaxDT.Text = valid.First(m => m.Systolic == aux).Time.Value
                                         .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                aux = valid.Where(m => m.Asleep.Value).Max(m => m.Systolic.Value);
                lblSystolicMaxN.Text = aux.ToString();
                lblSystolicMaxNT.Text = valid.First(m => m.Systolic.Value == aux).Time.Value
                                         .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);

                aux = valid.Where(m => !m.Asleep.Value).Max(m => m.Diastolic.Value);
                lblDiastolicMaxD.Text = aux.ToString();
                lblDiastolicMaxDT.Text = valid.First(m => m.Diastolic == aux).Time.Value
                                          .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                aux = valid.Where(m => m.Asleep.Value).Max(m => m.Diastolic.Value);
                lblDiastolicMaxN.Text = aux.ToString();
                lblDiastolicMaxNT.Text = valid.First(m => m.Diastolic == aux).Time.Value
                                          .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);

                aux = valid.Where(m => !m.Asleep.Value).Max(m => m.HeartRate.Value);
                lblHRMaxD.Text = aux.ToString();
                lblHRMaxDT.Text = valid.First(m => m.HeartRate == aux).Time.Value
                                   .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                aux = valid.Where(m => m.Asleep.Value).Max(m => m.HeartRate.Value);
                lblHRMaxN.Text = aux.ToString();
                lblHRMaxNT.Text = valid.First(m => m.HeartRate == aux).Time.Value
                                   .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);


                // Minimos
                aux = valid.Where(m => !m.Asleep.Value).Min(m => m.Systolic.Value);
                lblSystolicMinD.Text = aux.ToString();
                lblSystolicMinDT.Text = valid.First(m => m.Systolic == aux).Time.Value
                                         .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                aux = valid.Where(m => m.Asleep.Value).Min(m => m.Systolic.Value);
                lblSystolicMinN.Text = aux.ToString();
                lblSystolicMinNT.Text = valid.First(m => m.Systolic == aux).Time.Value
                                         .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);

                aux = valid.Where(m => !m.Asleep.Value).Min(m => m.Diastolic.Value);
                lblDiastolicMinD.Text = aux.ToString();
                lblDiastolicMinDT.Text = valid.First(m => m.Diastolic == aux).Time.Value
                                          .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                aux = valid.Where(m => m.Asleep.Value).Min(m => m.Diastolic.Value);
                lblDiastolicMinN.Text = aux.ToString();
                lblDiastolicMinNT.Text = valid.First(m => m.Diastolic == aux).Time.Value
                                          .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);

                aux = valid.Where(m => !m.Asleep.Value).Min(m => m.HeartRate.Value);
                lblHRMinD.Text = aux.ToString();
                lblHRMinDT.Text = valid.First(m => m.HeartRate == aux).Time.Value
                                   .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
                aux = valid.Where(m => m.Asleep.Value).Min(m => m.HeartRate.Value);
                lblHRMinN.Text = aux.ToString();
                lblHRMinNT.Text = valid.First(m => m.HeartRate == aux).Time.Value
                                   .ToString(ConfigurationManager.AppSettings["ShortTimeString"]);
            }
        }
    }
}
