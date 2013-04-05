using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
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

}
