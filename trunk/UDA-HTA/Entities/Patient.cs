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

        public void Merge(Patient patient)
        {
            if (!String.IsNullOrWhiteSpace(patient.DocumentId))
                DocumentId = patient.DocumentId;
            if (!String.IsNullOrWhiteSpace(patient.Names))
                Names = patient.Names;
            if (!String.IsNullOrWhiteSpace(patient.Surnames))
                Surnames = patient.Surnames;
            if (patient.BirthDate.HasValue)
                BirthDate = patient.BirthDate;
            if (patient.Sex.HasValue)
                Sex = patient.Sex;
            if (!String.IsNullOrWhiteSpace(patient.Address))
                Address = patient.Address;
            if (!String.IsNullOrWhiteSpace(patient.Neighbour))
                Neighbour = patient.Neighbour;
            if (!String.IsNullOrWhiteSpace(patient.City))
                City = patient.City;
            if (!String.IsNullOrWhiteSpace(patient.Department))
                Department = patient.Department;
            if (!String.IsNullOrWhiteSpace(patient.Phone))
                Phone = patient.Phone;
            if (!String.IsNullOrWhiteSpace(patient.Phone2))
                Phone2 = patient.Phone2;
            if (!String.IsNullOrWhiteSpace(patient.CellPhone))
                CellPhone = patient.CellPhone;
            if (!String.IsNullOrWhiteSpace(patient.Email))
                Email = patient.Email;
            if (patient.UdaId.HasValue)
                UdaId = patient.UdaId;
            if (!String.IsNullOrWhiteSpace(patient.RegisterNumber))
                RegisterNumber = patient.RegisterNumber;

            foreach (var dr in patient.DeviceReferences)
            {
                if(!DeviceReferences.Any(r => r.deviceReferenceId == dr.deviceReferenceId && r.deviceType == dr.deviceType))
                    DeviceReferences.Add(dr);
            }

            patient.ModifiedDate = DateTime.Now;
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
        public string Phone2 { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public long? UdaId { get; set; }
        public string RegisterNumber { get; set; }
        public ICollection<DeviceReference> DeviceReferences { get; set; } 

        public TemporaryData LastTempData { get; set; }

        public ICollection<EmergencyContact> EmergencyContactList { get; set; } 
        public ICollection<MedicalRecord> Background { get; set; } 
        public ICollection<Report> ReportList  { get; set; }
    }
}
