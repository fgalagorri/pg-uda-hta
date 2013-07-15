using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Measurement
    {
        public long ReportId { get; set; }

        public long Id { get; set; }
        public DateTime? Time { get; set; }
        public int? Systolic { get; set; }
        public int? Diastolic { get; set; }
        public int? Middle { get; set; }
        public int? HeartRate { get; set; }

        public string Comment { get; set; }
        public bool? Asleep { get; set; }
        public bool Valid { get; set; }
        public bool Retry { get; set; }

        // TODO: Ver el parámetro extra del spacelabs!!

    }
}
