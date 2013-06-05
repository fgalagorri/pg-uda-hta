using System;
using System.Collections.Generic;
using Entities;
using DataAccess;
using InterfaceBussinessLogic;

namespace BussinessLogic
{
    public class PatientManagement : IPatientManagement
    {
        public long createPatient(Patient patient)
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

        
        public ICollection<Patient> listPatients()
        {
            var pda = new PatientDataAccess();
            var lp = pda.ListPatients();
         
            return lp;
        }

        public Patient getPatientData(long patientId)
        {
            var pda = new PatientDataAccess();
            return pda.getPatientData(patientId);
        }

        public long? getPatientIdIfExist(string patientRefId)
        {
            var pda = new PatientDataAccess();
            return pda.GetPatientId(patientRefId);
        }

        public bool editPatient(Patient patient)
        {
            return false;
        }

    }
}
