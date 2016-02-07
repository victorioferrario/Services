using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums;
using LG.Data.Models.Shared;
using LG.Data.Models.Users;
using Action = LG.Data.Models.Enums.Action;

namespace LG.Data.Clients
{
    public static class ContactService
    {
        public static async Task<LG.Data.Models.Shared.Contact> Save(LG.Data.Models.Shared.Contact contact)
        {
            return await LG.Data.Core.Clients.ContactDataService.Save(contact);
        }


      
        public static async Task<LG.Data.Models.Users.UserModel> Update(LG.Data.Models.Shared.Contact contact)
        {

            var phones = new List<PhoneBase>();


            var user = new UserModel()
            {
                CorporationRID = 10,
                ClientRID = contact.ContactForRID,
                EventAction = Action.Update,
                GeneralInfo = new PersonInfo()
                {
                    FName = contact.PersonInfo.FName,
                    MName = contact.PersonInfo.MName,
                    LName = contact.PersonInfo.LName
                }
            };

            user.EventAction = Models.Enums.Action.Update;

            contact.EventAction = ContactAction.Update;
            for (var i = 0; i < contact.Phones.Count; i++)
            {
               await LG.Data.Shared.ContactInfoService.SavePhone(contact, i);
            }
            var r3 = LG.Data.Shared.ContactInfoService.SavePersonInfo(user);
            await r3;
            if (r3.IsCompleted)
            {
                return user;
            }
            return null;
        }
    }

}
