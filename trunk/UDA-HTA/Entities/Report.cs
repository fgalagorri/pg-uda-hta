using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Report
    {
        
        private int _ident;
        private DateTime _beginDate;
        private DateTime _endDate;
        private string _doctor;
        private string _diagnosis;
        private string _requestDoctor;
        private string _specialty;
        private int _dayAvgSys;
        private int _nightAvgSys;
        private int _totalAvgSys;
        private int _dayMaxSys;
        private int _nightMaxSys;
        private int _dayAvgDias;
        private int _nightAvgDias;
        private int _totalAvgDias;
        private int _dayMaxDias;
        private int _nightMaxDias;
        private int _idDev;
        private int _devReportId;
        private int _idTemporaryData;
        private int _idDailyCarnet;
        private int _idPatient;
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

        public DateTime BeginDate
        {
            get { return _beginDate; }
            set { _beginDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public string Doctor
        {
            get { return _doctor; }
            set { _doctor = value; }
        }

        public string Diagnosis
        {
            get { return _diagnosis; }
            set { _diagnosis = value; }
        }

        public string RequestDoctor
        {
            get { return _requestDoctor; }
            set { _requestDoctor = value; }
        }

        public string Specialty
        {
            get { return _specialty; }
            set { _specialty = value; }
        }

        public int DayAvgSys
        {
            get { return _dayAvgSys; }
            set { _dayAvgSys = value; }
        }

        public int NightAvgSys
        {
            get { return _nightAvgSys; }
            set { _nightAvgSys = value; }
        }

        public int TotalAvgSys
        {
            get { return _totalAvgSys; }
            set { _totalAvgSys = value; }
        }

        public int DayMaxSys
        {
            get { return _dayMaxSys; }
            set { _dayMaxSys = value; }
        }

        public int NightMaxSys
        {
            get { return _nightMaxSys; }
            set { _nightMaxSys = value; }
        }

        public int DayAvgDias
        {
            get { return _dayAvgDias; }
            set { _dayAvgDias = value; }
        }

        public int NightAvgDias
        {
            get { return _nightAvgDias; }
            set { _nightAvgDias = value; }
        }

        public int TotalAvgDias
        {
            get { return _totalAvgDias; }
            set { _totalAvgDias = value; }
        }

        public int DayMaxDias
        {
            get { return _dayMaxDias; }
            set { _dayMaxDias = value; }
        }

        public int NightMaxDias
        {
            get { return _nightMaxDias; }
            set { _nightMaxDias = value; }
        }

        public int IdDev
        {
            get { return _idDev; }
            set { _idDev = value; }
        }

        public int DevReportId
        {
            get { return _devReportId; }
            set { _devReportId = value; }
        }

        public int IdTemporaryData
        {
            get { return _idTemporaryData; }
            set { _idTemporaryData = value; }
        }

        public int IdDailyCarnet
        {
            get { return _idDailyCarnet; }
            set { _idDailyCarnet = value; }
        }

        public int IdPatient
        {
            get { return _idPatient; }
            set { _idPatient = value; }
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
