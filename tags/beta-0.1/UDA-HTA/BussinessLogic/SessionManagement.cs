using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Entities;

namespace BussinessLogic
{
    public class SessionManagement
    {

        public User Login(string userName, string pswdHashed)
        {
            User u = verifyPassword(userName, pswdHashed);
            return u;
        }

        public void Logout()
        {
        }

        // Los parametros currentPswd, newPswd, deben estar encriptados.
        public bool ChangePassword(string userName, string currentPswd, string newPswd)
        {
            // Verificar que el pswd actual es correcto
            if (verifyPassword(userName, currentPswd) != null)
            {
                // guardar nuevo paswd en la base
                UdaHtaDataAccess dataAccess = new UdaHtaDataAccess();
                dataAccess.UpdatePassword(userName, newPswd);
                return true;
            }

            var exception = new Exception("changePassword failed");
            throw exception;
        }

        // Devuelve el usuario si el Pwd es correcto
        private User verifyPassword(string userName, string pswdHashed)
        {
            // Verificar que el nombre de usuario es correcto
            UdaHtaDataAccess dataAccess = new UdaHtaDataAccess();
            User u = dataAccess.GetUser(userName);

            //Si existe el usuario, el password sera distinto de ""
            //Si el hash del password ingresado es igual al hash del password guardado,
            //entonces login exitoso, sino falla login 
            if (!u.Password.Equals("") && u.Password.Equals(pswdHashed))
                return u;

            var exception = new Exception("verifyPassword failed");
            throw exception;
        }

    }
}
