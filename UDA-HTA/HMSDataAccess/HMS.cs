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

        private DateTime parseDateTime(string timeStr)
        {
            char[] timeChar = timeStr.ToCharArray();
            
            string year = "";
            for (int i = 0; i < 4; i++)
            {
                year = year + timeChar[i];
            }
            int y = Convert.ToInt32(year);

            string month = "" + timeChar[5] + timeChar[6];
            int m = Convert.ToInt32(month);

            string day = "" + timeChar[8] + timeChar[9];
            int d = Convert.ToInt32(day);

            string hour = "" + timeStr[11] + timeChar[12];
            int h = Convert.ToInt32(hour);

            string minute = "" + timeStr[14] + timeChar[15];
            int min = Convert.ToInt32(minute);

            string second = "" + timeChar[17] + timeChar[18];
            int sec = Convert.ToInt32(second);

            return new DateTime(y, m, d, h, min, sec);
                        
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
            return GetReportsByPatientId(5);
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
            string columns = " AUFZEICHNUNG.ID, TIMEOFMEASUREMENT, NIBPSYS, NIBPMAD, NIBPDIAS, HR ";
            string cond = " AUFZEICHNUNG.PATIENT_ID=" + patIdStr;
            string sql = "SELECT" + columns + "FROM MEASUREMENTSBP INNER JOIN AUFZEICHNUNG ON AUFZEICHNUNG.ID = MEASUREMENTSBP.AUFZEICHNUNG_ID WHERE" + cond;
            ResultSet rs = stat.executeQuery( sql );
            ICollection<Report> reportList = new List<Report>();
            Report report = new Report();

            int i;
            int id;
            string timeStr;
            int lastId = Convert.ToInt32(rs.getString(1));

            while (rs.next())
            {
                Measurement m = new Measurement();

                i = 0;

                id = rs.getInt(++i);
                Console.Write(id.ToString() + "|");

                timeStr = rs.getString(++i);
                //Pareseo la fecha y hora para crear el DateTime
                DateTime time = parseDateTime(timeStr);
                m.setTime(time);
                Console.Write(time.ToString());

                m.setSystolic(rs.getInt(++i));
                Console.Write(rs.getInt(i) + "|");

                m.setAverage(rs.getInt(++i));
                Console.Write(rs.getInt(i) + "|");

                m.setDiastolic(rs.getInt(++i));
                Console.Write(rs.getInt(i) + "|");

                m.setHeartRate(rs.getInt(++i));
                Console.WriteLine(rs.getInt(i) + "|");

                //Si id es igual al ultimo id, agrego las medidas al reporte
                if (id == lastId)
                {
                    //Agrego la medida al reporte
                    report.addToMeasureList(m);
                }
                else
                { //Si el ultimo id es diferente al actual, entonces comenzaron las medidas de otro reporte

                    //Agrego el reporte del ultimo identificador a la lista
                    report.setIdent(id);
                    reportList.Add(report);

                    //Creo el siguiente reporte
                    report = new Report();
                }
            }
            return reportList;
        }

    }
}
