using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Shared;
using LG.Services.EMS;

namespace LG.Data.Models.Users
{
    public static class ParseFactory
    {
        public static UserModel ParseBEntity(LG.Services.EMS.LoadBEntityDataResponse entity)
        {
            var result = new UserModel()
            {
                UserRID = entity.BEntity.RID,
                Phone = ReturnPhone(entity.BEntity),
                EmailAddress = ReturnEmailAddress(entity.BEntity),
                UserTypeEnumInt = entity.BEntity.RTypeEnum.GetHashCode(),
                UserTypeEnum = entity.BEntity.RTypeEnum == RTypeEnum.Administrator
                ? LG.Services.UMS.UserTypeEnum.Admin
                : LG.Services.UMS.UserTypeEnum.User,
                IsError = entity.ReturnStatus.IsError,
                Message = entity.ReturnStatus.GeneralMessage
            };
            if (result.EmailAddress.Email.Length > 0)
            {
                result.SecurityInfo = new SecurityInfo()
                {
                    RID = result.UserRID,
                    Username = result.EmailAddress.Email
                };
            }
            return result;
        }

        internal static LG.Data.Models.Shared.EmailAddress ReturnEmailAddress(LG.Services.EMS.BEntity entity)
        {
                return entity.EmailAddresses.Any()
                    ? new LG.Data.Models.Shared.EmailAddress()
                    {
                        Email = entity.EmailAddresses.First().Email,
                        Id = entity.EmailAddresses.First().ID.HasValue ? entity.EmailAddresses.First().ID.Value : 0,
                        EmailUsageEnum =entity.EmailAddresses.First().EmailAddressUsages.First().EmailAddressUsageEnum,
                        IsPrimary = entity.EmailAddresses.First().EmailAddressUsages.First().IsPrimary
                    } : new LG.Data.Models.Shared.EmailAddress();
        }

        internal static LG.Data.Models.Shared.PhoneBase ReturnPhone(LG.Services.EMS.BEntity entity)
        {
            return entity.Phones.Any()
                ? new LG.Data.Models.Shared.PhoneBase()
                {
                    PhoneId = (!(!entity.Phones.First().ID.HasValue || entity.Phones.First().ID == null)
                        ? entity.Phones.First().ID.Value : 0),
                    PhoneNumber = entity.Phones.First().PhoneNumber,
                    CountryCode = entity.Phones.First().PhoneCountryCode,
                    PhoneExtension = entity.Phones.First().PhoneExtension,
                    PhoneUsageEnum = entity.Phones.First().PhoneUsages.First().PhoneUsageEnum,
                    IsPrimary = entity.Phones.First().PhoneUsages.First().IsPrimary
                } : new LG.Data.Models.Shared.PhoneBase();
        }
    }
}
