using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeviceDataAccess;
using Entities;
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
            stat = conn.createStatement(ResultSet.__Fields.TYPE_SCROLL_INSENSITIVE, ResultSet.__Fields.CONCUR_READ_ONLY);
            ResultSet rs = stat.executeQuery("SELECT 'Hello World'");
            while (rs.next())
            {
                Console.WriteLine(rs.getString(1));
            }

        }

        public void closeConnectionDataBase()
        {
            conn.close();
            stat.close();
        }

        public Report getReport(int idReport)
        {
            Report report = new Report();
            // TODO: Obtener el reporte con identificador idReport

            return report;
        }


        public ICollection<Patient> ListPatients()
        {
            ResultSet rs = stat.executeQuery("SELECT ID, BIRTHDATE,PATIENTID FROM PATIENT");
            ICollection<Patient> patientList = new List<Patient>();

            string timeStr;
            DateTime time;
            int id;

            while (rs.next())
            {
                Patient patient = new Patient();

                id = rs.getInt(1);
                patient.IdHms = id;

                timeStr = rs.getString(2);
                //Pareseo la fecha y hora para crear el DateTime
                time = parseDateTime(timeStr);
                patient.BirthDate = time;
                Console.Write(time.ToString());

                patient.DocumentId = rs.getString(3);
                Console.WriteLine(rs.getString(3));

                //Agrego el paciente a la lista
                patientList.Add(patient);
            }

            return patientList;
        }

        public ICollection<Report> ListAllReports()
        {
            ResultSet rs = stat.executeQuery("SELECT ID FROM PATIENT");
            ICollection<Report> result = new List<Report>();
            while (rs.next())
            {
                result.Concat( GetReportsByPatientId(rs.getInt(1)) );
            }
            return result;
        }


        // Devuelve una lista de los reportes del paciente 'patientId'
        public ICollection<Report> GetReportsByPatientId(int patientId)
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
            int lastId;
            if (rs.first())
            {
                rs.first();
                lastId = Convert.ToInt32(rs.getString(1));
            }
            else
            {
                //No tiene reportes asignados
                return null;
            }

            while (rs.next())
            {
                Measurement m = new Measurement();

                i = 0;

                id = rs.getInt(++i);
                Console.Write(id.ToString() + "|");

                timeStr = rs.getString(++i);
                //Pareseo la fecha y hora para crear el DateTime
                DateTime time = parseDateTime(timeStr);
                m.Time = time;
                Console.Write(time.ToString());

                m.Systolic = rs.getInt(++i);
                Console.Write(rs.getInt(i) + "|");

                m.Average = rs.getInt(++i);
                Console.Write(rs.getInt(i) + "|");

                m.Diastolic = rs.getInt(++i);
                Console.Write(rs.getInt(i) + "|");

                m.HeartRate = rs.getInt(++i);
                Console.WriteLine(rs.getInt(i) + "|");

                //Si id es igual al ultimo id, agrego las medidas al reporte
                if (id == lastId)
                {
                    //Agrego la medida al reporte
                    report.addToMeasureList(m);
                }
                else
                { //Si el ultimo id (lastId) es diferente al actual, entonces comenzaron las medidas de otro reporte

                    //Agrego el reporte del ultimo identificador a la lista
                    report.Ident = lastId;
                    reportList.Add(report);
                    
                    //Actualizo el ultimo id
                    lastId = id;
                    
                    //Creo el siguiente reporte y agrego la ultima medida procesada
                    report = new Report();
                    report.addToMeasureList(m);

                }
            }
            report.Ident = lastId;
            reportList.Add(report);
            return reportList;
        }

    }
}
