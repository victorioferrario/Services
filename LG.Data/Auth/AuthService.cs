using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Auth
{
    public static class AuthService
    {
        public static async Task<LG.Data.Models.Identity.ApplicationUser> Login(string username, string password)
        {
            return await LG.Data.Core.Auth.AuthenticationManager.Login(username, password);
        }
        public static async Task<LG.Data.Models.Auth.SecurityInfo> Save(LG.Data.Models.Auth.SecurityInfo eInput)
        {
            return await LG.Data.Core.Auth.AuthDataService.Save(eInput);
        }
    }
}
