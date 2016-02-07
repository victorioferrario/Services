using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Clients;
using LG.Services;
using LG.Services.CMS;
using LG.Services.GMS;

namespace LG.Data.Core.Clients
{
    public static class SearchDataService
    {
        public static async Task<Results> Search(LG.Data.Models.Shared.ValueItem item)
        {
            var client = ClientConnection.GetCmsConnection();
            try
            {
                client.Open();
                var response = await client.SearchClientAsync(new SearchClientRequest()
                {
                    SearchInput = item.Value,
                    IsIncludeStartsWith = true,
                    IsIncludeContains = true,
                    MessageGuid = Guid.NewGuid()
                });
                client.Close();
                var results = new Results();
                response.Clients.ForEach(record => results.List.Add(
                    ParseClient(record)));
                return results;
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.Results
                {
                    IsError = true,
                    Message = ex.Message.ToString(CultureInfo.InvariantCulture)
                };
            }
        }
        public static async Task<Results> ListAll()
        {
            var client = ClientConnection.GetCmsConnection();
            try
            {
                client.Open();
                var response = await client.GetClientsAsync(new GetClientsRequest()
                {
                    CorporationRID = 10,
                    MessageGuid = Guid.NewGuid()
                });
                client.Close();
                var results = new Results();
                response.Clients.ForEach(record => results.List.Add(
                    ParseClient(record)));
                return results;
            }
            catch (Exception ex)
            {
                client.Close();
                return new LG.Data.Models.Clients.Results
                {
                    IsError = true,
                    Message = ex.Message.ToString(CultureInfo.InvariantCulture)
                };
            }
        }
        private static SearchResult ParseClient(ClientSearchReturnRecord item)
        {
            return new SearchResult()
            {
                Name = item.Name,
                RID = item.RID,
                IsActive = item.IsActive,
                IsTesting = item.IsTesting
            };
        }
        //private static void AddClient(ref List<LG.Data.Models.Clients.SearchResult> list,
        //    ClientSearchReturnRecord item)
        //{
        //    list.Add(new SearchResult()
        //    {
        //        Name = item.Name,
        //        RID = item.RID,
        //        IsActive = item.IsActive,
        //        IsTesting = item.IsTesting
        //    });
        //}
    }
}
