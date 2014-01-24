using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var currentBack = uda.GetMedicalHistory(patient.UdaId.Value);

            //insertar medicalRecord nuevos
            foreach (var medicalRecord in patient.Background.Where(b => b.Id == null))
                uda.InsertMedicalHistory(patient.UdaId.Value, medicalRecord);

            var editedIds = patient.Background.Where(b => b.Id.HasValue).Select(b => b.Id.Value).ToList();

            //borrar los que no están más 
            foreach (var mr in currentBack.Where(b => b.Id.HasValue && !editedIds.Contains(b.Id.Value)))
                uda.DeleteMedicalHistory(patient.UdaId.Value, mr.Id.Value);

            
            foreach (var medication in patient.LastTempData.Medication)
            {
                MedicineDose md = new MedicineDose
                    {
                        Dose = medication.Dose,
                        Drug = new Drug
                            {
                                Category = medication.Drug.Category,
                                Active = medication.Drug.Active,
                                Name = medication.Drug.Name,
                                Id = medication.Drug.Id
                            },
                        Time = medication.Time,
                        Id = medication.Id
                        
                    };

                uda.InsertMedicineDose(md, patient.LastTempData.IdTemporaryData);                    
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
