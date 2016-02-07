using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Shared
{
    public interface IPhone
    {
        System.Boolean IsPrimary { get; set; }
        System.Int32 PhoneId { get; set; }
        System.String CountryCode { get; set; }
        System.String PhoneNumber { get; set; }
        System.String PhoneExtension { get; set; }
        LG.Services.EMS.PhoneUsageEnum PhoneUsageEnum { get; set; }
    }

    public class PhoneBase : IPhone
    {
        public System.Boolean IsPrimary { get; set; }
        public System.Int32 PhoneId { get; set; }
        public System.String CountryCode { get; set; }
        public System.String PhoneNumber { get; set; }
        public System.String PhoneExtension { get; set; }
        
        public LG.Services.EMS.PhoneUsageEnum PhoneUsageEnum { get; set; }
        public LG.Services.EMS.PhoneUsageEnum PhoneUsageEnumToUpdate { get; set; }
        public PhoneBase()
        {

        }
        public PhoneBase(IPhone phone)
        {
            if (phone == null) return;
            PhoneId = phone.PhoneId;
            CountryCode = phone.CountryCode;
            PhoneNumber = phone.PhoneNumber;
            PhoneExtension = phone.PhoneExtension;
            IsPrimary = phone.IsPrimary;
            PhoneUsageEnum = phone.PhoneUsageEnum;
        }
    }
}
