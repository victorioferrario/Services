using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Core.Clients;
using LG.Data.Models.Enums;

namespace LG.Data.Business
{
    public static class Settings
    {

        public static async Task<LG.Data.Models.Clients.BusinessSettings>
            Load(
            LG.Data.Models.Enums.BusinessLevel level,
            LG.Data.Models.Clients.BusinessSettings entity)
        {
            switch (level)
            {
                case BusinessLevel.Client:
                    return await LoadClientLevel(entity);
                case BusinessLevel.Group:
                    return await LoadGroupLevel(entity);
                case BusinessLevel.Membership:
                    return await LoadMembershipLevel(entity);
                default:
                    return await LoadClientLevel(entity);
            }
        }

        internal static async Task<LG.Data.Models.Clients.BusinessSettings>
            LoadClientLevel(LG.Data.Models.Clients.BusinessSettings entity)
        {
            var response = LG.Data.Core.Clients.BusinessSettings.Load(
                new LG.Data.Models.Clients.BusinessSettings()
                {
                    ClientRID = entity.ClientRID,
                    CorporationRID = entity.CorporationRID
                });
            await response;
            return response.Result;
        }

        internal static async Task<LG.Data.Models.Clients.BusinessSettings>
            LoadGroupLevel(LG.Data.Models.Clients.BusinessSettings entity)
        {
            var response = LG.Data.Core.Clients.BusinessSettings.Load(
                new LG.Data.Models.Clients.BusinessSettings()
                {
                    ClientRID = entity.ClientRID,
                    GroupRID = entity.GroupRID,
                    CorporationRID = entity.CorporationRID
                });
            await response;
            return response.Result;
        }
        internal static async Task<LG.Data.Models.Clients.BusinessSettings>
            LoadMembershipLevel(LG.Data.Models.Clients.BusinessSettings entity)
        {
            var response = LG.Data.Core.Clients.BusinessSettings.Load(
                new LG.Data.Models.Clients.BusinessSettings()
                {
                    MembershipPlanID = entity.MembershipPlanID,
                    ClientRID = entity.ClientRID,
                    GroupRID = entity.GroupRID,
                    CorporationRID = entity.CorporationRID
                });
            await response;
            return response.Result;
        }

        public static async Task<LG.Data.Models.Clients.BusinessSettings>
            Store(LG.Data.Models.Enums.BusinessLevel level,LG.Data.Models.Clients.BusinessSettings entity)
        {
            switch (level)
            {
                case BusinessLevel.Client:
                    return await StoreClientLevel(entity);
                case BusinessLevel.Group:
                    return await StoreGroupLevel(entity);
                case BusinessLevel.Membership:
                    return await StoreMembershipPlanLevel(entity);
                case BusinessLevel.All:

                    var response = StoreClientLevel(entity);await response;

                    var response2 = StoreGroupLevel(entity);await response2;

                    var response3 = StoreMembershipPlanLevel(entity);await response3;

                    if (response.IsCompleted && response2.IsCompleted && response3.IsCompleted)
                    {
                        return await LoadMembershipLevel(entity);
                    }
                    else
                    {
                        return null;
                    }
             }
            return null;
        }

        internal static async Task<LG.Data.Models.Clients.BusinessSettings> StoreClientLevel(
           LG.Data.Models.Clients.BusinessSettings entity)
        {
            var response = LG.Data.Core.Clients.BusinessSettings.Store(
                new LG.Data.Models.Clients.BusinessSettings()
                {
                    ClientRID = entity.ClientRID,
                    CorporationRID = entity.CorporationRID,
                    IsActive = true,
                    FMPM = entity.FMPM,
                    PMPM = entity.PMPM,
                    BillingType = entity.BillingType,
                }, BusinessLevel.Client);
            await response; return entity;
        }
        internal static async Task<LG.Data.Models.Clients.BusinessSettings> StoreGroupLevel(
            LG.Data.Models.Clients.BusinessSettings entity)
        {
            var response = LG.Data.Core.Clients.BusinessSettings.Store(
                new LG.Data.Models.Clients.BusinessSettings() 
            {
                GroupRID = entity.GroupRID,
                ClientRID = entity.ClientRID,
                CorporationRID = entity.CorporationRID,
                IsActive = entity.IsActive,
                FMPM = entity.FMPM,
                PMPM = entity.PMPM,
                BillingType = entity.BillingType,
            }, BusinessLevel.Group);
            await response; return entity;
        }
        internal static async Task<LG.Data.Models.Clients.BusinessSettings> StoreMembershipPlanLevel(
           LG.Data.Models.Clients.BusinessSettings entity)
        {
            var response = LG.Data.Core.Clients.BusinessSettings.Store(
                new LG.Data.Models.Clients.BusinessSettings()
                {
                    GroupRID = entity.GroupRID,
                    ClientRID = entity.ClientRID,
                    CorporationRID = entity.CorporationRID,
                    MembershipPlanID = entity.MembershipPlanID,
                    IsActive = true,
                    FMPM = entity.FMPM,
                    PMPM = entity.PMPM,
                    BillingType = entity.BillingType
                });
            await response;
            return entity;
        }
    }
}
