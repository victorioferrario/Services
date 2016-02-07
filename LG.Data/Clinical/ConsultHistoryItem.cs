using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Clinical
{
    public static class Disposition
    {
        public static async Task<LG.Data.Models.Clinical.ConsultationHistoryItem> GetDisposition(Int32 entity)
        {
            return await LG.Data.Core.Clinical.ConsultHistoryDataService.GetDisposition(entity);
        }
      
    }
}
