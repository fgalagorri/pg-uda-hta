using System;
using System.Collections.Generic;
using System.Transactions;
using Entities;
using DataAccess;

namespace BussinessLogic
{
    public class PatientManagement
    {
        public long CreatePatient(Patient patient)
        {
            //using (TransactionScope transaction = new TransactionScope())
            {
                long id;

                var pda = new PatientDataAccess();
                id = pda.InsertPatient(patient);

                var uda = new UdaHtaDataAccess();
                uda.insertPatientUda(id);

                //Insertar Medical History
                foreach (var mh in patient.Background)
                    uda.insertMedicalHistory(id, mh);

                pda.InsertEmergencyContact(id, patient.EmergencyContactList);

                return id;
            }
        }


        public void EditPatient(Patient patient)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                // Para los background y medical history 
                // hacer update de los que tienen id e 
                // insertar los que no tienen id
            }
        }


        public ICollection<PatientSearch> ListPatients(string documentId, string names, string surnames,
                                                       DateTime? birthDate, string registerNo)
        {
            var pda = new PatientDataAccess();
            var lp = pda.ListPatients(documentId, names, surnames, birthDate, registerNo);

            return lp;
        }


        public Patient GetPatient(long patientId)
        {
            var pda = new PatientDataAccess();
            return pda.GetPatient(patientId);
        }


        public long? GetPatientIdIfExist(string patientRefId, int dev)
        {
            var pda = new PatientDataAccess();
            return pda.GetPatientId(patientRefId,dev);
        }


        public TemporaryData GetLastTempData(long patientId)
        {
            var uda = new UdaHtaDataAccess();
            return uda.GetLastTempData(patientId);
        }
    }
}
