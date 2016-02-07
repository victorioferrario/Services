using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Clinical;
using LG.Services;
using LG.Services.CDMS;

namespace LG.Data.Clinical
{
    public static class MessagingService
    {
        /// <summary>
        /// Adds a message to an econsult.
        /// </summary>
        /// <param name="entity" type="LG.Data.Models.Clinical.MessagingEntity"></param>
        /// <returns>LG.Data.Models.Clinical.MessagingEntity</returns>
        public static async Task<MessagingEntity> AddMessageToConsultation(MessagingEntity entity)
        {
            return await LG.Data.Core.Clinical.MessagingDataService.AddMessageToConsultation(entity);
        }
        /// <summary>
        /// Get thread of messages for an econsults.
        /// </summary>
        /// <param name="entity" type="LG.Data.Models.Clinical.MessagingEntity"></param>
        /// <returns>LG.Data.Models.Clinical.MessagingEntity</returns>
        public static async Task<MessagingEntity> GetConsultationMessageExchange(MessagingEntity entity)
        {
            return await LG.Data.Core.Clinical.MessagingDataService.GetConsultationMessageExchange(entity);
        }
        /// <summary>
        /// Toggle Messages in e-consult
        /// </summary>
        /// <param name="entity" type="LG.Data.Models.Clinical.MessageInstance"></param>
        /// <returns></returns>
        public static async Task<bool> ToggleIsHiddenConsultationMessage(MessageInstance entity)
        {
            return await LG.Data.Core.Clinical.MessagingDataService.ToggleIsHiddenConsultationMessage(entity);
        }
    }
}
