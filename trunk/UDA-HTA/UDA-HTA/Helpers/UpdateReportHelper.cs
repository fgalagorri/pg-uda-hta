using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace UDA_HTA.Helpers
{
    public static class UpdateReportHelper
    {

        public static Report UpdateMeasure(Report report, Measurement measure)
        {
            // Actualizo la medición
            var oldMeasure = report.Measures.FirstOrDefault(rm => rm.Id == measure.Id && rm.ReportId == measure.ReportId);
            if (oldMeasure != null)
            {
                oldMeasure.Comment = measure.Comment;
                oldMeasure.IsEnabled = measure.IsEnabled;
            }

            var valid = report.Measures.Where(m => m.Valid && m.IsEnabled).ToList();

            // Actualizar Promedios
            report.SystolicTotalAvg = (int) Math.Round(valid.Average(m => m.Systolic.Value));
            report.SystolicDayAvg =
                (int) Math.Round(valid.Where(m => m.Asleep.HasValue && !m.Asleep.Value).Average(m => m.Systolic.Value));
            report.SystolicNightAvg =
                (int) Math.Round(valid.Where(m => m.Asleep.HasValue && m.Asleep.Value).Average(m => m.Systolic.Value));

            report.DiastolicTotalAvg = (int) Math.Round(valid.Average(m => m.Diastolic.Value));
            report.DiastolicDayAvg =
                (int) Math.Round(valid.Where(m => m.Asleep.HasValue && !m.Asleep.Value).Average(m => m.Diastolic.Value));
            report.DiastolicNightAvg =
                (int) Math.Round(valid.Where(m => m.Asleep.HasValue && m.Asleep.Value).Average(m => m.Diastolic.Value));

            report.MiddleTotalAvg = (int) Math.Round(valid.Average(m => m.Middle.Value));
            report.MiddleDayAvg =
                (int) Math.Round(valid.Where(m => m.Asleep.HasValue && !m.Asleep.Value).Average(m => m.Middle.Value));
            report.MiddleNightAvg =
                (int) Math.Round(valid.Where(m => m.Asleep.HasValue && m.Asleep.Value).Average(m => m.Middle.Value));

            report.HeartRateTotalAvg = (int) Math.Round(valid.Average(m => m.HeartRate.Value));
            report.HeartRateDayAvg =
                (int) Math.Round(valid.Where(m => m.Asleep.HasValue && !m.Asleep.Value).Average(m => m.HeartRate.Value));
            report.HeartRateNightAvg =
                (int) Math.Round(valid.Where(m => m.Asleep.HasValue && m.Asleep.Value).Average(m => m.HeartRate.Value));


            // Actualizar Desviaciones
            var validDay = valid.Where(v => v.Asleep.HasValue && !v.Asleep.Value).ToList();
            var validNight = valid.Where(v => v.Asleep.HasValue && v.Asleep.Value).ToList();
            report.StandardDeviationSysTotal =
                (decimal)
                    Math.Sqrt(valid.Sum(m => Math.Pow(m.Systolic.Value - report.SystolicTotalAvg.Value, 2))/
                              (double) valid.Count);
            report.StandardDeviationSysDay =
                (decimal)
                    Math.Sqrt(validDay.Sum(m => Math.Pow(m.Systolic.Value - report.SystolicDayAvg.Value, 2))/
                              (double) validDay.Count);
            report.StandardDeviationSysNight =
                (decimal)
                    Math.Sqrt(validNight.Sum(m => Math.Pow(m.Systolic.Value - report.SystolicNightAvg.Value, 2))/
                              (double) validNight.Count);

            report.StandardDeviationDiasTotal =
                (decimal)
                    Math.Sqrt(valid.Sum(m => Math.Pow(m.Diastolic.Value - report.DiastolicTotalAvg.Value, 2))/
                              (double) valid.Count);
            report.StandardDeviationDiasDay =
                (decimal)
                    Math.Sqrt(validDay.Sum(m => Math.Pow(m.Diastolic.Value - report.DiastolicDayAvg.Value, 2))/
                              (double) validDay.Count);
            report.StandardDeviationDiasNight =
                (decimal)
                    Math.Sqrt(validNight.Sum(m => Math.Pow(m.Diastolic.Value - report.DiastolicNightAvg.Value, 2))/
                              (double) validNight.Count);

            report.StandardDeviationTamTotal =
                (decimal)
                    Math.Sqrt(valid.Sum(m => Math.Pow(m.Middle.Value - report.MiddleTotalAvg.Value, 2))/
                              (double) valid.Count);
            report.StandardDeviationTamDay =
                (decimal)
                    Math.Sqrt(validDay.Sum(m => Math.Pow(m.Middle.Value - report.MiddleDayAvg.Value, 2))/
                              (double) validDay.Count);
            report.StandardDeviationTamNight =
                (decimal)
                    Math.Sqrt(validNight.Sum(m => Math.Pow(m.Middle.Value - report.MiddleNightAvg.Value, 2))/
                              (double) validNight.Count);

            report.StandardDeviationHeartRateTotal =
                (decimal)
                    Math.Sqrt(valid.Sum(m => Math.Pow(m.HeartRate.Value - report.HeartRateTotalAvg.Value, 2))/
                              (double) valid.Count);
            report.StandardDeviationHeartRateDay =
                (decimal)
                    Math.Sqrt(validDay.Sum(m => Math.Pow(m.HeartRate.Value - report.HeartRateDayAvg.Value, 2))/
                              (double) validDay.Count);
            report.StandardDeviationHeartRateNight =
                (decimal)
                    Math.Sqrt(validNight.Sum(m => Math.Pow(m.HeartRate.Value - report.HeartRateNightAvg.Value, 2))/
                              (double) validNight.Count);


            // Actualizar máximos
            if (!report.SystolicDayMax.HasValue || measure.Systolic.Value >= report.SystolicDayMax)
                report.SystolicDayMax = valid.Where(m => m.Asleep.HasValue && !m.Asleep.Value)
                    .Max(m => m.Systolic.Value);
            if (!report.SystolicNightMax.HasValue || measure.Systolic.Value >= report.SystolicNightMax)
                report.SystolicNightMax =
                    valid.Where(m => m.Asleep.HasValue && m.Asleep.Value).Max(m => m.Systolic.Value);

            if (!report.DiastolicDayMax.HasValue || measure.Diastolic.Value >= report.DiastolicDayMax)
                report.DiastolicDayMax =
                    valid.Where(m => m.Asleep.HasValue && !m.Asleep.Value).Max(m => m.Diastolic.Value);
            if (!report.DiastolicNightMax.HasValue || measure.Diastolic.Value >= report.DiastolicNightMax)
                report.DiastolicNightMax =
                    valid.Where(m => m.Asleep.HasValue && m.Asleep.Value).Max(m => m.Diastolic.Value);

            if (!report.HeartRateDayMax.HasValue || measure.HeartRate.Value >= report.HeartRateDayMax)
                report.HeartRateDayMax =
                    valid.Where(m => m.Asleep.HasValue && !m.Asleep.Value).Max(m => m.HeartRate.Value);
            if (!report.HeartRateNightMax.HasValue || measure.HeartRate.Value >= report.HeartRateNightMax)
                report.HeartRateNightMax =
                    valid.Where(m => m.Asleep.HasValue && m.Asleep.Value).Max(m => m.HeartRate.Value);


            // Actualizar mínimos
            if (!report.SystolicDayMin.HasValue || measure.Systolic.Value <= report.SystolicDayMin)
                report.SystolicDayMin = valid.Where(m => m.Asleep.HasValue && !m.Asleep.Value)
                    .Min(m => m.Systolic.Value);
            if (!report.SystolicNightMin.HasValue || measure.Systolic.Value <= report.SystolicNightMin)
                report.SystolicNightMin =
                    valid.Where(m => m.Asleep.HasValue && m.Asleep.Value).Min(m => m.Systolic.Value);

            if (!report.DiastolicDayMin.HasValue || measure.Diastolic.Value <= report.DiastolicDayMin)
                report.DiastolicDayMin =
                    valid.Where(m => m.Asleep.HasValue && !m.Asleep.Value).Min(m => m.Diastolic.Value);
            if (!report.DiastolicNightMin.HasValue || measure.Diastolic.Value <= report.DiastolicNightMin)
                report.DiastolicNightMin =
                    valid.Where(m => m.Asleep.HasValue && m.Asleep.Value).Min(m => m.Diastolic.Value);

            if (!report.HeartRateDayMin.HasValue || measure.HeartRate.Value <= report.HeartRateDayMin)
                report.HeartRateDayMin =
                    valid.Where(m => m.Asleep.HasValue && !m.Asleep.Value).Min(m => m.HeartRate.Value);
            if (!report.HeartRateNightMin.HasValue || measure.HeartRate.Value <= report.HeartRateNightMin)
                report.HeartRateNightMin =
                    valid.Where(m => m.Asleep.HasValue && m.Asleep.Value).Min(m => m.HeartRate.Value);


            // Actualizar Dipping
            report.SystolicDipping = (report.SystolicDayAvg.Value - report.SystolicNightAvg.Value)/
                                     (decimal) report.SystolicDayAvg.Value;
            report.DiastolicDipping = (report.DiastolicDayAvg.Value - report.DiastolicNightAvg.Value)/
                                      (decimal) report.DiastolicDayAvg.Value;

            return report;
        }
    }
}