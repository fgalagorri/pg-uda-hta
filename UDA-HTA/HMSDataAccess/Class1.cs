using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using java.sql;

namespace HMSDataAccess
{
    public class Class1 : IDeviceDataAccess
    {

        public void connectHMSDataBase()
        {
            org.h2.Driver.load();
            Connection conn = DriverManager.getConnection("jdbc:h2:~/HMS Client-Server_DB/database", "sa", "");
            Statement stat = conn.createStatement();
            ResultSet rs = stat.executeQuery("SELECT 'Hello World'");
            while (rs.next())
            {
                Console.WriteLine(rs.getString(1));
            }

        }

        public ICollection<Patient> ListPatients()
        {
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
