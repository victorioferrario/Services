using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Clients
{
    public interface IGroupBase
    {
        System.Int64 GroupRID { get; set; }
        System.Int64 ClientRID { get; set; }
      
        System.Boolean IsActive { get; set; }
        System.Boolean IsTesting { get; set; }
    }
    public interface IGroup
    {
        System.String GroupName { get; set; }
        System.String GroupNumber { get; set; }
        System.String LogoUrl { get; set; }
        System.String ActivationUrl { get; set; }
        List<MembershipPlan> List { get; set; } 
    }
    public class GroupBase
        : BaseModel, IGroupBase
    {
        public System.Int64 GroupRID { get; set; }
        public System.Int64 ClientRID { get; set; }
       
        public System.Boolean IsActive { get; set; }
        public System.Boolean IsTesting { get; set; }
       
        public GroupBase()
            : base()
        {
           
        }
    }
    public class Group 
        : GroupBase, IGroup
    {
        public Group()
            : base()
        {

        }
        public System.String GroupName { get; set; }
        public System.String GroupNumber { get; set; }
        public List<MembershipPlan> List { get; set; } 
        public System.String LogoUrl { get; set; }
        public System.String ActivationUrl { get; set; }

    }

    public class GroupSearch : BaseModel
    {
        public System.String SearchText { get; set; }
        public List<LG.Services.GMS.GroupSearchReturnRecord> Results { get; set; }
        public System.Boolean IsIncludeContains { get; set; }
        public System.Boolean IsIncludeStartsWith { get; set; }
    }
   
}
