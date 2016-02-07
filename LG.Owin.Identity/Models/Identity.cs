using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Owin.Identity.Models
{
    public class IdentityUser
    {
        public Int64 RID { get; set; }
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string TokenID { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsPrimaryMember { get; set; }
        public bool IsAccountManager { get; set; }
    }
}
