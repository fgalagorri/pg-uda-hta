using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Investigation
    {
        private string _name;
        private DateTime _creationDate;

        public Investigation()
        {
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateTime creationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

    }
}
