using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Shared
{
    public interface ISecurityInfo
    {
        System.Int64 RID { get; set; }
        System.Boolean IsActive { get; set; }
        System.String Username { get; set; }
        System.String Password { get; set; }
        System.Boolean IsTempPassword { get; set; }
        System.DateTime? DateTimePasswordExpires { get; set; }


    }

    public class SecurityInfo : ISecurityInfo
    {
        public System.Int64 RID { get; set; }
        public System.Boolean IsActive { get; set; }
        public System.String Username { get; set; }
        public System.String Password { get; set; }
        public System.Boolean IsTempPassword { get; set; }
        public System.DateTime? DateTimePasswordExpires { get; set; }
        public SecurityInfo()
        {

        }
        public SecurityInfo(ISecurityInfo securityInfo)
        {
            if (securityInfo != null)
            {
                this.Populate(securityInfo);
            }
        }

        private void Populate(ISecurityInfo securityInfo)
        {
            this.IsActive = securityInfo.IsActive;
            this.IsTempPassword = securityInfo.IsTempPassword;
            this.Password = securityInfo.Password;
            this.Username = securityInfo.Username;
            if (securityInfo.DateTimePasswordExpires.HasValue) this.DateTimePasswordExpires = DateTimePasswordExpires.Value;

        }
    }
}
