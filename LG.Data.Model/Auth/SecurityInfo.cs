using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Auth
{
    public class SecurityInfo : BaseModel
    {
        public System.Int64 RID { get; set; }
        public System.String UserName { get; set; }
        public System.String PlainPassword { get; set; }
        public System.Boolean IsActive { get; set; }
        public System.Boolean IsTemporaryPassword { get; set; }
        public System.DateTime DTUTC_PasswordExpires { get; set; }

        public System.String PropBag { get { return "LG.Data.Models.Auth.SecurityInfo"; } }

    }
    public class LoginInfo : BaseModel
    {
        public System.String Username { get; set; }
        public System.String Password { get; set; }
        public System.String PropBag { get { return "LG.Data.Models.Auth.SecurityInfo"; } }

    }
}
