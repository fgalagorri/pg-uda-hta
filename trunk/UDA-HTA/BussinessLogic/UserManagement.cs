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
        //El password del usuario debe estar encriptado
        public void CreateUser(User usr)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.InsertUser(usr.Login, usr.Password, usr.Role, usr.Name);
        }

        public void DeleteUser(User usr)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.DeleteUSer(usr);
        }

        public void EditUser(User usr)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.EditUSer(usr);
        }

        public User GetUser(string userName)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.GetUser(userName);
        }

    }
}
