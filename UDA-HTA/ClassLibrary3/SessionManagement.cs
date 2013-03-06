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
        private string currentUser;

        public int login(string userName, string pswdHashed)
        {
            // Verificar que el nombre de usuario es correcto
            UdaHtaDataAccess dataAccess = new UdaHtaDataAccess();
            dataAccess.connectToDataBase();

            bool exist = dataAccess.existUser(userName);

            dataAccess.closeConnectionDataBase();
            // Obtener hash del pasword guardado en la base de datos
            // comparar ambos password
            // si son iguales login exitoso, sino falla login
            return 1;
        }

        public void logout()
        {
        }

        // Los parametros currentPswd, newPswd, deben estar encriptados.
        public int changePassword(string currentPswd, string newPswd)
        {
            // Verificar que el pswd actual es correcto
            // guardar nuevo paswd en la base
            return 1;
        }

    }
}
