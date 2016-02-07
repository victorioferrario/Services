using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Members
{
    public class SearchRequest
        : SearchBaseRequest
    {
        public SearchRequest() { }
        public LG.Data.Models.Enums.SearchType Type { get; set; }
    }

    public class SearchBaseRequest
    {
        public DateTime Dob { get; set; }
        public System.String FName { get; set; }
        public System.String LName { get; set; }
        public System.String MemberNumber { get; set; }
        public System.Int64 ClientID { get; set; }
        public System.Int64 GroupID { get; set; }
        
        public System.Int64 MembershipPlanID { get; set; }
        public System.Boolean IsAllowingApproximateMatches { get; set; }
    }

    public class SortedSearchRequest : SearchBaseRequest
    {
        public SortedSearchRequest() : base() { }
        public System.Int32 PageSize { get; set; }
        public System.Int32 PageIndex { get; set; }
        public System.String LastNameStartsWith { get; set; }
        public LG.Services.MSS.SortOrderEnum SortOrder { get; set; }
        public LG.Services.MSS.IsActiveMemberFilterEnum IsActiveMember { get; set; }
    }

    /// <summary>
    /// Results
    /// </summary>
    public class SearchResults : LG.Data.Models.BaseModel
    {
        public SearchResults() : base() { }
        public List<LG.Services.MSS.FoundMemberRecord> Records { get; set; }
    }
    public class SortedSearchResults : SearchResults
    {
        public SortedSearchResults() : base() { }
        public System.Int32 TotalPages { get; set; }
        public System.Int32 TotalMemberCount { get; set; }
    }
}
