using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LG.Data.Models.Users
{

    public interface IUserBase
    {
        System.Int64 UserRID { get; set; }
        System.Int64 ClientRID { get; set; }
        System.Int32 UserTypeEnumInt { get; set; }
        LG.Services.UMS.UserTypeEnum UserTypeEnum { get; set; }
    }
    public interface IUserModel : IUserBase
    {
        Models.Enums.Action EventAction { get; set; }
        LG.Data.Models.Shared.PhoneBase Phone { get; set; }
        LG.Data.Models.Shared.EmailAddress EmailAddress { get; set; }
        LG.Data.Models.Shared.PersonInfo GeneralInfo { get; set; }
        LG.Data.Models.Shared.SecurityInfo SecurityInfo { get; set; }
    }
    public class UserModel : UserBase, IUserModel
    {
        public UserModel()
            : base()
        {
            EventAction = Models.Enums.Action.None;
        }
        public Models.Enums.Action EventAction { get; set; }
        public LG.Data.Models.Shared.PhoneBase Phone { get; set; }
        public List<LG.Data.Models.Shared.PhoneBase> Phones { get; set; }
        public List<LG.Data.Models.Shared.Address> Addresses { get; set; }
        public LG.Data.Models.Shared.EmailAddress EmailAddress { get; set; }
        public List<LG.Data.Models.Shared.EmailAddress> EmailAddresses { get; set; }

        public LG.Data.Models.Shared.PersonInfo GeneralInfo { get; set; }
        public LG.Data.Models.Shared.SecurityInfo SecurityInfo { get; set; }

    }
    public class UserBase : BaseModel, IUserBase
    {
        public System.Int64 UserRID { get; set; }
        public System.Int64 ClientRID { get; set; }
        public System.Int32 UserTypeEnumInt { get; set; }
        public LG.Services.UMS.UserTypeEnum UserTypeEnum { get; set; }
        public System.Int32 PrevUserTypeEnumInt { get; set; }

        public UserBase() : base() { }
    }
    public static class Utilities
    {
        public static T GetAreaEnum<T>(int value)
        {
            var returnValue = default(T);
            if (!System.Enum.IsDefined(typeof(T), value))
            {
                return returnValue;
            }
            else
            {
                return (T)(object)returnValue;
            }
            // return (T)

            //var t = System.Enum.GetNames(typeof(T)).ToList();
            //for (var i = 0; i < t.Count() - 1; i++)
            //{
            //    if (t[i] == value)
            //    {
            //        var re = (AreaEnum)System.Enum.Parse(typeof(AreaEnum), value); return re;
            //    }
            //}
            // return AreaEnum.Undefined;
        }
    }
}