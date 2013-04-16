using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DataAccess;

namespace BussinessLogic
{
    public class PatientManagement
    {
        public void createPatient(int idInDev, string name, string surname, string address, string dni, DateTime birth, Entities.Patient.sexType sex, string neighbour, string city, string tel, string cell, string e_mail)
        {
            Patient p = new Patient();
            p.Address = address;
            p.BirthDate = birth;
            p.CellPhone = cell;
            p.City = city;
            p.DocumentId = dni;
            p.EMail = e_mail;
            p.IdInDevice = idInDev;
            p.Name = name;
            p.Surname = surname;
            p.Neighbour = neighbour;
            p.Phone = tel;
            p.Sex = sex;

            PatientDataAccess pda = new PatientDataAccess();
            try
            {
                pda.insertPatient(p);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        
        ICollection<Patient> listPatients()
        {
            PatientDataAccess pda = new PatientDataAccess();
            ICollection<Patient> lp = pda.listPatients();
         
            return lp;
        }



    }
}
