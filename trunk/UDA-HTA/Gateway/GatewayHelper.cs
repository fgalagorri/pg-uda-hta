﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace Gateway
{
    public static class GatewayHelper
    {
        public static bool IsAsleep(TimeSpan start, TimeSpan end, DateTime time)
        {
            if (start < end)
                return start <= time.TimeOfDay && time.TimeOfDay <= end;
            else
                return time.TimeOfDay < end || start <= time.TimeOfDay;
        }


        public static Report UpdateMeasureSummary(Report report)
        {
            var valid = report.Measures.Where(m => m.Valid && m.IsEnabled).ToList();
            var validDay = valid.Where(v => v.Asleep.HasValue && !v.Asleep.Value).ToList();
            var validNight = valid.Where(v => v.Asleep.HasValue && v.Asleep.Value).ToList();


            if (valid.Any())
            {
                // Actualizar Promedios
                report.SystolicTotalAvg = (int) Math.Round(valid.Average(m => m.Systolic.Value));
                report.DiastolicTotalAvg = (int) Math.Round(valid.Average(m => m.Diastolic.Value));
                report.MiddleTotalAvg = (int) Math.Round(valid.Average(m => m.Middle.Value));
                report.HeartRateTotalAvg = (int) Math.Round(valid.Average(m => m.HeartRate.Value));

                // Actualizar Desviaciones
                report.StandardDeviationSysTotal =
                    (decimal)
                    Math.Sqrt(valid.Sum(m => Math.Pow(m.Systolic.Value - report.SystolicTotalAvg.Value, 2))/
                              (double) valid.Count);
                report.StandardDeviationDiasTotal =
                    (decimal)
                    Math.Sqrt(valid.Sum(m => Math.Pow(m.Diastolic.Value - report.DiastolicTotalAvg.Value, 2))/
                              (double) valid.Count);
                report.StandardDeviationTamTotal =
                    (decimal)
                    Math.Sqrt(valid.Sum(m => Math.Pow(m.Middle.Value - report.MiddleTotalAvg.Value, 2))/
                              (double) valid.Count);
                report.StandardDeviationHeartRateTotal =
                    (decimal)
                    Math.Sqrt(valid.Sum(m => Math.Pow(m.HeartRate.Value - report.HeartRateTotalAvg.Value, 2))/
                              (double) valid.Count);
            }
            else
            {
                report.SystolicTotalAvg = null;
                report.DiastolicTotalAvg = null;
                report.MiddleTotalAvg = null;
                report.HeartRateTotalAvg = null;

                report.StandardDeviationSysTotal = null;
                report.StandardDeviationDiasTotal = null;
                report.StandardDeviationTamTotal = null;
                report.StandardDeviationHeartRateTotal = null;
            }


            if (validDay.Any())
            {
                // Actualizar Promedios
                report.SystolicDayAvg = (int) Math.Round(validDay.Average(m => m.Systolic.Value));
                report.DiastolicDayAvg = (int) Math.Round(validDay.Average(m => m.Diastolic.Value));
                report.MiddleDayAvg = (int) Math.Round(validDay.Average(m => m.Middle.Value));
                report.HeartRateDayAvg = (int) Math.Round(validDay.Average(m => m.HeartRate.Value));

                // Actualizar Desviaciones
                report.StandardDeviationSysDay =
                    (decimal)
                    Math.Sqrt(validDay.Sum(m => Math.Pow(m.Systolic.Value - report.SystolicDayAvg.Value, 2))/
                              (double) validDay.Count);
                report.StandardDeviationDiasDay =
                    (decimal)
                    Math.Sqrt(validDay.Sum(m => Math.Pow(m.Diastolic.Value - report.DiastolicDayAvg.Value, 2))/
                              (double) validDay.Count);
                report.StandardDeviationTamDay =
                    (decimal)
                    Math.Sqrt(validDay.Sum(m => Math.Pow(m.Middle.Value - report.MiddleDayAvg.Value, 2))/
                              (double) validDay.Count);
                report.StandardDeviationHeartRateDay =
                    (decimal)
                    Math.Sqrt(validDay.Sum(m => Math.Pow(m.HeartRate.Value - report.HeartRateDayAvg.Value, 2))/
                              (double) validDay.Count);

                // Actualizar máximos
                report.SystolicDayMax = validDay.Max(m => m.Systolic.Value);
                report.DiastolicDayMax = validDay.Max(m => m.Diastolic.Value);
                report.HeartRateDayMax = validDay.Max(m => m.HeartRate.Value);

                // Actualizar mínimos
                report.SystolicDayMin = validDay.Min(m => m.Systolic.Value);
                report.DiastolicDayMin = validDay.Min(m => m.Diastolic.Value);
                report.HeartRateDayMin = validDay.Min(m => m.HeartRate.Value);

            }
            else
            {
                report.SystolicDayAvg = null;
                report.DiastolicDayAvg = null;
                report.MiddleDayAvg = null;
                report.HeartRateDayAvg = null;

                report.StandardDeviationSysDay = null;
                report.StandardDeviationDiasDay = null;
                report.StandardDeviationTamDay = null;
                report.StandardDeviationHeartRateDay = null;

                report.SystolicDayMax = null;
                report.DiastolicDayMax = null;
                report.HeartRateDayMax = null;

                report.SystolicDayMin = null;
                report.DiastolicDayMin = null;
                report.HeartRateDayMin = null;
            }


            if (validNight.Any())
            {
                // Actualizar Promedios
                report.SystolicNightAvg = (int) Math.Round(validNight.Average(m => m.Systolic.Value));
                report.DiastolicNightAvg = (int) Math.Round(validNight.Average(m => m.Diastolic.Value));
                report.MiddleNightAvg = (int) Math.Round(validNight.Average(m => m.Middle.Value));
                report.HeartRateNightAvg = (int) Math.Round(validNight.Average(m => m.HeartRate.Value));

                // Actualizar Desviaciones
                report.StandardDeviationSysNight =
                    (decimal)
                    Math.Sqrt(validNight.Sum(m => Math.Pow(m.Systolic.Value - report.SystolicNightAvg.Value, 2))/
                              (double) validNight.Count);
                report.StandardDeviationDiasNight =
                    (decimal)
                    Math.Sqrt(validNight.Sum(m => Math.Pow(m.Diastolic.Value - report.DiastolicNightAvg.Value, 2))/
                              (double) validNight.Count);
                report.StandardDeviationTamNight =
                    (decimal)
                    Math.Sqrt(validNight.Sum(m => Math.Pow(m.Middle.Value - report.MiddleNightAvg.Value, 2))/
                              (double) validNight.Count);
                report.StandardDeviationHeartRateNight =
                    (decimal)
                    Math.Sqrt(validNight.Sum(m => Math.Pow(m.HeartRate.Value - report.HeartRateNightAvg.Value, 2))/
                              (double) validNight.Count);

                // Actualizar máximos
                report.SystolicNightMax = validNight.Max(m => m.Systolic.Value);
                report.DiastolicNightMax = validNight.Max(m => m.Diastolic.Value);
                report.HeartRateNightMax = validNight.Max(m => m.HeartRate.Value);

                // Actualizar mínimos
                report.SystolicNightMin = validNight.Min(m => m.Systolic.Value);
                report.DiastolicNightMin = validNight.Min(m => m.Diastolic.Value);
                report.HeartRateNightMin = validNight.Min(m => m.HeartRate.Value);
            }
            else
            {
                report.SystolicNightAvg = null;
                report.DiastolicNightAvg = null;
                report.MiddleNightAvg = null;
                report.HeartRateNightAvg = null;

                report.StandardDeviationSysNight = null;
                report.StandardDeviationDiasNight = null;
                report.StandardDeviationTamNight = null;
                report.StandardDeviationHeartRateNight = null;

                report.SystolicNightMax = null;
                report.DiastolicNightMax = null;
                report.HeartRateNightMax = null;

                report.SystolicNightMin = null;
                report.DiastolicNightMin = null;
                report.HeartRateNightMin = null;
            }


            if (validDay.Any() && validNight.Any())
            {
                // Actualizar Dipping
                report.SystolicDipping = (report.SystolicDayAvg.Value - report.SystolicNightAvg.Value)/
                                         (decimal) report.SystolicDayAvg.Value;
                report.DiastolicDipping = (report.DiastolicDayAvg.Value - report.DiastolicNightAvg.Value)/
                                          (decimal) report.DiastolicDayAvg.Value;
            }
            else
            {
                report.SystolicDipping = null;
                report.DiastolicDipping = null;
            }

            return report;
        }
    }
}
