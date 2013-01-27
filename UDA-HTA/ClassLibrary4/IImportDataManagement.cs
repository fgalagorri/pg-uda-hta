using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Patient;

namespace InterfaceBussinessLogic
{
    interface IImportDataManagement
    {
        ICollection<Entities.Report> listNewPatientReports();

        bool importData(int idReport);
    }
}
