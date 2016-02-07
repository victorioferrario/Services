using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.RPS;

namespace LG.Data.Core.Reports
{
    public static class ConsultationReports
    {
        public static async Task<LG.Data.Models.Reports.ConsultSummaryResults> ConsultationSummary(
            LG.Data.Models.Reports.ConsultMetrics metrics)
        {
            var client = LG.Services.ClientConnection.GetRps_Connection();
            var results = new LG.Data.Models.Reports.ConsultSummaryResults();
            try
            {
                client.Open();
                var response = await client.GetConsultationSummaryReportAsync(
                    new GetConsultationSummaryReportRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    MSPRID = metrics.MSPRID,
                    GroupRID = metrics.GroupRID,
                    ClientRID = metrics.ClientRID,
                    CorporationRID = metrics.CorporationRID,
                    DTUTC_From = metrics.DFrom,
                    DTUTC_To = metrics.DTo.ToUniversalTime(),
                    //IsTestingMedicalPractitioner = metrics.IsTestingMedicalPractitioner,
                    MedicalPractitionerRID = metrics.MedicalPractionerRID,
                    StateCode = metrics.StateCode
                });
                results.Results = response.ConsultationDetailsRecords;
                
            }
            catch (Exception ex)
            {
                client.Abort();
                results.IsError = true;
                results.Message = "Error" + ex.ToString();
            }
            finally
            {
                client.Close();
            }
            return results;
        }
    }
    
}
