using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DeviceDataAccess;
using Entities;
using java.sql;

namespace HMSDataAccess
{
    public class HMS : IDeviceDataAccess
    {
        private const int DeviceId = 0;
        private Statement _stat;
        private Connection _conn;

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
        
        public void ConnectToDataBase()
        {
            try
            {
                org.h2.Driver.load();
                _conn = DriverManager.getConnection("jdbc:h2:~/HMS Client-Server_DB/database","sa","");
                //_conn = DriverManager.getConnection(ConfigurationManager.ConnectionStrings["Hms"].ConnectionString);
                _stat = _conn.createStatement(ResultSet.__Fields.TYPE_SCROLL_INSENSITIVE, ResultSet.__Fields.CONCUR_READ_ONLY);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                throw;
            }
        }

        public void CloseConnectionDataBase()
        {
            _conn.close();
            _stat.close();
        }

        // Obtiene el reporte con identificador idReport, el identificador hace referencia al ID de la tabla AUFZEICHNUNG
        public Report GetReport(string idReport)
        {
            var report = new Report();
            var columns = "ID, BEFUND, CALIBDATE, DAYSTART, IMPORTDATE, NIGHTSTART, PROTDESC, PROTNUM, PROTOCOLDAYSTART, PROTOCOLNIGHTSTART, SERNUM, TIMESTAMP, PATIENT_ID";
            var rs = _stat.executeQuery("SELECT " + columns + " FROM AUFZEICHNUNG WHERE AUFZEICHNUNG.ID = " + idReport);
            if (rs != null  && rs.next())
            {
                var id = rs.getString(1);

                report.DevReportId = id;

                columns = "ID, ALARM, DEACTIVATED, DEVICETYPE, KOMMENTAR, MESTYPE, TIMEOFMEASUREMENT, TIMESTAMP, UPDATE, CODE, HR, NIBPDIAS, NIBPMAD, NIBPSYS, AUFZEICHNUNG_ID";
                rs = _stat.executeQuery("SELECT " + columns + " FROM MEASUREMENTSBP WHERE MEASUREMENTSBP.AUFZEICHNUNG_ID = " + id);
            
                // Para cada medida obtenida, agregarla a la lista de medidas incluida en el estudio.
                while (rs.next())
                {
                    var measure = new Measurement();
                    measure.Comment = rs.getString(5); //Kommentar

                    var timeStr = rs.getString(7);
                    //Pareseo la fecha y hora para crear el DateTime
                    var time = parseDateTime(timeStr);
                    measure.Time = time;

                    measure.HeartRate = rs.getInt(11); //HR
                    measure.Diastolic = rs.getInt(12); //NIBPDIAS
                    measure.Average = rs.getInt(13); //NIBPMAD
                    measure.Systolic = rs.getInt(14); //NIBPSYS

                    report.addToMeasureList(measure);
                }
            }

            return report;
        }

        public Patient GetPatient(string idPatient)
        {
            var patient = new Patient();
            //Obtener datos del paciente
            var rs = _stat.executeQuery("SELECT * FROM PATIENT, ADRESSE WHERE PATIENT.ID = " + idPatient + "AND PATIENT.ADRESS_ID = ADRESSE.ID");

            if (rs != null && rs.next())
            {
                patient.IdInDevice = rs.getInt(2);
                
                var timeStr = rs.getString(3); //Timeofmeasurement
                //Pareseo la fecha y hora para crear el DateTime
                patient.BirthDate = parseDateTime(timeStr);

                patient.DocumentId = rs.getString(8);

                patient.Sex = rs.getInt(13)==0 ? Patient.SexType.F : Patient.SexType.M;

                patient.City = rs.getString(26);
                patient.EMail = rs.getString(29);
                patient.Name = rs.getString(30);
                patient.Surname = rs.getString(32);
                patient.CellPhone = rs.getString(35);
                patient.Phone = rs.getString(36);
                patient.Neighbour = rs.getString(37);
                patient.Address = rs.getString(38);

            }

            return patient;

        }

        public ICollection<Patient> ListPatients()
        {
            ResultSet rs = _stat.executeQuery("SELECT ID, BIRTHDATE,PATIENTID FROM PATIENT");
            ICollection<Patient> patientList = new List<Patient>();

            string timeStr;
            DateTime time;
            int id;

            while (rs.next())
            {
                Patient patient = new Patient();

                id = rs.getInt(1);
                patient.IdInDevice = id;

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

        public ICollection<PatientReport> ListAllReports()
        {
            string col = "p.id, p.patientid, a1.id, a1.firstname, a1.lastname, a2.id, a2.timestamp";
            string tables = "patient AS p, adresse AS a1, aufzeichnung AS a2";
            string cond = "( p.ID = a2.PATIENT_ID AND p.adress_id = a1.ID )";
            ResultSet rs = _stat.executeQuery("SELECT " + col + " FROM " + tables + " WHERE " + cond + ";");
            ICollection<PatientReport> result = new List<PatientReport>();
            while (rs.next())
            {
                //Creo el nodo de la lista result de tipo PatientReport
                PatientReport pr = new PatientReport
                    {
                        PatientId = rs.getString(1),
                        PatientDocument = rs.getString(2),
                        PatientName = rs.getString(4),
                        PatientLastName = rs.getString(5),
                        ReportId = rs.getString(6),
                        ReportDevice = DeviceId,
                        ReportDate = parseDateTime(rs.getString(7))
                    };

                result.Add(pr);
            }
            return result;
        }

        // Devuelve una lista de los reportes del paciente 'patientId'
        public ICollection<Report> GetReportsByPatientId(string patientId)
        {
            string patIdStr = patientId.ToString();
            //Obtengo una lista de presiones de reportes para el paciente patientId
            string columns = " AUFZEICHNUNG.ID, TIMEOFMEASUREMENT, NIBPSYS, NIBPMAD, NIBPDIAS, HR ";
            string cond = " AUFZEICHNUNG.PATIENT_ID=" + patIdStr;
            string sql = "SELECT" + columns + "FROM MEASUREMENTSBP INNER JOIN AUFZEICHNUNG ON AUFZEICHNUNG.ID = MEASUREMENTSBP.AUFZEICHNUNG_ID WHERE" + cond;
            ResultSet rs = _stat.executeQuery( sql );
            ICollection<Report> reportList = new List<Report>();
            Report report = new Report();

            int i;
            int id;
            string timeStr;
            long lastId;
            if (rs.first())
            {
                rs.first();
                lastId = Convert.ToInt64(rs.getString(1));
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
                    report.Id = lastId;
                    reportList.Add(report);
                    
                    //Actualizo el ultimo id
                    lastId = id;
                    
                    //Creo el siguiente reporte y agrego la ultima medida procesada
                    report = new Report();
                    report.addToMeasureList(m);

                }
            }
            report.Id = lastId;
            reportList.Add(report);
            return reportList;
        }


    }
}
