using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class MedicineDose
    {
        public int? Id { get; set; }

        public string Dose { get; set; }
        public Drug Drug { get; set; }
        public DateTime Time { get; set; }

    }
}
