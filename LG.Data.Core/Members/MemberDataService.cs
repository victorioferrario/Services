using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums;
using LG.Services.ACS;
using LG.Services.MSS;

namespace LG.Data.Core.Members
{
    public static class MemberDataService
    {
        public static async Task<LG.Data.Models.Members.Account> GetAccountInfo(System.Int64 RID)
        {
            var client = LG.Services.ClientConnection.GetAcsConnection();
            var result = new LG.Data.Models.Members.Account()
            {
                RID = RID
            };
            try
            {
                client.Open();
                var response = await client.GetAccountInfoAsync(new GetAccountInfoRequest()
                    {
                        RID = RID,
                        MessageGuid = Guid.NewGuid()
                    });
                result.AccountInfo = response.AccountInfo;
            }
            catch (Exception ex)
            {
                client.Abort();
                result.IsError = true;
                result.Message = ex.ToString();
            }
            finally
            {
                client.Close();
            }
            return result;
        }
        public static async Task<LG.Data.Models.Members.Account> GetAccountInfo(System.Int64 RID, System.Int32 AccountID)
        {
            var client = LG.Services.ClientConnection.GetAcsConnection();
            var result = new LG.Data.Models.Members.Account()
            {
                RID = RID,
                AccountID = AccountID
            };
            try
            {
                client.Open();
                var response = await client.GetAccountInfoAsync(new GetAccountInfoRequest()
                {
                    RID = RID,
                    AccountID = AccountID,
                    MessageGuid = Guid.NewGuid()
                });
                result.AccountInfo = response.AccountInfo;
            }
            catch (Exception ex)
            {
                client.Abort();
                result.IsError = true;
                result.Message = ex.ToString();
            }
            finally
            {
                client.Close();
            }
            return result;
        }

        public static async Task<LG.Data.Models.Members.Account> JitAccount(System.Int64 RID)
        {
            var client = LG.Services.ClientConnection.GetAcsConnection();
            try
            {
                client.Open();
                var response = await client.JITAccountAsync(
                        new JITAccountRequest()
                        {
                            RID = RID,
                            MessageGuid = Guid.NewGuid(),
                            IsTesting = false,
                            PropBag = "<PropBag>LG.Data.Core.Members.JitAccount</PropBag>"
                        });
                return await GetAccountInfo(RID, response.AccountID);
            }
            catch (Exception ex)
            {
                client.Abort();
                return new LG.Data.Models.Members.Account()
                {
                    RID = RID,
                    IsError = true,
                    Message = ex.ToString()
                };
                
            }
            finally
            {
                client.Close();
            }
            return null;
        }

        public static async Task<LG.Data.Models.Members.SearchResults>
           Search(LG.Data.Models.Members.SearchRequest entityRequest)
        {
            switch (entityRequest.Type)
            {
                case SearchType.ByName:
                    return await SearchByName(entityRequest);
                case SearchType.ByDateOfBirth:
                    return await SearchByDateOfBirth(entityRequest);
                case SearchType.ByMemberNumber:
                    return await SearchByMemberNumber(entityRequest);
            }
            return null;
        }
        
        internal static async Task<LG.Data.Models.Members.SearchResults> 
            SearchByName(LG.Data.Models.Members.SearchRequest entityRequest)
        {
            var client = LG.Services.ClientConnection.GetMssConnection();
            var result = new LG.Data.Models.Members.SearchResults();
            try
            {
                client.Open();
                var response =
                    await client.SearchMemberByNameAsync(
                        new SearchMemberByNameRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            FName = entityRequest.FName,
                            LName = entityRequest.LName,
                            IsAllowingApproximateMatches = entityRequest.IsAllowingApproximateMatches
                        });
                result.IsError = false;
                result.Records = response.ListOfFoundMemberRecords;
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.ToString();
                client.Abort();
            }
            finally
            {
                client.Close();
            }
            return result;
        }
        internal static async Task<LG.Data.Models.Members.SearchResults>
           SearchByDateOfBirth(LG.Data.Models.Members.SearchRequest entityRequest)
        {
            var client = LG.Services.ClientConnection.GetMssConnection();
            var result = new LG.Data.Models.Members.SearchResults();
            try
            {
                client.Open();
                var response =
                    await client.SearchMemberByDOBAsync(
                        new SearchMemberByDOBRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            DOB = entityRequest.Dob
                        });
                result.IsError = false;
                result.Records = response.ListOfFoundMemberRecords;
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.ToString();
                client.Abort();
            }
            finally
            {
                client.Close();
            }
            return result;
        }
        internal static async Task<LG.Data.Models.Members.SearchResults>
           SearchByMemberNumber(LG.Data.Models.Members.SearchRequest entityRequest)
        {
            var client = LG.Services.ClientConnection.GetMssConnection();
            var result = new LG.Data.Models.Members.SearchResults();
            try
            {
                client.Open();
                var response =
                    await client.SearchMemberByMemberNumberAsync(
                        new SearchMemberByMemberNumberRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            MemberNumber = entityRequest.MemberNumber
                        });
                result.IsError = false;
                result.Records = response.ListOfFoundMemberRecords;
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.ToString();
                client.Abort();
            }
            finally
            {
                client.Close();
            }
            return result;
        }

        public static async Task<LG.Data.Models.Members.SortedSearchResults> GetClientMembers(
            LG.Data.Models.Members.SortedSearchRequest entityRequest)
        {
            var client = LG.Services.ClientConnection.GetMssConnection();
            var result = new LG.Data.Models.Members.SortedSearchResults();
            try
            {
                client.Open();
                var response =
                    await client.GetMembersInOneSortedPageUsingFilterAsync(
                        new GetMembersInOneSortedPageUsingFilterRequest()
                        {
                            Filter = new GetMembersInOneSortedPageFilter()
                            {
                                ClientRID = entityRequest.ClientID,
                                PageSize = entityRequest.PageSize,
                                PageIndex = entityRequest.PageIndex,
                                SortOrder = entityRequest.SortOrder,
                                IsActiveMemberFilter = entityRequest.IsActiveMember
                            }
                        });
                result.TotalPages = response.TotalPageCount;
                result.Records = response.ListOfMemberRecords;
                result.TotalMemberCount = response.TotalMembersWithFilterCount;
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.ToString();
                client.Abort();

            }
            finally
            {
                client.Close();
            }
            return result;
        }



    }
}
