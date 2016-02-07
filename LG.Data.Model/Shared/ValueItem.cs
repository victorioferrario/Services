using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Shared
{
    public class ValueItem
            : LG.Data.Models.BaseModel
    {
        public Int64 RID { get; set; }
        public String Value { get; set; }
        public ValueItem()
            : base()
        {

        }
    }
}
