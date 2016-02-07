using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Orders;

namespace LG.Data.Orders
{
    public static class Orders
    {
        public static async Task<LG.Data.Models.Orders.OrderInProcess> FindItemsInProcess(LG.Data.Models.Orders.OrderInProcess entity)
        {

            IEnumerable<LG.Services.MPMS.MedicalPractitionerInfo> mdList = await OrderDetails.Get();

            if (!mdList.Any()) return entity;

            entity.OrderAction = Models.Enums.OrderAction.FindConsultationsInProcessState;
            var result = await LG.Data.Core.Orders.OrderDataService.FindItemsInProcess(entity);
            var list = result.ConsultationsFound.Select(item => new ConsultationStatusFoundExpanded
            {
                DrName = "NA",
                AssignedToRID = item.AssignedToRID,
                DTUTC_Created = item.DTUTC_Created,
                ConsultationCustomLabel = item.ConsultationCustomLabel,
                ConsultationID = item.ConsultationID,
                ConsultationName = item.ConsultationName,
                ConsultationProcessingProcessState = item.ConsultationProcessingProcessState,
                ConsultationRecipientAccountID = item.ConsultationRecipientAccountID,
                IsTesting = item.IsTesting,
                ListeningForValue = item.ListeningForValue,
                StateCodeRecipientLocatedDuringConsultation = item.StateCodeRecipientLocatedDuringConsultation,
                ContactInfoOfRecipientDuringConsultation = item.ContactInfoOfRecipientDuringConsultation,
                ProductID = item.ProductID,
                DTUTC_AssignedToRID = item.DTUTC_AssignedToRID,
                ConsultationServicingProcessState = item.ConsultationServicingProcessState
            }).ToList();
            foreach (var item in list)
            {
                foreach (var dr in mdList.Where(dr => dr.MedicalPractitioner.RID == item.AssignedToRID))
                {
                    item.DrName = dr.MedicalPractitioner.PrintedName;
                }
            }
            entity.ConsultationsFoundExpanded = list;
            return entity;
        }

    }


        public class OrderDetails
    {
        internal static async Task<List<LG.Services.MPMS.MedicalPractitionerInfo>> Get()
        {
            var response =  await LG.Data.Doctors.DoctorService.Search();
            return response.Results.ToList();
        }
        private void GetName(LG.Data.Models.Orders.ConsultationStatusFoundExpanded item)
        {
            if (item.AssignedToRID == 0) return;
            foreach (var dr in mdList.Where(dr => dr.MedicalPractitioner.RID == item.AssignedToRID))
            {
                item.DrName = dr.MedicalPractitioner.PrintedName;
            }
        }

            private IEnumerable<LG.Services.MPMS.MedicalPractitionerInfo> mdList { get; set; }

        public async Task<LG.Data.Models.Orders.OrderInProcess> FindItemsInProcess(LG.Data.Models.Orders.OrderInProcess entity)
        {
            mdList =  await Get();
            if (!mdList.Any()) return entity;
            entity.OrderAction = Models.Enums.OrderAction.FindConsultationsInProcessState;
            var result = await LG.Data.Core.Orders.OrderDataService.FindConsultationInProcess(entity);

            try
            {
                var list = result.ConsultationsFound.Select(item => new ConsultationStatusFoundExpanded
                {
                    DrName = "NA",
                    AssignedToRID = item.AssignedToRID,
                    DTUTC_Created = item.DTUTC_Created,
                    ConsultationCustomLabel = item.ConsultationCustomLabel,
                    ConsultationID = item.ConsultationID,
                    ConsultationName = item.ConsultationName,
                    ConsultationProcessingProcessState = item.ConsultationProcessingProcessState,
                    ConsultationRecipientAccountID = item.ConsultationRecipientAccountID,
                    IsTesting = item.IsTesting,
                    ListeningForValue = item.ListeningForValue,
                    StateCodeRecipientLocatedDuringConsultation = item.StateCodeRecipientLocatedDuringConsultation,
                    ContactInfoOfRecipientDuringConsultation = item.ContactInfoOfRecipientDuringConsultation,
                    ProductID = item.ProductID,
                    DTUTC_AssignedToRID = item.DTUTC_AssignedToRID,
                    ConsultationServicingProcessState = item.ConsultationServicingProcessState
                }).ToList();
                list.ForEach(GetName);
                entity.ConsultationsFoundExpanded = list;
            }
            catch (Exception ex)
            {
                entity.IsError = true;
                entity.Message = ex.Message;
            }
          
            return entity;
        }


        public async Task<LG.Data.Models.Orders.OrderInProcess> FindItemsCompleted(LG.Data.Models.Orders.OrderInProcess entity)
        {
            mdList = await Get();
            if (mdList.Any())
            {
                entity.OrderAction = Models.Enums.OrderAction.FindConsultationsInProcessState;
                var result = await LG.Data.Core.Orders.OrderDataService.FindConsultationCompletedByAccountID(entity);
                var list = result.ConsultationsFound.Select(item => new ConsultationStatusFoundExpanded
                {
                    DrName = "NA",
                    AssignedToRID = item.AssignedToRID,
                    DTUTC_Created = item.DTUTC_Created,
                    ConsultationCustomLabel = item.ConsultationCustomLabel,
                    ConsultationID = item.ConsultationID,
                    ConsultationName = item.ConsultationName,
                    ConsultationProcessingProcessState = item.ConsultationProcessingProcessState,
                    ConsultationRecipientAccountID = item.ConsultationRecipientAccountID,
                    IsTesting = item.IsTesting,
                    ListeningForValue = item.ListeningForValue,
                    StateCodeRecipientLocatedDuringConsultation = item.StateCodeRecipientLocatedDuringConsultation,
                    ContactInfoOfRecipientDuringConsultation = item.ContactInfoOfRecipientDuringConsultation,
                    ProductID = item.ProductID,
                    DTUTC_AssignedToRID = item.DTUTC_AssignedToRID,
                    ConsultationServicingProcessState = item.ConsultationServicingProcessState
                }).ToList();
                list.ForEach(GetName);
                entity.ConsultationsFoundExpanded = list;
            }
            return entity;
        }
        public static async Task<LG.Data.Models.Clinical.ActiveConsultation> GetInfo(LG.Data.Models.Clinical.ActiveConsultation entity)
       {
           return await LG.Data.Core.Orders.OrderDetailServices.GetInfo(entity);
       }

    }
}
