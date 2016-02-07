using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Enums
{
    public static class EnumHelpers
    {
        public static IEnumerable<T> GetEnumValues<T>()
        {
            if (typeof(T).BaseType != typeof(System.Enum))
            {
                throw new ArgumentException("T must be of type System.Enum");
            }
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
    public enum RolodexItemType : int
    {
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Undefined = 0,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        System = 1,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Corporation = 2,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MedicalServicesProvider = 3,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Practitioner = 4,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Client = 5,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Group = 6,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Member = 7,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Creator = 8,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Administrator = 9,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PaymentGatewayProvider = 10,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        IvrProvider = 11,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Pharmacy = 12,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MemberServicesRep = 13,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Contact = 14,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MessageSender = 15,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        User = 16,
    }

    public enum RTypeEnum : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Undefined = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        System = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Corporation = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MedicalServicesProvider = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Practitioner = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Client = 5,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Group = 6,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Member = 7,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Creator = 8,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Administrator = 9,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        PaymentGatewayProvider = 10,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        IvrProvider = 11,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Pharmacy = 12,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MemberServicesRep = 13,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Contact = 14,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MessageSender = 15,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        User = 16,
    }

}
