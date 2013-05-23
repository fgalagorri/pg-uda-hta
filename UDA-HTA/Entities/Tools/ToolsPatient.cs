using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Tools
{
    public class ToolsPatient
    {
        public string PatientId { get; set; }
        public string CI { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public DateTime Birthday { get; set; }
        public Sex? Gender { get; set; }
        public string Address { get; set; }
        public string Neighbour { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public string Telephone { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
    }
}
