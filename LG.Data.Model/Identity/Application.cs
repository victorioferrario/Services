using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Identity
{
    public class Application
    {
        public Application()
        {
            Areas = new List<LG.Data.Models.Applications.Area>();
        }
        public LG.Data.Models.Enums.Applications.ApplicationPortal AppPortalType { get; set; }
        public List<LG.Data.Models.Applications.Area> Areas { get; set; }
        public LG.Data.Models.Identity.Graph.AccessDetails Details { get; set; }
    }
}
