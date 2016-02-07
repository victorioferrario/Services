using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Clients;
using LG.Data.Models.Enums;

namespace LG.Data.Clients
{
    public static class
        GroupService
    {
        public static async Task<LG.Data.Models.Clients.Group>
            Get(System.Int64 groupID)
        {
            return await LG.Data.Core.Clients.GroupDataService.Get(groupID);
        }

        public static async Task<LG.Data.Models.Clients.GroupSearch> SearchByGroup(
            LG.Data.Models.Clients.GroupSearch entity)
        {
            return await LG.Data.Core.Clients.GroupDataService.SearchGroupTask(entity);
        }


        /// <summary>
        /// This method creates a Group for a Client
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.Group>
            CreateGroup(LG.Data.Models.Clients.Group group)
        {
            return await LG.Data.Core.Clients.GroupDataService.Create(group);
        }

        internal static
            LG.Data.Models.Clients.BusinessSettings
            InitializeBussinsessSettingsPerGroup(LG.Data.Models.Clients.Group group)
        {
            return new LG.Data.Models.Clients.BusinessSettings()
                {
                    FMPM = 0M,
                    PMPM = 0M,
                    IsActive = group.IsActive,
                    ClientRID = group.ClientRID,
                    GroupRID = group.GroupRID,
                    CorporationRID = group.CorporationRID,
                    MembershipPlanID = 0
                };
        }

        internal static
            void InitializeBussinsessSettingsPerMembership(
               ref List<BusinessSettings> list,
            LG.Data.Models.Clients.MembershipPlan item,
            LG.Data.Models.Clients.Group group)
        {
            var businessSettings
                = new LG.Data.Models.Clients.BusinessSettings()
            {
                FMPM = 0M,
                PMPM = 0M,
                IsActive = true,
                ClientRID = group.ClientRID,
                GroupRID = group.GroupRID,
                CorporationRID = group.CorporationRID,
                MembershipPlanID = item.MembershipPlanID,
            };
            list.Add(businessSettings);
        }

        /// <summary>
        /// This Method creates all the default membership plans.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>

        public static async Task<LG.Data.Models.Clients.Group>
            CreateGroupMembershipPlans(LG.Data.Models.Clients.Group group)
        {
            var plans = new LG.Data.Utilities.Clients.GenericMembershipPlans(
                group.GroupRID);

            var response =
                LG.Data.Core.Clients.MembershipDataService.CreateMembershipPlans(plans.EmployeeBasic);

            await response;

            var response2 =
               LG.Data.Core.Clients.MembershipDataService.CreateMembershipPlans(plans.EmployeeSpouse);

            await response2;

            var response3 =
              LG.Data.Core.Clients.MembershipDataService.CreateMembershipPlans(plans.EmployeeChildren);

            await response3;

            var response4 =
             LG.Data.Core.Clients.MembershipDataService.CreateMembershipPlans(plans.EmployeeFamily);

            await response4;

            if (response.IsCompleted
                && response2.IsCompleted
                && response3.IsCompleted
                && response4.IsCompleted)
            {

                var list = new List<BusinessSettings>();

                InitializeBussinsessSettingsPerMembership(ref list,
                    response.Result, group);

                InitializeBussinsessSettingsPerMembership(ref list,
                    response2.Result, group);

                InitializeBussinsessSettingsPerMembership(ref list,
                    response3.Result, group);

                InitializeBussinsessSettingsPerMembership(ref list,
                    response4.Result, group);

                var responseSettings0 = LG.Data.Business.Settings.Store(
                    BusinessLevel.Membership, list[0]);
                await responseSettings0;

                var responseSettings1 = LG.Data.Business.Settings.Store(
                    BusinessLevel.Membership, list[1]);
                await responseSettings1;

                var responseSettings2 = LG.Data.Business.Settings.Store(
                    BusinessLevel.Membership, list[2]);
                await responseSettings2;

                var responseSettings3 = LG.Data.Business.Settings.Store(
                   BusinessLevel.Membership, list[3]);

                await responseSettings3;

                if (responseSettings0.IsCompleted
                    && responseSettings1.IsCompleted
                    && responseSettings2.IsCompleted
                    && responseSettings3.IsCompleted)
                {
                    return await LG.Data.Core.Clients.GroupDataService.Get(group.GroupRID);
                }
            }
            return null;
        }
        public static async Task<LG.Data.Models.Clients.Group>
           UpdateGroupInformation(LG.Data.Models.Clients.Group group)
        {
            var response
                = UpdateName(group);
            await response;

            var response2
                = UpdateNumber(group);

            await response2;

            var response3
                = UpdateIsActive(group);

            await response3;

            var response4
                = UpdateIsTesting(group);

            await response4;

            var response5
                = UpdateActivationUrl(group);
            await response5;

            var responseSettings0 = LG.Data.Business.Settings.Store(
                BusinessLevel.Group, InitializeBussinsessSettingsPerGroup(group));


            await responseSettings0;

            if (response.IsCompleted
                && response2.IsCompleted
                && response3.IsCompleted
                && response4.IsCompleted
                && response5.IsCompleted
                && responseSettings0.IsCompleted)
            {
                return await LG.Data.Core.Clients.GroupDataService.Get(group.GroupRID);
            }
            return null;
        }
        public static async Task<LG.Data.Models.Clients.Group>
            UpdateIsActive(LG.Data.Models.Clients.Group group)
        {
            return await LG.Data.Core.Clients.GroupDataService.UpdateIsActive(group);
        }
        public static async Task<LG.Data.Models.Clients.Group>
            UpdateIsTesting(LG.Data.Models.Clients.Group group)
        {
            return await LG.Data.Core.Clients.GroupDataService.UpdateIsActive(group);
        }
        public static async Task<LG.Data.Models.Clients.Group>
            UpdateName(LG.Data.Models.Clients.Group group)
        {
            return await LG.Data.Core.Clients.GroupDataService.UpdateGroupName(group);
        }
        public static async Task<LG.Data.Models.Clients.Group>
            UpdateNumber(LG.Data.Models.Clients.Group group)
        {
            return await LG.Data.Core.Clients.GroupDataService.UpdateGroupNumber(group);
        }
        public static async Task<LG.Data.Models.Clients.Group>
            UpdateLogo(LG.Data.Models.Clients.Group group)
        {
            return await LG.Data.Core.Clients.GroupDataService.UpdateGroupLogo(group);
        }
        public static async Task<LG.Data.Models.Clients.Group>
            UpdateActivationUrl(LG.Data.Models.Clients.Group group)
        {
            return await LG.Data.Core.Clients.GroupDataService.UpdateGroupActivationUrl(group);
        }



    }
}
