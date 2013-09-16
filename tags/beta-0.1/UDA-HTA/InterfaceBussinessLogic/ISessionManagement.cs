using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceBussinessLogic
{
    public interface ISessionManagement
    {
        bool login(string userName, string password);

        void logout();

        bool changePassword(string userName, string currentPass, string newPas);
    }
}
