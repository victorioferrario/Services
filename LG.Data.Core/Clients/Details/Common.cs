using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Core.Clients.Helpers;
using LG.Services.CMS;

namespace LG.Data.Core.Clients.Details
{
    public class GeneralInfoObject : LG.Data.Models.BaseModel
    {
        public LG.Data.Models.Enums.Action EventAction { get; set; }
        public Int64 RID { get; set; }

        public System.Boolean IsActive { get; set; }
        public System.Boolean IsTesting { get; set; }

        public System.String LogoPath { get; set; }
        public System.String WebsiteUrl { get; set; }

        public GeneralInfoObject() : base() { }

        public LG.Services.CMS.SetClientIsActiveStatusResponse IsActiveStatusResponse { get; set; }
        public LG.Services.CMS.SetClientIsTestingStatusResponse IsTestingStatusResponse { get; set; }
        public LG.Services.CMS.UpdateClientPathToLogoImageResponse UpdateLogoPath { get; set; }
        public LG.Services.CMS.UpdateWebsiteURLResponse UpdateWebsiteUrlResponse { get; set; }

    }




    public static class General
    {

        public static async Task<GeneralInfoObject> SetIsActive(GeneralInfoObject entity)
        {
            entity.IsActiveStatusResponse = await GeneralSettings.SetIsActive(entity.RID, entity.IsActive);
            return entity;
        }

        public static async Task<GeneralInfoObject> SetIsTesting(GeneralInfoObject entity)
        {
            entity.IsTestingStatusResponse = await GeneralSettings.SetIsTesting(entity.RID, entity.IsTesting);
            return entity;
        }

        public static async Task<GeneralInfoObject> SaveLogo(GeneralInfoObject entity)
        {
            entity.UpdateLogoPath = await GeneralSettings.SaveLogo(entity.RID, entity.LogoPath);
            return entity;
        }

        public static async Task<GeneralInfoObject> SaveUrl(GeneralInfoObject entity)
        {
            entity.UpdateWebsiteUrlResponse = await GeneralSettings.SaveUrl(entity.RID, entity.WebsiteUrl);
            return entity;
        }
    }

   

    public class EmailObject : LG.Data.Models.BaseModel
    {
        public LG.Data.Models.Enums.Action EventAction { get; set; }
        public Int64 RID { get; set; }
        public LG.Services.CMS.EmailAddress Value { get; set; }
        public EmailObject() : base() { }
        public AddClientEmailAddressResponse AddResponse { get; set; }
        public UpdateClientEmailAddressResponse UpdateResponse { get; set; }
        public RemoveClientEmailAddressResponse RemoveResponse { get; set; }
    }
    public static class Email
    {
        public static async Task<EmailObject> Run(EmailObject entity)
        {
            var client = Connection.Get;
            switch (entity.EventAction)
            {
                case Models.Enums.Action.Add:
                    entity.AddResponse = await client.AddClientEmailAddressAsync(
                       new AddClientEmailAddressRequest()
                       {
                           RID = entity.RID,
                           MessageGuid = Guid.NewGuid(),
                           EmailAddress = entity.Value,
                           PropBag = Helpers.Settings.PropBag
                       });
                    break;
                case Models.Enums.Action.Update:
                    if (entity.Value.ID.HasValue)
                        entity.UpdateResponse = await client.UpdateClientEmailAddressAsync(
                            new UpdateClientEmailAddressRequest()
                            {
                                RID = entity.RID,
                                MessageGuid = Guid.NewGuid(),
                                NewEmailAddress = entity.Value,
                                EmailAddressIDToUpdate = entity.Value.ID.Value,
                                EmailAddressUsageEnumToUpdate = entity.Value.EmailAddressUsages[0].EmailAddressUsageEnum,
                                NewEmailAddressUsageEnum = EmailAddressUsageEnum.Group,
                                PropBag = Helpers.Settings.PropBag
                            });
                    break;
                case Models.Enums.Action.Remove:
                    if (entity.Value.ID.HasValue)
                        entity.RemoveResponse = await client.RemoveClientEmailAddressAsync(new RemoveClientEmailAddressRequest()
                        {
                            RID = entity.RID,
                            MessageGuid = Guid.NewGuid(),
                            EmailAddressID = entity.Value.ID.Value,
                            PropBag = "<PROPBAG></PROPBAG>"
                        });
                    break;

            }
            return entity;
        }

    }

    public class PhoneObject : LG.Data.Models.BaseModel
    {
        public Models.Enums.Action EventAction { get; set; }

