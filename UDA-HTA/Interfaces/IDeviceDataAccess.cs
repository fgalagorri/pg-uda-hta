using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IDeviceDataAccess
    {

        ICollection<Patient> ListPatients();

        ICollection<Report> ListReports();

        ICollection<Report> GetReportById();


    }
}
