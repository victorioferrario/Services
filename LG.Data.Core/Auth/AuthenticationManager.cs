using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Identity;
using LG.Data.Models.Identity.Graph;

namespace LG.Data.Core.Auth
{
    public static class AuthenticationManager
    {
        public static async Task<LG.Data.Models.Identity.ApplicationUser> Login(String username, String password)
        {
            var user = new ApplicationUser();
            var result = await CallLoginApi(username, password);
            Copy<LG.Data.Models.Identity.UserEntity, LG.Data.Models.Identity.ApplicationUser>(
                ref result, ref user);

            if (user == null || !user.IsActive) return null;

            user.AuthGraphList = await InternalLoadAuthGraph(user.RolodexItemId);

            return user;
        }
        public static List<Application> Load(List<AccessConfiguration> data)
        {
            return LG.Data.Models.Identity.ApplicationFactory.Load(data);
        }
        internal static async Task<LG.Data.Models.Identity.UserEntity>
            CallLoginApi(String username, String password)
        {
            return await LG.Data.Core.Auth.UserManager.Login(username, password);
        }
        internal static async Task<List<LG.Data.Models.Identity.Graph.AccessConfiguration>> InternalLoadAuthGraph(System.Int64 RId)
        {
            var result =  LG.Data.Core.Auth.UserManager.LoadAuthGraphEntity(RId);
            await result;
            if (result.IsCompleted)
            {
                return LG.Data.Core.Auth.Utilities.Interpretor.LoadData(result.Result.AuthGraph);
            }
            return null;
        }

        internal static void Copy<T, U>(ref T source, ref U target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();
            var sourceProperties = sourceType.GetProperties();
            var targetProperties = targetType.GetProperties();
            foreach (var tp in targetProperties)
            {
                foreach (var sp in sourceProperties.Where(sp => tp.Name == sp.Name))
                {
                    tp.SetValue(target, sp.GetValue(source, null), null);
                }
            }
        }
    }
}
