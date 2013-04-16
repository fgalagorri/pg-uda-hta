using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace InterfaceBussinessLogic
{
    public interface IPatientManagement
    {
        /*
         * Crea un paciente que no existe en la base con los datos ingresados como parametros.
         */
        void createPatient(int idDev, string name, string surname, string address, string dni, DateTime birth, Patient.sexType sex, string neighbour, string city, string tel, string cell, string e_mail);

        /*
         * Lista todos los pacientes de la base
         */
        ICollection<Patient> listPatients();

        /*
         * Modifica la informacion actual del paciente
         */
        bool editPatient(string name, string surname, string address, string dni, DateTime birth, Patient.sexType sex, string neighbour, string city, string tel, string cell, string e_mail);
    }
}
