using System;

namespace Entities
{
    public class PatientReport
    {
        public string PatientName { get; set; }

        public string PatientLastName { get; set; }

        public string PatientDocument { get; set; }

        public string PatientIdent { get; set; }

        public DateTime ReportDate { get; set; }

        public int ReportDevice { get; set; }

        public string ReportIdent { get; set; }
    }
}
