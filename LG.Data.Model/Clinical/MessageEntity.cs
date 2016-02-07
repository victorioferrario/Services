using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Clinical
{
    public class MessageInstance : LG.Data.Models.BaseModel
    {
        public Int32 RecordID { get; set; }
        public bool IsHidden { get; set; }

    }


    public class MessagingEntity:LG.Data.Models.BaseModel
    {
        public string NewMessage { get; set; }
        public Int32 NewMessageID { get; set; }
        public Int64 SenderID { get; set; }
        public Int64 MemberRID { get; set; }
        public Int32 OrderID { get; set; }
        public Int32 ConsultationID { get; set; }
        public IEnumerable<LG.Services.CDMS.ConsultationMessage> Messages { get; set; } 
    }
}
