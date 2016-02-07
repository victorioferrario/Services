using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Enums.AuthGraph
{
    public enum AccessModeEnum
    {
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Granted = 0,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Denied = 1,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Undefined = 2,
    }
}
