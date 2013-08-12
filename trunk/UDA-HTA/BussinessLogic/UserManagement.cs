using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DataAccess;

namespace BussinessLogic
{
    public class UserManagement
    {
        public void CreateUser(User usr)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.InsertUser(usr.Login, usr.Password, usr.Role);
        }

        public void DeleteUser(User usr)
        {
            // TODO
        }

        public void EditUser(User usr)
        {
            // TODO
        }

        public User GetUser(string userName)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.GetUser(userName);
        }

    }
}
