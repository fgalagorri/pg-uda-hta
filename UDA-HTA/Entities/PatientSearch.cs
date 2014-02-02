using System;

namespace Entities
{
    public class PatientSearch
    {
        public long? UdaId { get; set; }

        public string DocumentId { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public DateTime? BirthDate { get; set; }
        public string RegisterNumber { get; set; }
    }
}
