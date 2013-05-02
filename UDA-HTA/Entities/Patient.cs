using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Patient
    {
        public enum SexType{F,M};

        private int _idInDevice;
        private string _documentId;
        private string _name;
        private string _surname;
        private DateTime _birthDate;
        private SexType _sex;
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

        public int IdInDevice
        {
            get { return _idInDevice; }
            set { _idInDevice = value; }
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

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public SexType Sex
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
}
