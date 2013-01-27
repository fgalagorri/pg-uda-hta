using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceBussinessLogic
{
    interface IReportManagement
    {
        // ABM medicamentos
        void addDrug(string type, string name);

        void deleteDrug(string name);

        void editDrug(string type, string name);


        ICollection<Entities.Report> listPatientReports(int idPatient);

        void printReport(int idReport);

        void exportReportPDF(int idReport);

    }
}
