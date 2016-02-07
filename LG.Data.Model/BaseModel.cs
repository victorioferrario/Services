using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models
{
    public interface IBaseModel
    {
        String Message { get; set; }
        Boolean IsError { get; set; }
    }
    public class BaseModel : IBaseModel
    {
        private Int64 _corporationRID = 10;
        public System.Int64 CorporationRID { get { return _corporationRID; } set { _corporationRID = value; } }
        public String Message { get; set; }
        public Boolean IsError { get; set; }
        public BaseModel()
        {
            this.IsError = false;
            this.Message = String.Empty;
        }
    }
}
