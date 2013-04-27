using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceBussinessLogic;
using Entities;
using DataAccess;

namespace BussinessLogic
{
    public class ReportManagement : IReportManagement
    {

        public void addDrug(string type, string name)
        {
        }

        public void deleteDrug(string name)
        {
        }

        public void editDrug(string type, string name)
        {
        }


        public ICollection<Entities.Report> listPatientReports(int idPatient)
        {
            return null;
        }

        public void printReport(int idReport)
        {
        }

        public void exportReportPDF(int idReport)
        {
        }

        public void addReport(Report report, int idPatient)
        {
            UdaHtaDataAccess uhda = new UdaHtaDataAccess();
            uhda.insertReport(idPatient, report);
        }
    }
}
