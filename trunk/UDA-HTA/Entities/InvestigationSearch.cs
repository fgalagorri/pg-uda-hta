using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class InvestigationSearch
    {
        public int IdInvestigation { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Comment { get; set; }

        public InvestigationSearch()
        {
        }

        public InvestigationSearch(int id, string name, DateTime date, string comment)
        {
            IdInvestigation = id;
            Name = name;
            CreationDate = date;
            Comment = comment;
        }

    }
}
