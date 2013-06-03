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

        public int? InsertPatient(Patient p)
        {
            var udaContext = new patient_info_dbEntities();

            ObjectParameter lastIdPatient = new ObjectParameter("id", typeof(int));
            try
            {
                Int32 devPatId = Convert.ToInt32(p.DevicePatientId);
                var sex = SexType.M.ToString();
                if ( p.Sex != null &&
                    ((p.Sex.Value == SexType.F) || 
                    (p.Sex.Value == SexType.M)) )
                {
                    sex = p.Sex.Value.ToString();
                }
                udaContext.insertPatient(lastIdPatient, devPatId, p.Names, p.Surnames, p.Address, p.DocumentId,
                                         p.BirthDate, sex, p.Neighbour, p.City, p.Phone, p.CellPhone, p.Email, p.RegisterNumer);

            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw (e);
            }
            return (int)lastIdPatient.Value;
        }

        public void insertEmergencyContact()
        {
            
        }

        /*
         * Lista todos los pacientes existentes en la base.
         */
        public ICollection<Patient> ListPatients()
        {
            
            var patientContext = new patient_info_dbEntities();
            ICollection<Patient> patientQuery = patientContext.patient.Select(p=> new Entities.Patient
                {
                    Names = p.name,
                    City = p.city
                }).ToList();

            return patientQuery;

        }

        public Patient getPatientData(string patientId)
        {
            var patientContext = new patient_info_dbEntities();
            Int32 patId = Convert.ToInt32(patientId);
            var pat =patientContext.patient.Where(p => p.idPatient == patId).Select(p => new {p.address,p.birthday,p.cell_phone,p.city,p.document,
                                                                                     p.e_mail,p.emergency_contact,p.gender,p.name,p.neighborhood,
                                                                                     p.patientReference,p.surname,p.telephone}).ToList().First();
            Patient patient = new Patient();
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
            Int32 patRef = Convert.ToInt32(patientReference);
            return patientContext.patient.Any(p => p.patientReference == patRef);
        }

        public long? GetPatientId(string patientReference)
        {
            var patientContext = new patient_info_dbEntities();
            Int32 patRef = Convert.ToInt32(patientReference);
            var pat = patientContext.patient.Where(p => p.patientReference == patRef).Select(p => new { p.idPatient }).ToList().FirstOrDefault();
            if (pat == null)
            {
                return null;
            }
            return pat.idPatient;
        }
    }
}
