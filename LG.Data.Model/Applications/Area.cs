using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Applications
{
    public class Area
    {
        public LG.Data.Models.Enums.Applications.AreaEnum Name { get; set; }
        public LG.Data.Models.Applications.AreaRole User { get; set; }
        public LG.Data.Models.Applications.AreaRole Administrator { get; set; }
    }
    public class AreaRole
    {
        public LG.Data.Models.Enums.Applications.AreaEnum Role { get; set; }
        public LG.Data.Models.Enums.AuthGraph.AccessTypeEnum AccessType { get; set; }
        public LG.Data.Models.Enums.AuthGraph.AccessModeEnum AccessMode { get; set; }
        public LG.Data.Models.Enums.AuthGraph.ActionModeEnum ActionMode { get; set; }
    }
}
