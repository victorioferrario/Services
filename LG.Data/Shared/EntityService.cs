using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Shared
{
    public static class EntityService
    {
        public static async Task<LG.Services.EMS.BEntity> Load(System.Int64 RID)
        {
            return await LG.Data.Core.Shared.LoadPersonInfoDataService.Load(RID);
        }
        public static async Task<LG.Data.Models.Members.Entity> InjectMember(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Core.Shared.EntityDataService.InjectIntoEligibiltiyEntityTask(entity);
        }
        public static async Task<LG.Services.EMS.PersonInfo> LoadPersonInfo(Int64 id)
        {
            return await LG.Data.Core.Shared.EntityDataService.LoadPersonInfo(id);
        }
        public static async Task<LG.Data.Models.Members.Entity> CreateEntity(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Core.Shared.EntityDataService.CreateEntityTask(entity);
        }
        public static async Task<LG.Data.Models.Members.Entity> Email(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Core.Shared.EntityDataService.EmailSaveTask(entity);
        }
        public static async Task<LG.Data.Models.Members.Entity> Phone(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Core.Shared.EntityDataService.PhoneSaveTask(entity);
        }
        public static async Task<LG.Data.Models.Members.Entity> Address(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Core.Shared.EntityDataService.AddressSaveTask(entity);
        }
        public static async Task<LG.Data.Models.Members.Entity> PersonalInfo(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Core.Shared.EntityDataService.PersonalInfoSaveTask(entity);
        }
        public static async Task<LG.Data.Models.Members.Entity> SecurityInfo(LG.Data.Models.Members.Entity entity)
        {
            return await LG.Data.Core.Shared.EntityDataService.SecurityInfoSaveTask(entity);
        }
    }
}