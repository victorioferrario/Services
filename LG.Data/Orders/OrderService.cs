using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Orders
{
    public static class OrderService
    {
        public static async Task<LG.Data.Models.Orders.Order> StartOrder(LG.Data.Models.Orders.Order entity)
        {
            return await LG.Data.Core.Orders.OrderDataService.StartOrder(entity);
        }

        public static async Task<LG.Data.Models.Orders.ConsultationFileAttachment> StoreFile(LG.Data.Models.Orders.ConsultationFileAttachment entity)
        {
            return await LG.Data.Core.Orders.OrderDataService.StoreFile(entity);
        }


        public static async Task<LG.Data.Models.Orders.FilesAssociatedWithConsultation> GetFiles(
            LG.Data.Models.Orders.FilesAssociatedWithConsultation entity)
        {
            return await LG.Data.Core.Orders.OrderDataService.GetFiles(entity);
        }

        public static async Task<LG.Data.Models.Orders.FilesAssociatedWithConsultation> ToggleFile(
            LG.Data.Models.Orders.FilesAssociatedWithConsultation entity)
        {
            return await LG.Data.Core.Orders.OrderDataService.ToggleFile(entity);
        }

        public static async Task<LG.Data.Models.Orders.OrderInProcess> FindItemsInProcess(LG.Data.Models.Orders.OrderInProcess entity)
        {
            return await LG.Data.Core.Orders.OrderDataService.FindItemsInProcess(entity);
        } 
        public static async Task<LG.Data.Models.Orders.Consultations> Consultations(
            LG.Data.Models.Orders.Consultations entity)
        {
            return await LG.Data.Core.Orders.OrderDataService.Consultations(entity);
        }

        public static async Task<LG.Data.Models.Orders.ManuallyAssignOrder> AssignConsultation(
            LG.Data.Models.Orders.ManuallyAssignOrder entity)
        {
            return await LG.Data.Core.Orders.OrderDataService.ManuallyAssignPractitioner(entity);
        }
        public static async Task<LG.Data.Models.Orders.ManuallyAssignOrder> ReAssignConsultation(
            LG.Data.Models.Orders.ManuallyAssignOrder entity)
        {
            return await LG.Data.Core.Orders.OrderDataService.ReAssignConsultation(entity);
        }
    }
}
