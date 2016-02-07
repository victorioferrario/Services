using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums;
using LG.Services.MPS;
using MembershipPlan = LG.Data.Models.Clients.MembershipPlan;

namespace LG.Data.Utilities.Clients
{
    public class GenericMembershipPlans
    {
        public LG.Data.Models.Clients.MembershipPlan EmployeeBasic { get; set; }
        public LG.Data.Models.Clients.MembershipPlan EmployeeFamily { get; set; }
        public LG.Data.Models.Clients.MembershipPlan EmployeeSpouse { get; set; }
        public LG.Data.Models.Clients.MembershipPlan EmployeeChildren { get; set; }
        public GenericMembershipPlans(System.Int64 groupID)
        {
            EmployeeBasic = new MembershipPlan()
            {
                IsActive = true,
                GroupID = groupID,
                PerMemberPerMonth = 0M,
                FamilyMemberPerMonth = 0M,
                BillingType = BillingTypeEnum.PerMemberPerMonth,
                CoverageCode = LG.Data.Models.Enums.CoverageCode.EE.ToString(),
                MembershipLabel = GetLabel(LG.Data.Models.Enums.CoverageCode.EE),
            };
            EmployeeSpouse = new MembershipPlan()
            {
                IsActive = true,
                GroupID = groupID,
                PerMemberPerMonth = 0M,
                FamilyMemberPerMonth = 0M,
                BillingType = BillingTypeEnum.PerMemberPerMonth,
                CoverageCode = LG.Data.Models.Enums.CoverageCode.ES.ToString(),
                MembershipLabel = GetLabel(LG.Data.Models.Enums.CoverageCode.ES),
            };
            EmployeeChildren = new MembershipPlan()
            {
                IsActive = true,
                GroupID = groupID,
                PerMemberPerMonth = 0M,
                FamilyMemberPerMonth = 0M,
                BillingType = BillingTypeEnum.PerEmployeePerMonth,
                CoverageCode = LG.Data.Models.Enums.CoverageCode.EC.ToString(),
                MembershipLabel = GetLabel(LG.Data.Models.Enums.CoverageCode.EC),
            };
            EmployeeFamily = new MembershipPlan()
            {
                IsActive = true,
                GroupID = groupID,
                PerMemberPerMonth = 0M,
                FamilyMemberPerMonth = 0M,
                BillingType = BillingTypeEnum.PerMemberPerMonth,
                CoverageCode = LG.Data.Models.Enums.CoverageCode.FAM.ToString(),
                MembershipLabel = GetLabel(LG.Data.Models.Enums.CoverageCode.FAM),
            };
        }
        public static System.String GetLabel(LG.Data.Models.Enums.CoverageCode value)
        {
            switch (value)
            {
                case CoverageCode.EE:
                    return "Employee Basic";
                case CoverageCode.ES:
                    return "Employee and Spouse";
                case CoverageCode.EC:
                    return "Employee and Children";
                case CoverageCode.FAM:
                    return "Family";
            }
            return String.Empty;
        }
    }
}
