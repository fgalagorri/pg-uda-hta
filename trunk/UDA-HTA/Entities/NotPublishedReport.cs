using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class NotPublishedReport
    {
        public string PatientName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientDocument { get; set; }
        public long PatientId { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public long ReportId { get; set; }
    }
}
