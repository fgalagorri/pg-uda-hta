using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Patient;

namespace InterfaceBussinessLogic
{
    interface IPatientManagement
    {
        bool createPatient(string name, string surname, string address, string dni, DateTime birth, Entities.Patient.sexType sex, string neighbour, string city, string tel, string cell, string e_mail);

        ICollection<Entities.Patient> listPatients();

        bool editPatient(string name, string surname, string address, string dni, DateTime birth, Entities.Patient.sexType sex, string neighbour, string city, string tel, string cell, string e_mail);
    }
}
