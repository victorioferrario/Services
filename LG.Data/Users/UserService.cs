using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Users;


namespace LG.Data.Users
{
    public static class UserService
    {
        public static LG.Data.Models.Shared.ValidEmail VerifyEmail(String value)
        {
            return LG.Data.Core.Users.UsersDataService.VerifyEmail(value);
        }
        public static Boolean VerifyEmailAgainstList(String value, Int64 rid)
        {
            var list = LG.Data.Core.Users.UsersDataService.VerifyEmailList(value);
            if (list.Any(item => item == rid))
            {
                return true;
            }
            return true;
        }

        public static UserModel AddToClient(LG.Data.Models.Users.UserModel entity)
        {
            return LG.Data.Core.Users.UsersDataService.AddUser(entity);
        }

        public static async Task<LG.Data.Models.Users.UserModel> SavePrimaryAsync(
            LG.Data.Models.Users.UserModel entity)
        {
            return await LG.Data.Core.Users.UsersDataService.SavePrimaryAsync(entity);
        }

        public static async Task<LG.Data.Models.Users.UserModel> CreateAndSavePrimaryAsync(
            LG.Data.Models.Users.UserModel entity)
        {
            return await LG.Data.Core.Users.UsersDataService.CreateAndSavePrimaryAsync(entity);
        }

        public async static Task<LG.Data.Models.Users.UserModel> GetUser(LG.Data.Models.Users.UserModel entity)
        {
            return await LG.Data.Core.Users.UsersDataService.Get(entity);
        }
        public async static Task<LG.Data.Models.Users.UserListModel> AddClientUser(LG.Data.Models.Users.UserModel entity)
        {
            var result
                = LG.Data.Core.Users.UsersDataService.CreateAsync(entity);

            await result;

            if (result.IsCompleted)
            {
                var result2 = LG.Data.Core.Users.UsersDataService.UpdateAsync(
                    entity, result.Result.UserRID, LG.Data.Models.Enums.Action.Add);

                await result2;

                entity.UserRID = result.Result.UserRID;
                LG.Data.Core.Users.UsersDataService.AddUser(entity);
                return await LG.Data.Core.Users.UsersDataService.GetListAsync(entity);
            }
            return await LG.Data.Core.Users.UsersDataService.GetListAsync(entity);

        }

        public static async Task<UserListModel> LinkToCorporation(LG.Data.Models.Users.UserModel user)
        {
            return await LG.Data.Core.Users.UsersDataService.AddUserToCorporation(user);
        }

        public async static Task<LG.Data.Models.Users.UserListModel> AddCorporationUser(LG.Data.Models.Users.UserModel entity)
        {
            var result
                = LG.Data.Core.Users.UsersDataService.CreateAsync(entity);

            await result;
            if (result.IsCompleted)
            {
                var result2 = LG.Data.Core.Users.UsersDataService.UpdateAsync(
                    entity, result.Result.UserRID, 
                    LG.Data.Models.Enums.Action.Add);

                await result2;

                entity.UserRID = result.Result.UserRID;

                if (result2.IsCompleted)
                {
                    return await LG.Data.Core.Users.UsersDataService.AddUserToCorporation(
                        entity);
                }
                if (result2.IsFaulted)
                {
                    return await LG.Data.Core.Users.UsersDataService.GetCorporationListAsync();
                }
               
            }
            if (result.IsFaulted)
            {
                return null;
            }
            return null;
        }
        public async static Task<LG.Data.Models.Users.UserListModel> RemoveCorporationUser(UserModel user)
        {
            var result
                = LG.Data.Core.Users.UsersDataService.RemoveCorporationUser(user);

            await result;
            if (result.IsCompleted)
            {
                return await LG.Data.Core.Users.UsersDataService.GetCorporationListAsync();
            }
            else if (result.IsFaulted)
            {
                return await LG.Data.Core.Users.UsersDataService.GetCorporationListAsync();
            }
            return null;
        }

        public async static Task<LG.Data.Models.Users.UserListModel> UpdateCorporateUserAsync(LG.Data.Models.Users.UserModel entity)
        {
            var result = LG.Data.Core.Users.UsersDataService.UpdateCorporateUserAsync(entity);
            await result;
            if (result.IsCompleted)
            {
                return await LG.Data.Core.Users.UsersDataService.GetCorporationListAsync();
            }
            else if (result.IsFaulted)
            {
                return await LG.Data.Core.Users.UsersDataService.GetCorporationListAsync();
            }
            return null;
        }


        public async static Task<LG.Data.Models.Users.UserModel> UpdateClientUser(LG.Data.Models.Users.UserModel entity)
        {
            var result = LG.Data.Core.Users.UsersDataService.UpdateAsync(entity);
            await result;
            if (result.IsCompleted)
            {
                return result.Result;

            }
            return null;
        }
        public async static Task<LG.Data.Models.Users.UserListModel> RemoveClientUser(LG.Data.Models.Users.UserModel entity)
        {
            var result = LG.Data.Core.Users.UsersDataService.RemoveAsync(entity);
            if (!result.IsError)
            {
                return await GetClientUsers(entity);
            }
            return null;
        }
        public async static Task<LG.Data.Models.Users.UserListModel> GetClientUsers(LG.Data.Models.Users.UserModel entity)
        {
            return await LG.Data.Core.Users.UsersDataService.GetListAsync(entity);
        }
        public static async Task<UserListModel> GetCorporationListAsync()
        {
            return await LG.Data.Core.Users.UsersDataService.GetCorporationListAsync();
        }
    }
}
