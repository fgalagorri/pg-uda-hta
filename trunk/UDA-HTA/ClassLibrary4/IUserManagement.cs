using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceBussinessLogic
{
    interface IUserManagement
    {

        void createUser(string userName, string password, string rol);

        void deleteUser(int idUser);

        void editUser(string userName, string password, string rol);
        
        ICollection<Entities.User> listUsers();

    }
}
