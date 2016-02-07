using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Core.Shared;
using LG.Data.Models.Enums;
using LG.Services;
using LG.Services.ACS;
using LG.Services.EMS;

namespace LG.Data.Core.Members
{
    public static class AccountDataService
    {
        public static String PropBag = "<PropBag>LG.Data.Core.Members.AccountDataService</PropBag>";
        public static async Task<LG.Data.Models.Members.Entity> AccountCreateTask(LG.Data.Models.Members.Entity entity)
        {
            var client = ClientConnection.GetAcsConnection();
            try
            {
                client.Open();
                if (entity.Events.EventAction == Models.Enums.Action.Add)
                {
                    #region [@  Add     @]

                    var response = await client.CreateAccountAsync(new CreateAccountRequest()
                    {
                        AccountInfo_Input = entity.AccountInfoInput,
                        MessageGuid = Guid.NewGuid(),
                        PropBag = AccountDataService.PropBag
                    });
                    client.Close();

                    #endregion

                    entity.IsError = false;
                    entity.AccountInfo.AccountID = response.AccountID;
                    entity.Events.EventActionResult = ActionResult.Success;

                    return entity;
                }
                else
                {
                    entity.IsError = true;
                    entity.Message = "Need to set EventAction";
                    entity.Events.EventActionResult = ActionResult.Failed;
                    return entity;
                }
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.Events.EventActionResult = ActionResult.Failed;
                return entity;
            }
        }

        public static async Task<LG.Data.Models.Members.Entity> AccountLoadTask(LG.Data.Models.Members.Entity entity)
        {
            var client = ClientConnection.GetAcsConnection();
            try
            {
                client.Open();
                switch (entity.Events.AccountAction)
                {
                    case AccountAction.LoadCreditCard:
                        #region [@  Method     @]

                        var response = await client.GetCreditCardsOnTheAccountAsync(new GetCreditCardsOnTheAccountRequest()
                        {
                            AccountID = entity.AccountInfo.AccountID,
                            MessageGuid = Guid.NewGuid()
                        });

                        client.Close();
                        entity.IsError = false;
                        entity.Events.AccountActionResult = ActionResult.Success;
                        entity.CreditCards = response.CreditCards;

                        return entity;

                        #endregion
                        break;
                    case AccountAction.LoadDependents:
                        #region [@  Method     @]

                        var response2 = await client.GetPrimaryAndDependentAccountsAsync(new GetPrimaryAndDependentAccountsRequest()
                        {
                            AccountID = entity.AccountInfo.AccountID,
                            MessageGuid = Guid.NewGuid()
                        });
                        client.Close();
                        entity.IsError = false;
                        entity.Events.AccountActionResult = ActionResult.Success;
                        entity.Accounts = response2.Accounts;
                        return entity;

                        #endregion
                        break;
                    default:
                        client.Close();
                        entity.IsError = true;
                        entity.Message = "Failed to Declare AccountAction";
                        entity.Events.AccountActionResult = ActionResult.Failed;
                        return entity;
                }
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.Events.EventActionResult = ActionResult.Failed;
                return entity;
            }

        }


        public static async Task<List<AccountInfoExtended>> LoadDependents(int AccountID)
        {
            var result = new List<AccountInfoExtended>();
            var client = ClientConnection.GetAcsConnection();
            try
            {
                client.Open();
                #region [@  Method     @]
                var response2 = await client.GetPrimaryAndDependentAccountsAsync(new GetPrimaryAndDependentAccountsRequest()
                {
                    AccountID = AccountID,
                    MessageGuid = Guid.NewGuid()
                });
                client.Close();
                result = response2.Accounts;
                #endregion
            }
            catch (Exception ex)
            {
                client.Close();
               
            }

            return result;

        }

