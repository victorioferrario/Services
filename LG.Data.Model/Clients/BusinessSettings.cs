using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Clients
{
    public class BusinessSettings:BaseModel
    {
        public LG.Data.Models.Enums.BusinessLevel Level { get; set; }

        public Decimal FMPM { get; set; }
        public Decimal PMPM { get; set; }
        public Int64 GroupRID { get; set; }
        public Int64 ClientRID { get; set; }

        public LG.Services.BSCS.BillingTypeEnum BillingType { get; set; }
       
        public Int32 MembershipPlanID { get; set; }
        public System.Boolean IsActive { get; set; }

        public System.String ProgBag
        {
            get { return "<PropBag><Application>Web.Api</Application></PropBag>"; }
        }
    }
}
