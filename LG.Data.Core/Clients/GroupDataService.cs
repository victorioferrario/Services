using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models;
using LG.Data.Models.Clients;
using LG.Data.Models.Enums;
using LG.Services;
using LG.Services.GMS;
using LG.Services.MPS;
using MembershipPlan = LG.Data.Models.Clients.MembershipPlan;

namespace LG.Data.Core.Clients
{
    public static class GroupDataService
    {
        public static Int64 CorporationRID
        {
            get { return 10; }
        }

        public static async Task<LG.Data.Models.Clients.GroupSearch> SearchGroupTask(LG.Data.Models.Clients.GroupSearch entity)
        {
            var client = ClientConnection.GetGmsConnection();
            try
            {
                client.Open();
                var response = await client.SearchGroupAsync(
                    new SearchGroupRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        SearchInput = entity.SearchText,
                        IsIncludeContains = entity.IsIncludeContains,
                        IsIncludeStartsWith = entity.IsIncludeStartsWith
                    });
                client.Close();
                entity.Results = response.Groups.ToList();
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            return entity;
        }

        /// <summary>
        /// This method gets group details
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.Group>
            Get(System.Int64 groupID)
        {
            var client = ClientConnection.GetGmsConnection();
            try
            {
                client.Open();
                var response = await client.GetGroupInfoAsync(
                    new GetGroupInfoRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        GroupRID = groupID
                    });
                client.Close();

                return new LG.Data.Models.Clients.Group()
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = response.ReturnStatus.GeneralMessage,
                    GroupRID = response.Group.GroupRID,
                    CorporationRID = GroupDataService.CorporationRID,
                    ClientRID = response.Group.ClientRID,
                    GroupName = response.Group.GroupName,
                    GroupNumber = response.Group.GroupNumber,
                    ActivationUrl = response.Group.ActivationURL,
                    LogoUrl = response.Group.PathToLogoImage,
                    IsActive = response.Group.IsActive,
                    IsTesting = response.Group.IsTesting,
                    List = ParseList(response.Group.MembershipPlans, response.Group.ClientRID)
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.Group
                {
                    IsError = true,
                    Message = ex.Message.ToString(CultureInfo.InvariantCulture)
                };
            }
        }

        private static List<MembershipPlan> ParseList(List<LG.Services.GMS.MembershipPlan> data, Int64 clientRID)
        {
            var result = new List<MembershipPlan>();
            if (data.Count > 0)
            {
                data.ForEach(item => AddPlan(ref result, item, clientRID));
            }
            return result;
        }

        private static void AddPlan(ref List<MembershipPlan> list, LG.Services.GMS.MembershipPlan plan, Int64 clientRID)
        {
            var data = new MembershipPlan()
            {
                GroupRID = plan.GroupRID,
                ClientRID = clientRID,
                CorporationRID = GroupDataService.CorporationRID,
                MembershipLabel = plan.Label,
                CoverageCode = plan.CoverageCode,
                PerMemberPerMonth = plan.PMPM,
                FamilyMemberPerMonth = plan.FMPM,
                MembershipPlanID = plan.MembershipPlanID,
                BillingType = plan.BillingType == LG.Services.GMS.BillingTypeEnum.PerMemberPerMonth
                ? LG.Services.MPS.BillingTypeEnum.PerMemberPerMonth : LG.Services.MPS.BillingTypeEnum.PerEmployeePerMonth,
                IsActive = plan.IsActive
            };
            list.Add(data);
        }

