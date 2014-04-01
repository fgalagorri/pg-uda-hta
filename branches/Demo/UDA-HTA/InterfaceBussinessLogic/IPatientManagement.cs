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

        long createPatient(Patient patient);

        /*
         * Lista todos los pacientes de la base
         */
        ICollection<Patient> listPatients();

        /*
         * Obtiene los datos del paciente con identificador 'patientId'
         */
        Patient getPatientData(long patientId);

        /*
         * Obtiene el identificador del paciente con referencia a su base original 'patientRefId', en caso de que exista
         * si no existe, devuelve null
         */
        long? getPatientIdIfExist(string patientRefId);

        /*
         * Modifica la informacion actual del paciente
         */

        bool editPatient(Patient patient);
    }
}
