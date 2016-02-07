using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Reports
{
    public class ConsultationReports
    {
        public static async Task<LG.Data.Models.Reports.ConsultSummaryResults> ConsultationSummary(
            LG.Data.Models.Reports.ConsultMetrics metrics)
        {
            return await LG.Data.Core.Reports.ConsultationReports.ConsultationSummary(metrics);
        }
    }
}
 