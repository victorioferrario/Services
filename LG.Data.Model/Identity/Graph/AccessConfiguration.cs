using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LG.Data.Models.Identity.Graph
{
    public class AccessConfiguration
    {
        public System.Int64 RId { get; set; }
        public System.Int32 RoleId { get; set; }
        public System.String RoleName { get; set; }
        public AccessDetails AccessDetails { get; set; }
    }

    public class AccessDetails
    {
        public LG.Data.Models.Enums.Applications.ApplicationPortal FeatureName { get; set; }
        public LG.Data.Models.Enums.AuthGraph.AccessTypeEnum AccessType { get; set; }
        public LG.Data.Models.Enums.AuthGraph.AccessModeEnum AccessMode { get; set; }
        public LG.Data.Models.Enums.AuthGraph.ActionModeEnum ActionMode { get; set; }
        public LG.Data.Models.Identity.Graph.AccessScope Scope { get; set; }
        public List<Access> AreaList { get; set; }
    }

    public class Access
    {
        public LG.Data.Models.Enums.Applications.AreaEnum FeatureName { get; set; }
        public LG.Data.Models.Enums.AuthGraph.AccessTypeEnum AccessType { get; set; }

        public LG.Data.Models.Enums.AuthGraph.AccessModeEnum AccessMode { get; set; }
        public LG.Data.Models.Enums.AuthGraph.ActionModeEnum ActionMode { get; set; }
        public AccessScope Scope { get; set; }
        public List<Role> Roles { get; set; }
    }

    public class Role
    {
        public LG.Data.Models.Enums.Applications.AreaEnum RoleName { get; set; }
        public LG.Data.Models.Enums.AuthGraph.AccessTypeEnum AccessType { get; set; }
        public LG.Data.Models.Enums.AuthGraph.AccessModeEnum AccessMode { get; set; }
        public LG.Data.Models.Enums.AuthGraph.ActionModeEnum ActionMode { get; set; }
        public List<Role> Roles { get; set; }
        public AccessScope Scope { get; set; }
    }
}
