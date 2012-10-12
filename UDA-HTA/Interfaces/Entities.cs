using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public class Patient
    {
        private ICollection<Report> reportList;

        public Patient()
        {
            reportList = new List<Report>();
        }

        public void addToReportList(Report rep)
        {
            reportList.Add(rep);
        }

        public ICollection<Report> getReportList()
        {
            return reportList;
        }

    }   // end patient


    public class Measurement
    {
        private DateTime time;
        private int systolic;
        private int average;
        private int diastolic;
        private int heartRate;
        private string comment;

        public Measurement()
        {

        }

        public void setTime(DateTime t)
        {
            time = t;
        }

        public DateTime getTime()
        {
            return time;
        }

        public void setSystolic(int sys)
        {
            systolic = sys;
        }

        public int getSystolic()
        {
            return systolic;
        }

        public void setAverage(int avg)
        {
            average = avg;
        }

        public int getAverage()
        {
            return average;
        }

        public void setDiastolic(int dias)
        {
            diastolic = dias;
        }

        public int getDiastolic()
        {
            return diastolic;
        }

        public void setHeartRate(int hrate)
        {
            heartRate = hrate;
        }

        public int getHeartRate()
        {
            return heartRate;
        }

        public void setComment(string comm)
        {
            comment = comm;
        }

        public string getComment()
        {
            return comment;
        }

    }   //end measurement


    public class Report
    {
        private int ident;
        private ICollection<Measurement> measureList;

        public Report()
        {
            measureList = new List<Measurement>();
        }

        public void setIdent(int id)
        {
            ident = id;
        }

        public void addToMeasureList(Measurement measure)
        {
            measureList.Add(measure);        
        }

        public ICollection<Measurement> getMeasureList()
        {
            return measureList;
        }

    }   //end report
}
