using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Clinical
{
    public class AudioCallList:LG.Data.Models.BaseModel
    {
        public System.Int32 ConsultationID { get; set; }
        public IEnumerable<LG.Services.IVR.AUDIO.IVRCallInfo> ListOfCalls { get; set; }
    }
    public class AudioRecording : LG.Data.Models.BaseModel
    {
        public String CallSid { get; set; }
        public Boolean IsForcedToRefresh { get; set; }
        public String LocalPathToMP3AudioFile { get; set; }
        public LG.Services.IVR.AUDIO.IVRProvidersEnum Provider { get; set; }
       
    }
}
