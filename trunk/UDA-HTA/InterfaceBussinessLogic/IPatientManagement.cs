using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace InterfaceBussinessLogic
{
    public interface IPatientManagement
    {
        void createPatient(int idDev, string name, string surname, string address, string dni, DateTime birth, Entities.Patient.sexType sex, string neighbour, string city, string tel, string cell, string e_mail);

        ICollection<Entities.Patient> listPatients();

        bool editPatient(string name, string surname, string address, string dni, DateTime birth, Entities.Patient.sexType sex, string neighbour, string city, string tel, string cell, string e_mail);
    }
}
