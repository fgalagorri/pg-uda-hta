using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceBussinessLogic
{
    interface ISessionManagement
    {
        int login(string userName, string password);

        void logout();

        int changePassword(string currentPass, string newPas);
    }
}
