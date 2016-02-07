using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Enums.Applications
{
    public enum FeatureRoleEnum
    {
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Administrator = 1,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        User = 2,
    }

    public enum AreaEnum
    {
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Dashboard = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ClientManagement = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MemberManagement = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserManagement = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        HealthServicesProviderManagement = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Reports = 5,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Accounting = 6,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Undefined = 1000,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Administrator = 34,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        User = 35,

    }

    public enum ApplicationPortal
    {
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        AdministrativeDashboard = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ClientManagement = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MemberManagement = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        HealthServicesProviderManagement = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserAdministration = 5,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Reports = 6,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Accounting = 7

    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "FeatureNameEnum", Namespace = "http://schemas.datacontract.org/2004/07/LG.Contracts.AUTH")]
    public enum FeatureNameEnum : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        AdministrationDash = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        CorporationPortal = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        CorporationAdminArea = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        CorporationUserArea = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        EntityManagement = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Reports = 5,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Accounting = 6,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSPPortal = 7,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSPAdminArea = 8,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSPUserArea = 9,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        PractitionerManagement = 10,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Scheduling = 11,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ConsultManagement = 12,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ClientManagement = 13,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ClientAdminArea = 14,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ClientUserArea = 15,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        AccountSettings = 16,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MemberManagement = 17,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        EligibilityManagement = 18,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        GroupManagement = 19,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSPortal = 20,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSAdminArea = 21,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSUserArea = 22,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSRepManagement = 23,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MemberAccess = 24,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        BrokerPortal = 25,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        BrokerAdminArea = 26,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        BrokerAccountManagement = 27,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ContractManagement = 28,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        BrokerUserArea = 29,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        PractitionerDashboard = 30,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MemberDashboard = 31,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserAdministration = 32,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 33,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Administrator = 34,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        User = 35,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        HealthServicesProviderManagement = 36,


    }
}
