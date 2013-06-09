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
            try
            {
                var sex = SexType.M.ToString();
                if ( p.Sex != null &&
                    ((p.Sex.Value == SexType.F) || 
                    (p.Sex.Value == SexType.M)) )
                {
                    sex = p.Sex.Value.ToString();
                }
                udaContext.insertPatient(lastIdPatient, p.DevicePatientId, p.Names, p.Surnames, p.Address, p.DocumentId,
                                         p.BirthDate, sex, p.Neighbour, p.City, p.Phone, p.CellPhone, p.Email, p.RegisterNumer);

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
        public ICollection<Patient> ListPatients()
        {
            
            var patientContext = new patient_info_dbEntities();
            ICollection<Patient> patientQuery = patientContext.patient.Select(p=> new Entities.Patient
                {
                    DocumentId = p.document,
                    Names = p.name,
                    Surnames = p.surname,
                    BirthDate = p.birthday,
                    City = p.city
                }).ToList();

            return patientQuery;

        }

        public Patient getPatientData(long patientId)
        {
            var patientContext = new patient_info_dbEntities();
            var pat = patientContext.patient.Where(p => p.idPatient == patientId).Select(p => new {p.idPatient, p.address,p.birthday,p.cell_phone,p.city,p.document,
                                                                                     p.e_mail,p.emergency_contact,p.gender,p.name,p.neighborhood,
                                                                                     p.patientReference,p.surname,p.telephone}).ToList().First();
            Patient patient = new Patient();
            patient.UdaId = pat.idPatient;
            patient.Address = pat.address;
            patient.BirthDate = pat.birthday;
            patient.CellPhone = pat.cell_phone;
            patient.City = pat.city;
            patient.DocumentId = pat.document;
            patient.DevicePatientId = pat.patientReference.ToString();
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

        public bool ExistPatientReference(string patientReference)
        {
            var patientContext = new patient_info_dbEntities();
            return patientContext.patient.Any(p => p.patientReference == patientReference);
        }

        public long? GetPatientId(string patientReference)
        {
            var patientContext = new patient_info_dbEntities();
            Int32 patRef = Convert.ToInt32(patientReference);
            var pat = patientContext.patient.Where(p => p.patientReference == patientReference).Select(p => new { p.idPatient }).ToList().FirstOrDefault();
            if (pat == null)
            {
                return null;
            }
            return pat.idPatient;
        }
    }
}
