using System;
using System.Configuration;
using System.IO;

namespace EventLogger
{
    public class LogFileManagement
    {
        private string sLogFormat;
        private string sErrorTime;

        public LogFileManagement()
        {
            if (!Directory.Exists(ConfigurationManager.AppSettings["LogPath"]))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["LogPath"]);

            //sLogFormat utilizado para crear el formato del archivo de log:
            // dd/mm/yyyy hh:mm:ss ==> Tipo Mensaje: Log Message
            sLogFormat = DateTime.Now.ToShortDateString() + " " +
                         DateTime.Now.ToLongTimeString() + " ==> ";

            //variable utilizada para crear el formato del nombre del archivo
            //por ejemplo: ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString("D2");
            string sDay = DateTime.Now.Day.ToString("D2");
            sErrorTime = sYear + sMonth + sDay;
        }

        public void ErrorLog(string sPathName, string sErrMsg, Exception innerException)
        {
            StreamWriter sw = new StreamWriter(sPathName + sErrorTime, true);
            sw.WriteLine(sLogFormat + sErrMsg);
            if(innerException!= null)
                sw.WriteLine("\t\t" + innerException.Message);
            sw.Flush();
            sw.Close();
        }
    }
}
