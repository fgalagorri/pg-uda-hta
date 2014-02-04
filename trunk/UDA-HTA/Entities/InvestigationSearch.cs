using System;

namespace Entities
{
    public class InvestigationSearch
    {
        public long IdInvestigation { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Comment { get; set; }

        public InvestigationSearch()
        {
        }

        public InvestigationSearch(long id, string name, DateTime date, string comment)
        {
            IdInvestigation = id;
            Name = name;
            CreationDate = date;
            Comment = comment;
        }

    }
}
