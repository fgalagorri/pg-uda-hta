using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public class Patient
    {
    }

    public class Report
    {
        private int ident;
        private ICollection<int> bpMedList;

        public Report()
        {
            bpMedList = new List<int>();
        }

        public void setIdent(int id)
        {
            ident = id;
        }

        public void addToBpMedList(int bpMed)
        {
            bpMedList.Add(bpMed);        
        }

        public ICollection<int> getBpMedList()
        {
            return bpMedList;
        }



    }
}
