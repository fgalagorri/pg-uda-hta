using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class TemporaryData
    {
        public int IdTemporaryData { get; set; }
        public decimal weight { get; set; }
        public decimal height { get; set; }
        public int age { get; set; }
        public decimal body_max_index { get; set; }
        public bool smoker { get; set; }
        public bool dyslipidemia { get; set; }
        public bool diabetic { get; set; }
        public bool known_hypertensive { get; set; }
        public decimal fat_percentage { get; set; }
        public decimal muscle_percentage { get; set; }
        public int kcal { get; set; }
    }
}
