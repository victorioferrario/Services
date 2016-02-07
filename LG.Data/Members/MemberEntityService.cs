using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.ACS;

namespace LG.Data.Members
{
    public static class AccountService
    {
        public static async Task<LG.Data.Models.Members.Entity> Create(LG.Data.Models.Members.Entity entity)
        {
             return await LG.Data.Core.Members.AccountDataService.AccountCreateTask(entity);
        }
        public static async Task<LG.Data.Models.Members.Entity> Load(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Core.Members.AccountDataService.AccountLoadTask(entity);
        }
        public static async Task<List<CreditCardInfo>> LoadCreditCards(Int32 AccountID)
        {
            return await LG.Data.Core.Members.AccountDataService.CreditCardLoadTask(AccountID);
        }
        public static async Task<List<AccountInfoExtended>> LoadDependents(int AccountID)
        {
            return await LG.Data.Core.Members.AccountDataService.LoadDependents(AccountID);
        }
        

        public static async Task<LG.Data.Models.Members.Entity> Save(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Core.Members.AccountDataService.AccountSaveTask(entity);
        }
    }
    public static class SharedService
    {
        public static async Task<LG.Data.Models.Members.Entity> 
            CreateEntity(LG.Data.Models.Members.Entity entity){
            return await LG.Data.Shared.EntityService.CreateEntity(entity);
        }
        public static async Task<LG.Data.Models.Members.Entity> Email(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Shared.EntityService.Email(entity);
        }
        public static async Task<LG.Data.Models.Members.Entity> Phone(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Shared.EntityService.Phone(entity);
        }
        public static async Task<LG.Data.Models.Members.Entity> Address(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Shared.EntityService.Address(entity);
        }
        public static async Task<LG.Data.Models.Members.Entity> SecurityInfo(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Shared.EntityService.SecurityInfo(entity);
        }
        public static async Task<LG.Data.Models.Members.Entity> PersonInfo(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Shared.EntityService.PersonalInfo(entity);
        }
    }
}
