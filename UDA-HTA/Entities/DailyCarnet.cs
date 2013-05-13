using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class DailyCarnet
    {
        public string Technical { get; set; }
        public int Init_bp1 { get; set; }
        public int Init_bp2 { get; set; }
        public int Init_bp3 { get; set; }
        public int Init_hr1 { get; set; }
        public int Init_hr2 { get; set; }
        public int Init_hr3 { get; set; }
        public int Final_bp1 { get; set; }
        public int Final_bp2 { get; set; }
        public int Final_bp3 { get; set; }
        public int Final_hr1 { get; set; }
        public int Final_hr2 { get; set; }
        public int Final_hr3 { get; set; }
        public DateTime Begin_sleep_time { get; set; }
        public DateTime End_sleep_time { get; set; }
        public string How_sleep { get; set; }
        public DateTime Main_meal_time { get; set; }
    }

}