        public Int64 RID { get; set; }
        public LG.Services.CMS.Phone Value { get; set; }
        public LG.Services.CMS.PhoneInput ValueInput { get; set; }

        public PhoneObject() : base() { }
        public LG.Services.CMS.AddClientPhoneResponse AddResponse { get; set; }
        public LG.Services.CMS.UpdateClientPhoneResponse UpdateResponse { get; set; }
        public LG.Services.CMS.RemoveClientPhoneResponse RemoveResponse { get; set; }
    }
    public static class Phone
    {
        public static async Task<PhoneObject> Run(PhoneObject entity)
        {
            var client = Connection.Get;
            switch (entity.EventAction)
            {
                case Models.Enums.Action.Add:
                    entity.AddResponse = await client.AddClientPhoneAsync(
                        new AddClientPhoneRequest()
                        {
                            RID = entity.RID,
                            MessageGuid = Guid.NewGuid(),
                            Phone = new Services.CMS.Phone()
                            {
                                PhoneCountryCode = "1",
                                PhoneNumber = entity.Value.PhoneNumber,
                                PhoneUsages = entity.Value.PhoneUsages
                            },
                            PropBag = Helpers.Settings.PropBag
                        });
                    break;
                case Models.Enums.Action.Update:
                    if (entity.Value.ID.HasValue)
                        entity.UpdateResponse = await client.UpdateClientPhoneAsync(
                            new UpdateClientPhoneRequest()
                            {
                                RID = entity.RID,
                                MessageGuid = Guid.NewGuid(),
                                NewPhone = entity.Value,
                                PhoneIDToUpdate = entity.Value.ID.Value,
                                NewPhoneUsageEnum = PhoneUsageEnum.Business,
                                PhoneUsageEnumToUpdate = PhoneUsageEnum.Business,
                                PropBag = Helpers.Settings.PropBag
                            });
                    break;
                case Models.Enums.Action.Remove:
                    if (entity.Value.ID.HasValue)
                        entity.RemoveResponse = await client.RemoveClientPhoneAsync(new RemoveClientPhoneRequest()
                        {
                            RID = entity.RID,
                            MessageGuid = Guid.NewGuid(),
                            PhoneID = entity.Value.ID.Value,
                            PropBag = "<PROPBAG></PROPBAG>"
                        });
                    break;

            }
            return entity;
        }
    }


    public class AddressObject : LG.Data.Models.BaseModel
    {
        public Models.Enums.Action EventAction { get; set; }

        public Int64 RID { get; set; }
        public LG.Services.CMS.Address Value { get; set; }
        public LG.Services.CMS.AddressInput ValueInput { get; set; }

        public AddressObject() : base() { }
        public LG.Services.CMS.AddClientAddressResponse AddResponse { get; set; }
        public LG.Services.CMS.UpdateClientAddressResponse UpdateResponse { get; set; }
        public LG.Services.CMS.RemoveClientAddressResponse RemoveResponse { get; set; }
    }

    public static class Address
    {
        public static async Task<AddressObject> Run(AddressObject entity)
        {
            var client = Connection.Get;
            switch (entity.EventAction)
            {
                case Models.Enums.Action.Add:
                    entity.AddResponse = await client.AddClientAddressAsync(
                        new AddClientAddressRequest()
                        {
                            RID = entity.RID,
                            MessageGuid = Guid.NewGuid(),
                            Address = entity.Value,
                            PropBag = Helpers.Settings.PropBag
                        });
                    break;
                case Models.Enums.Action.Update:
                    if (entity.Value.ID.HasValue)
                        entity.UpdateResponse = await client.UpdateClientAddressAsync(
                            new UpdateClientAddressRequest()
                            {
                                RID = entity.RID,
                                MessageGuid = Guid.NewGuid(),
                                NewAddress = entity.Value,
                                AddressIDToUpdate = entity.Value.ID.Value,
                                AddressUsageEnumToUpdate = entity.Value.AddressUsages[0].AddressUsageEnum,
                                NewAddressUsageEnum = entity.Value.AddressUsages[0].AddressUsageEnum,
                                PropBag = Helpers.Settings.PropBag
                            });
                    break;
                case Models.Enums.Action.Remove:
                    if (entity.Value.ID.HasValue)
                        entity.RemoveResponse = await client.RemoveClientAddressAsync(new RemoveClientAddressRequest()
                        {
                            RID = entity.RID,
                            MessageGuid = Guid.NewGuid(),
                            AddressID = entity.Value.ID.Value,
                            PropBag = "<PROPBAG></PROPBAG>"
                        });
                    break;
            }
            return entity;
        }
    }
}
