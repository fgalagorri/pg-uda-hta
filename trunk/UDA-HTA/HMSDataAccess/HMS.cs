using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using DeviceDataAccess;
using Entities;
using java.sql;

namespace HMSDataAccess
{
    public class HMS : IDeviceDataAccess
    {
        private const int deviceId = 0;
        private const string deviceName = "HMS";
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
                //_conn = DriverManager.getConnection("jdbc:h2:~/HMS Client-Server_DB/database","sa","");
                _conn = DriverManager.getConnection(ConfigurationManager.ConnectionStrings["Hms"].ConnectionString);
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
            //                    (1)         (2)                (3)               (4)         (5)          (6)            
            var columnsPatient = "PATIENT.ID, PATIENT.BIRTHDATE, PATIENT.PATIENTID,PATIENT.SEX,PATIENT.SIZE,PATIENT.WEIGHT,";
            //                    (7)          (8)             (9)           (10)              (11)             (12)                (13)          (14)           (15)           
            var columnsAdresse = "ADRESSE.CITY,ADRESSE.COUNTRY,ADRESSE.EMAIL,ADRESSE.FIRSTNAME,ADRESSE.LASTNAME,ADRESSE.MOBILEPHONE,ADRESSE.PHONE,ADRESSE.REGION,ADRESSE.STREET,";
            //                    (16)hasta     (17)                (18)                (19)desde     
            var columnsIllness = "KRANKHEIT.BIS,KRANKHEIT.KOMMENTAR,KRANKHEIT.KRANKHEIT,KRANKHEIT.VON,";
            //                     (20)                 (21)comercial          (22)activo            
            var columnsMedicine = "MEDIKATION.DOSIERUNG,MEDIKATION.HANDELSNAME,MEDIKATION.WIRKSTOFF,";
            //                   (23)            (24)                (25)                  (26)                    (27)                   
            var columnsReport = "AUFZEICHNUNG.ID,AUFZEICHNUNG.BEFUND,AUFZEICHNUNG.DAYSTART,AUFZEICHNUNG.NIGHTSTART,AUFZEICHNUNG.TIMESTAMP";
            
            var columns = columnsPatient + columnsAdresse + columnsIllness + columnsMedicine + columnsReport;
            var illnessTable = "(PATIENT_KRANKHEIT INNER JOIN KRANKHEIT ON KRANKHEITEN_ID = KRANKHEIT.ID)";
            var medicineTable = "(PATIENT_MEDIKATION INNER JOIN MEDIKATION ON MEDIKATIONEN_ID = MEDIKATION.ID)";
            var table = "(((PATIENT LEFT JOIN ADRESSE ON ADRESSE.ID = PATIENT.ADRESS_ID)LEFT JOIN " + illnessTable + " ON PATIENT.ID = PATIENT_KRANKHEIT.PATIENT_ID) LEFT JOIN " + medicineTable + " ON PATIENT.ID = PATIENT_MEDIKATION.PATIENT_ID) INNER JOIN AUFZEICHNUNG";
            var condition = "AUFZEICHNUNG.PATIENT_ID = PATIENT.ID AND AUFZEICHNUNG.ID = " + idReport;
            var rs = _stat.executeQuery("SELECT " + columns + " FROM " + table + " ON " + condition);


