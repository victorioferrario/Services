using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Members
{
    public static class MemberService
    {
        public static async Task<LG.Data.Models.Members.SortedSearchResults> 
            GetClientMembers(LG.Data.Models.Members.SortedSearchRequest entityRequest)
        {
            return await LG.Data.Core.Members.MemberDataService.GetClientMembers(
                entityRequest);
        }
        public static async Task<LG.Data.Models.Members.SearchResults>
            Search(LG.Data.Models.Members.SearchRequest entityRequest)
        {
            return await LG.Data.Core.Members.MemberDataService.Search(entityRequest);
        }
        public static async Task<LG.Data.Models.Members.Account> JitAccount(System.Int64 RID)
        {
            return await LG.Data.Core.Members.MemberDataService.JitAccount(RID);
        }
        public static async Task<LG.Data.Models.Members.Account> GetAccountInfo(System.Int64 RID, System.Int32 AccountID)
        {
            return await LG.Data.Core.Members.MemberDataService.GetAccountInfo(RID, AccountID);
        }

        public static async Task<LG.Data.Models.Members.Account> GetAccountInfo(System.Int64 RID)
        {
            return await LG.Data.Core.Members.MemberDataService.GetAccountInfo(RID);
        }

        public static async Task<LG.Services.EMS.BEntity> LoadDetail(System.Int64 RID)
        {
            return await LG.Data.Core.Shared.LoadPersonInfoDataService.Load(RID);
        }
    }
}
