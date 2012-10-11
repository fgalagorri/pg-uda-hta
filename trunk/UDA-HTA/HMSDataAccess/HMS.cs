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
            return GetReportsByPatientId(3);
        }

        public ICollection<Report> GetReportById()
        {
            return null;
        }

        // Devuelve una lista con los identificadores de los reportes del paciente
        private ICollection<Report> GetReportsByPatientId(int patientId)
        {
            string patIdStr = patientId.ToString();
            //Obtengo una lista de identificadores de reportes para el paciente patientId
            string sql = "SELECT NIBPMAD FROM MEASUREMENTSBP INNER JOIN AUFZEICHNUNG ON AUFZEICHNUNG.ID = MEASUREMENTSBP.AUFZEICHNUNG_ID WHERE AUFZEICHNUNG.PATIENT_ID=" + patIdStr;
            ResultSet rsAuf = stat.executeQuery( sql );
            ICollection<Report> reportList = new List<Report>();
            Report report = new Report();

            int bpMed;
            string bpMedStr;
            while (rsAuf.next())
            {
                bpMed = rsAuf.getInt(1);
                bpMedStr = rsAuf.getString(1);
                report.setIdent(bpMed);
                Console.WriteLine(bpMedStr);

                //Agrego el reporte a la lista
                reportList.Add(report);
            }
            return reportList;
        }

    }
}
