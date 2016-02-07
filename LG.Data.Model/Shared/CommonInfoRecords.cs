using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.CES;

namespace LG.Data.Models.Shared
{
    public class CommonInfoRecords:LG.Data.Models.BaseModel
    {
        public  CommonInfoCounts Data { get; set; }
    }
}