            var first = true;
            while (rs != null  && rs.next())
            {
                if (first)
                { // Si es la primera vez que ejecuta el while, cargar todos los datos paciente y reporte.
                    /*
                     * Datos paciente
                      */
                    report.Patient.DeviceReferences.Add(new DeviceReference(deviceId,rs.getString(1)));

                    var timeStr = rs.getString(2);
                    //Pareseo la fecha y hora para crear el DateTime
                    report.Patient.BirthDate = parseDateTime(timeStr);

                    report.Patient.DocumentId = rs.getString(3);

                    report.Patient.Sex = rs.getInt(4) == 0 ? SexType.M : SexType.F;

                    report.TemporaryData.Weight = rs.getInt(6);
                    report.TemporaryData.Height = decimal.Parse(rs.getString(5).Replace(",", "."),
                                                                 NumberStyles.Float,
                                                                 CultureInfo.InvariantCulture);

                    report.Patient.City = rs.getString(7);
                    report.Patient.Email = rs.getString(9);
                    report.Patient.Names = rs.getString(10);
                    report.Patient.Surnames = rs.getString(11);
                    report.Patient.CellPhone = rs.getString(12);
                    report.Patient.Phone = rs.getString(13);
                    report.Patient.Neighbour = rs.getString(14);
                    report.Patient.Address = rs.getString(15);

                    /*
                     * Datos reporte
                     */
                    report.DeviceReportId = rs.getString(23);
                    report.Diagnosis = rs.getString(24);
                    report.BeginDate = parseDateTime(rs.getString(27));

                    int dayStart = rs.getInt(25);
                    Int32 dayStartHr = (dayStart / 100);
                    Int32 dayStartMin = dayStart - (dayStartHr * 100);
                    const int sec0 = 0;

                    int nightStart = rs.getInt(26);
                    Int32 nightStartHr = (nightStart / 100);
                    Int32 nightStartMin = nightStart - (nightStartHr * 100);

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

                    first = false; //ya se procesaron los datos generales del paciente y reporte.

                }
                
                /* 
                 * Historial Medico
                 */
                if (rs.getString(16) != null || 
                    rs.getString(17) != null ||
                    rs.getString(18) != null ||
                    rs.getString(19) != null)
                {
                    MedicalRecord mr = new MedicalRecord();
                    if (rs.getString(16) != null)
                    {
                        mr.Until = parseDateTime(rs.getString(16));                        
                    }
                    mr.Comment = rs.getString(17);
                    mr.Illness = rs.getString(18);
                    if (rs.getString(19) != null)
                    {
                        mr.Since = parseDateTime(rs.getString(19));
                    }
                    report.Patient.Background.Add(mr);
                }


                /*
                 * Medicinas
                 */
                if (rs.getString(20) != null ||
                    rs.getString(21) != null ||
                    rs.getString(22) != null)
                {
                    MedicineDose md = new MedicineDose();
                    md.Dose = rs.getString(20);
                    md.Drug = new Drug("", rs.getString(22), rs.getString(21));

                    // TODO MEDICINE 
                    // report.TemporaryData.LMedicines.Add(md);                    
                }

            }

            return report;
        }

