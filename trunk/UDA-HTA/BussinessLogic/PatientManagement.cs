using System.Collections.Generic;
using Entities;
using DataAccess;

namespace BussinessLogic
{
    public class PatientManagement
    {
        public void CreatePatient(Patient patient)
        {
            var p = new Patient();
            p.Address = p.Address;
            p.BirthDate = p.BirthDate;
            p.CellPhone = p.CellPhone;
            p.City = p.City;
            p.DocumentId = p.DocumentId;
            p.EMail = p.EMail;
            p.IdInDevice = p.IdInDevice;
            p.Name = p.Name;
            p.Surname = p.Surname;
            p.Neighbour = p.Neighbour;
            p.Phone = p.Phone;
            p.Sex = p.Sex;

            var pda = new PatientDataAccess();
            pda.InsertPatient(p);
        }

        
        ICollection<Patient> ListPatients()
        {
            var pda = new PatientDataAccess();
            var lp = pda.ListPatients();
         
            return lp;
        }



    }
}
