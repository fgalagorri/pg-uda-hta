using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceBussinessLogic
{
    public interface ICriptographyManagement
    {
        string sha256Encryipt(string clearPswd);
    }
}