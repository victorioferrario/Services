using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using LG.Services;
using LG.Services.CDMS;
using LG.Services.IVR.AUDIO;

namespace LG.Data.Core.Clinical
{
    public static class AudioManagementDataService
    {
        public async static Task<LG.Data.Models.Clinical.AudioRecording> GetCallRecording(LG.Data.Models.Clinical.AudioRecording entityAudioRecording)
        {
            var client = ClientConnection.GetIVRAudio_Connection();
            try
            {
                client.Open();
                var response
                    = await client.GetIVRAudioRecordingAsync(new GetIVRAudioRecordingRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        CallSid = entityAudioRecording.CallSid,
                        IVRProvider = IVRProvidersEnum.Twilio
                    });
                entityAudioRecording.LocalPathToMP3AudioFile = response.LocalPathToMP3AudioFile;
                entityAudioRecording.IsError = response.ReturnStatus.IsError;
            }
            catch (Exception ex)
            {
                client.Abort();
                entityAudioRecording.IsError = true;
                entityAudioRecording.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            return entityAudioRecording;
        }
        public async static Task<LG.Data.Models.Clinical.AudioRecording>EnsureLocalCopyOfIVRCallRecording(LG.Data.Models.Clinical.AudioRecording entityAudioRecording)
        {
            var client = ClientConnection.GetIVRAudio_Connection();
            try
            {
                client.Open();
                var response
                    = await client.EnsureLocalCopyOfIVRAudioRecordingAsync(new EnsureLocalCopyOfIVRAudioRecordingRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        CallSid = entityAudioRecording.CallSid,
                        IVRProvider = IVRProvidersEnum.Twilio
                    });
                entityAudioRecording.IsError = response.ReturnStatus.IsError;
            }
            catch (Exception ex)
            {
                client.Abort();
                entityAudioRecording.IsError = true;
                entityAudioRecording.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            return entityAudioRecording;
        }
        public async static Task<LG.Data.Models.Clinical.AudioCallList> GetCallAssociatedWithConsultation(Int32 consultationId)
        {
            var result = new LG.Data.Models.Clinical.AudioCallList();
            var client = ClientConnection.GetIVRAudio_Connection();
            try
            {
                client.Open();
                var response
                    = await client.GetCallsAssociatedWithConsultationAsync(new GetCallsAssociatedWithConsultationRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        ConsultationID = consultationId
                    });
                result.ListOfCalls = response.ListOfCallInfo.ToList();result.IsError = response.ReturnStatus.IsError;
            }
            catch (Exception ex)
            {
                client.Abort();
                result.IsError = true;
                result.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            return result;
        }
    }
}
