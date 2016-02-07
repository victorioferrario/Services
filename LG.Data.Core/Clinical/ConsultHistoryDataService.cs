using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Clinical;
using LG.Services;
using LG.Services.CDMS;

namespace LG.Data.Core.Clinical
{
    public static class ConsultHistoryDataService
    {
        public static async Task<LG.Data.Models.Clinical.ConsultationHistoryItem> GetDisposition(Int32 entity)
        {
            var data =
                await
                    LG.Data.Core.Orders.OrderDetailServices.GetEncounterlInfo(new LG.Data.Models.Clinical.ActiveConsultation() {ConsultationID = entity});
            var result = new ConsultationHistoryItem
            {
                ConsultationID = entity,
                EncounterData = data.EncounterData,
                OrderDetails = data.OrderDetails,
                ListDiagnoses = await GetDiagnosis(entity),
                PlanOfCare = await GetPlanOfCareLegal(entity)
            };
            return result;
        }

       
        internal static async Task<IEnumerable<LG.Services.CDMS.Diagnosis>> GetDiagnosis(Int32 entity)
        {
            var result = new List<LG.Services.CDMS.Diagnosis>();
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.GetDiagnosisRecordsAsync(new GetDiagnosisRecordsRequest()
                    {
                        ConsultationID = entity,
                        MessageGuid = Guid.NewGuid()
                    });
                result = response.ListOfDiagnosisRecords;
            }
            catch (Exception ex)
            {
                client.Abort();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            return result;
        }

        internal static async Task<LG.Services.CDMS.PlanOfCareLegal> GetPlanOfCareLegal(Int32 entity)
        {
            var result = new PlanOfCareLegal();
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.GetPlanOfCareAsync(new GetPlanOfCareRequest()
                    {
                        ConsultationID = entity,
                        MessageGuid = Guid.NewGuid()
                    });
                result = response.PlanOfCare;
            }
            catch (Exception ex)
            {
                client.Abort();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            return result;
        }
    }
}
