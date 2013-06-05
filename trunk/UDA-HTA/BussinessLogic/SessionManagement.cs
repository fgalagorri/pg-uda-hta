using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceBussinessLogic;
using DataAccess;

namespace BussinessLogic
{
    public class SessionManagement : ISessionManagement
    {
        public string CurrentUser { get; set; }

        public bool login(string userName, string pswdHashed)
        {
            if (verifyPassword(userName, pswdHashed))
            {
                CurrentUser = userName;
                return true;
            }
            else
            {
                return false;
            }


        }

        public void logout()
        {
        }

        // Los parametros currentPswd, newPswd, deben estar encriptados.
        public bool changePassword(string userName, string currentPswd, string newPswd)
        {
            // Verificar que el pswd actual es correcto
            if ( verifyPassword(userName,currentPswd) )
            {
                // guardar nuevo paswd en la base
                UdaHtaDataAccess dataAccess = new UdaHtaDataAccess();

                bool ret = dataAccess.updatePassword(userName,newPswd);
                return ret;
            }
            return false;
        }

        private bool verifyPassword(string userName, string pswdHashed)
        {
            // Verificar que el nombre de usuario es correcto
            UdaHtaDataAccess dataAccess = new UdaHtaDataAccess();

            string pswd = dataAccess.getPassword(userName);

            dataAccess.CloseConnectionDataBase();

            //Si existe el usuario, el password sera distinto de ""
            //Si el hash del password ingresado es igual al hash del password guardado,
            //entonces login exitos, sino falla login 
            if (!pswd.Equals("") && pswd.Equals(pswdHashed))
            {
                return true;                        
            }
            else
            {
                return false;
            }

        }

    }
}
