using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Enums.AuthGraph
{
    public enum AccessTypeEnum
    {
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DescendantsInheritCurrentPermissions = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MustCheckDescendantsPermissions = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        DescendantsDenied = 2,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Unknown = 3,
    }
}
