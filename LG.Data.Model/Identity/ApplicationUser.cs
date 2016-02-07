using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Identity
{
    public class ApplicationUser 
        : LG.Data.Models.Users.UserIdentity
    {
        public ApplicationUser():base() { }
        public List<LG.Data.Models.Identity.Application> Applications { get; set; }
        public List<LG.Data.Models.Identity.Graph.AccessConfiguration> AuthGraphList { get; set; }
      
    }
}
