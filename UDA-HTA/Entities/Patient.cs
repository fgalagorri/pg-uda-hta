using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public enum SexType { M, F };

    public class Patient
    {
        public Patient()
        {
            UdaId = null;
            EmergencyContactList = new List<EmergencyContact>();
            Background = new List<MedicalRecord>();
            ReportList = new List<Report>();
            DeviceReferences = new List<DeviceReference>();
        }
        
        public string DocumentId { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public DateTime? BirthDate { get; set; }
        public SexType? Sex { get; set; }
        public string Address { get; set; }
        public string Neighbour { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public long? UdaId { get; set; }
        public long? RegisterNumer { get; set; }
        public ICollection<DeviceReference> DeviceReferences { get; set; } 

        public TemporaryData LastTempData { get; set; }

        public ICollection<EmergencyContact> EmergencyContactList { get; set; } 
        public ICollection<MedicalRecord> Background { get; set; } 
        public ICollection<Report> ReportList  { get; set; }
    }
}
