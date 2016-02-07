using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LG.Data.Models.Enums
{
    public enum SearchType
    {
        Undefined=0,
        ByName = 1,
        ByMemberNumber = 2,
        ByDateOfBirth = 3,
        ByClientID = 4,
    }
}