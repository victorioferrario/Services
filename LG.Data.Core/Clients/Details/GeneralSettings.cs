using System;
using System.Threading.Tasks;
using LG.Data.Core.Clients.Helpers;
using LG.Services.CMS;

namespace LG.Data.Core.Clients.Details
{
    public static class GeneralSettings
    {
        public static async Task<SetClientIsActiveStatusResponse> SetIsActive(Int64 rId, Boolean isActive)
        {
            var client = Connection.Get;
            return await client.SetClientIsActiveStatusAsync(new SetClientIsActiveStatusRequest()
            {
                MessageGuid = Guid.NewGuid(),
                IsActive = isActive,
                RID = rId,
                PropBag = Helpers.Settings.PropBag
            });
        }

        public static async Task<SetClientIsTestingStatusResponse> SetIsTesting(Int64 rId, Boolean isActive)
        {
            var client = Connection.Get;
            return await client.SetClientIsTestingStatusAsync(new SetClientIsTestingStatusRequest()
            {
                RID = rId,
                IsTesting = isActive,
                MessageGuid = Guid.NewGuid(),
                PropBag = Helpers.Settings.PropBag
            });
        }

        public static async Task<LG.Services.CMS.UpdateClientPathToLogoImageResponse> SaveLogo(Int64 RID, String logoPathUrl)
        {
            var client = Connection.Get;
            return await  client.UpdateClientPathToLogoImageAsync(new UpdateClientPathToLogoImageRequest()
            {
                ClientRID = RID,
                MessageGuid = Guid.NewGuid(),
                PathToLogoImage = logoPathUrl,
                PropBag = Helpers.Settings.PropBag
            });
        }

        public static async Task<LG.Services.CMS.UpdateWebsiteURLResponse> SaveUrl(Int64 RID, String Url)
        {
            var client = Connection.Get;
            return await client.UpdateWebsiteURLAsync(new UpdateWebsiteURLRequest()
            {
                ClientRID = RID,
                MessageGuid = Guid.NewGuid(),
                WebsiteURL = Url,
                PropBag = Helpers.Settings.PropBag
            });
        }
    }
}