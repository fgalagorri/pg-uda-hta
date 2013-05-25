using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class TemporaryData
    {
        public int IdTemporaryData { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal BodyMassIndex { get; set; }
        public int Age { get; set; }
        public bool Smoker { get; set; }
        public bool Dyslipidemia { get; set; }
        public bool Diabetic { get; set; }
        public bool Hypertensive { get; set; }
        public decimal FatPercentage { get; set; }
        public decimal MusclePercentage { get; set; }
        public int Kcal { get; set; }
    }
}
