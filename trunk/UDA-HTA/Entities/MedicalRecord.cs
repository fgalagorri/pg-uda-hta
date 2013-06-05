using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class MedicalRecord
    {
        public string Illness { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }
        public string Comment { get; set; }
    }
}
