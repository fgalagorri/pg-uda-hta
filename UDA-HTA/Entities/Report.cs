﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Report
    {
        
        private int _ident;
        private ICollection<Measurement> measureList;

        public Report()
        {
            measureList = new List<Measurement>();
        }

        public int Ident
        {
            get { return _ident; }
            set { _ident = value; }
        }

        public void addToMeasureList(Measurement measure)
        {
            measureList.Add(measure);
        }

        public ICollection<Measurement> getMeasureList()
        {
            return measureList;
        }

    }   //end report
}
