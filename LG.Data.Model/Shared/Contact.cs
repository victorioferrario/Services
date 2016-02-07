using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Shared
{

    public class Contact : LG.Data.Models.BaseModel
    {
        public System.Int64 RID { get; set; }
        public LG.Data.Models.Enums.ContactAction EventAction { get; set; }
        public System.Boolean IsActive { get; set; }
        public PersonInfo PersonInfo { get; set; }
        public System.Int64 ContactForRID { get; set; }
        public List<LG.Services.EMS.Phone> Phones { get; set; }
        public List<LG.Services.EMS.EmailAddress> EmailAddresses { get; set; }
        public List<LG.Services.EMS.PhoneInput> NewPhones { get; set; }
        public List<LG.Services.EMS.EmailAddressInput> NewEmailAddresses { get; set; }
        public List<LG.Services.EMS.ContactUsage> ContactUsages { get; set; }

    }
}
