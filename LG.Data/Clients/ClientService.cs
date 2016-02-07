using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Core.Clients.Details;
using LG.Data.Models.Clients;
using LG.Services.CMS;

namespace LG.Data.Clients
{
    public static class ClientService
    {
        public static async Task<ResponseClient> ClientWithGroup(
             LG.Services.CMS.ClientInfoInput entity)
        {
            return await LG.Data.Core.Clients.ClientDataService.ClientWithGroup(entity);
        }

        public static async Task<LG.Data.Models.Clients.EligibilitySettings> UpdateEligibilityInfo(
            LG.Data.Models.Clients.EligibilitySettings entity)
        {
            return await LG.Data.Core.Clients.ClientDataService.UpdateEligibilityInfo(entity);
        }
        public static async Task<ResponseClient> GetClientInfo(Int64 RID)
        {
            return await LG.Data.Core.Clients.ClientDataService.GetClientInfo(RID);
        }

        public static async Task<SetClientIsActiveStatusResponse> SetIsActive(Int64 rId, Boolean isActive)
        {
            return await GeneralSettings.SetIsActive(rId, isActive);
        }

        public static async Task<SetClientIsTestingStatusResponse> SetIsTesting(Int64 rId, Boolean isActive)
        {
            return await GeneralSettings.SetIsTesting(rId, isActive);
        }

        public static async Task<LG.Services.CMS.UpdateClientPathToLogoImageResponse> SaveLogo(Int64 RID, String logoPathUrl)
        {
            return await GeneralSettings.SaveLogo(RID, logoPathUrl);
        }

        public static async Task<LG.Services.CMS.UpdateWebsiteURLResponse> SaveUrl(Int64 RID, String Url)
        {
            return await GeneralSettings.SaveUrl(RID, Url);
        }


        public static async Task<EmailObject> UpdateEmail(EmailObject entity)
        {
            return await LG.Data.Core.Clients.Details.Email.Run(entity);
        }

        public static async Task<AddressObject> UpdateAddress(AddressObject entity)
        {
            return await LG.Data.Core.Clients.Details.Address.Run(entity);
        }

        public static async Task<PhoneObject> UpdatePhone(PhoneObject entity)
        {
            return await LG.Data.Core.Clients.Details.Phone.Run(entity);
        }

    }
}
