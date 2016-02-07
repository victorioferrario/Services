using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Shared
{
    public class ValidEmail
         : LG.Data.Models.BaseModel
    {
        public Int64 RID { get; set; }
        public String Value { get; set; }
        public Boolean Valid { get; set; }
        public ValidEmail()
            : base()
        {
            this.Valid = false;
        }
    }
}
