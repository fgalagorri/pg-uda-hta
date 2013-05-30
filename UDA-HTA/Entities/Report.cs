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
        }

        // Propiuedades que solo se usan con la BD de UDA
        #region UDA Properties

        public long? UdaId { get; set; }
        public User Doctor { get; set; }
        public string RequestDoctor { get; set; }
        public string RequestDoctorSpeciality { get; set; }
        public string Diagnosis { get; set; }

        public int? DailyCarnetId { get; set; } // TODO cambiar a LONG
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
        public int? DiastolicTotalMin { get; set; }
        public DateTime? DiastolicTotalMaxTime { get; set; }  
        public DateTime? DiastolicTotalMinTime { get; set; }


        // Heart rate measurements
        public int? HeartRateAvg { get; set; }

        #endregion
    }
}
