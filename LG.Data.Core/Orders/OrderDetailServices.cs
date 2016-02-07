using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Clinical;
using LG.Data.Models.Enums;
using LG.Services;
using LG.Services.CDMS;
using LG.Services.OMS;

namespace LG.Data.Core.Orders
{
    public static class OrderDetailServices
    {
        public static async Task<LG.Data.Models.Clinical.ActiveConsultation> GetInfo(LG.Data.Models.Clinical.ActiveConsultation entity)
        {
            try
            {
                var response = GetOrderProcessingData(entity);
                await response;
                if (response.IsCompleted)
                {
                    return await GetEncounterlInfo(response.Result);
                }
                if (response.IsFaulted)
                {
                    response.Result.IsError = true;
                    response.Result.Message = response.Result.Message + "|@|response.IsFaulted";
                    return response.Result;
                }
                if (response.IsCanceled)
                {
                    response.Result.IsError = true;
                    response.Result.Message = response.Result.Message + "|@|response.IsFaulted";
                    return response.Result;
                }
            }
            catch (Exception ex)
            {
                entity.IsError = true;
                entity.Message = ex.Message + "|@|Exception ex";
                return entity;
            }
            return null;
        }
        internal static async Task<LG.Data.Models.Clinical.ActiveConsultation> GetEncounterlInfo(LG.Data.Models.Clinical.ActiveConsultation entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response =  client.GetChiefComplaintsByCIDAsync(new GetChiefComplaintsByCIDRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    ConsultationID = entity.ConsultationID
                });
                await response;
                var response2 = client.GetFilesAssociatedWithConsultationAsync(new GetFilesAssociatedWithConsultationRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    ConsultationID = entity.ConsultationID
                });
                await response2;
                if (response.IsCompleted && response2.IsCompleted)
                {
                    entity.EncounterData = new ClinicalData()
                    {
                        ChiefComplaints = response.Result.ChiefComplaints,
                        Files = response2.Result.ListOfFilesAssociatedWithConsultation,
                        IsError = response.Result.ReturnStatus.IsError || response2.Result.ReturnStatus.IsError,
                        Message = string.Format("Response:{0} || Response2:{1}", response.Result.ReturnStatus.GeneralMessage, response2.Result.ReturnStatus.GeneralMessage)
                    };
                }
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            return entity;
        }
        internal static async Task<LG.Data.Models.Clinical.ActiveConsultation> GetOrderProcessingData(LG.Data.Models.Clinical.ActiveConsultation entity)
        {
            var client = ClientConnection.GetOMS_Connection();
            try
            {
                client.Open();
                var response = client.GetOrderStatusAsync(new GetOrderStatusRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    OrderID = entity.ConsultationID
                });
                await response;
                var response2 = client.GetConsultationStatusAsync(new GetConsultationStatusRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    ConsultationID = entity.ConsultationID
                });
                await response2;
                if (response.IsCompleted && response2.IsCompleted)
                {
                    entity.OrderDetails = new LG.Data.Models.Clinical.OrderData()
                    {
                        OrderStatus = response.Result.OrderStatusDetails,
                        ConsultStatus = response2.Result.ConsultationStatusDetails,
                        IsError = response.Result.ReturnStatus.IsError || response2.Result.ReturnStatus.IsError,
                        Message = string.Format("Response:{0} || Response2:{1}", response.Result.ReturnStatus.GeneralMessage, response2.Result.ReturnStatus.GeneralMessage)
                    };
                }
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            return entity;
        }
    }
}
