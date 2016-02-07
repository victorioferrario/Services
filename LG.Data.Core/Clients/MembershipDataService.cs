using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services;
using LG.Services.MPS;

namespace LG.Data.Core.Clients
{
    public static class MembershipDataService
    {
        /// <summary>
        /// This method get detail of MembershipPlan
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.MembershipPlan>
           GetInfo(System.Int32 planID)
        {
            var client = ClientConnection.GetMpsConnection();
            try
            {
                client.Open();
                var response = await client.GetMembershipPlanInfoAsync(
                    new GetMembershipPlanInfoRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        MembershipPlanID = planID,
                    });
                client.Close();
                return new LG.Data.Models.Clients.MembershipPlan
                {
                    MembershipPlanID = planID,
                    MembershipLabel = response.MembershipPlan.Label,
                    BillingType = BillingTypeEnum.PerEmployeePerMonth,
                    CoverageCode = response.MembershipPlan.CoverageCode,
                    FamilyMemberPerMonth = response.MembershipPlan.FMPM,
                    PerMemberPerMonth = response.MembershipPlan.PMPM,
                    IsActive = response.MembershipPlan.IsActive,
                    GroupID = response.MembershipPlan.GroupRID,
                    IsError = true,
                    Message = response.ReturnStatus.GeneralMessage
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.MembershipPlan
                {
                    MembershipPlanID = 0,
                    IsError = true,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// This method creates a new Membership Plan
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.MembershipPlan>
            CreateMembershipPlans(LG.Data.Models.Clients.MembershipPlan plan)
        {
            var client = ClientConnection.GetMpsConnection();
            try
            {
                client.Open();
                var response = await client.CreateMembershipPlanAsync(new CreateMembershipPlanRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    MembershipPlan = new NewMembershipPlanInput()
                    {
                        GroupRID = plan.GroupID,
                        IsActive = plan.IsActive,
                        Label = plan.MembershipLabel,
                        CoverageCode = plan.CoverageCode,
                        BillingType = plan.BillingType,
                        PMPM = plan.PerMemberPerMonth,
                        FMPM = plan.FamilyMemberPerMonth,
                    },
                    PropBag = PropBag
                });
                client.Close();
                plan.MembershipPlanID = response.MembershipPlanID;
                plan.IsError = response.ReturnStatus.IsError;
                plan.Message = response.ReturnStatus.GeneralMessage;
                return plan;
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.MembershipPlan
                {
                    MembershipPlanID = 0,
                    IsError = true,
                    Message = ex.Message
                };
            }
        }

        public static async Task<LG.Data.Models.Clients.MembershipPlan>
            UpdateIsActive(LG.Data.Models.Clients.MembershipPlan plan)
        {
            var client = ClientConnection.GetMpsConnection();
            try
            {
                client.Open();
                var response = await client.SetMembershipPlanIsActiveStatusAsync(
                    new SetMembershipPlanIsActiveStatusRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        IsActive = plan.IsActive,
                        MembershipPlanID = plan.MembershipPlanID,
                        PropBag = PropBag
                    });
                client.Close();
                plan.IsError = response.ReturnStatus.IsError;
                plan.Message = response.ReturnStatus.GeneralMessage;
                return plan;
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.MembershipPlan
                {
                    MembershipPlanID = 0,
                    IsError = true,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// This method updates the Label of membership
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.MembershipPlan>
            UpdateMembershipName(LG.Data.Models.Clients.MembershipPlan plan)
        {
            var client = ClientConnection.GetMpsConnection();
            try
            {
                client.Open();
                var response = await client.UpdateMembershipPlanNameAsync(
                    new UpdateMembershipPlanNameRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        Label = plan.MembershipLabel,
                        MembershipPlanID = plan.MembershipPlanID,
                        PropBag = PropBag
                    });
                client.Close();
                plan.IsError = response.ReturnStatus.IsError;
                plan.Message = response.ReturnStatus.GeneralMessage;
                return plan;
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.MembershipPlan
                {
                    MembershipPlanID = 0,
                    IsError = true,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// This method update the code coverage
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Clients.MembershipPlan>
            UpdateCoverageCode(LG.Data.Models.Clients.MembershipPlan plan)
        {
            var client = ClientConnection.GetMpsConnection();
            try
            {
                client.Open();
                var response = await client.UpdateCoverageCodeAsync(
                    new UpdateCoverageCodeRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    CoverageCode = plan.CoverageCode,
                    MembershipPlanID = plan.MembershipPlanID,
                    PropBag = PropBag
                });
                client.Close();
                plan.IsError = response.ReturnStatus.IsError;
                plan.Message = response.ReturnStatus.GeneralMessage;
                return plan;
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.MembershipPlan
                {
                    MembershipPlanID = 0,
                    IsError = true,
                    Message = ex.Message
                };
            }
        }


        private const System.String PropBag = 
            "<PropBag><Lib>LG.Data.Core</Lib><Class>LG.Data.Core.Clients.MembershipDataService</Class></PropBag>";
    }
}


/*
  public static async Task<LG.Data.Models.Clients.MembershipPlan>
            UpdateIsActive(LG.Data.Models.Clients.MembershipPlan plan){
 
 
 }
 public static async Task<LG.Data.Models.Clients.MembershipPlan>
            UpdateMembershipName(LG.Data.Models.Clients.MembershipPlan plan){
 
 
 }
 * UpdateCoverageCode
 
 */
// UpdateIsActive