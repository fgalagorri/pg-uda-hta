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

        public int InsertPatient(Patient p)
        {
            var udaContext = new patient_info_dbEntities();

            ObjectParameter lastIdPatient = new ObjectParameter("id", typeof(int));
            try
            {
                udaContext.insertPatient(lastIdPatient, Convert.ToInt32(p.IdInDevice), p.Name, p.Surname, p.Address, p.DocumentId,
                                         p.BirthDate, p.Sex.ToString(),
                                         p.Neighbour, p.City, p.Phone, p.CellPhone,
                                         p.EMail);

            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw (e);
            }
            return (int)lastIdPatient.Value;
        }

        /*
         * Lista todos los pacientes existentes en la base.
         */
        public ICollection<Patient> ListPatients()
        {
            
            var patientContext = new patient_info_dbEntities();
            ICollection<Patient> patientQuery = patientContext.patient.Select(p=> new Entities.Patient
                {
                    Name = p.name,
                    City = p.city
                }).ToList();

            return patientQuery;


            /* 
            ICollection<Patient> lp = new List<Patient>();

            string stm = "SELECT * FROM patient";
            MySqlCommand mc = new MySqlCommand(stm, _conn);

            _conn.Open();
            MySqlDataReader rdr = mc.ExecuteReader();

            while (rdr.Read())
            {
                Patient p = new Patient();
                p.Name = rdr.GetString(2);
                p.Surname = rdr.GetString(3);
                p.DocumentId = rdr.GetString(4);
                lp.Add(p);
            }

            rdr.Close();
            _conn.Close();
            
            return lp;
            */
        }

        public bool ExistPatientReference(string patientReference)
        {
            var patientContext = new patient_info_dbEntities();
            return patientContext.patient.Any(p => p.patientReference == Convert.ToInt32(patientReference));
        }

        public int GetPatientId(string patientReference)
        {
            var patientContext = new patient_info_dbEntities();
            var pat = patientContext.patient.Where(p => p.patientReference == Convert.ToInt32(patientReference)).Select(p => new { p.idPatient }).ToList().First();
            return pat.idPatient;
        }
    }
}
