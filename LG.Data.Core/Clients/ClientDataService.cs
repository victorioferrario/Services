using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Core.Clients.Helpers;
using LG.Data.Models.Clients;
using LG.Services;
using LG.Services.CMS;
using LG.Services.EMS;
using LG.Services.GMS;
using EmailAddressUsageEnum = LG.Services.CMS.EmailAddressUsageEnum;
using PhoneUsageEnum = LG.Services.CMS.PhoneUsageEnum;

namespace LG.Data.Core.Clients
{
    public static class ClientDataService
    {
        public static async Task<LG.Data.Models.Clients.EligibilitySettings> 
            UpdateEligibilityInfo(LG.Data.Models.Clients.EligibilitySettings entity)
        {
            var client = ClientConnection.GetCmsConnection();
            try
            {
                client.Open();
                var response = await client.UpdateEligibilityDataConfigurationAsync(
                    new UpdateEligibilityDataConfigurationRequest()
                    {
                        MessageGuid = entity.MessageGuid,
                        HavingFMEN = entity.HavingFMEN,
                        HavingPMEN = entity.HavingPMEN,
                        ClientRID = entity.ClientRID,
                        IsSendingFMData = entity.IsSendingFMData,
                        PropBag = entity.PropBag
                    });
                client.Close();
                entity.IsError = false;
                entity.Message = response.ReturnStatus.GeneralMessage;
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = false;
                entity.Message = ex.Message;
                return entity;
            }
        }


        public static async Task<ResponseClient> GetClientInfo(Int64 RID)
        {
            var client = ClientConnection.GetCmsConnection();
            try
            {
                client.Open();
                var response = await client.GetClientInfoAsync(
                    new GetClientInfoRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        RID = RID
                    });
                client.Close();
                return new ResponseClient()
                {
                    ServiceResponse = response,
                    IsError = false
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new ResponseClient()
                {
                    IsError = true,
                    Message = ex.Message
                };
            }
        }


