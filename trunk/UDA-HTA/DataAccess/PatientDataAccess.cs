using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using MySql.Data.MySqlClient;
using Entities;

namespace DataAccess
{
    public class PatientDataAccess
    {
        private const string ConnectionString = "SERVER=localhost;DATABASE=patient_info_db;UID=root;PASSWORD=rootudahta;";
        private readonly MySqlConnection _conn;

        public PatientDataAccess()
        {
            _conn = new MySqlConnection(ConnectionString);
        }

        public void CloseConnectionDataBase()
        {
            _conn.Close();
        }

        public long InsertPatient(Patient p)
        {
            var udaContext = new patient_info_dbEntities();

            ObjectParameter lastIdPatient = new ObjectParameter("id", typeof(int));
            ObjectParameter lastIdDevRef = new ObjectParameter("id", typeof(int));
            
            try
            {
                var sex = SexType.M.ToString();
                if ( p.Sex != null &&
                    ((p.Sex.Value == SexType.F) || 
                    (p.Sex.Value == SexType.M)) )
                {
                    sex = p.Sex.Value.ToString();
                }
                udaContext.insertPatient(lastIdPatient, p.Names, p.Surnames, p.Address, p.DocumentId,
                                         p.BirthDate, sex, p.Neighbour, p.City, p.Phone, p.CellPhone, p.Email, p.RegisterNumer);
                foreach (var devRef in p.DeviceReferences)
                {
                    udaContext.insertDeviceReference(lastIdDevRef, devRef.deviceType, devRef.deviceReferenceId, (long)lastIdPatient.Value);
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw (e);
            }
            return (long) lastIdPatient.Value;
        }

        public void insertEmergencyContact(Patient patient)
        {
            var patientContext = new patient_info_dbEntities();
            ObjectParameter lastIdEmergencyContact = new ObjectParameter("id", typeof(long));

            foreach (var ec in patient.EmergencyContactList)
            {
                patientContext.insertEmergencyContact(lastIdEmergencyContact,ec.Name,ec.Surname,ec.Phone,Convert.ToInt32(patient.UdaId));
            }
        }

        /*
         * Lista todos los pacientes existentes en la base.
         */

        public ICollection<PatientSearch> ListPatients(string documentId, string names, string surnames,
                                                       DateTime? birthDate, long? registerNo)
        {

            using (var patientContext = new patient_info_dbEntities())
            {
                var list = patientContext.patient.AsQueryable();

                if (!string.IsNullOrWhiteSpace(documentId))
                    list = list.Where(p => p.document == documentId);
                if (!string.IsNullOrWhiteSpace(names))
                    list = list.Where(p => p.name == names);
                if (!string.IsNullOrWhiteSpace(surnames))
                    list = list.Where(p => p.surname == surnames);
                if (birthDate.HasValue)
                    list = list.Where(p => p.birthday.Value.Date == birthDate.Value.Date);
                if (registerNo.HasValue)
                    list = list.Where(p => p.register_number == registerNo.Value);

                return list.Select(p => new PatientSearch
                    {
                        UdaId = p.idPatient,
                        DocumentId = p.document,
                        Names = p.name,
                        Surnames = p.surname,
                        BirthDate = p.birthday,
                        RegisterNumer = p.register_number
                    }).ToList();
            }
        }

        public Patient GetPatient(long patientId)
        {
            var patientContext = new patient_info_dbEntities();
            var pat = patientContext.patient.Where(p => p.idPatient == patientId).
                                            Select(p => new 
                                            {
                                                p.idPatient, 
                                                p.address,
                                                p.birthday,
                                                p.cell_phone,
                                                p.city,
                                                p.document,
                                                p.e_mail,
                                                p.emergency_contact,
                                                p.gender,
                                                p.name,
                                                p.neighborhood,
                                                p.surname,
                                                p.telephone
                                            }).ToList().First();
            Patient patient = new Patient();
            patient.UdaId = pat.idPatient;
            patient.Address = pat.address;
            patient.BirthDate = pat.birthday;
            patient.CellPhone = pat.cell_phone;
            patient.City = pat.city;
            patient.DocumentId = pat.document;
            patient.Email = pat.e_mail;
            patient.Names = pat.name;
            patient.Neighbour = pat.neighborhood;
            patient.Phone = pat.telephone;
            patient.Sex = pat.gender == "M" ? SexType.M : SexType.F;
            patient.Surnames = pat.surname;

            foreach (var ec in pat.emergency_contact)
            {
                EmergencyContact emergencyContact = new EmergencyContact();
                emergencyContact.EmergencyContactId = ec.idemergency_contact;
                emergencyContact.Name = ec.name;
                emergencyContact.Phone = ec.phone;
                emergencyContact.Surname = ec.surname;
                patient.EmergencyContactList.Add(emergencyContact);
            }

            return patient;
        }

        public bool ExistPatientReference(string patientRef, int devType)
        {
            var patientContext = new patient_info_dbEntities();
            return patientContext.device_reference.Any(p => p.device_ref == patientRef && p.device_type == devType);
        }

        public long? GetPatientId(string patientRef, int dev)
        {
            var patientContext = new patient_info_dbEntities();
            var pat = patientContext.device_reference.Where(p => (p.device_ref == patientRef && p.device_type == dev))
                                    .Select(p => new {p.patient_idPatient})
                                    .FirstOrDefault();

            return (pat != null) ? (long?) pat.patient_idPatient : null;
        }
    }
}
