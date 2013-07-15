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
            uda.InsertUser(usr.Login,usr.Password,usr.Role);
        }

        public void deleteUser(User usr)
        {
            
        }

        public void editUser(User usr)
        {
            
        }

    }
}
