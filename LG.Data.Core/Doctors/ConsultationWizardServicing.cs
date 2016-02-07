using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Doctors.ConsultWizard;
using LG.Services;
using LG.Services.CDMS;
using LG.Services.OMS;

namespace LG.Data.Core.Doctors
{
    public static class ConsultationWizardServicing
    {
        public static string PropBag => "<PropBag>LG.Data.Core.Doctors.ConsultationWizardServicing</PropBag>";
        /// <summary>
        /// Diagnosis
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.Diagnosis> AddDiagnosis(LG.Data.Models.Doctors.ConsultWizard.Diagnosis entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response = await client.AddDiagnosisToConsultationAsync(new AddDiagnosisToConsultationRequest()
                {
                    PropBag = PropBag,
                    MessageGuid = Guid.NewGuid(),
                    Diagnosis = entity.Input
                });
                entity.ID = response.ID;
                entity.IsError = response.ReturnStatus.IsError;
                entity.Message = response.ReturnStatus.IsError ?  response.ReturnStatus.ErrorMessage : response.ReturnStatus.GeneralMessage;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();

                }
            }
            return entity;
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.DiagnosisList> GetDiagnosis(
            LG.Data.Models.Doctors.ConsultWizard.DiagnosisList entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.GetDiagnosisRecordsAsync(new GetDiagnosisRecordsRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        ConsultationID = entity.ConsultationID
                    });
                entity.Result = response.ListOfDiagnosisRecords;
                entity.IsError = response.ReturnStatus.IsError;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();

                }
            }
            return entity;
        }
        /// <summary>
        /// Plan Of Care
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity> GetPlanOfCare(LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.GetPlanOfCareAsync(new GetPlanOfCareRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        ConsultationID = entity.ConsultationID
                    });
                entity.DispositionLegal = response.PlanOfCare;
                entity.IsError = response.ReturnStatus.IsError;
                entity.Message = response.ReturnStatus.GeneralMessage + response.ReturnStatus.ErrorMessage;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();

                }
            }
            return entity;
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity> SavePlanOfCare(LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.SavePlanOfCareAsync(new SavePlanOfCareRequest()
                    {
                        PropBag = PropBag,
                        MessageGuid = Guid.NewGuid(),
                        PlanOfCareLegal = entity.DispositionLegal,
                    });
                entity.IsError = response.ReturnStatus.IsError;
                entity.Message = response.ReturnStatus.GeneralMessage + response.ReturnStatus.ErrorMessage;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();

                }
            }
            return entity;
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity> SavePlanOfCareDraft(LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.SavePlanOfCareDraftAsync(new SavePlanOfCareDraftRequest()
                    {
                        PropBag = PropBag,
                        MessageGuid = Guid.NewGuid(),
                        PlanOfCareDraft = entity.Draft,
                    });
                entity.IsError = response.ReturnStatus.IsError;
                entity.Message = response.ReturnStatus.GeneralMessage + response.ReturnStatus.ErrorMessage;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();

                }
            }
            return entity;
        }
        /// <summary>
        /// Consult Messaging => EConsult
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.MessageConsultation> SendMessage(LG.Data.Models.Doctors.ConsultWizard.MessageConsultation entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.AddMessageToConsultationAsync(new AddMessageToConsultationRequest()
                    {
                        PropBag = PropBag,
                        MessageGuid = Guid.NewGuid(),
                        OrderID = entity.ConsultationID,
                        ConsultationID = entity.ConsultationID,
                        Message = entity.EConsultationMessage,
                        MemberRID = entity.PatientRID,
                        SendByRID = entity.SendByRID,
                    });
                entity.ID = response.ID;
                entity.IsError = response.ReturnStatus.IsError;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();

                }
            }
            return entity;
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.MessageConsultation> GetMessageExchange(LG.Data.Models.Doctors.ConsultWizard.MessageConsultation entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.GetConsultationMessageExchangeAsync(new GetConsultationMessageExchangeRequest()
                    {

                        MessageGuid = Guid.NewGuid(),
                        ConsultationID = entity.ConsultationID,
                    });
                entity.IsError = response.ReturnStatus.IsError;
                entity.MessageExchange = response.ConsultationMessages;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();

                }
            }
            return entity;
        }
        /// <summary>
        /// Consult Notes
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.Note> InsertGeneralNote(LG.Data.Models.Doctors.ConsultWizard.Note entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.InsertGeneralNoteAsync(new InsertGeneralNoteRequest()
                    {
                        PropBag = PropBag,
                        MessageGuid = Guid.NewGuid(),
                        OrderID = entity.ConsultationID,
                        ConsultationID = entity.ConsultationID,
                        Note = entity.NoteText,
                        MemberRID = entity.PatientRID,
                        CreatedByRID = entity.CreatedByRID,
                        IsHidden = entity.IsHidden
                    });
                entity.ID = response.ID;
                entity.IsError = response.ReturnStatus.IsError;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();

                }
            }
            return entity;
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.Note> GetGeneralNotes(LG.Data.Models.Doctors.ConsultWizard.Note entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.GetGeneralNotesAsync(new GetGeneralNotesRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        ConsultationID = entity.ConsultationID,
                        IsHidden = entity.IsHidden
                    });
                entity.Notes = response.GeneralNotes;
                entity.IsError = response.ReturnStatus.IsError;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            return entity;
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.Note> ToggleGeneralNote(LG.Data.Models.Doctors.ConsultWizard.Note entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.ToggleIsHiddenGeneralNoteAsync(new ToggleIsHiddenGeneralNoteRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        ID = entity.ID,
                        IsHidden = entity.IsHidden,
                        PropBag = PropBag
                    });
                entity.IsError = response.ReturnStatus.IsError;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            return entity;
        }
        /// <summary>
        /// Click to call
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.CallInstance> StartClickToCall(LG.Data.Models.Doctors.ConsultWizard.StartClickToCall entity)
        {
            var client = ClientConnection.GetOMS_Connection();
            var result = new CallInstance()
            {
                ConsultationID = entity.ConsultationID
            };
            try
            {
                client.Open();
                var response = await client.StartClickToCallPhoneConsultationAsync(new StartClickToCallPhoneConsultationRequest()
                {
                    PropBag = PropBag,
                    MessageGuid = Guid.NewGuid(),
                    ConsultationID = entity.ConsultationID,
                    MedicalPractitionerRID = entity.MedicalPractionerRID,
                    MedicalPractitionerPhoneNumber = entity.MedicalPractionerPhoneNumber,
                });

                result.PinCode = response.PinCode;
                result.CallSid = response.CallSid;
                entity.IsError = response.ReturnStatus.IsError;
                entity.Message = response.ReturnStatus.ErrorMessage;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
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

        public static int NumberOfRows = 25;

        public static async Task<LG.Data.Models.Doctors.ConsultWizard.SearchDiagnosisEntity> SearchDiagnosis(
            string input)
        {
            var result = new SearchDiagnosisEntity()
            {
                InputString = input
            };
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response = await client.SearchICD10DiagnosesAsync(new SearchICD10DiagnosesRequest()
                {

                    MessageGuid = Guid.NewGuid(),
                    Input = result.InputString,
                    NumberOfRows = NumberOfRows
                });
                result.Result = response.Diagnoses;
                result.IsError = response.ReturnStatus.IsError;
                result.Message =! response.ReturnStatus.IsError? response.ReturnStatus.GeneralMessage : response.ReturnStatus.ErrorMessage;
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


        public static async Task<LG.Data.Models.Doctors.ConsultWizard.CallOutcome> SaveCallOutcome(
            LG.Data.Models.Doctors.ConsultWizard.CallOutcome entity)
        {
            var client = ClientConnection.GetOMS_Connection();
            try
            {
                client.Open();
                var response = await client.SubmitConsultationProcessOutcomeAsync(new SubmitConsultationProcessOutcomeRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    ConsultationID = entity.ConsultationID,
                    MedicalPractitionerRID = entity.MedicalPractionerRID,
                    MedicalConsultationProcessOutcome = entity.Outcome,
                });
                entity.IsError = response.ReturnStatus.IsError;
                entity.Message = response.ReturnStatus.ErrorMessage;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();

                }
            }
            return entity;
        }

        public static async Task<LG.Data.Models.Doctors.ConsultWizard.CallOutcomeEntity> AddCallOutcome(
           LG.Data.Models.Doctors.ConsultWizard.CallOutcomeEntity entity)
        {
            var client = ClientConnection.GetOMS_Connection();

            try
            {
                client.Open();
                var response = await client.AddIVRCallOutcomeAsync(new AddIVRCallOutcomeRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    CallSid = entity.CallSid,
                    IVRProvider = entity.Provider,
                    IVRCallOutcome = entity.Outcome
                });
                entity.IsError = response.ReturnStatus.IsError;
                entity.Message = response.ReturnStatus.ErrorMessage;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();

                }
            }
            return entity;
        }

        public static async Task<LG.Data.Models.Doctors.ConsultWizard.CallOutcome> SaveStepsOutcome(
          LG.Data.Models.Doctors.ConsultWizard.CallOutcome entity)
        {
            var client = ClientConnection.GetOMS_Connection();

            try
            {
                client.Open();
                var response = await client.SubmitPostConsultationStepsCompleteAsync(new SubmitPostConsultationStepsCompleteRequest()
                {

                    MessageGuid = Guid.NewGuid(),
                    ConsultationID = entity.ConsultationID,
                    MedicalPractitionerRID = entity.MedicalPractionerRID
                });

                entity.IsError = response.ReturnStatus.IsError;
                entity.Message = response.ReturnStatus.ErrorMessage;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();

                }
            }
            return entity;
        }


        public static async Task<LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity> 
            SaveDisposition(LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity pc)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response = await client.SavePlanOfCareAsync(new SavePlanOfCareRequest()
                {
                    PropBag = PropBag,
                    MessageGuid = Guid.NewGuid(),
                    PlanOfCareLegal = new PlanOfCareLegal()
                    {
                        PlanOfCare = pc.Disposition,
                        ListOfLegalDisclaimers =  pc.Disclaimers
                    }
                });
                pc.IsError = response.ReturnStatus.IsError;
                pc.Message = !response.ReturnStatus.IsError ? response.ReturnStatus.GeneralMessage: response.ReturnStatus.ErrorMessage;
            }
            catch (Exception ex)
            {
                client.Abort();
                pc.IsError = true;
                pc.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Close();

                }
            }
            return pc;
        }

        public static async Task<LG.Data.Models.Doctors.ConsultWizard.CallInstance> GetCallStatus(LG.Data.Models.Doctors.ConsultWizard.CallInstance entity)
        {
            var client = ClientConnection.GetOMS_Connection();
            try
            {
                client.Open();
                var response = await client.GetCallStatusAsync(new GetCallStatusRequest()
                    {
                        CallSid = entity.CallSid,
                        MessageGuid = Guid.NewGuid(),
                        IVRProvider = IVRProvidersEnum.Twilio
                    });
                entity.CallStatus = response.CallStatus;
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            finally
            {
                if (client.State != CommunicationState.Closed){client.Close();}
            }
            return entity;
        }
    }
}