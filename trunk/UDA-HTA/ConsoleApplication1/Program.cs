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
            IDeviceDataAccess iDataAccess = new HMS();
            iDataAccess.connectToDataBase();
            ICollection<Patient> patientList = iDataAccess.ListPatients();
            iDataAccess.closeConnectionDataBase();
            //HMS hms = new HMS();
            //hms.connect();
            //ICollection<Patient> patientList = hms.ListPatients();
            //hms.closeConnectionHMSDataBase();
            

        }
    }
}
