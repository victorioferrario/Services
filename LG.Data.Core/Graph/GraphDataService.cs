using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Users;
using LG.Services;
using LG.Services.UMS;

namespace LG.Data.Core.Graph
{
    public static class GraphDataService
    {

        private static async Task<UserListModel> GetCorporationUsersTask()
        {
            var client = ClientConnection.GetConnection();
            try
            {
                client.Open();
                var response = await client.GetCorporationUsersAsync(new GetCorporationUsersRequest()
                {
                    MessageGuid = new Guid(),
                    CorporationRID = 10
                });
                client.Close();
                return new UserListModel
                {
                    List = response.Users,
                    IsError = false,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new UserListModel
                {
                    IsError = false,
                    Message = "Failde:" + ex
                };
            }
        }


        public static async Task<LG.Data.Models.Identity.Graph.AuthGraphEntity>
                Load(System.Int64 RID)
            {
                var result = new LG.Data.Models.Identity.Graph.AuthGraphEntity();
                var client = LG.Services.ClientConnection.GetConnection();
                try
                {
                    client.Open();
                    var response =await client.GetAuthGraphForUserAsync(
                        new LG.Services.UMS.GetAuthGraphForUserRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            UserRID = RID
                        });
                    result.AuthGraph = response.AuthGraph;
                }
                catch (Exception exception)
                {
                    client.Abort();
                    result.IsError = true;
                    result.Message = exception.Message; client.Abort();
                }
                finally
                {
                    if (client.State != System.ServiceModel.CommunicationState.Closed)
                    {
                        client.Close();
                    }
                }
                return result;
            }
    }
}
