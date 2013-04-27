using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceBussinessLogic;
using DataAccess;

namespace BussinesLogic
{
    public class SessionManagement : ISessionManagement
    {
        private string _currentUser;

        public string CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public bool login(string userName, string pswdHashed)
        {
            if (verifyPassword(userName, pswdHashed))
            {
                _currentUser = userName;
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
                dataAccess.connectToDataBase();

                bool ret = dataAccess.updatePassword(userName,newPswd);
                return ret;
            }
            return false;
        }

        private bool verifyPassword(string userName, string pswdHashed)
        {
            // Verificar que el nombre de usuario es correcto
            UdaHtaDataAccess dataAccess = new UdaHtaDataAccess();
            dataAccess.connectToDataBase();

            string pswd = dataAccess.getPassword(userName);

            dataAccess.closeConnectionDataBase();

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
