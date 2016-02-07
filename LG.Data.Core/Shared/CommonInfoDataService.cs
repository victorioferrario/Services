using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums;
using LG.Services.CDMS;
using LG.Services.CES;

namespace LG.Data.Core.Shared
{
    public static class CommonInfoDataService
    {
        public static async Task<LG.Data.Models.Shared.CommonInfoRecords> DataTask()
        {
            var result = new LG.Data.Models.Shared.CommonInfoRecords();
            var client = LG.Services.ClientConnection.GetCES_Connection();
            try
            {
                client.Open();
                var response = await client.GetClientGroupDoctorsMembersCountAsync(new GetClientGroupDoctorsMembersCountRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                });
                client.Close();
                result.IsError = false;
                result.Data = response.CommonInfoCounts;
                return result;
            }
            catch (Exception ex)
            {
                client.Close();
                result.IsError = true;
                result.Message = ex.ToString();
                return result;
            }
        }
    }
}
