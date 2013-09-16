using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EventLogger
{
    public class LogFileManagement
    {
        private string sLogFormat;
        private string sErrorTime;

        public LogFileManagement()
        {
            //sLogFormat utilizado para crear el formato del archivo de log:
            // dd/mm/yyyy hh:mm:ss ==> Tipo Mensaje: Log Message
            sLogFormat = DateTime.Now.ToShortDateString() + " " +
                         DateTime.Now.ToLongTimeString() + " ==> ";

            //variable utilizada para crear el formato del nombre del archivo
            //por ejemplo: ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            sErrorTime = sYear + sMonth + sDay;
        }

        public void ErrorLog(string sPathName, string sErrMsg)
        {
            StreamWriter sw = new StreamWriter(sPathName+sErrorTime,true);
            sw.WriteLine(sLogFormat + sErrMsg);
            sw.Flush();
            sw.Close();
        }
    }
}
