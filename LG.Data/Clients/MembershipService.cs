using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums;

namespace LG.Data.Clients
{
    public static class MembershipService
    {
        public static async Task<LG.Data.Models.Clients.MembershipPlan>
            GetInfo(System.Int32 planID)
        {
            return await LG.Data.Core.Clients.MembershipDataService.GetInfo(planID);
        }

        public static async Task<LG.Data.Models.Clients.Group>
            Update(LG.Data.Models.Clients.MembershipPlan plan)
        {
            var response = LG.Data.Clients.MembershipService.UpdateIsActive(plan);
            await response;

            var response2 = LG.Data.Clients.MembershipService.UpdateCoverageCode(plan);
            await response2;

            var response3 = LG.Data.Clients.MembershipService.UpdateMembershipName(plan);
            await response3;

            if (response.IsCompleted
                && response2.IsCompleted
                && response3.IsCompleted)
            {
                var businessSettings
                = new LG.Data.Models.Clients.BusinessSettings()
                {
                    FMPM = plan.FamilyMemberPerMonth,
                    PMPM = plan.PerMemberPerMonth,
                    IsActive = plan.IsActive,
                    ClientRID = plan.ClientRID,
                    GroupRID = plan.GroupRID,
                    CorporationRID = plan.CorporationRID,
                    MembershipPlanID = plan.MembershipPlanID,
                };
                var response4 = LG.Data.Business.Settings.Store(
                    BusinessLevel.Membership, businessSettings);

                await response4;
                if (response4.IsCompleted)
                {
                    return await LG.Data.Clients.GroupService.Get(plan.GroupRID);
                }
            }
            return null;
        }

        public static async Task<LG.Data.Models.Clients.MembershipPlan>
            UpdateIsActive(LG.Data.Models.Clients.MembershipPlan plan)
        {
            return await LG.Data.Core.Clients.MembershipDataService.UpdateIsActive(plan);
        }
        public static async Task<LG.Data.Models.Clients.MembershipPlan>
            UpdateMembershipName(LG.Data.Models.Clients.MembershipPlan plan)
        {
            return await LG.Data.Core.Clients.MembershipDataService.UpdateMembershipName(plan);

        }
        public static async Task<LG.Data.Models.Clients.MembershipPlan>
            UpdateCoverageCode(LG.Data.Models.Clients.MembershipPlan plan)
        {
            return await LG.Data.Core.Clients.MembershipDataService.UpdateCoverageCode(plan);

        }


    }
}

// public static async Task<LG.Data.Models.Clients.MembershipPlan>GetInfo(System.Int32 planID)
// public static async Task<LG.Data.Models.Clients.MembershipPlan>UpdateIsActive(LG.Data.Models.Clients.MembershipPlan plan){}
// public static async Task<LG.Data.Models.Clients.MembershipPlan>UpdateMembershipName(LG.Data.Models.Clients.MembershipPlan plan){}
// public static async Task<LG.Data.Models.Clients.MembershipPlan>UpdateCoverageCode(LG.Data.Models.Clients.MembershipPlan plan){}