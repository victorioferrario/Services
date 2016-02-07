using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LG.UX.Identity.Utilities.AuthGraph
{
    public static class AccessConfigurationXNames
    {
        public static XName XrId = "RID";
        public static XName XRoleId = "roleID";
        public static XName XRoleName = "roleName";
        public static XName XAccessConfiguration = "AccessConfiguration";
        public static XName XAccess = "Access";
    }
    public static class AccessXNames
    {
        public static XName XAccessType = "accessType";
        public static XName XAccessMode = "accessMode";
        public static XName XActionMode = "actionMode";
        public static XName XFeatureName = "featureName";
    }
    public static class ScopeXNames
    {
        public static XName XscopeType = "scopeType";
        public static XName XisInherit = "isInherit";
        public static XName XentityTypeId = "entityTypeID";
        public static XName XentityTypeName = "entityTypeName";
    }
}
