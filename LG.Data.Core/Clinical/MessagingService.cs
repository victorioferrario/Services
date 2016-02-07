using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Clinical;
using LG.Services;
using LG.Services.CDMS;
using LG.Services.IVR.AUDIO;

namespace LG.Data.Core.Clinical
{
    public static  class MessagingDataService
    {
        public static string Propbag = "<PropBag>LG.Data.Core.Clinical.MessagingDataService</PropBag>";
        /// <summary>
        /// Adds a message to an econsult.
        /// </summary>
        /// <param name="entity" type="LG.Data.Models.Clinical.MessagingEntity"></param>
        /// <returns>LG.Data.Models.Clinical.MessagingEntity</returns>
        public static async Task<MessagingEntity> AddMessageToConsultation(MessagingEntity entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.AddMessageToConsultationAsync(new AddMessageToConsultationRequest()
                    {
                        OrderID = entity.OrderID,
                        SendByRID = entity.SenderID,
                        MemberRID = entity.MemberRID,
                        ConsultationID = entity.ConsultationID,
                        MessageGuid = Guid.NewGuid(),
                        Message = entity.NewMessage,
                        PropBag = Propbag
                    });
                entity.NewMessageID = response.ID;
                entity.IsError = response.ReturnStatus.IsError;
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
        /// <summary>
        /// Get thread of messages for an econsults.
        /// </summary>
        /// <param name="entity" type="LG.Data.Models.Clinical.MessagingEntity"></param>
        /// <returns>LG.Data.Models.Clinical.MessagingEntity</returns>
        public static async Task<MessagingEntity> GetConsultationMessageExchange(MessagingEntity entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.GetConsultationMessageExchangeAsync(new GetConsultationMessageExchangeRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        ConsultationID = entity.ConsultationID
                    });
                entity.IsError = response.ReturnStatus.IsError;
                entity.Messages = response.ConsultationMessages.ToList();
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
        /// <summary>
        /// Toggle Messages in e-consult
        /// </summary>
        /// <param name="entity" type="LG.Data.Models.Clinical.MessageInstance"></param>
        /// <returns></returns>
        public static async Task<bool> ToggleIsHiddenConsultationMessage(MessageInstance entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.ToggleIsHiddenConsultationMessageAsync(new ToggleIsHiddenConsultationMessageRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        RecordID = entity.RecordID,
                        IsHidden = entity.IsHidden,
                        PropBag = Propbag
                    });
                entity.IsError = response.ReturnStatus.IsError;
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
            return !entity.IsError;
        }
    }
}
