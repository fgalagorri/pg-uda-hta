using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class User
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; } //encriptado
        public string Role { get; set; }
    }
}
