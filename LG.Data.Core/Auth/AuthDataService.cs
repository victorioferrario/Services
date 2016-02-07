using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.AMS;

namespace LG.Data.Core.Auth
{
    public static class AuthDataService
    {

        public static async Task<LG.Services.AMS.AuthenticateUserResponse>
            Login(LG.Data.Models.Auth.SecurityInfo eInput)
        {
             var client = LG.Services.ClientConnection.GetAmsConnection();
             var response = client.AuthenticateUserAsync(new AuthenticateUserRequest()
             {
                 MessageGuid = Guid.NewGuid(),
                 UserName = eInput.UserName,
                 Password = eInput.PlainPassword
             });
            try
            {
                
                await response;
                if (response.IsCompleted)
                {
                    return response.Result;
                }


            }
            catch (Exception ex)
            {
                return response.Result;
                
            }
            return null;
        }

        public static async Task<LG.Data.Models.Auth.SecurityInfo> 
            Load(LG.Data.Models.Auth.SecurityInfo eInput)
        {
            var client = LG.Services.ClientConnection.GetAmsConnection();
            try
            {
                //var response = client.
            }
            catch (Exception ex)
            {
                
            }

            return null;
        }

            public static
            async Task<LG.Data.Models.Auth.SecurityInfo> 
            Save(LG.Data.Models.Auth.SecurityInfo eInput)
        {
            var client = LG.Services.ClientConnection.GetAmsConnection();
            try
            {
                var response = client.CreateLoginAsync(
                    new CreateLoginRequest()
                {
                    RID = eInput.RID,
                    UserName = eInput.UserName,
                    PlainPassword = eInput.PlainPassword,
                    IsActive = eInput.IsActive,
                    DTUTC_PasswordExpires = DateTime.UtcNow.AddYears(1),
                    MessageGuid = Guid.NewGuid(),
                    IsTemporaryPassword = eInput.IsTemporaryPassword,
                    PropBag = eInput.PropBag
                });
                await response;
                if (response.IsCompleted)
                {
                    eInput.IsError = response.Result.ReturnStatus.IsError;
                    eInput.Message = response.Result.ReturnStatus.GeneralMessage;
                    return eInput;
                }
                if (response.IsFaulted)
                {
                    eInput.IsError = true;
                    eInput.Message = "Faulted";
                    return eInput;
                }


            }
            catch (Exception ex)
            {
                eInput.IsError = true;
                eInput.Message = ex.Message;
                return eInput;
            }
            return null;

        }



    }

    public static class AuthManagement
    {
        public static async Task<LG.Data.Models.Identity.UserEntity> Login(String username, String password)
        {
            return await AuthenticationOperations.IdentityLogin(username, password);
        }
        public static async Task<LG.Data.Models.Identity.Graph.AuthGraphEntity> LoadAuthGraphEntity(System.Int64 rid)
        {
            return await LG.Data.Core.Graph.GraphDataService.Load(rid);
        }
    }
    public static class UserManager
    {
        public static async Task<LG.Data.Models.Identity.UserEntity> Login(String username, String password)
        {
            return await AuthenticationOperations.IdentityLogin(
                username, password);
        }

        public static async Task<LG.Data.Models.Identity.Graph.AuthGraphEntity> LoadAuthGraphEntity(System.Int64 rid)
        {
            return await LG.Data.Core.Graph.GraphDataService.Load(rid);
        }
    }
}
