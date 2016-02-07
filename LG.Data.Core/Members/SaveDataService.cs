using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Core.Members
{
    public static class SaveDataService
    {
        public static async Task<LG.Data.Models.BaseModel> Address(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Shared.SaveContactInformation.SaveAddressTask(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> Phone(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Shared.SaveContactInformation.SavePhoneTask(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> Email(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Shared.SaveContactInformation.SaveEmailTask(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> PersonInfo(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Shared.SaveContactInformation.SavePersonInfoTask(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> SecurityInfo(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Shared.SaveContactInformation.SaveSecurityInfoTask(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> CreditCard(LG.Services.ACS.CreditCardInfo_Input entityInstance)
        {
            return await LG.Data.Core.Shared.SaveContactInformation.SaveCreditCardInfoTask(entityInstance);
        }
    }

    public static class RemoveDataService
    {

        public static async Task<LG.Data.Models.BaseModel> Address(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Shared.RemoveContactInformation.RemoveAddressTask(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> AddressUsage(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Shared.RemoveContactInformation.RemoveAddressUsageTask(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> Phone(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Shared.RemoveContactInformation.RemovePhoneTask(entityInstance);
        }
        public static async Task<LG.Data.Models.BaseModel> PhoneUsage(LG.Data.Models.Members.MemberInstance entityInstance)
        {
            return await LG.Data.Core.Shared.RemoveContactInformation.RemovePhoneUsageTask(entityInstance);
        }
    }
}
