using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace InterfaceBussinessLogic
{
    public interface IReportManagement
    {
        // ABM medicamentos
        void addDrug(int type, string name);

        void deleteDrug(string name);

        void editDrug(string type, string name);


        ICollection<Entities.Report> listPatientReports(int idPatient);

        void printReport(int idReport);

        void exportReportPDF(Report report, string fileName);

        //Agrega el reporte report al paciente con identificador idPatient en la base udaHta
        void addReport(Report report, string idPatient, DailyCarnet dailyCarnet, TemporaryData temporaryData);

    }
}
