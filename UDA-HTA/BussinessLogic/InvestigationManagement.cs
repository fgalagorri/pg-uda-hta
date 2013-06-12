using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DataAccess;

namespace BussinessLogic
{
    public class InvestigationManagement
    {
        public ICollection<Investigation> listInvestigations()
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.listInvestigations();
        }

        public void ExportInvestigation(int idInvestigation)
        {
            
        }

        public int CreateInvestigation(string name, DateTime creationDate)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.insertInvestigation(name,creationDate);
        }

        public void EditInvestigation(string name, DateTime creationDate)
        {
            
        }

        public void AddReportToInvestigation(Report report, int idInvestigation)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.addReportToInvestigation((long)report.UdaId,(long)report.Patient.UdaId,idInvestigation);
        }

        public void DeleteReportFromInvestigation(Report report, int idInvestigation)
        {
            
        }

    }
}
