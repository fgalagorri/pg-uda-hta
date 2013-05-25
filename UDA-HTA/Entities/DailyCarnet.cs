using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class DailyCarnet
    {
        public int? InitSystolic1 { get; set; }
        public int? InitSystolic2 { get; set; }
        public int? InitSystolic3 { get; set; }
        public int? InitDiastolic1 { get; set; }
        public int? InitDiastolic2 { get; set; }
        public int? InitDiastolic3 { get; set; }
        public int? InitHeartRate1 { get; set; }
        public int? InitHeartRate2 { get; set; }
        public int? InitHeartRate3 { get; set; }

        public int? FinalSystolic1 { get; set; }
        public int? FinalSystolic2 { get; set; }
        public int? FinalSystolic3 { get; set; }
        public int? FinalDiastolic1 { get; set; }
        public int? FinalDiastolic2 { get; set; }
        public int? FinalDiastolic3 { get; set; }
        public int? FinalHeartRate1 { get; set; }
        public int? FinalHeartRate2 { get; set; }
        public int? FinalHeartRate3 { get; set; }

        public DateTime SleepTimeStart { get; set; }
        public DateTime SleepTimeEnd { get; set; }
        public string SleepQuality { get; set; }
        public DateTime MealTime { get; set; }

        public ICollection<Effort> Efforts { get; set; }
        public ICollection<Complication> Complications { get; set; }
        public ICollection<Medication> Medications { get; set; } 
    }

}
