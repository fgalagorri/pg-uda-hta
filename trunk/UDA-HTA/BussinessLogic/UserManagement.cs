using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DataAccess;

namespace UserManagement
{
    public class UserManagement
    {
        public void createUser(User usr)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            try
            {
                uda.InsertUser(usr.Login, usr.Password, usr.Role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void deleteUser(User usr)
        {
            // TODO
        }

        public void editUser(User usr)
        {
            // TODO
        }

    }
}
