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
            try
            {
                uda.InsertUser(usr.Login, usr.Password, usr.Role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            // TODO
            return null;
        }

    }
}
