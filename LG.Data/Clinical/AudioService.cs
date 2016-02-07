using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Clinical
{
   public class AudioService
    {
        public async static Task<LG.Data.Models.Clinical.AudioRecording> GetCallRecording(
            LG.Data.Models.Clinical.AudioRecording entityAudioRecording)
        {
            return await LG.Data.Core.Clinical.AudioManagementDataService.GetCallRecording(entityAudioRecording);
        }
        public async static Task<LG.Data.Models.Clinical.AudioRecording> EnsureLocalCopyOfIVRCallRecording(
            LG.Data.Models.Clinical.AudioRecording entityAudioRecording)
        {
            return await LG.Data.Core.Clinical.AudioManagementDataService.EnsureLocalCopyOfIVRCallRecording(entityAudioRecording);
        }
        public async static Task<LG.Data.Models.Clinical.AudioCallList> GetCallAssociatedWithConsultation(
            Int32 consultationId)
        {
            return await LG.Data.Core.Clinical.AudioManagementDataService.GetCallAssociatedWithConsultation(consultationId);
        }
    }
}
