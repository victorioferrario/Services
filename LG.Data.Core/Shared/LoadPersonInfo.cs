using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Users;
using LG.Services;
using LG.Services.EMS;
using LG.Services.UMS;

namespace LG.Data.Core.Shared
{
    public static class LoadPersonInfoDataService
    {
        public static async Task<LG.Services.EMS.BEntity> Load(System.Int64 RID)
        {

            var client = ClientConnection.GetEmsConnection();

            try
            {
                client.Open();
                var response = await client.LoadBEntityDataAsync(new LoadBEntityDataRequest()
                {
                   MessageGuid = Guid.NewGuid(),
                   RID = RID
                   
                });
                client.Close();
                return  response.BEntity;
            }
            catch (Exception ex)
            {
                client.Close();
                return new BEntity()
                {
                    
                };
            }
        }

    }
}
