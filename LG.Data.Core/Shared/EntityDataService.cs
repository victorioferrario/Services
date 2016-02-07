using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums;
using LG.Data.Models.Users;
using LG.Services;
using LG.Services.AMS;
using LG.Services.EMS;
using LG.Services.IMS;
using LG.Services.UMS;
using RTypeEnum = LG.Services.EMS.RTypeEnum;

namespace LG.Data.Core.Shared
{
    public static class EntityDataService
    {

        public static async Task<LG.Data.Models.Members.Entity> InjectIntoEligibiltiyEntityTask(LG.Data.Models.Members.Entity entity)
        {

            var client = ClientConnection.GetImsConnection();
            try
            {
                client.Open();
                var response = await client.StoreMemberEligibilityAsync(new StoreMemberEligibilityRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    ClientRID = entity.ClientRID,
                    EventGUID = Guid.NewGuid(),
                    RequestedByRID = entity.RID,
                    MemberEligibilityDataInput = new MemberEligibilityData_Input()
                    {
                        DTUTC_EffectiveDate = entity.EffectiveDate,
                        CoverageCode = entity.CoverageCode,
                        GroupNumber = entity.GroupNumber,
                        DOB = entity.PersonInfo.Dob,
                        FName = entity.PersonInfo.FName,
                        LName = entity.PersonInfo.LName,
                        Gender = entity.PersonInfo.Gender,
                        MemberNumber = entity.MemberNumber,
                        DTUTC_TerminationDate = entity.EffectiveDate.AddYears(1).ToUniversalTime(),
                        PhoneNumber = entity.Phone,
                        EmailAddress = entity.Email,
                        AddressLine1 = entity.Address.AddressLine1,
                        AddressLine2 = entity.Address.AddressLine2,
                        City = entity.Address.City,
                        State = entity.Address.State,
                        ZipCode = entity.Address.ZipCode,
                        CountryCode = entity.Address.CountryCode,
                        
                    },
                    PropBag = EntityDataService.PropBag
                });
                client.Close();
                entity.IsError = false;
                entity.Events.EventActionResult = ActionResult.Success;
                return entity;
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
        public static async Task<LG.Services.EMS.PersonInfo> LoadPersonInfo(Int64 id)
        {
            var client = ClientConnection.GetEmsConnection();
            var result = new LG.Services.EMS.PersonInfo();
            try
            {
                client.Open();
                var response = await client.LoadPersonInfoAsync(
                    new LoadPersonInfoRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        RID = id
                    });
                result = response.PersonInfo;
            }
            catch (Exception ex)
            {
                client.Abort();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            return result;
        }
        public static async Task<LG.Data.Models.Members.Entity> CreateEntityTask(LG.Data.Models.Members.Entity entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                var response = await client.CreateBEntityAsync(new CreateBEntityRequest
                {
                    MessageGuid = new Guid(),
                    RType = entity.EntityType,
                    IsActive = entity.AccountInfo.IsActive,
                    IsTesting = entity.AccountInfo.IsTesting,
                    PropBag = EntityDataService.PropBag
                });
                client.Close();
                if (response.RID != null) entity.RID = response.RID.Value;
                entity.IsError = false;
                entity.Events.EventActionResult = ActionResult.Success;
                return entity;
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
        public static async Task<LG.Data.Models.Members.Entity> EmailSaveTask(LG.Data.Models.Members.Entity entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                if (entity.Events.EventAction == Models.Enums.Action.Add)
                {
                    #region [@  Add     @]

                    var response = await client.AddEmailAddressAsync(
                        new AddEmailAddressRequest
                        {
                            EmailAddress = entity.EmailAddresses[0],
                            MessageGuid = new Guid(),
                            PropBag = PropBag,
                            RID = entity.RID
                        });
                    client.Close();

                    #endregion
                    
                    client.Close();
                    entity.IsError = false;
                    entity.Events.EventActionResult = ActionResult.Success;
                    return entity;
                }
                else
                {
                    #region [@  Update  @]
                    var response = await client.UpdateEmailAddressAsync(new UpdateEmailAddressRequest
                    {
                        NewEmailAddress = entity.EmailAddresses[1],
                        EmailAddressIDToUpdate = entity.EmailAddresses[0].ID,
                        EmailAddressUsageEnumToUpdate = entity.EmailAddresses[0].EmailAddressUsages[0].EmailAddressUsageEnum,
                        NewEmailAddressUsageEnum = entity.EmailAddresses[1].EmailAddressUsages[0].EmailAddressUsageEnum,
                        IsPrimary = true,
                        MessageGuid = new Guid(),
                        PropBag = PropBag,
                        RID = entity.RID
                    });
                    client.Close();
                    #endregion

                    client.Close();
                    entity.IsError = false;
                    entity.Events.EventActionResult = ActionResult.Success;
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
        public static async Task<LG.Data.Models.Members.Entity> PhoneSaveTask(LG.Data.Models.Members.Entity entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                if (entity.Events.EventAction == Models.Enums.Action.Add)
                {
                    #region [@  Add     @]

                    var response = await client.AddPhoneAsync(
                        new AddPhoneRequest
                        {
                            MessageGuid = Guid.NewGuid(),
                            RID = entity.RID,
                            Phone = entity.Phones[0],
                            PropBag = PropBag
                        });
                    client.Close();

                    #endregion

                    entity.IsError = false;
                    entity.Events.EventActionResult = ActionResult.Success;
                    return entity;
                }
                else
                {
                    #region [@  Update     @]

                    var response = await client.UpdatePhoneAsync(
                        new UpdatePhoneRequest
                        {
                            MessageGuid = new Guid(),
                            RID = entity.RID,
                            NewPhone = entity.Phones[1],
                            IsPrimary = entity.Phones[1].PhoneUsages[0].IsPrimary,
                            PhoneIDToUpdate = entity.Phones[0].ID,
                            NewPhoneUsageEnum = entity.Phones[1].PhoneUsages[0].PhoneUsageEnum,
                            PhoneUsageEnumToUpdate = entity.Phones[0].PhoneUsages[0].PhoneUsageEnum,
                            PropBag = PropBag
                        });
                    client.Close();
                    #endregion

                    entity.IsError = false;
                    entity.Events.EventActionResult = ActionResult.Success;
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
        public static async Task<LG.Data.Models.Members.Entity> AddressSaveTask(LG.Data.Models.Members.Entity entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                if (entity.Events.EventAction == Models.Enums.Action.Add)
                {
                    #region [@  Add     @]

                    var response = await client.AddAddressAsync(
                        new AddAddressRequest()
                        {
                            Address = entity.Addresses[0]
                        });
                    client.Close();

                    #endregion

                    entity.IsError = false;
                    entity.Events.EventActionResult = ActionResult.Success;
                    return entity;
                }
                else
                {
                    #region [@  Update  @]
                    var response = await client.UpdateAddressAsync(
                         new UpdateAddressRequest()
                         {
                             NewAddress = entity.Addresses[1],
                             IsPrimary = entity.Addresses[1].AddressUsages[0].IsPrimary,
                             AddressIDToUpdate = entity.Addresses[0].ID,
                             NewAddressUsageEnum = entity.Addresses[1].AddressUsages[0].AddressUsageEnum,
                             AddressUsageEnumToUpdate = entity.Addresses[0].AddressUsages[0].AddressUsageEnum,
                             MessageGuid = new Guid(),
                             PropBag = EntityDataService.PropBag,
                             RID = entity.RID,
                         });
                    client.Close();
                    #endregion

                    entity.IsError = false;
                    entity.Events.EventActionResult = ActionResult.Success;
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
        public static async Task<LG.Data.Models.Members.Entity> PersonalInfoSaveTask(LG.Data.Models.Members.Entity entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                if (entity.Events.EventAction == Models.Enums.Action.Add)
                {
                    var response = await client.SavePersonInfoAsync(
                        new SavePersonInfoRequest()
                        {
                            MessageGuid = new Guid(),
                            RID = entity.RID,
                            DOB = entity.PersonInfo.Dob,
                            FName = entity.PersonInfo.FName,
                            MName = entity.PersonInfo.MName,
                            LName = entity.PersonInfo.LName,
                            Gender = entity.PersonInfo.Gender,
                            PropBag = PropBag
                        });

                    client.Close();
                    entity.IsError = false;
                    entity.Events.EventActionResult = ActionResult.Success;
                    return entity;
                }
                else
                {
                    var response = await client.SavePersonInfoAsync(
                        new SavePersonInfoRequest()
                        {
                            MessageGuid = new Guid(),
                            RID = entity.RID,
                            DOB = entity.PersonInfo.Dob,
                            FName = entity.PersonInfo.FName,
                            MName = entity.PersonInfo.MName,
                            LName = entity.PersonInfo.LName,
                            Gender = entity.PersonInfo.Gender,
                            PropBag = PropBag
                        });
                    client.Close();
                    entity.IsError = false;
                    entity.Events.EventActionResult = ActionResult.Success;
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
        public static async Task<LG.Data.Models.Members.Entity> SecurityInfoSaveTask(LG.Data.Models.Members.Entity entity)
        {
            var client = ClientConnection.GetAmsConnection();
            try
            {
                client.Open();
                if (entity.Events.EventAction == Models.Enums.Action.Add)
                {
                    var response = await client.CreateLoginAsync(
                        new CreateLoginRequest
                        {
                            MessageGuid = new Guid(),
                            RID = entity.RID,
                            UserName = entity.LoginInfo.UserName,
                            PlainPassword = entity.LoginInfo.PlainPassword,
                            IsTemporaryPassword = entity.LoginInfo.IsTemporaryPassword.HasValue && entity.LoginInfo.IsTemporaryPassword.Value ,
                            IsActive = true,
                            DTUTC_PasswordExpires = new DateTime(1970, 1, 1),
                            PropBag = PropBag
                        });
                    client.Close();
                    entity.IsError = false;
                    entity.Events.EventActionResult = ActionResult.Success;
                    return entity;
                }
                else
                {
                    var response = await client.UpdatePasswordAsync(
                       new UpdatePasswordRequest
                       {
                           MessageGuid = new Guid(),
                           RID = entity.RID,
                           Password = entity.LoginInfo.PlainPassword,
                           IsTemporaryPassword = entity.LoginInfo.IsTemporaryPassword.HasValue && entity.LoginInfo.IsTemporaryPassword.Value,
                           DTUTC_PasswordExpires = new DateTime(1970, 1, 1),
                           PropBag = PropBag
                       });
                    client.Close();
                    entity.IsError = false;
                    entity.Events.EventActionResult = ActionResult.Success;
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

        public static String PropBag = "<PropBag>LG.Data.Core.Shared.EntityDataService</PropBag>";
    }
}
