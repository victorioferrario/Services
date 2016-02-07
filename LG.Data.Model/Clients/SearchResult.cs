using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Clients
{
    public class SearchResult
    {
        public long RID { get; set; }
        public System.String Name { get; set; }
        public System.Boolean IsActive { get; set; }
        public System.Boolean IsTesting { get; set; }
    }

    public class Results : BaseModel
    {
        public Results()
            : base()
        {
            List = new List<SearchResult>();
        }
        public List<SearchResult> List { get; set; }
    }
}