        /// <summary>
        /// This method creates a New Group.
        /// </summary>
        /// <param name="group">LG.Data.Models.Clients.Group</param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.Group>
            Create(LG.Data.Models.Clients.Group group)
        {
            var client = ClientConnection.GetGmsConnection();
            try
            {
                client.Open();
                var response = await client.CreateGroupAsync(
                    new CreateGroupRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        Group = new LG.Services.GMS.NewGroupInput()
                        {
                            ClientRID = group.ClientRID,
                            GroupName = group.GroupName,
                            GroupNumber = group.GroupNumber,
                            IsActive = group.IsActive,
                            IsTesting = group.IsTesting,
                            ActivationURL = group.ActivationUrl,
                            PathToLogoImage = group.LogoUrl
                        },
                        PropBag = PropBag
                    });
                client.Close();

                return new LG.Data.Models.Clients.Group()
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = response.ReturnStatus.GeneralMessage,
                    GroupRID = response.GroupRID,
                    ClientRID = group.ClientRID,
                    GroupName = group.GroupName,
                    GroupNumber = group.GroupNumber,
                    ActivationUrl = group.ActivationUrl
                };

            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.Group
                {
                    IsError = true,
                    Message = ex.Message.ToString(CultureInfo.InvariantCulture)
                };
            }
        }

        /// <summary>
        /// This method updates Is Active for the group.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.Group>
           UpdateIsActive(LG.Data.Models.Clients.Group group)
        {
            var client = ClientConnection.GetGmsConnection();
            try
            {
                client.Open();
                var response = await client.SetGroupIsActiveStatusAsync(new SetGroupIsActiveStatusRequest()
                {
                    PropBag = PropBag,
                    RID = group.GroupRID,
                    IsActive = group.IsActive,
                    MessageGuid = Guid.NewGuid(),
                });
                client.Close();
                return new LG.Data.Models.Clients.Group()
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = response.ReturnStatus.GeneralMessage,
                    GroupRID = group.GroupRID,
                    IsActive = group.IsActive,
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.Group
                {
                    IsError = true,
                    Message = ex.Message.ToString(CultureInfo.InvariantCulture)
                };
            }
        }

        /// <summary>
        /// This method updates IsTesting for the group.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.Group>
          UpdateIsTesting(LG.Data.Models.Clients.Group group)
        {
            var client = ClientConnection.GetGmsConnection();
            try
            {
                client.Open();
                var response = await client.SetGroupIsTestingStatusAsync(new SetGroupIsTestingStatusRequest
                {
                    PropBag = PropBag,
                    RID = group.GroupRID,
                    IsTesting = group.IsTesting,
                    MessageGuid = Guid.NewGuid(),
                });
                client.Close();
                return new LG.Data.Models.Clients.Group()
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = response.ReturnStatus.GeneralMessage,
                    GroupRID = group.GroupRID,
                    IsTesting = group.IsTesting,
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.Group
                {
                    IsError = true,
                    Message = ex.Message.ToString(CultureInfo.InvariantCulture)
                };
            }
        }

        /// <summary>
        /// This method updates the Group Name
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.Group>
            UpdateGroupName(LG.Data.Models.Clients.Group group)
        {
            var client = ClientConnection.GetGmsConnection();
            try
            {
                client.Open();
                var response = await client.UpdateGroupNameAsync(new UpdateGroupNameRequest
                {
                    MessageGuid = Guid.NewGuid(),
                    RID = group.GroupRID,
                    GroupName = group.GroupName,
                });
                client.Close();
                return new LG.Data.Models.Clients.Group()
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = response.ReturnStatus.GeneralMessage,
                    GroupRID = group.GroupRID,
                    GroupName = group.GroupName
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.Group()
                {
                    IsError = true,
                    Message = ex.ToString(),
                    GroupRID = group.GroupRID,
                    GroupName = group.GroupName
                };
            }
        }

        /// <summary>
        /// This method updates group number
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.Group>
           UpdateGroupNumber(LG.Data.Models.Clients.Group group)
        {
            var client = ClientConnection.GetGmsConnection();
            try
            {
                client.Open();
                var response = await client.UpdateGroupNumberAsync(new UpdateGroupNumberRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    RID = group.GroupRID,
                    GroupNumber = group.GroupNumber,
                });
                client.Close();
                return new LG.Data.Models.Clients.Group()
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = response.ReturnStatus.GeneralMessage,
                    GroupRID = group.GroupRID,
                    GroupNumber = group.GroupNumber
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.Group()
                {
                    IsError = true,
                    Message = ex.ToString(),
                    GroupRID = group.GroupRID,
                    GroupNumber = group.GroupNumber
                };
            }
        }

        /// <summary>
        /// This method updates Logo for a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.Group>
            UpdateGroupLogo(LG.Data.Models.Clients.Group group)
        {
            var client = ClientConnection.GetGmsConnection();
            try
            {
                client.Open();
                var response = await client.UpdateGroupPathToLogoImageAsync(new UpdateGroupPathToLogoImageRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    RID = group.GroupRID,
                    PathToLogoImage = group.LogoUrl,
                });
                client.Close();
                return new LG.Data.Models.Clients.Group()
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = response.ReturnStatus.GeneralMessage,
                    GroupRID = group.GroupRID,
                    LogoUrl = group.LogoUrl
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.Group()
                {
                    IsError = true,
                    Message = ex.ToString(),
                    GroupRID = group.GroupRID,
                    LogoUrl = group.LogoUrl
                };
            }
        }

        /// <summary>
        /// This method updates Activation Url for a Group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.Group>
            UpdateGroupActivationUrl(LG.Data.Models.Clients.Group group)
        {
            var client = ClientConnection.GetGmsConnection();
            try
            {
                client.Open();
                var response = await client.UpdateActivationURLAsync(new UpdateActivationURLRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    RID = group.GroupRID,
                    ActivationURL = group.ActivationUrl,
                });
                client.Close();
                return new LG.Data.Models.Clients.Group()
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = response.ReturnStatus.GeneralMessage,
                    GroupRID = group.GroupRID,
                    ActivationUrl = group.ActivationUrl
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.Group
                {
                    IsError = true,
                    Message = ex.Message.ToString(CultureInfo.InvariantCulture),
                    GroupRID = group.GroupRID,
                    ActivationUrl = group.ActivationUrl
                };
            }
        }

        private const System.String PropBag = "<PropBag><Lib>LG.Data.Core</Lib><Class>LG.Data.Core.Clients.GroupDataService</Class></PropBag>";
    }
}