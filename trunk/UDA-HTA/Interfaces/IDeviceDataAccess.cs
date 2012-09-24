using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IDeviceDataAccess
    {

        #region list members

        ICollection<Patient> ListPatients()
        {

        }

        ICollection<Report> ListReports()
        {

        }

        #endregion




    }
}
