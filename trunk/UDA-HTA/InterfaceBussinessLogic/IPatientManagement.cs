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
         * Obtiene los datos del paciente con identificador 'patientId'
         */
        Patient getPatientData(string patientId);

        /*
         * Devuelve true si existe el paciente con referencia a Spacelab o HMS 'patientRefId'
         */
        bool existPatientReference(string patientRefId);

        /*
         * Modifica la informacion actual del paciente
         */

        bool EditPatient(string name, string surname, string address, string dni, DateTime birth, SexType sex,
                         string neighbour, string city, string tel, string cell, string email);
    }
}
