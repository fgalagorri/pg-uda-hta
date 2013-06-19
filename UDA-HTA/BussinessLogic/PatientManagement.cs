using System;
using System.Collections.Generic;
using Entities;
using DataAccess;

namespace BussinessLogic
{
    public class PatientManagement
    {
        public long CreatePatient(Patient patient)
        {
            long id;
            try
            {
                var pda = new PatientDataAccess();
                id = pda.InsertPatient(patient);
                pda.CloseConnectionDataBase();

                var uda = new UdaHtaDataAccess();
                uda.insertPatientUda(id);
                //Insertar Medical History
                foreach (var mh in patient.Background)
                {
                    uda.insertMedicalHistory(id, mh);
                }
                
                uda.CloseConnectionDataBase();    
                return id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public ICollection<PatientSearch> ListPatients(string documentId, string names, string surnames,
                                                       DateTime? birthDate, long? registerNo)
        {
            var pda = new PatientDataAccess();
            var lp = pda.ListPatients(documentId, names, surnames, birthDate, registerNo);

            return lp;
        }

        public Patient GetPatientData(long patientId)
        {
            var pda = new PatientDataAccess();
            return pda.getPatientData(patientId);
        }

        public long? GetPatientIdIfExist(string patientRefId, int dev)
        {
            var pda = new PatientDataAccess();
            return pda.GetPatientId(patientRefId,dev);
        }

        public bool EditPatient(Patient patient)
        {
            return false;
        }

    }
}