        public static async Task<List<CreditCardInfo>> CreditCardLoadTask(Int32 AccountID)
        {
            var result = new List<CreditCardInfo>();
            var client = ClientConnection.GetAcsConnection();
            try
            {
                client.Open();
               
                        #region [@  Method     @]

                        var response = await client.GetCreditCardsOnTheAccountAsync(new GetCreditCardsOnTheAccountRequest()
                        {
                            AccountID = AccountID,
                            MessageGuid = Guid.NewGuid()
                        });

                        client.Close();
                        result= response.CreditCards;

                #endregion
            }
            catch (Exception ex)
            {
                client.Close();

            }

            return result;

        }
        //public static async Task<LG.Data.Models.Members.Entity> AccountLoadTask(int AccountID, AccountAction Action)
        //{
        //    var client = ClientConnection.GetAcsConnection();
        //    try
        //    {
        //        client.Open();
        //        switch (Action)
        //        {
        //            case AccountAction.LoadCreditCard:
        //                #region [@  Method     @]

        //                var response = await client.GetCreditCardsOnTheAccountAsync(new GetCreditCardsOnTheAccountRequest()
        //                {
        //                    AccountID = entity.AccountInfo.AccountID,
        //                    MessageGuid = Guid.NewGuid()
        //                });

        //                client.Close();
        //                entity.IsError = false;
        //                entity.Events.AccountActionResult = ActionResult.Success;
        //                entity.CreditCards = response.CreditCards;

        //                return entity;

        //                #endregion
        //                break;
        //            case AccountAction.LoadDependents:
        //                #region [@  Method     @]

        //                var response2 = await client.GetPrimaryAndDependentAccountsAsync(new GetPrimaryAndDependentAccountsRequest()
        //                {
        //                    AccountID = entity.AccountInfo.AccountID,
        //                    MessageGuid = Guid.NewGuid()
        //                });
        //                client.Close();
        //                entity.IsError = false;
        //                entity.Events.AccountActionResult = ActionResult.Success;
        //                entity.Accounts = response2.Accounts;
        //                return entity;

