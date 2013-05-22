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
            var p = new Patient();
            p.Address = patient.Address;
            p.BirthDate = patient.BirthDate;
            p.CellPhone = patient.CellPhone;
            p.City = patient.City;
            p.DocumentId = patient.DocumentId;
            p.EMail = patient.EMail;
            p.IdInDevice = patient.IdInDevice;
            p.Name = patient.Name;
            p.Surname = patient.Surname;
            p.Neighbour = patient.Neighbour;
            p.Phone = patient.Phone;
            p.Sex = patient.Sex;

            int id;
            try
            {
                var pda = new PatientDataAccess();
                if (!pda.ExistPatientReference(p.IdInDevice))
                {
                    id = pda.InsertPatient(p);
                }
                else
                {
                    id = pda.GetPatientId(p.IdInDevice);
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
