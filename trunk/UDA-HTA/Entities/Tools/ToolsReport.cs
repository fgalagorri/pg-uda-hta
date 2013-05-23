using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Tools
{
    public class ToolsReport
    {
        public int DeviceId { get; set; }
        public string ReportId { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? SystolicMax { get; set; }
        public int? SystolicAvg { get; set; }
        public int? SystolicMin { get; set; }

        public int? DiastolicMax { get; set; }
        public int? DiastolicAvg { get; set; }
        public int? DiastolicMin { get; set; }

        public int? HeartRateMax { get; set; }
        public int? HeartRateAvg { get; set; }
        public int? HeartRateMin { get; set; }

        public ToolsPatient Patient { get; set; }
    }
}
