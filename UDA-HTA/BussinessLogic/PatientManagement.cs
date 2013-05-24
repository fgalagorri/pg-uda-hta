using System;
using System.Collections.Generic;
using Entities;
using DataAccess;

namespace BussinessLogic
{
    public class PatientManagement
    {
        public int CreatePatient(Patient patient)
        {
            var p = new Patient
                {
                    Address = patient.Address,
                    BirthDate = patient.BirthDate,
                    CellPhone = patient.CellPhone,
                    City = patient.City,
                    DocumentId = patient.DocumentId,
                    Email = patient.Email,
                    DevicePatientId = patient.DevicePatientId,
                    Names = patient.Names,
                    Surnames = patient.Surnames,
                    Neighbour = patient.Neighbour,
                    Phone = patient.Phone,
                    Sex = patient.Sex
                };

            int id;
            try
            {
                var pda = new PatientDataAccess();
                if (!pda.ExistPatientReference(p.DevicePatientId))
                {
                    id = pda.InsertPatient(p);
                }
                else
                {
                    id = pda.GetPatientId(p.DevicePatientId);
                }
                pda.CloseConnectionDataBase();

                var uda = new UdaHtaDataAccess();
                if (!uda.ExistPatient(id))
                {
                    uda.insertPatientUda(id);    
                }
                uda.CloseConnectionDataBase();

                return id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
        ICollection<Patient> ListPatients()
        {
            var pda = new PatientDataAccess();
            var lp = pda.ListPatients();
         
            return lp;
        }



    }
}
