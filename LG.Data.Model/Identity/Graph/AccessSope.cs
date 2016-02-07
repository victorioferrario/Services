using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Identity.Graph
{
    public class AccessScope
    {
        public System.Int64 RId { get; set; }
        public System.String Inherit { get; set; }
        public System.String ScopeType { get; set; }
        public AccessEntity EntityType { get; set; }
    }
}
