using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceBussinessLogic;

namespace BussinesLogic
{
    class SessionManagement : ISessionManagement
    {
        private string currentUser;

        private int login(string userName, string pswdHashed)
        {
            // Verificar que nombre de usuario es correcto
            // Obtener hash del pasword guardado en la base de datos
            // comparar ambos password
            // si son iguales login exitoso, sino falla login
            return 1;
        }

        private void logout()
        {
        }

        // Los parametros currentPswd, newPswd, deben estar encriptados.
        private int changePassword(string currentPswd, string newPswd)
        {
            // Verificar que el pswd actual es correcto
            // guardar nuevo paswd en la base
            return 1;
        }

    }
}
