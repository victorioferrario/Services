using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums;
using LG.Services.EMS;
using Action = LG.Data.Models.Enums.Action;

namespace LG.Data.Models.Core
{
    public class Events
    {
        public Models.Enums.Action EventAction { get; set; }
        public Models.Enums.ActionResult EventActionResult { get; set; }
        public Models.Enums.AccountAction AccountAction { get; set; }
        public Models.Enums.ActionResult AccountActionResult { get; set; }
    }
    public class BaseEntity : LG.Data.Models.BaseModel
    {
        public System.Int64 RID { get; set; }
        public BaseEntity() : base() { }
        public List<LG.Services.EMS.Phone> Phones { get; set; }
        public LG.Services.EMS.PersonInfo PersonInfo { get; set; }
        public List<LG.Services.EMS.Address> Addresses { get; set; }
        public List<LG.Services.EMS.EmailAddress> EmailAddresses { get; set; }

    }
    public class Entity : BaseEntity
    {
        public Events Events { get; set; }
        public Models.Enums.Action EventAction { get; set; }
        public Models.Enums.ActionResult EventActionResult { get; set; }
        public Models.Enums.AccountAction AccountAction { get; set; }
        public Models.Enums.ActionResult AccountActionResult { get; set; }
        
        public Entity()
            : base()
        {
            EventAction = Action.None;
            EventActionResult = ActionResult.None;
            Phones = new List<Phone>();
            Addresses = new List<Address>();
            EmailAddresses = new List<EmailAddress>();
        }

        public System.Int64 GroupRID { get; set; }
        public System.Int64 ClientRID { get; set; }
        public System.Int32 MembershipPlanID { get; set; }
        public LG.Services.EMS.RTypeEnum EntityType { get; set; }

    }
}
