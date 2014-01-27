using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public abstract class Event
    {
        protected Event()
        {
        }

        protected Event(DateTime time, string description)
        {
            Id = null;
            Time = time;
            Description = description;
        }

        protected Event(long id, DateTime time, string description)
        {
            Id = id;
            Time = time;
            Description = description;
        }

        public long? Id { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
    }


    public class Complication : Event
    {
        public Complication()
        {
        }

        public Complication(DateTime time, string description)
            : base(time, description)
        {
        }

        public Complication(long id, DateTime time, string description)
            : base(id, time, description)
        { 
        }
    }


    public class Effort : Event
    {
        public Effort()
        {
        }

        public Effort(DateTime time, string description)
            : base(time, description)
        {
        }

        public Effort(long id, DateTime time, string description)
            : base(id, time, description)
        {
        }
    }


    public class Medication : Event
    {
        public Medication(DateTime time, Drug drug)
            : base(time, drug.Category + ", " + drug.Active + ", " + drug.Name)
        {
            Drug = drug;
        }

        public Medication(long id, DateTime time, Drug drug)
            : base(id, time, drug.Category + ", " + drug.Active + ", " + drug.Name)
        {
            Drug = drug;
        }

        public string Dose { get; set; }
        public Drug Drug { get; set; }
        public int? MedicineId { get; set; }
    }


}
