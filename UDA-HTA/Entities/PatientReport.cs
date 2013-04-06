using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class PatientReport
    {
        private string _patientName;
        private string _patientLastName;
        private string _patientDocument;
        private int _patientIdent;

        private DateTime _reportDate;
        private int _reportDevice;
        private int _reportIdent;

        public PatientReport()
        {
        }

        public string patientName
        {
            get { return _patientName; }
            set { _patientName = value; }
        }

        public string patientLastName
        {
            get { return _patientLastName; }
            set { _patientLastName = value; }
        }

        public string patientDocument
        {
            get { return _patientDocument; }
            set { _patientDocument = value; }
        }

        public int patientIdent
        {
            get { return _patientIdent; }
            set { _patientIdent = value; }
        }

        public DateTime reportDate
        {
            get { return _reportDate; }
            set { _reportDate = value; }
        }

        public int reportDevice
        {
            get { return _reportDevice; }
            set { _reportDevice = value; }
        }

        public int reportIdent
        {
            get { return _reportIdent; }
            set { _reportIdent = value; }
        }

        

    }
}