        public List<Measurement> GetMeasures(Report report)
        {
            var list = new List<Measurement>();
            var columns = "ID, ALARM, DEACTIVATED, DEVICETYPE, KOMMENTAR, MESTYPE, TIMEOFMEASUREMENT, TIMESTAMP, UPDATE, CODE, HR, NIBPDIAS, NIBPMAD, NIBPSYS, AUFZEICHNUNG_ID";
            var rs = _stat.executeQuery("SELECT " + columns + " FROM MEASUREMENTSBP WHERE MEASUREMENTSBP.AUFZEICHNUNG_ID = " + report.DeviceReportId);

            int code;

            // Para cada medida obtenida, agregarla a la lista de medidas incluida en el estudio.
            while (rs.next())
            {
                var measure = new Measurement
                    {
                        Comment = rs.getString(5),
                        Time = parseDateTime(rs.getString(7)),
                        HeartRate = rs.getInt(11),
                        Diastolic = rs.getInt(12),
                        Middle = rs.getInt(13),
                        Systolic = rs.getInt(14),
                        Retry = false
                    };

                //Pareseo la fecha y hora para crear el DateTime

                code = rs.getInt(10);

                measure.Valid = (code == 0 || code == 130 ||
                                 ((measure.HeartRate != 999 && measure.Systolic != 999
                                   && measure.Diastolic != 999 && measure.Middle != 999)
                                  &&
                                  (measure.HeartRate != 0 && measure.Systolic != 0
                                   && measure.Diastolic != 0 && measure.Middle != 0)));
                
                measure.IsEnabled = measure.Valid;

                measure.Asleep = (measure.Time >= report.Carnet.SleepTimeStart.Value &&
                                  measure.Time <= report.Carnet.SleepTimeEnd.Value);

                list.Add(measure);

                report.EndDate = measure.Time;
            }

            report.MiddleDayAvg = 0;
            report.MiddleDayMax = 0;
            report.MiddleDayMin = 0;
            report.MiddleNightAvg = 0;
            report.MiddleNightMax = 0;
            report.MiddleNightMin = 0;
            report.MiddleTotalAvg = 0;
            report.MiddleTotalMax = 0;
            report.MiddleTotalMin = 0;

            report.SystolicDayAvg = 0;
            report.SystolicDayMax = 0;
            report.SystolicDayMin = 0;
            report.SystolicNightAvg = 0;
            report.SystolicNightMax = 0;
            report.SystolicNightMin = 0;
            report.SystolicTotalAvg = 0;
            report.SystolicTotalMax = 0;
            report.SystolicTotalMin = 0;

            report.DiastolicDayAvg = 0;
            report.DiastolicDayMax = 0;
            report.DiastolicDayMin = 0;
            report.DiastolicNightAvg = 0;
            report.DiastolicNightMax = 0;
            report.DiastolicNightMin = 0;
            report.DiastolicTotalAvg = 0;
            report.DiastolicTotalMax = 0;
            report.DiastolicTotalMin = 0;
/*
            report.DiastolicDayMax = list.Where(m => m.Valid && (bool) !m.Asleep).Max(m => m.Diastolic);
            report.DiastolicNightMax = list.Where(m => m.Valid && (bool) m.Asleep).Max(m => m.Diastolic);

            report.SystolicDayMax = list.Where(m => m.Valid && (bool) !m.Asleep).Max(m => m.Systolic);
            report.SystolicNightMax = list.Where(m => m.Valid && (bool)m.Asleep).Max(m => m.Systolic);

            int countDay = list.Count(m => (bool) !m.Asleep && m.Valid);
            int countNight = list.Count(m => (bool)m.Asleep && m.Valid);
            int countTotal = countDay + countNight;
            
            report.HeartRateTotalAvg = list.Where(m => m.Valid).Sum(m => m.HeartRate)/countTotal;
            report.DiastolicTotalAvg = list.Where(m => m.Valid).Sum(m => m.Diastolic) / countTotal;
            report.SystolicTotalAvg = list.Where(m => m.Valid).Sum(m => m.Systolic) / countTotal;

            if (countDay != 0)
            {
                report.HeartRateDayAvg = list.Where(m => m.Valid && (bool) !m.Asleep).Sum(m => m.HeartRate)/countDay;
                report.DiastolicDayAvg = list.Where(m => m.Valid && (bool)!m.Asleep).Sum(m => m.Diastolic) / countDay;
                report.SystolicDayAvg = list.Where(m => m.Valid && (bool)!m.Asleep).Sum(m => m.Systolic) / countDay;                
            }
            else
            {
                report.HeartRateDayAvg = 0;
                report.DiastolicDayAvg = 0;
                report.SystolicDayAvg = 0;                                
            }

            if (countNight != 0)
            {
                report.HeartRateNightAvg = list.Where(m => m.Valid && (bool)m.Asleep).Sum(m => m.HeartRate) / countNight;
                report.DiastolicNightAvg = list.Where(m => m.Valid && (bool)m.Asleep).Sum(m => m.Diastolic) / countNight;
                report.SystolicNightAvg = list.Where(m => m.Valid && (bool)m.Asleep).Sum(m => m.Systolic) / countNight;
                
            }
            else
            {
                report.HeartRateNightAvg = 0;
                report.DiastolicNightAvg = 0;
                report.SystolicNightAvg = 0;                
            }

 */
            return list;
        }  

        public Patient GetPatient(string idPatient)
        {
            var patient = new Patient();
            //Obtener datos del paciente
            var rs = _stat.executeQuery("SELECT * FROM PATIENT, ADRESSE WHERE PATIENT.ID = " + idPatient + "AND PATIENT.ADRESS_ID = ADRESSE.ID");

            if (rs != null && rs.next())
            {
                patient.DeviceReferences.Add(new DeviceReference(deviceId,rs.getString(2)));
                
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
                patient.DeviceReferences.Add(new DeviceReference(deviceId,id));

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
                        ReportDevice = deviceId,
                        ReportDeviceName = deviceName,
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
                Console.WriteLine(rs.getInt(i) + "|"); // TODO CONSOLE.WRITELINE
                
                m.Retry = false;

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
