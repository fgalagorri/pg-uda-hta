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
            var columns = "ID, BEFUND, CALIBDATE, DAYSTART, NIGHTSTART, TIMESTAMP, PATIENT_ID";
            var rs = _stat.executeQuery("SELECT " + columns + " FROM AUFZEICHNUNG WHERE AUFZEICHNUNG.ID = " + idReport);
            if (rs != null  && rs.next())
            {
                report.DeviceReportId = rs.getString(1);
                report.Diagnosis = rs.getString(2);
                report.BeginDate = parseDateTime(rs.getString(6));
                
                int dayStart = rs.getInt(4);
                Int32 dayStartHr = (dayStart/100);
                Int32 dayStartMin = dayStart - (dayStartHr*100);
                const int sec0 = 0;

                int nightStart = rs.getInt(5);
                Int32 nightStartHr = (nightStart/100);
                Int32 nightStartMin = nightStart - (nightStartHr*100);

                // Si se durmio despues de las 23:59, dreamStart = dreamEnd = BeginDate + 1
                DateTime date;
                if (nightStart <= dayStart)
                {
                    date = report.BeginDate.Value.AddDays(1);
                    report.Carnet.SleepTimeStart = new DateTime(date.Year, date.Month, date.Day, nightStartHr, nightStartMin, sec0);
                    report.Carnet.SleepTimeEnd = new DateTime(date.Year, date.Month, date.Day, dayStartHr, dayStartMin, sec0);                    
                }
                else // Si se durmio antes de las 00:00, el dia de dreamEnd = dreamStart + 1
                {
                    report.Carnet.SleepTimeStart = new DateTime(report.BeginDate.Value.Year, report.BeginDate.Value.Month, report.BeginDate.Value.Day, nightStartHr, nightStartMin, sec0);
                    date = report.BeginDate.Value.AddDays(1);
                    report.Carnet.SleepTimeEnd = new DateTime(date.Year, date.Month, date.Day, dayStartHr, dayStartMin, sec0);
                }
                report.Doctor = new User();

            }

            return report;
        }

        public List<Measurement> GetMeasures(Report report)
        {
            var list = new List<Measurement>();
            var columns = "ID, ALARM, DEACTIVATED, DEVICETYPE, KOMMENTAR, MESTYPE, TIMEOFMEASUREMENT, TIMESTAMP, UPDATE, CODE, HR, NIBPDIAS, NIBPMAD, NIBPSYS, AUFZEICHNUNG_ID";
            var rs = _stat.executeQuery("SELECT " + columns + " FROM MEASUREMENTSBP WHERE MEASUREMENTSBP.AUFZEICHNUNG_ID = " + report.DeviceReportId);

            int maxDiasDay = 0;
            int maxDiasNight = 0;

            int maxSysDay = 0;
            int maxSysNight = 0;

            int sumSysDay = 0;
            int sumSysNight = 0;
            int sumDiasDay = 0;
            int sumDiasNight = 0;
            int countDay = 0;
            int countNight = 0;

            // Para cada medida obtenida, agregarla a la lista de medidas incluida en el estudio.
            while (rs.next())
            {
                var measure = new Measurement();
                measure.Comment = rs.getString(5); //Kommentar

                //Pareseo la fecha y hora para crear el DateTime
                measure.Time = parseDateTime(rs.getString(7));

                measure.HeartRate = rs.getInt(11); //HR
                measure.Diastolic = rs.getInt(12); //NIBPDIAS
                measure.Middle = rs.getInt(13); //NIBPMAD
                measure.Systolic = rs.getInt(14); //NIBPSYS

                if (measure.Time >= report.Carnet.SleepTimeStart.Value && measure.Time <= report.Carnet.SleepTimeEnd.Value)
                { // Medida tomada mientras dormia, actualizar maximos
                    measure.Asleep = true;
                    
                    if (measure.Diastolic > maxDiasNight)
                        maxDiasNight = measure.Diastolic;

                    if (measure.Systolic > maxSysNight)
                        maxSysNight = measure.Systolic;

                    sumDiasNight = sumDiasNight + measure.Diastolic;
                    sumSysNight = sumSysNight + measure.Systolic;
                    countNight++;
                }
                else
                { // Si no esta durmiendo, actualizar maximos del dia
                    if (measure.Diastolic > maxDiasDay)
                        maxDiasDay = measure.Diastolic;

                    if (measure.Systolic > maxSysDay)
                        maxSysDay = measure.Systolic;

                    sumDiasDay = sumDiasDay + measure.Diastolic;
                    sumSysDay = sumSysDay + measure.Systolic;
                    countDay++;
                }

                list.Add(measure);

                report.EndDate = measure.Time;
            }

            report.DiastolicDayMax = maxDiasDay;
            report.DiastolicNightMax = maxDiasNight;
            report.SystolicDayMax = maxSysDay;
            report.SystolicNightMax = maxSysNight;

            report.HeartRateAvg = list.Sum(m => m.HeartRate)/list.Count;
            report.DiastolicTotalAvg = list.Sum(m => m.Diastolic)/list.Count;
            report.SystolicTotalAvg = list.Sum(m => m.Systolic)/list.Count;
            if (countDay != 0)
            {
                report.DiastolicDayAvg = sumDiasDay / countDay;
                report.SystolicDayAvg = sumSysDay / countDay;                
            }
            else
            {
                report.DiastolicDayAvg = 0;
                report.SystolicDayAvg = 0;                                
            }

            if (countNight != 0)
            {
                report.DiastolicNightAvg = sumDiasNight / countNight;
                report.SystolicNightAvg = sumSysNight / countNight;
                
            }
            else
            {
                report.DiastolicNightAvg = 0;
                report.SystolicNightAvg = 0;                
            }

            return list;
        }  

        public Patient GetPatient(string idPatient)
        {
            var patient = new Patient();
            //Obtener datos del paciente
            var rs = _stat.executeQuery("SELECT * FROM PATIENT, ADRESSE WHERE PATIENT.ID = " + idPatient + "AND PATIENT.ADRESS_ID = ADRESSE.ID");

            if (rs != null && rs.next())
            {
                patient.DevicePatientId = rs.getString(2);
                
                var timeStr = rs.getString(3); 
                //Pareseo la fecha y hora para crear el DateTime
                patient.BirthDate = parseDateTime(timeStr);

                patient.DocumentId = rs.getString(8);

                patient.Sex = rs.getInt(13) == 0 ? SexType.M : SexType.F;

                patient.City = rs.getString(26);
                patient.Email = rs.getString(29);
                patient.Names = rs.getString(30);
                patient.Surnames = rs.getString(32);
                patient.CellPhone = rs.getString(35);
                patient.Phone = rs.getString(36);
                patient.Neighbour = rs.getString(38);
                patient.Address = rs.getString(39);

            }

            return patient;

        }

        public ICollection<Patient> ListPatients()
        {
            ResultSet rs = _stat.executeQuery("SELECT ID, BIRTHDATE,PATIENTID FROM PATIENT");
            ICollection<Patient> patientList = new List<Patient>();

            string timeStr;
            DateTime time;
            string id;

            while (rs.next())
            {
                Patient patient = new Patient();

                id = rs.getString(1);
                patient.DevicePatientId = id;

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

                m.Middle = rs.getInt(++i);
                Console.Write(rs.getInt(i) + "|");

                m.Diastolic = rs.getInt(++i);
                Console.Write(rs.getInt(i) + "|");

                m.HeartRate = rs.getInt(++i);
                Console.WriteLine(rs.getInt(i) + "|");

                //Si id es igual al ultimo id, agrego las medidas al reporte
                if (id == lastId)
                {
                    //Agrego la medida al reporte
                    report.Measures.Add(m);
                }
                else
                { //Si el ultimo id (lastId) es diferente al actual, entonces comenzaron las medidas de otro reporte

                    //Agrego el reporte del ultimo identificador a la lista
                    report.UdaId = lastId;
                    reportList.Add(report);
                    
                    //Actualizo el ultimo id
                    lastId = id;
                    
                    //Creo el siguiente reporte y agrego la ultima medida procesada
                    report = new Report();
                    report.Measures.Add(m);

                }
            }
            report.UdaId = lastId;
            reportList.Add(report);
            return reportList;
        }


    }
}
