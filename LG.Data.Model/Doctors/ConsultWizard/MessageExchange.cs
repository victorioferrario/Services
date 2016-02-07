using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Doctors.ConsultWizard
{
    public class MessageExchange : LG.Data.Models.BaseModel
    {
        public int Id { get; set; }
        public string NewMessage { get; set; }
        public string DateTimeCreated { get; set; }
    }
    public class DataTransportEntity : LG.Data.Models.BaseModel
    {
        public MessageExchange ConsultExchange { get; set; }
    }
}

