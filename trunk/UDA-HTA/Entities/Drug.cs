﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Drug
    {
        public int? Id { get; set; }
        public string Category { get; set; }
        public string Active { get; set; }
        public string Name { get; set; }

        public Drug(string category, string active, string name)
        {
            Category = category;
            Active = active;
            Name = name;
        }
    }
}