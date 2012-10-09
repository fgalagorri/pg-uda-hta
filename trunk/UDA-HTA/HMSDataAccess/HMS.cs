using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using java.sql;

namespace HMSDataAccess
{
    public class HMS : IDeviceDataAccess
    {
        private Statement stat;
        private Connection conn;

        public HMS()
        { 
        }

        public void connectToDataBase()
        {
            org.h2.Driver.load();
            conn = DriverManager.getConnection("jdbc:h2:~/HMS Client-Server_DB/database", "sa", "");
            stat = conn.createStatement();
            ResultSet rs = stat.executeQuery("SELECT 'Hello World'");
            while (rs.next())
            {
                Console.WriteLine(rs.getString(1));
            }

        }

        public void closeConnectionDataBase()
        {
            conn.close();
        }


        public ICollection<Patient> ListPatients()
        {
            ResultSet rs = stat.executeQuery("SELECT PATIENTID FROM PATIENT");
            ICollection<string> patientIDList = new List<string>();
            while (rs.next())
            {
                patientIDList.Add(rs.getString(1));
                Console.WriteLine(rs.getString(1));
            }

            return null;
        }

        public ICollection<Report> ListReports()
        {
            return null;
        }

        public ICollection<Report> GetReportById()
        {
            return null;
        }

    }
}
