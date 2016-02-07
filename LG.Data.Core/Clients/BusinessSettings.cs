using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums;
using LG.Services.BSCS;

namespace LG.Data.Core.Clients
{
    public static class BusinessSettings
    {
        public static async Task<LG.Data.Models.Clients.BusinessSettings> Store(
             LG.Data.Models.Clients.BusinessSettings entity)
        {
            var client = LG.Services.ClientConnection.GetBscsConnection();
            var response = await client.StoreBusinessSettingAsync(new StoreBusinessSettingRequest()
            {
                MessageGuid = new Guid(),
                ClientRID = entity.ClientRID,
                PropBag = entity.ProgBag,
                IsActive = entity.IsActive,
                FMPM = entity.FMPM,
                PMPM = entity.PMPM,
                BillingType = entity.BillingType,
                CorporationRID = entity.CorporationRID,
                GroupRID = entity.GroupRID,
                MembershipPlanID = entity.MembershipPlanID
            });
            return new LG.Data.Models.Clients.BusinessSettings()
            {
                ClientRID = entity.ClientRID,
                GroupRID = entity.GroupRID,
                MembershipPlanID = entity.MembershipPlanID,
                CorporationRID = entity.CorporationRID,
                BillingType = entity.BillingType,
                FMPM = entity.FMPM,
                PMPM = entity.PMPM,
                IsActive = entity.IsActive,
                IsError = response.ReturnStatus.IsError,
                Message = response.ReturnStatus.GeneralMessage
            };
        }

        internal static StoreBusinessSettingRequest CreateRequest(LG.Data.Models.Clients.BusinessSettings entity,
            LG.Data.Models.Enums.BusinessLevel level)
        {
            switch (level)
            {
                case BusinessLevel.Client:
                    return new StoreBusinessSettingRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        ClientRID = entity.ClientRID,
                        PropBag = entity.ProgBag,
                        IsActive = entity.IsActive,
                        FMPM = entity.FMPM,
                        PMPM = entity.PMPM,
                        BillingType =entity.BillingType,
                        CorporationRID = entity.CorporationRID,
                    };
                case BusinessLevel.Group:
                    return new StoreBusinessSettingRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        ClientRID = entity.ClientRID,
                        PropBag = entity.ProgBag,
                        IsActive = entity.IsActive,
                        FMPM = entity.FMPM,
                        PMPM = entity.PMPM,
                        BillingType = entity.BillingType,
                        CorporationRID = entity.CorporationRID,
                        GroupRID = entity.GroupRID
                    };
                case BusinessLevel.Membership:
                    return new StoreBusinessSettingRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        ClientRID = entity.ClientRID,
                        PropBag = entity.ProgBag,
                        IsActive = entity.IsActive,
                        FMPM = entity.FMPM,
                        PMPM = entity.PMPM,
                        BillingType = entity.BillingType,
                        CorporationRID = entity.CorporationRID,
                        GroupRID = entity.GroupRID,
                        MembershipPlanID = entity.MembershipPlanID
                    };
                default:
                    return new StoreBusinessSettingRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        ClientRID = entity.ClientRID,
                        PropBag = entity.ProgBag,
                        IsActive = entity.IsActive,
                        FMPM = entity.FMPM,
                        PMPM = entity.PMPM,
                        BillingType = entity.BillingType,
                        CorporationRID = entity.CorporationRID,
                        GroupRID = entity.GroupRID,
                        MembershipPlanID = entity.MembershipPlanID
                    };
            }
        }
        public static async Task<LG.Data.Models.Clients.BusinessSettings> Store(
            LG.Data.Models.Clients.BusinessSettings entity, LG.Data.Models.Enums.BusinessLevel level)
        {
            var client = LG.Services.ClientConnection.GetBscsConnection();
            var response = await client.StoreBusinessSettingAsync(CreateRequest(entity, level));
            return new LG.Data.Models.Clients.BusinessSettings()
            {
                ClientRID = entity.ClientRID,
                GroupRID = entity.GroupRID,
                MembershipPlanID = entity.MembershipPlanID,
                CorporationRID = entity.CorporationRID,
                FMPM = entity.FMPM,
                PMPM = entity.PMPM,
                BillingType = entity.BillingType,
                IsActive = entity.IsActive,
                IsError = response.ReturnStatus.IsError,
                Message = response.ReturnStatus.GeneralMessage
            };
        }

        public static async Task<LG.Data.Models.Clients.BusinessSettings> Load(
             LG.Data.Models.Clients.BusinessSettings entity)
        {
            var client = LG.Services.ClientConnection.GetBscsConnection();
            var response = await client.LoadBusinessSettingsAsync(new LoadBusinessSettingsRequest()
            {
                MessageGuid = new Guid(),
                GroupRID = entity.GroupRID,
                ClientRID = entity.ClientRID,
                MembershipPlanID = entity.MembershipPlanID,
                CorporationRID = entity.CorporationRID
            });
            return new LG.Data.Models.Clients.BusinessSettings()
            {
                ClientRID = entity.ClientRID,
                GroupRID = entity.GroupRID,
                CorporationRID = entity.CorporationRID,
                MembershipPlanID = entity.MembershipPlanID,
                FMPM = response.BusinessSettings.FMPM.HasValue ? response.BusinessSettings.FMPM.Value : 0M,
                PMPM = response.BusinessSettings.PMPM.HasValue ? response.BusinessSettings.PMPM.Value : 0M,
                IsActive = entity.IsActive,
                BillingType = response.BusinessSettings.BillingType,
                IsError = response.ReturnStatus.IsError,
                Message = response.ReturnStatus.GeneralMessage
            };
        }
    }
}
