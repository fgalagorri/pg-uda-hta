using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public abstract class Event
    {
        protected Event(DateTime time, string description)
        {
            Time = time;
            Description = description;
        }

        public DateTime Time { get; set; }
        public string Description { get; set; }
    }


    public class Complication : Event
    {
        public Complication(DateTime time, string description) : base(time, description)
        { 
        }
    }


    public class Effort : Event
    {
        public Effort(DateTime time, string description) : base(time, description)
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

        public Drug Drug { get; set; }
    }


}
