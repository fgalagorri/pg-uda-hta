﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    class PatientDataAccess
    {
        private string ConnectionString = "SERVER=localhost;DATABASE=patient_info_db;UID=root;PASSWORD=rootudahta;";
        private MySqlConnection conn;

        public PatientDataAccess()
        {
        }
    }
}