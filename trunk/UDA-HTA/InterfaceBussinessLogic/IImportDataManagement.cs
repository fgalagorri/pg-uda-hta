using System.Collections.Generic;
using Entities;
using Entities.Tools;

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
        ToolsReport ImportReport(string idReport, int device);
    }
}
