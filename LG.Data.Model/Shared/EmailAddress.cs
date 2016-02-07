using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Shared
{
    public interface IEmailAddress
    {
        System.Int32 Id { get; set; }
        System.String Email { get; set; }
        System.Boolean IsPrimary { get; set; }
        LG.Services.EMS.EmailAddressUsageEnum EmailUsageEnum { get; set; }
    }

    public class EmailAddress : IEmailAddress
    {
        public System.Int32 Id { get; set; }

        public System.String Email { get; set; }

        public System.Boolean IsPrimary { get; set; }

        public LG.Services.EMS.EmailAddressUsageEnum EmailUsageEnum { get; set; }

        public EmailAddress()
        {

        }

        public EmailAddress(IEmailAddress email)
        {
            this.Populate(email);
        }
        public void Populate(IEmailAddress email)
        {
            if (email == null) return;
            this.Email = email.Email;
            this.Id = email.Id;
            this.IsPrimary = email.IsPrimary;
            this.EmailUsageEnum = email.EmailUsageEnum;
        }
    }
}
