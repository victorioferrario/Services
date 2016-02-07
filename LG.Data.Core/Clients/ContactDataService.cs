using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums;
using LG.Data.Models.Shared;
using LG.Services.EMS;

namespace LG.Data.Core.Clients
{
    public static class ContactDataService
    {
        public static async Task<LG.Data.Models.Shared.Contact> Save(LG.Data.Models.Shared.Contact contact)
        {
            switch (contact.EventAction)
            {
                case ContactAction.Add:
                    var client = LG.Services.ClientConnection.GetEmsConnection();
                    try
                    {
                        client.Open();
                        var response
                            = await client.AddNewBEntityAsContactAsync(new AddNewBEntityAsContactRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            Contact = new ContactInput()
                            {
                                IsActive = contact.IsActive,
                                ContactForRID = contact.ContactForRID,
                                ContactUsages = contact.ContactUsages,
                                EmailAddresses = contact.NewEmailAddresses,
                                Addresses = new List<AddressInput>(),
                                Phones = contact.NewPhones,
                                PersonInfo = new PersonInfoInput()
                                {
                                    FName = contact.PersonInfo.FName,
                                    MName = contact.PersonInfo.MName,
                                    LName = contact.PersonInfo.LName
                                },
                            },
                            PropBag = "<PropBag>LG.Data.Core.Clients.ContactDataService.Save(LG.Data.Models.Shared.Contact contact)</PropBag>"
                        });
                        client.Close();
                        contact.RID = response.ContactRID;
                    }
                    catch (Exception ex)
                    {
                        contact.IsError = true;
                        contact.Message = ex.ToString();
                    }
                    break;
                case ContactAction.Update:
                    var userModel = new LG.Data.Models.Users.UserModel()
                    {
                        CorporationRID = 10,
                        ClientRID = contact.ContactForRID,
                        GeneralInfo = new LG.Data.Models.Shared.PersonInfo()
                        {
                            FName = contact.PersonInfo.FName,
                            MName = contact.PersonInfo.MName,
                            LName = contact.PersonInfo.LName
                        },
                        UserRID = contact.RID,

                    };
                    break;

            }
            return contact;
        }
    }
}
