using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LG.Owin.Security.Models
{
    public class IdentityUserPractioner
    {
        public Guid AuthGuid { get; set; }
        public NameEntity Name { get; set; }
        public long RolodexItemID { get; set; }
        public bool IsAuthenticated { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpiry { get; set; }
    }
    public class NameEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrintedName { get; set; }
    }
}
