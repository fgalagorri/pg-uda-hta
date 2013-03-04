using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceBussinessLogic
{
    interface ICriptographyManagement
    {
        string sha256Encryipt(string clearPswd);

        bool goodPassword(string inputPswd, string savedPswd);
    }
}