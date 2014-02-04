using System;

namespace Entities
{
    public class MedicineDose
    {
        public long? Id { get; set; }

        public string Dose { get; set; }
        public Drug Drug { get; set; }
        public DateTime Time { get; set; }

    }
}
