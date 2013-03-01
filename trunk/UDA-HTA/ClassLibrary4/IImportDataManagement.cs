using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace InterfaceBussinessLogic
{
    interface IImportDataManagement
    {

        /* Lista los pacientes con los respectivos estudios que no han sido cargados en la aplicacion.
         * Solo se devuelve la informacion necesario para poder identificar al reporte.
        */
        ICollection<Entities.Report.PatientReport> listNewPatientReports();

        /*
         * Importa todos los datos de un estudio
         */
        Report importData(int idReport, int device);
    }
}
