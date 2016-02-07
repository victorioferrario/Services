using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Shared
{
    public static class CommonService
    {
        public async static Task<LG.Data.Models.Shared.CommonInfoRecords> Data()
        {
            return await LG.Data.Core.Shared.CommonInfoDataService.DataTask();
        }
    }
}
