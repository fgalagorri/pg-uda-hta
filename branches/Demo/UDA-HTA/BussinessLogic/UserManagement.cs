﻿using System.Collections.Generic;
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

        public ICollection<User> ListUsers(string name, string role, string login)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            return uda.GetUsers(name, role, login);
        }

        public void EnableUser(long userId)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.EnableUser(userId);
        }

        public void DisableUser(long userId)
        {
            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            uda.DisableUser(userId);
        }
    }
}