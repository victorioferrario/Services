using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models;
using LG.Data.Models.Users;

namespace LG.Data.Shared
{
    public static class SecurityInformationService
    {
        public static async Task<BaseModel> SaveCredentials(UserModel entity)
        {
            return await LG.Data.Core.Shared.SecurityInformationDataService.SaveCredentials(entity);
        }
    }
}
