using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Limits
    {
        public int MaxDiasDay { get; set; }
        public int MaxDiasNight { get; set; }
        public int MaxDiasTotal { get; set; }
        public int MaxSysDay { get; set; }
        public int MaxSysNight { get; set; }
        public int MaxSysTotal { get; set; }


        // Chart Limits
        public int HiSysTotal { get; set; }
        public int HiSysDay { get; set; }
        public int HiSysNight { get; set; }
        public int HiDiasTotal { get; set; }
        public int HiDiasDay { get; set; }
        public int HiDiasNight { get; set; }

        public int LoSysTotal { get; set; }
        public int LoSysDay { get; set; }
        public int LoSysNight { get; set; }
        public int LoDiasTotal { get; set; }
        public int LoDiasDay { get; set; }
        public int LoDiasNight { get; set; } 
    }
}
