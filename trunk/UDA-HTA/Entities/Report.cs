using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Report
    {
        public Report()
        {
            Doctor = new User();
            TemporaryData = new TemporaryData();
            Carnet = new DailyCarnet();
            Measures = new List<Measurement>();
            Patient = new Patient();
        }

        // Propiuedades que solo se usan con la BD de UDA
        #region UDA Properties

        public long? UdaId { get; set; }
        public User Doctor { get; set; }
        public string RequestDoctor { get; set; }
        public string RequestDoctorSpeciality { get; set; }
        public string Diagnosis { get; set; }

        public long? DailyCarnetId { get; set; }
        public int? TemporaryDataId { get; set; }
        public TemporaryData TemporaryData { get; set; }
        public DailyCarnet Carnet { get; set; }
        public ICollection<Measurement> Measures { get; set; } 

        #endregion


        // Propiedades comunes a los dispositivos
        #region Common properties

        public int DeviceId { get; set; }
        public string DeviceReportId { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Patient Patient { get; set; }


        // Systolic measurements
        public int? SystolicDayAvg { get; set; }
        public int? SystolicDayMax { get; set; }
        public int? SystolicDayMin { get; set; }
        public DateTime? SystolicDayMaxTime { get; set; }
        public DateTime? SystolicDayMinTime { get; set; }

        public int? SystolicNightAvg { get; set; }
        public int? SystolicNightMax { get; set; }
        public int? SystolicNightMin { get; set; }
        public DateTime? SystolicNightMaxTime { get; set; }
        public DateTime? SystolicNightMinTime { get; set; }
        
        public int? SystolicTotalAvg { get; set; }
        public int? SystolicTotalMax { get; set; }
        public int? SystolicTotalMin { get; set; }
        public DateTime? SystolicTotalMaxTime { get; set; }
        public DateTime? SystolicTotalMinTime { get; set; }
        
        
        // Diastolic measurements 
        public int? DiastolicDayAvg { get; set; }
        public int? DiastolicDayMax { get; set; }
        public int? DiastolicDayMin { get; set; }
        public DateTime? DiastolicDayMaxTime { get; set; }
        public DateTime? DiastolicDayMinTime { get; set; }

        public int? DiastolicNightAvg { get; set; }
        public int? DiastolicNightMax { get; set; }
        public int? DiastolicNightMin { get; set; }
        public DateTime? DiastolicNightMaxTime { get; set; }
        public DateTime? DiastolicNightMinTime { get; set; }

        public int? DiastolicTotalAvg { get; set; }
        public int? DiastolicTotalMax { get; set; }
        public int? DiastolicTotalMin { get; set; }
        public DateTime? DiastolicTotalMaxTime { get; set; }  
        public DateTime? DiastolicTotalMinTime { get; set; }

        public int? MiddleDayAvg { get; set; }
        public int? MiddleDayMax { get; set; }
        public int? MiddleDayMin { get; set; }
        public DateTime? MiddleDayMaxTime { get; set; }
        public DateTime? MiddleDayMinTime { get; set; }

        public int? MiddleNightAvg { get; set; }
        public int? MiddleNightMax { get; set; }
        public int? MiddleNightMin { get; set; }
        public DateTime? MiddleNightMaxTime { get; set; }
        public DateTime? MiddleNightMinTime { get; set; }

        public int? MiddleTotalAvg { get; set; }
        public int? MiddleTotalMax { get; set; }
        public int? MiddleTotalMin { get; set; }
        public DateTime? MiddleTotalMaxTime { get; set; }
        public DateTime? MiddleTotalMinTime { get; set; }

        // Heart rate measurements
        public int? HeartRateDayAvg { get; set; }
        public int? HeartRateDayMax { get; set; }
        public int? HeartRateDayMin { get; set; }
        public DateTime? HeartRateDayMaxTime { get; set; }
        public DateTime? HeartRateDayMinTime { get; set; }

        public int? HeartRateNightAvg { get; set; }
        public int? HeartRateNightMax { get; set; }
        public int? HeartRateNightMin { get; set; }
        public DateTime? HeartRateNightMaxTime { get; set; }
        public DateTime? HeartRateNightMinTime { get; set; }

        public int? HeartRateTotalAvg { get; set; }
        public int? HeartRateTotalMax { get; set; }
        public int? HeartRateTotalMin { get; set; }
        public DateTime? HeartRateTotalMaxTime { get; set; }
        public DateTime? HeartRateTotalMinTime { get; set; }

        public decimal? StandardDeviationSysTotal { get; set; }
        public decimal? StandardDeviationDiasTotal { get; set; }
        public decimal? StandardDeviationTamTotal { get; set; }
        public decimal? StandardDeviationHeartRateTotal { get; set; }

        public decimal? StandardDeviationSysDay { get; set; }
        public decimal? StandardDeviationDiasDay { get; set; }
        public decimal? StandardDeviationTamDay { get; set; }
        public decimal? StandardDeviationHeartRateDay { get; set; }
        
        public decimal? StandardDeviationSysNight { get; set; }
        public decimal? StandardDeviationDiasNight { get; set; }
        public decimal? StandarDeviationTamNight { get; set; }
        public decimal? StandardDeviationHeartRateNight { get; set; }

        #endregion
    }
}
