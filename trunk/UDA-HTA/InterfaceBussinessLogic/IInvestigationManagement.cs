using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace InterfaceBussinessLogic
{
    public interface IInvestigationManagement
    {
        ICollection<Entities.Investigation> listInvestigations();

        void exportInvestigation(int idInvestigation);

        int createInvestigation(string name, DateTime creationDate);

        void editInvestigation(string name, DateTime creationDate);

        void addReportToInvestigation(Report report, int idInvestigation);

        void deleteReportFromInvestigation(Report report, int idInvestigation);

    }
}
