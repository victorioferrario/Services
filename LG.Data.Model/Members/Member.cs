using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Shared;
using LG.Services.AMS;

namespace LG.Data.Models.Members
{
    public class MemberInstance : LG.Data.Models.Users.UserModel
    {
        public System.Int64 GroupRID { get; set; }
        public System.Boolean IsActive { get; set; }
        public Account AccountDetails { get; set; }
        public MemberInstance()
        {

        }
    }


    public class EligibilityEntityInstance : Entity
    {
        public System.Int64 GroupRID { get; set; }
        public System.Boolean IsActive { get; set; }
        public Account AccountDetails { get; set; }
        public PersonInfo PInfo { get; set; }
    }

    public class Entity : LG.Data.Models.Core.Entity
    {
        public Entity()
            : base()
        {

        }
        public System.String Email { get; set; }
        public System.String Phone { get; set; }
        public System.String CoverageCode { get; set; }
        public System.String GroupNumber { get; set; }
        public System.String MemberNumber { get; set; }
        public System.DateTime EffectiveDate { get; set; }

        public LG.Services.ACS.Address Address { get; set; }

        public LG.Services.AMS.LoginInfo LoginInfo { get; set; }
        public List<LG.Services.ACS.AccountInfoExtended> Accounts { get; set; }
        public LG.Services.ACS.AccountInfo AccountInfo { get; set; }
        public LG.Services.ACS.AccountInfo_Input AccountInfoInput { get; set; }
        public List<LG.Services.ACS.CreditCardInfo> CreditCards { get; set; }
        public LG.Services.ACS.CreditCardInfo_Input CreditCardInfoInput { get; set; }
    }

}
