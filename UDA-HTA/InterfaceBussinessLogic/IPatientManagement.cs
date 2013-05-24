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

        void CreatePatient(int idDev, string name, string surname, string address, string dni, DateTime birth,
                           SexType sex, string neighbour, string city, string tel, string cell, string email);

        /*
         * Lista todos los pacientes de la base
         */
        ICollection<Patient> ListPatients();

        /*
         * Modifica la informacion actual del paciente
         */

        bool EditPatient(string name, string surname, string address, string dni, DateTime birth, SexType sex,
                         string neighbour, string city, string tel, string cell, string email);
    }
}
