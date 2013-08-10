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
        public string CurrentUser { get; set; }

        public User Login(string userName, string pswdHashed)
        {
            var um = new UserManagement();
            if (verifyPassword(userName, pswdHashed))
            {
                CurrentUser = userName;
                return um.GetUser(userName);
            }
 
            return null;
        }

        public void Logout()
        {
        }

        // Los parametros currentPswd, newPswd, deben estar encriptados.
        public bool ChangePassword(string userName, string currentPswd, string newPswd)
        {
            // Verificar que el pswd actual es correcto
            if ( verifyPassword(userName,currentPswd) )
            {
                // guardar nuevo paswd en la base
                UdaHtaDataAccess dataAccess = new UdaHtaDataAccess();
                try
                {
                    dataAccess.UpdatePassword(userName, newPswd);
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }

        private bool verifyPassword(string userName, string pswdHashed)
        {
            // Verificar que el nombre de usuario es correcto
            UdaHtaDataAccess dataAccess = new UdaHtaDataAccess();

            string pswd = dataAccess.GetPassword(userName);

            //Si existe el usuario, el password sera distinto de ""
            //Si el hash del password ingresado es igual al hash del password guardado,
            //entonces login exitoso, sino falla login 
            if (!pswd.Equals("") && pswd.Equals(pswdHashed))
            {
                return true;                        
            }

            return false;
            
        }

    }
}
