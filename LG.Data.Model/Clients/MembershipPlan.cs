using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Clients
{

    public interface IMembershipPlanBase
    {
        System.Int64 GroupID { get; set; }
        System.Int64 GroupRID { get; set; }
        System.Int64 ClientRID { get; set; }
     
        System.Int32 MembershipPlanID { get; set; }
    }

    public interface IMembershipPlan
    {
        System.Boolean IsActive { get; set; }
        System.String CoverageCode { get; set; }
        System.String MembershipLabel { get; set; }
        System.Decimal PerMemberPerMonth { get; set; }
        System.Decimal FamilyMemberPerMonth { get; set; }
        LG.Services.MPS.BillingTypeEnum BillingType { get; set; }
    }

    public class MembershipPlanBase : BaseModel, IMembershipPlanBase
    {
        public System.Int64 GroupID { get; set; }
        public System.Int64 GroupRID { get; set; }
        public System.Int64 ClientRID { get; set; }
     
        public System.Int32 MembershipPlanID { get; set; }
    }
    public class MembershipPlan : MembershipPlanBase, IMembershipPlan
    {
        public System.Boolean IsActive { get; set; }
        public System.String CoverageCode { get; set; }
        public System.String MembershipLabel { get; set; }
        public System.Decimal PerMemberPerMonth { get; set; }
        public System.Decimal FamilyMemberPerMonth { get; set; }
        public LG.Services.MPS.BillingTypeEnum BillingType { get; set; }
    }
}