        static async Task<CreateClientWithDefaultGroupResponse> ClientWithGroupTask(
              LG.Services.CMS.ClientInfoInput entity)
        {
            var client = ClientConnection.GetCmsConnection();
            return await client.CreateClientWithDefaultGroupAsync(
                new CreateClientWithDefaultGroupRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    CorporationRID = ClientConnection.CorporationId,
                    Client = new ClientInfoInput()
                    {
                        Name = entity.Name,
                        WebsiteURL = entity.WebsiteURL,
                        Addresses = entity.Addresses,
                        ClientContacts = entity.ClientContacts,
                        Phones = new List<Services.CMS.PhoneInput>(),
                        EmailAddresses = new List<Services.CMS.EmailAddressInput>(),
                        IsActive = entity.IsActive,
                        IsTesting = entity.IsTesting,
                        EligibilityDataConfiguration = entity.EligibilityDataConfiguration,

                    },
                    PropBag = Helpers.Settings.PropBag
                });
        }
        public static async Task<Boolean> ActivateTask(LG.Services.EMS.Contact contact)
        {
            var client = ClientConnection.GetEmsConnection();
            var result = await client.SetContactUsageIsActiveStatusAsync(new SetContactUsageIsActiveStatusRequest()
            {
                IsActive = true,
                ContactRID = contact.RID,
                MessageGuid = Guid.NewGuid(),
                ContactForRID = contact.ContactForRID,
                ContactUsageEnum = contact.ContactUsages[0].ContactUsageEnum,
                PropBag = Helpers.Settings.PropBag
            });
            return !result.ReturnStatus.IsError;
        }
        public static async Task<ResponseClient> ClientWithGroup(
              LG.Services.CMS.ClientInfoInput entity)
        {
            var result = ClientWithGroupTask(entity);
            await result;
            if (result.IsCompleted)
            {
                var client = ClientConnection.GetEmsConnection();
                client.Open();
                var list = await client.LoadContactsAsync(new LG.Services.EMS.LoadContactsRequest()
                {
                    IsActive = false,
                    RID = result.Result.ClientRID,
                    MessageGuid = Guid.NewGuid()
                });
                list.Contacts.ForEach(item => ActivateTask(item));

               return await GetClientInfo(result.Result.ClientRID);
            }
            return null;


        }
    }
    namespace Helpers
    {
        public static class Settings
        {
            public static String PropBag
            {
                get { return "<PropBag>LG.Data.Core.Clients</PropBag>"; }
            }
        }
        public static class Connection
        {
            public static LG.Services.CMS.ClientManagementServiceClient Get
            {
                get { return ClientConnection.GetCmsConnection(); }
            }
        }
    }
    namespace Details
    {



        //public class GeneralInfoObject : LG.Data.Models.BaseModel
        //{
        //    public Action EventAction { get; set; }
        //    public Int64 RID { get; set; }

        //    public System.Boolean IsActive { get; set; }
        //    public System.Boolean IsTesting { get; set; }

        //    public System.String LogoPath { get; set; }
        //    public System.String WebsiteUrl { get; set; }

        //    public GeneralInfoObject() : base() { }

        //    public LG.Services.CMS.SetClientIsActiveStatusResponse IsActiveStatusResponse { get; set; }
        //    public LG.Services.CMS.SetClientIsTestingStatusResponse IsTestingStatusResponse { get; set; }
        //    public LG.Services.CMS.UpdateClientPathToLogoImageResponse UpdateLogoPath { get; set; }
        //    public LG.Services.CMS.UpdateWebsiteURLResponse UpdateWebsiteUrlResponse { get; set; }

        //}




        //public static class General
        //{

        //    public static async Task<GeneralInfoObject> SetIsActive(GeneralInfoObject entity)
        //    {
        //        entity.IsActiveStatusResponse = await GeneralSettings.SetIsActive(entity.RID, entity.IsActive);
        //        return entity;
        //    }

        //    public static async Task<GeneralInfoObject> SetIsTesting(GeneralInfoObject entity)
        //    {
        //        entity.IsTestingStatusResponse = await GeneralSettings.SetIsTesting(entity.RID, entity.IsTesting);
        //        return entity;
        //    }

        //    public static async Task<GeneralInfoObject> SaveLogo(GeneralInfoObject entity)
        //    {
        //        entity.UpdateLogoPath = await GeneralSettings.SaveLogo(entity.RID, entity.LogoPath);
        //        return entity;
        //    }

        //    public static async Task<GeneralInfoObject> SaveUrl(GeneralInfoObject entity)
        //    {
        //        entity.UpdateWebsiteUrlResponse = await GeneralSettings.SaveUrl(entity.RID, entity.WebsiteUrl);
        //        return entity;
        //    }
        //}

        //public enum Action
        //{
        //    None = 0,
        //    Add = 1,
        //    Update = 2,
        //    Remove = 3
        //}

        //public class EmailObject : LG.Data.Models.BaseModel
        //{
        //    public Action EventAction { get; set; }
        //    public Int64 RID { get; set; }
        //    public LG.Services.CMS.EmailAddress Value { get; set; }
        //    public EmailObject() : base() { }
        //    public AddClientEmailAddressResponse AddResponse { get; set; }
        //    public UpdateClientEmailAddressResponse UpdateResponse { get; set; }
        //    public RemoveClientEmailAddressResponse RemoveResponse { get; set; }
        //}
        //public static class Email
        //{
        //    public static async Task<EmailObject> Run(EmailObject entity)
        //    {
        //        var client = Connection.Get;
        //        switch (entity.EventAction)
        //        {
        //            case Action.Add:
        //                entity.AddResponse = await client.AddClientEmailAddressAsync(
        //                   new AddClientEmailAddressRequest()
        //                   {
        //                       RID = entity.RID,
        //                       MessageGuid = Guid.NewGuid(),
        //                       EmailAddress = entity.Value,
        //                       PropBag = Helpers.Settings.PropBag
        //                   });
        //                break;
        //            case Action.Update:
        //                if (entity.Value.ID.HasValue)
        //                    entity.UpdateResponse = await client.UpdateClientEmailAddressAsync(
        //                        new UpdateClientEmailAddressRequest()
        //                        {
        //                            RID = entity.RID,
        //                            MessageGuid = Guid.NewGuid(),
        //                            NewEmailAddress = entity.Value,
        //                            EmailAddressIDToUpdate = entity.Value.ID.Value,
        //                            EmailAddressUsageEnumToUpdate = entity.Value.EmailAddressUsages[0].EmailAddressUsageEnum,
        //                            NewEmailAddressUsageEnum = EmailAddressUsageEnum.Group,
        //                            PropBag = Helpers.Settings.PropBag
        //                        });
        //                break;
        //            case Action.Remove:
        //                if (entity.Value.ID.HasValue)
        //                    entity.RemoveResponse = await client.RemoveClientEmailAddressAsync(new RemoveClientEmailAddressRequest()
        //                    {
        //                        RID = entity.RID,
        //                        MessageGuid = Guid.NewGuid(),
        //                        EmailAddressID = entity.Value.ID.Value,
        //                        PropBag = "<PROPBAG></PROPBAG>"
        //                    });
        //                break;

        //        }
        //        return entity;
        //    }

        //}

        //public class PhoneObject : LG.Data.Models.BaseModel
        //{
        //    public Action EventAction { get; set; }

        //    public Int64 RID { get; set; }
        //    public LG.Services.CMS.Phone Value { get; set; }
        //    public LG.Services.CMS.PhoneInput ValueInput { get; set; }

        //    public PhoneObject() : base() { }
        //    public LG.Services.CMS.AddClientPhoneResponse AddResponse { get; set; }
        //    public LG.Services.CMS.UpdateClientPhoneResponse UpdateResponse { get; set; }
        //    public LG.Services.CMS.RemoveClientPhoneResponse RemoveResponse { get; set; }
        //}
        //public static class Phone
        //{
        //    public static async Task<PhoneObject> Run(PhoneObject entity)
        //    {
        //        var client = Connection.Get;
        //        switch (entity.EventAction)
        //        {
        //            case Action.Add:
        //                entity.AddResponse = await client.AddClientPhoneAsync(
        //                    new AddClientPhoneRequest()
        //                    {
        //                        RID = entity.RID,
        //                        MessageGuid = Guid.NewGuid(),
        //                        Phone = entity.Value,
        //                        PropBag = Helpers.Settings.PropBag
        //                    });
        //                break;
        //            case Action.Update:
        //                if (entity.Value.ID.HasValue)
        //                    entity.UpdateResponse = await client.UpdateClientPhoneAsync(
        //                        new UpdateClientPhoneRequest()
        //                        {
        //                            RID = entity.RID,
        //                            MessageGuid = Guid.NewGuid(),
        //                            NewPhone = entity.Value,
        //                            PhoneIDToUpdate = entity.Value.ID.Value,
        //                            NewPhoneUsageEnum = PhoneUsageEnum.Business,
        //                            PhoneUsageEnumToUpdate = PhoneUsageEnum.Business,
        //                            PropBag = Helpers.Settings.PropBag
        //                        });
        //                break;
        //            case Action.Remove:
        //                if (entity.Value.ID.HasValue)
        //                    entity.RemoveResponse = await client.RemoveClientPhoneAsync(new RemoveClientPhoneRequest()
        //                    {
        //                        RID = entity.RID,
        //                        MessageGuid = Guid.NewGuid(),
        //                        PhoneID = entity.Value.ID.Value,
        //                        PropBag = "<PROPBAG></PROPBAG>"
        //                    });
        //                break;

        //        }
        //        return entity;
        //    }
        //}


        //public class AddressObject : LG.Data.Models.BaseModel
        //{
        //    public Action EventAction { get; set; }

        //    public Int64 RID { get; set; }
        //    public LG.Services.CMS.Address Value { get; set; }
        //    public LG.Services.CMS.AddressInput ValueInput { get; set; }

        //    public AddressObject() : base() { }
        //    public LG.Services.CMS.AddClientAddressResponse AddResponse { get; set; }
        //    public LG.Services.CMS.UpdateClientAddressResponse UpdateResponse { get; set; }
        //    public LG.Services.CMS.RemoveClientAddressResponse RemoveResponse { get; set; }
        //}

        //public static class Address
        //{
        //    public static async Task<AddressObject> Run(AddressObject entity)
        //    {
        //        var client = Connection.Get;
        //        switch (entity.EventAction)
        //        {
        //            case Action.Add:
        //                entity.AddResponse = await client.AddClientAddressAsync(
        //                    new AddClientAddressRequest()
        //                    {
        //                        RID = entity.RID,
        //                        MessageGuid = Guid.NewGuid(),
        //                        Address = entity.Value,
        //                        PropBag = Helpers.Settings.PropBag
        //                    });
        //                break;
        //            case Action.Update:
        //                if (entity.Value.ID.HasValue)
        //                    entity.UpdateResponse = await client.UpdateClientAddressAsync(
        //                        new UpdateClientAddressRequest()
        //                        {
        //                            RID = entity.RID,
        //                            MessageGuid = Guid.NewGuid(),
        //                            NewAddress = entity.Value,
        //                            AddressIDToUpdate = entity.Value.ID.Value,
        //                            AddressUsageEnumToUpdate = LG.Services.CMS.AddressUsageEnum.Business,
        //                            NewAddressUsageEnum = LG.Services.CMS.AddressUsageEnum.Business,
        //                            PropBag = Helpers.Settings.PropBag
        //                        });
        //                break;
        //            case Action.Remove:
        //                if (entity.Value.ID.HasValue)
        //                    entity.RemoveResponse = await client.RemoveClientAddressAsync(new RemoveClientAddressRequest()
        //                    {
        //                        RID = entity.RID,
        //                        MessageGuid = Guid.NewGuid(),
        //                        AddressID = entity.Value.ID.Value,
        //                        PropBag = "<PROPBAG></PROPBAG>"
        //                    });
        //                break;
        //        }
        //        return entity;
        //    }
        //}
    }
}
