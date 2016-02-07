using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using LG.Data.Models.Enums;
using LG.Services;
using LG.Services.CDMS;
using LG.Services.OMS;

namespace LG.Data.Core.Orders
{
    public static class OrderDataService
    {
        public static string Propbag = "<PropBag>LG.Data.Core.Orders.OrderDataService</PropBag>";

        public static async Task<LG.Data.Models.Orders.Order> StartOrder(LG.Data.Models.Orders.Order entity)
        {
            var temp = entity.OrderInput.ContactInfoOfRecipientDuringConsultation;
            var phone = entity.OrderInput.ProductID != "SV2" ? temp.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "") : "temp@1800md.com";
            var client = ClientConnection.GetOMS_Connection();
            try
            {
                client.Open();
                entity.OrderInputResponse
                    = await client.StartOrderProcessingAsync(new StartOrderProcessingRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        PropBag = Propbag,
                        SingleConsultationOrderInput = new SingleConsultationOrderInput()
                        {
                            ChiefComplaint = entity.OrderInput.ChiefComplaint,
                            ConsultationClientAdjustedPrice = entity.OrderInput.ConsultationClientAdjustedPrice,
                            ConsultationMemberAdjustedPrice = entity.OrderInput.ConsultationMemberAdjustedPrice,
                            ConsultationRecipientAccountID = entity.OrderInput.ConsultationRecipientAccountID,
                            ContactInfoOfRecipientDuringConsultation = phone,
                            CorporationRID = LG.Services.ClientConnection.CorporationId,
                            GuarantorAccountID = entity.OrderInput.GuarantorAccountID,
                            IsReRouting = entity.OrderInput.IsReRouting,
                            IsTesting = entity.OrderInput.IsTesting,
                            PlacedByRID = entity.OrderInput.PlacedByRID,
                            MSPRID = entity.OrderInput.MSPRID,
                            RxNTPharmacyID = entity.OrderInput.RxNTPharmacyID,
                            AlternativeMedicalCareType = entity.OrderInput.AlternativeMedicalCareType,
                            ProductID = entity.OrderInput.ProductID,
                            StateCodeRecipientLocatedDuringConsultation = entity.OrderInput.StateCodeRecipientLocatedDuringConsultation,
                        }
                    });
                entity.OrderResult = OrderActionResult.Success;
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.OrderResult = OrderActionResult.Failed;
                return entity;
            }
        }
        public static async Task<LG.Data.Models.Orders.FilesAssociatedWithConsultation> GetFiles(LG.Data.Models.Orders.FilesAssociatedWithConsultation entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.GetFilesAssociatedWithConsultationAsync(new GetFilesAssociatedWithConsultationRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        ConsultationID = entity.ConsultationID
                    });
                entity.IsError = response.ReturnStatus.IsError;
                entity.Files = response.ListOfFilesAssociatedWithConsultation;
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
        public static async Task<LG.Data.Models.Orders.FilesAssociatedWithConsultation> ToggleFile(LG.Data.Models.Orders.FilesAssociatedWithConsultation entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.ToggleIsHiddenFileAssociatedWithMedicalRecordAsync(new ToggleIsHiddenFileAssociatedWithMedicalRecordRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        FileGUID = entity.FileItem.FileGUID,
                        IsHidden = entity.FileItem.IsHidden,
                        PropBag = "<PropBag></PropBag>"
                    });
                entity.IsError = response.ReturnStatus.IsError;
                return await GetFiles(entity);
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
        public static async Task<LG.Data.Models.Orders.ConsultationFileAttachment> StoreFile(LG.Data.Models.Orders.ConsultationFileAttachment entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response
                    = await client.AddFileToMedicalRecordAsync(new AddFileToMedicalRecordRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        PropBag = Propbag,
                        ConsultationID = entity.ConsultationID,
                        Description = entity.Description,
                        FilePlainBytes = entity.FilePlainBytes,
                        FileFullName = entity.FileFullName
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
        public static async Task<LG.Data.Models.Orders.OrderInProcess> FindItemsInProcess(
            LG.Data.Models.Orders.OrderInProcess entity)
        {
            var client = ClientConnection.GetOMS_Connection();
            try
            {
                client.Open();
                switch (entity.OrderAction)
                {
                    case OrderAction.FindOrdersInProcessState:

                        var response = await client.FindOrdersInProcessStateAsync(new FindOrdersInProcessStateRequest()
                        {
                            AccountID = entity.AccountID,
                            MessageGuid = Guid.NewGuid(),
                            ProcessState = entity.ProcessState
                        });
                        entity.OrderResult = OrderActionResult.Success;
                        entity.OrdersFound = response.OrdersFound.ToList();
                        break;
                    case OrderAction.FindConsultationsInProcessState:
                        var response2 = await client.FindConsultationsInProcessStateAsync(new FindConsultationsInProcessStateRequest()
                                {
                            
                                    ConsultationRecipientAccountID = entity.AccountID,
                                    MessageGuid = Guid.NewGuid(),
                                    ProcessState = entity.ProcessState
                        });
                        entity.OrderResult = OrderActionResult.Success;
                        entity.ConsultationsFound = response2.ConsultationsFound.ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.OrderResult = OrderActionResult.Failed;               
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
        public static async Task<LG.Data.Models.Orders.OrderInProcess> FindConsultationInProcess(
           LG.Data.Models.Orders.OrderInProcess entity)
        {
            var client = ClientConnection.GetOMS_Connection();
            try
            {
                client.Open();
                switch (entity.OrderAction)
                {
                    case OrderAction.FindOrdersInProcessState:

                        var response = await client.FindOrdersInProcessStateAsync(new FindOrdersInProcessStateRequest()
                        {
                            AccountID = entity.AccountID,
                            MessageGuid = Guid.NewGuid(),
                            ProcessState = entity.ProcessState
                        });
                        entity.OrderResult = OrderActionResult.Success;
                        entity.OrdersFound = response.OrdersFound.ToList();
                        break;
                    case OrderAction.FindConsultationsInProcessState:
                        var response2 =
                            await
                                client.FindConsultationsInProcessStateAsync(new FindConsultationsInProcessStateRequest()
                                {
                                    ConsultationRecipientAccountID = entity.AccountID,
                                    MessageGuid = Guid.NewGuid(),
                                    ProcessState = entity.ProcessState
                                });
                        entity.OrderResult = OrderActionResult.Success;
                        entity.ConsultationsFound = response2.ConsultationsFound.ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.OrderResult = OrderActionResult.Failed;
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

        public static async Task<LG.Data.Models.Orders.OrderInProcess> FindConsultationCompletedByAccountID(
          LG.Data.Models.Orders.OrderInProcess entity)
        {
            var client = ClientConnection.GetOMS_Connection();
            try
            {
                client.Open();
                switch (entity.OrderAction)
                {
                    case OrderAction.FindOrdersInProcessState:

                        var response = await client.FindOrdersInProcessStateAsync(new FindOrdersInProcessStateRequest()
                        {
                            AccountID = entity.AccountID,
                            MessageGuid = Guid.NewGuid(),
                            ProcessState = ProcessStateEnum.Completed
                        });
                        entity.OrdersFound = response.OrdersFound.ToList();
                        break;
                    case OrderAction.FindConsultationsInProcessState:
                        var response2 =
                            await
                                client.FindConsultationsInProcessStateAsync(new FindConsultationsInProcessStateRequest()
                                {
                                    ConsultationRecipientAccountID = entity.AccountID,
                                    MessageGuid = Guid.NewGuid(),
                                    ProcessState =ProcessStateEnum.Completed
                                });
                        entity.OrderResult = OrderActionResult.Success;
                        entity.ConsultationsFound = response2.ConsultationsFound.ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                client.Abort();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.OrderResult = OrderActionResult.Failed;
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

        public static async Task<LG.Data.Models.Orders.Consultations> Consultations(
          LG.Data.Models.Orders.Consultations entity)
        {
            var client = ClientConnection.GetOMS_Connection();
            try
            {
                client.Open();
                switch (entity.OrderAction)
                {
                    case OrderAction.GetConsultationsToBeAssigned:
                        var response = await client.GetConsultatationsToBeAssignedAsync(new GetConsultatationsToBeAssignedRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            IsTesting = entity.IsTesting
                        });
                        entity.OrderResult = OrderActionResult.Success;
                        entity.ToBeAssigned = response.ListOfConsultationsToBeAssigned.ToList();                       
                        break;
                    case OrderAction.GetConsultationsToBeServiced:
                        var response2 =
                            await
                                client.GetConsultationsToBeServicedAsync(new GetConsultationsToBeServicedRequest()
                                {
                                    MessageGuid = Guid.NewGuid(),
                                    IsTesting = entity.IsTesting
                                });
                        entity.OrderResult = OrderActionResult.Success;
                        entity.ToBeServiced = response2.ListOfConsultationsToBeServiced.ToList();                       
                        break;
                }
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.OrderResult = OrderActionResult.Failed;           
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


        public static async Task<LG.Data.Models.Orders.ManuallyAssignOrder> ManuallyAssignPractitioner(LG.Data.Models.Orders.ManuallyAssignOrder entity)
        {
                var client = ClientConnection.GetOMS_Connection();
                try
                {
                    client.Open();
                    var response = await client.ManuallyAssignMedicalPractitionerAsync(new ManuallyAssignMedicalPractitionerRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            ConsultationID = entity.ConsultationID,
                            MedicalPractitionerRID = entity.MedicalPractitionerRID,
                            AssignedByRID = entity.AssignedByRID
                        });
                    entity.IsError = response.ReturnStatus.IsError;
                    entity.Message = !response.ReturnStatus.IsError ?  response.ReturnStatus.GeneralMessage : response.ReturnStatus.ErrorMessage; 
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
        public static async Task<LG.Data.Models.Orders.ManuallyAssignOrder> ReAssignConsultation(LG.Data.Models.Orders.ManuallyAssignOrder entity)
        {
            var client = ClientConnection.GetOMS_Connection();
            try
            {
                client.Open();
                var response = await client.TryReAssignMedicalPractitionerAsync(new TryReAssignMedicalPractitionerRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    ConsultationID = entity.ConsultationID,
                    MedicalPractitionerRID = entity.MedicalPractitionerRID,
                    AssignedByRID = entity.AssignedByRID
                });
                entity.IsError = response.ReturnStatus.IsError;
                entity.Message = !response.ReturnStatus.IsError ? response.ReturnStatus.GeneralMessage : response.ReturnStatus.ErrorMessage;
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

    }
}
