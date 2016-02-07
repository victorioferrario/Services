using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Members
{
    public static class SaveService
    {
        public static async Task<LG.Data.Models.BaseModel> Address(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Members.SaveDataService.Address(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> Phone(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Members.SaveDataService.Phone(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> Email(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Members.SaveDataService.Email(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> PersonInfo(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Members.SaveDataService.PersonInfo(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> SecurityInfo(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Members.SaveDataService.SecurityInfo(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> CreditCard(LG.Services.ACS.CreditCardInfo_Input entityInstance)
        {
            return await LG.Data.Core.Members.SaveDataService.CreditCard(entityInstance);
        }
    }

    public static class RemoveService
    {
        public static async Task<LG.Data.Models.BaseModel> Address(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Members.RemoveDataService.Address(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> AddressUsage(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Members.RemoveDataService.AddressUsage(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> Phone(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Members.RemoveDataService.Phone(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> PhoneUsage(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Members.RemoveDataService.PhoneUsage(entityInstance);
        }
    }
}