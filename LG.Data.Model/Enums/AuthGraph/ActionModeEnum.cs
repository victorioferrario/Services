using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Enums.AuthGraph
{
    public enum ActionModeEnum : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        All = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ReadOnly = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 2,
    }
}
