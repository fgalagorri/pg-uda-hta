using System.Collections.Generic;
using Entities;

namespace InterfaceBussinessLogic
{
    public interface IImportDataManagement
    {

        /* 
         * Lista los pacientes con los respectivos estudios que no han sido cargados en la aplicacion.
         * Solo se devuelve la informacion necesario para poder identificar al reporte.
        */
        ICollection<PatientReport> ListNewPatientReports();

        /* 
         * Importa los datos del paciente
         */
        Patient ImportPatient(string idPatient, int device);

        /*
         * Importa todos los datos de un estudio del dispositivo device con identificador idReport
         */
        Report ImportReport(string idReport, int device);

        /*
         * Importa todas las medidas de un estudio del dispositivo device con identificador idReport
         */
        List<Measurement> ImportMeasures(Report report);
    }
}
