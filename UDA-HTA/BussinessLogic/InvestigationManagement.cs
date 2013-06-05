using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceBussinessLogic;
using Entities;
using DataAccess;

namespace BussinessLogic
{
    public class InvestigationManagement : IInvestigationManagement
    {
        public ICollection<Investigation> listInvestigations()
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.listInvestigations();
        }

        public void exportInvestigation(int idInvestigation)
        {
            
        }

        public int createInvestigation(string name, DateTime creationDate)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.insertInvestigation(name,creationDate);
        }

        public void editInvestigation(string name, DateTime creationDate)
        {
            
        }

        public void addReportToInvestigation(Report report, int idInvestigation)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.addReportToInvestigation((long)report.UdaId,(long)report.Patient.UdaId,idInvestigation);
        }

        public void deleteReportFromInvestigation(Report report, int idInvestigation)
        {
            
        }

    }
}
