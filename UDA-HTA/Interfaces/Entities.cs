using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public class Patient
    {
        public enum sexType{F,M};
        
        private string documentId;
        private string name;
        private DateTime birthDate;
        private sexType sex;
        private string address;
        private string neighbour;
        private string city;
        private string phone;
        private string cellPhone;
        private string eMail;
        private ICollection<Report> reportList;

        public Patient()
        {
            reportList = new List<Report>();
        }

        public void setDocumentId(string di)
        {
            documentId = di;
        }

        public string getDocumentId()
        {
            return documentId;
        }

        public void setName(string n)
        {
            name = n;
        }

        public string getName()
        {
            return name;
        }

        public void setBirthDate(DateTime bd)
        {
            birthDate = bd;
        }

        public DateTime getBirthDate()
        {
            return birthDate;
        }

        public void setSex(sexType s)
        {
            sex= s;
        }

        public sexType getSex()
        {
            return sex;
        }

        public void setAddress(string adr)
        {
            address = adr;
        }

        public string getAddress()
        {
            return address;
        }

        public void setNeighbour(string n)
        {
            neighbour = n;
        }

        public string getNeighbour()
        {
            return neighbour;
        }

        public void setCity(string c)
        {
            city = c;
        }

        public string getCity()
        {
            return city;
        }

        public void setPhone(string p)
        {
            phone = p;
        }

        public string getPhone()
        {
            return phone;
        }

        public void setCellPhone(string cp)
        {
            cellPhone = cp;
        }

        public string getCellPhone()
        {
            return cellPhone;
        }

        public void setEmail(string em)
        {
            eMail = em;
        }

        public string getEmail()
        {
            return eMail;
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
