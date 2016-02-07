using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models;
using LG.Data.Models.Users;

namespace LG.Data.Shared
{
    public static class ContactInfoService
    {
        public static async Task<BaseModel> SavePhone(LG.Data.Models.Shared.Contact entity, Int32 index)
        {
            return await LG.Data.Core.Shared.SaveContactInformation.SavePhoneTask(entity, index);
        }
        public static async Task<BaseModel> SavePersonInfo(LG.Data.Models.Users.UserModel entity)
        {
            return await LG.Data.Core.Shared.SaveContactInformation.SavePersonInfoTask(entity);
        }
        public static async Task<BaseModel> SaveEmail(LG.Data.Models.Users.UserModel entity)
        {
            return await LG.Data.Core.Shared.SaveContactInformation.SaveEmailTask(entity);
        }
    }
}
