using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public class Patient
    {
        public enum sexType{F,M};

        private int _idHms;
        private string _documentId;
        private string _name;
        private DateTime _birthDate;
        private sexType _sex;
        private string _address;
        private string _neighbour;
        private string _city;
        private string _phone;
        private string _cellPhone;
        private string _eMail;
        private ICollection<Report> _reportList;

        public Patient()
        {
            _reportList = new List<Report>();
        }

        public int IdHms
        {
            get { return _idHms; }
            set { _idHms = value; }
        }
        
        public string DocumentId
        {
            get { return _documentId; }
            set { _documentId = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public sexType Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string Neighbour
        {
            get { return _neighbour; }
            set { _neighbour = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string CellPhone
        {
            get { return _cellPhone; }
            set { _cellPhone = value; }
        }

        public string EMail
        {
            get { return _eMail; }
            set { _eMail = value; }
        }


        public ICollection<Report> ReportList
        {
            get { return _reportList; }
            set { _reportList = value; }
        }
        
        public void addToReportList(Report rep)
        {
            _reportList.Add(rep);
        }

    }   // end patient


    public class Measurement
    {
        private DateTime _time;
        private int _systolic;
        private int _average;
        private int _diastolic;
        private int _heartRate;
        private string _comment;

        public Measurement()
        {

        }

        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public int Systolic
        {
            get { return _systolic; }
            set { _systolic = value; }
        }

        public int Average
        {
            get { return _average; }
            set { _average = value; }
        }

        public int Diastolic
        {
            get { return _diastolic; }
            set { _diastolic = value; }
        }

        public int HeartRate
        {
            get { return _heartRate; }
            set { _heartRate = value; }
        }

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

    }   //end measurement


    public class Report
    {
        private int _ident;
        private ICollection<Measurement> measureList;

        public Report()
        {
            measureList = new List<Measurement>();
        }

        public int Ident
        {
            get { return _ident; }
            set { _ident = value; }
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
