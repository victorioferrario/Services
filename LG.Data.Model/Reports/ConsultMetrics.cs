using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.RPS;

namespace LG.Data.Models.Reports
{
    public class Data
    {
        public ConsultMetrics Metrics{get; set;}
        public ConsultSummaryResults Results { get; set; }
    }
    public class ConsultMetrics:LG.Data.Models.BaseModel
    {
            public long MSPRID { get; set; }
            public DateTime DTo { get; set; }
            public long? GroupRID { get; set; }
            public DateTime DFrom { get; set; }
            public long? ClientRID { get; set; }
            public string StateCode { get; set; }
            public long? MedicalPractionerRID { get; set; }
            public bool? IsTestingMedicalPractitioner { get; set; }
    }
    public class ConsultSummaryResults : LG.Data.Models.BaseModel
    {
        public List<ConsultationDetailsRecord> Results { get; set; }
    }
}