using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using Entities;
using DataAccess;

namespace BussinessLogic
{
    public class PatientManagement
    {
        public long CreatePatient(Patient patient)
        {
            long id;

            var pda = new PatientDataAccess();
            id = pda.InsertPatient(patient);

            if (id != 0)
            {
                pda.InsertEmergencyContact(id, patient.EmergencyContactList);

                var uda = new UdaHtaDataAccess();
                uda.InsertPatientUda(id);

                //Insertar Medical History
                foreach (var mh in patient.Background)
                    uda.InsertMedicalHistory(id, mh);
            }
            else
            {
                Exception e = new InvalidDataException();
                throw e;
            }

            return id;
        }


        public void EditPatient(Patient patient)
        {
            // Para el medical history 
            // hacer update de los que tienen id e 
            // insertar los que no tienen id
            PatientDataAccess pda = new PatientDataAccess();
            pda.EditPatient(patient);

            if (patient.UdaId != null) pda.InsertEmergencyContact((long) patient.UdaId,patient.EmergencyContactList);

            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            foreach (var medicalRecord in patient.Background)
            {
                if (medicalRecord.Id != null)
                {
                    //actualizar medicalRecord
                    uda.EditMedicalHistory(medicalRecord, patient.UdaId.Value);
                }
                else
                {
                    //insertar medicalRecord
                    uda.InsertMedicalHistory(patient.UdaId.Value, medicalRecord);
                }
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
            var patient = pda.GetPatient(patientId);
            var uda = new UdaHtaDataAccess();
            patient.Background = uda.GetMedicalHistory(patientId);
            return patient;
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