        //                #endregion
        //                break;
        //            default:
        //                client.Close();
        //                entity.IsError = true;
        //                entity.Message = "Failed to Declare AccountAction";
        //                entity.Events.AccountActionResult = ActionResult.Failed;
        //                return entity;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        client.Close();
        //        entity.IsError = true;
        //        entity.Message = ex.ToString();
        //        entity.Events.EventActionResult = ActionResult.Failed;
        //        return entity;
        //    }
        //}
        public static async Task<LG.Data.Models.Members.Entity> AccountSaveTask(LG.Data.Models.Members.Entity entity)
        {
            var client = ClientConnection.GetAcsConnection();
            try
            {
                client.Open();
                switch (entity.Events.AccountAction)
                {
                    case AccountAction.IsActive:
                        #region [@  Method     @]

                        var response = await client.SetIsActiveAsync(new SetIsActiveRequest()
                        {
                            AccountID = entity.AccountInfo.AccountID,
                            IsActive = entity.AccountInfo.IsActive,
                            MessageGuid = Guid.NewGuid(),
                            PropBag = AccountDataService.PropBag
                        });
                        client.Close();
                        entity.IsError = false;
                        entity.Events.AccountActionResult = ActionResult.Success;
                        return entity;

                        #endregion
                        break;
                    case AccountAction.ParentAccount:
                        #region [@  Method     @]

                        var response2 = await client.SetParentAccountAsync(new SetParentAccountRequest()
                        {
                            AccountID = entity.AccountInfo.AccountID,
                            PrimaryAccountID = entity.AccountInfo.PrimaryAccountID,
                            MessageGuid = Guid.NewGuid(),
                            PropBag = AccountDataService.PropBag
                        });
                        client.Close();
                        entity.IsError = false;
                        entity.Events.AccountActionResult = ActionResult.Success;
                        return entity;

                        #endregion
                        break;
                    case AccountAction.IsSelfManaged:
                        #region [@  Method     @]

                        var response3 = await client.SetIsSelfManagedAsync(new SetIsSelfManagedRequest()
                        {
                            AccountID = entity.AccountInfo.AccountID,
                            IsSelfManaged = entity.AccountInfo.IsTesting,
                            MessageGuid = Guid.NewGuid(),
                            PropBag = AccountDataService.PropBag
                        });
                        client.Close();
                        entity.IsError = false;
                        entity.Events.AccountActionResult = ActionResult.Success;
                        return entity;

                        #endregion
                        break;
                    case AccountAction.IsAutorenewal:
                        #region [@  Method     @]

                        var response4 = await client.SetIsAutorenewalAsync(new SetIsAutorenewalRequest()
                        {
                            AccountID = entity.AccountInfo.AccountID,
                            IsAutoRenewal = entity.AccountInfo.IsAutorenewal,
                            MessageGuid = Guid.NewGuid(),
                            PropBag = AccountDataService.PropBag
                        });
                        client.Close();
                        entity.IsError = false;
                        entity.Events.AccountActionResult = ActionResult.Success;
                        return entity;

                        #endregion
                        break;
                    case AccountAction.IsTesting:
                        #region [@  Method     @]

                        var response5 = await client.SetIsTestingAsync(new SetIsTestingRequest()
                        {
                            AccountID = entity.AccountInfo.AccountID,
                            IsTesting = entity.AccountInfo.IsTesting,
                            MessageGuid = Guid.NewGuid(),
                            PropBag = AccountDataService.PropBag
                        });
                        client.Close();
                        entity.IsError = false;
                        entity.Events.AccountActionResult = ActionResult.Success;
                        return entity;

                        #endregion
                        break;
                    case AccountAction.MembershipPlan:
                        #region [@  Method     @]

                        var response6 = await client.SetMembershipPlanAsync(new SetMembershipPlanRequest()
                        {
                            AccountID = entity.AccountInfo.AccountID,
                            MembershipPlanID = entity.AccountInfo.MembershipPlanID,
                            MessageGuid = Guid.NewGuid(),
                            PropBag = AccountDataService.PropBag
                        });
                        client.Close();
                        entity.IsError = false;
                        entity.Events.AccountActionResult = ActionResult.Success;
                        return entity;

                        #endregion
                        break;
                    case AccountAction.ExpirationDate:
                        #region [@  Method     @]

                        var response7 = await client.SetExpirationDateAsync(new SetExpirationDateRequest()
                        {
                            AccountID = entity.AccountInfo.AccountID,
                            DTUTC_Expiration = entity.AccountInfo.DTUTC_Expires,
                            MessageGuid = Guid.NewGuid(),
                            PropBag = AccountDataService.PropBag
                        });
                        client.Close();
                        entity.IsError = false;
                        entity.Events.AccountActionResult = ActionResult.Success;
                        return entity;

                        #endregion
                        break;
                    case AccountAction.CreditCard:
                        #region [@  Method     @]

                        var response8 = await client.SaveCreditCardInformationAsync(new SaveCreditCardInformationRequest()
                        {
                            CreditCardInfo_Input = entity.CreditCardInfoInput,
                            MessageGuid = Guid.NewGuid(),
                            PropBag = AccountDataService.PropBag
                        });
                        client.Close();
                        entity.IsError = false;
                        entity.Events.AccountActionResult = ActionResult.Success;
                        return entity;
                        #endregion
                        break;
                    case AccountAction.AccountInfo:
                        #region [@  Method     @]

                        var response9 = await client.GetAccountInfoAsync(new GetAccountInfoRequest()
                        {
                            AccountID = entity.AccountInfo.AccountID,
                            RID = entity.RID,
                            MessageGuid = Guid.NewGuid(),
                        });
                        client.Close();
                        entity.IsError = false;
                        entity.Events.EventActionResult = ActionResult.Success;
                        return entity;

                        #endregion
                        break;
                    default:
                        client.Close();
                        entity.IsError = true;
                        entity.Message = "Failed to Declare AccountAction";
                        entity.Events.AccountActionResult = ActionResult.Failed;
                        return entity;
                }
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.Events.EventActionResult = ActionResult.Failed;
                return entity;
            }
        }
    }
}
