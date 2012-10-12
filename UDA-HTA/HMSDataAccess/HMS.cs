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

        // Devuelve una lista de los reportes del paciente 'patientId'
        private ICollection<Report> GetReportsByPatientId(int patientId)
        {
            string patIdStr = patientId.ToString();
            //Obtengo una lista de presiones de reportes para el paciente patientId
            string columns = " TIMEOFMEASUREMENT, NIBPSYS, NIBPMAD, NIBPDIAS, HR ";
            string cond = " AUFZEICHNUNG.PATIENT_ID=" + patIdStr;
            string sql = "SELECT" + columns + "FROM MEASUREMENTSBP INNER JOIN AUFZEICHNUNG ON AUFZEICHNUNG.ID = MEASUREMENTSBP.AUFZEICHNUNG_ID WHERE" + cond;
            ResultSet rs = stat.executeQuery( sql );
            ICollection<Report> reportList = new List<Report>();
            Report report = new Report();

            while (rs.next())
            {
                Measurement m = new Measurement();
                m.setSystolic(rs.getInt(1));
                Console.Write(rs.getInt(1) + "|");

                m.setAverage(rs.getInt(2));
                Console.Write(rs.getInt(2) + "|");

                m.setDiastolic(rs.getInt(3));
                Console.Write(rs.getInt(3) + "|");

                m.setHeartRate(rs.getInt(4));
                Console.WriteLine(rs.getInt(4) + "|");

                report.addToMeasureList(m);

                //Agrego el reporte a la lista
                reportList.Add(report);
            }
            return reportList;
        }

    }
}
