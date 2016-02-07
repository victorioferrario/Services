using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Shared
{
    public interface IAddress
    {
        int ID { get; set; }
        System.Int64 RID { get; set; }
        System.String AddressLine1 { get; set; }
        System.String AddressLine2 { get; set; }
        System.String City { get; set; }
        System.String State { get; set; }
        System.String ZipCode { get; set; }
        System.String CountryCode { get; set; }
        System.Boolean IsValid { get; set; }
        System.Boolean IsPrimary { get; set; }
        System.Int32 AddressUsageEnum { get; set; }
        LG.Services.EMS.AddressUsageEnum AddressUsageEnumValue { get; set; }
        LG.Services.EMS.AddressUsageEnum AddressUsageEnumToUpdate { get; set; }
    }

    public class Address : IAddress
    {
        public int ID { get; set; }
        public System.Int64 RID { get; set; }
        public System.String AddressLine1 { get; set; }
        public System.String AddressLine2 { get; set; }
        public System.String City { get; set; }
        public System.String State { get; set; }
        public System.String ZipCode { get; set; }
        public System.String CountryCode { get; set; }
        public System.Boolean IsValid { get; set; }
        public System.Boolean IsPrimary { get; set; }
        public System.Int32 AddressUsageEnum { get; set; }
        public LG.Services.EMS.AddressUsageEnum AddressUsageEnumValue { get; set; }
        public LG.Services.EMS.AddressUsageEnum AddressUsageEnumToUpdate { get; set; }
    }
}
