using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HMSDataAccess;
using Interfaces;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            HMS hms = new HMS();
            hms.connectHMSDataBase();
            ICollection<Patient> patientList = hms.ListPatients();
            hms.closeConnectionHMSDataBase();
            

        }
    }
}
