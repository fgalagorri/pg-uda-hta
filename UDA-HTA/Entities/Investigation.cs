﻿using System;
using System.Collections.Generic;

namespace Entities
{
    public class Investigation
    {
        public long IdInvestigation { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Comment { get; set; }

        public ICollection<Report> LReports { get; set; } 

        public Investigation()
        {
            LReports = new List<Report>();
        }

        public Investigation(long id, string name, DateTime date, string comment)
        {
            IdInvestigation = id;
            Name = name;
            CreationDate = date;
            Comment = comment;
            LReports = new List<Report>();
        }

    }
}
