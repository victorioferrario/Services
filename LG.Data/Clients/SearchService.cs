using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Clients;

namespace LG.Data.Clients
{
    public static class SearchService
    {
        public static async Task<Results> Search(LG.Data.Models.Shared.ValueItem item)
        {
            return await LG.Data.Core.Clients.SearchDataService.Search(item);
        }
        public static async Task<Results> ListAll()
        {
            return await LG.Data.Core.Clients.SearchDataService.ListAll();
        }
    }
}
