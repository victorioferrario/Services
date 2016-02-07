using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Core
{
    public interface IRolodexItemObject
    {
        System.Boolean IsActive { get; set; }
        System.Boolean IsTesting { get; set; }
        System.Int64 RolodexItemId { get; set; }
        LG.Data.Models.Enums.RolodexItemType RolodexItemType { get; set; }
    }

    public class RolodexItemObject : IRolodexItemObject
    {
        public System.Boolean IsActive { get; set; }
        public System.Boolean IsTesting { get; set; }
        public System.Int64 RolodexItemId { get; set; }
        public LG.Data.Models.Enums.RolodexItemType RolodexItemType { get; set; }
    }
}
