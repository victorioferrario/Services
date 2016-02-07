using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Shared
{
    public class File
    {
        public System.Int64 RID { get; set; }
        public System.Int64 ChildRID { get; set; }
        public String Name { get; set; }
        public System.String Uri { get; set; }
        public System.String FilePath { get; set; }
        public System.Boolean IsError { get; set; }
        public System.String ErrorMessage { get; set; }
    }
    public class MediaItem
    {
        public Int64 RID { get; set; }
        public string Value { get; set; }
    }
}
