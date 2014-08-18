﻿using System;
using DataAccess;
using Entities;

namespace BussinessLogic
{
    public class SessionManagement
    {

        public User Login(string userName, string pswdHashed, out bool enabled)
        {
            User u = verifyPassword(userName, pswdHashed, out enabled);
            return u;
        }

        public void Logout()
        {
        }

        // Los parametros currentPswd, newPswd, deben estar encriptados.
        public bool ChangePassword(string userName, string currentPswd, string newPswd)
        {
            bool enabled;
            // Verificar que el pswd actual es correcto
            if (verifyPassword(userName, currentPswd, out  enabled) != null)
            {
                // guardar nuevo paswd en la base
                UdaHtaDataAccess dataAccess = new UdaHtaDataAccess();
                dataAccess.UpdatePassword(userName, newPswd);
                return true;
            }

            var exception = new Exception("El cambio de contraseña falló");
            throw exception;
        }

        // Devuelve el usuario si el Pwd es correcto
        private User verifyPassword(string userName, string pswdHashed, out bool enabled)
        {
            // Verificar que el nombre de usuario es correcto
            UdaHtaDataAccess dataAccess = new UdaHtaDataAccess();
            User u = dataAccess.GetUser(userName);
            enabled = true;

            if (u != null && !u.Enabled)
            {
                enabled = u.Enabled;
                return null;
            }

            //Si existe el usuario, el password sera distinto de ""
            //Si el hash del password ingresado es igual al hash del password guardado,
            //entonces login exitoso, sino falla login 
            if (u != null && !u.Password.Equals("") && u.Password.Equals(pswdHashed))
                return u;
            else
                return null;
        }

    }
}