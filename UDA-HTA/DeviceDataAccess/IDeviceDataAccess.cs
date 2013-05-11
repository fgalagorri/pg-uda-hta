﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace DeviceDataAccess
{
    public interface IDeviceDataAccess
    {
        void ConnectToDataBase();

        void CloseConnectionDataBase();

        Report GetReport(string idReport);

        Patient GetPatient(string idPatient);
        
        ICollection<Patient> ListPatients();

        ICollection<PatientReport> ListAllReports();

        ICollection<Report> GetReportsByPatientId(string patientId);


    }
}