using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums;
using LG.Data.Models.Users;
using LG.Services.CMS;
using LG.Services.MPMS;

namespace LG.Data.Core.Doctors
{
    public static class DoctorDataService
    {

        public static async Task<bool> SaveContactInfo(UserModel entity)
        {
            var task1 = LG.Data.Core.Shared.SaveContactInformation.SaveEmailTask(entity);
            await task1;

            var task2 = LG.Data.Core.Shared.SaveContactInformation.SavePhoneTask(entity);
            await task2;

            var task3 = LG.Data.Core.Shared.SaveContactInformation.SaveSecurityInfoTask(entity);
            await task3;

            if (task1.IsCompleted && task2.IsCompleted && task3.IsCompleted)
            {
                return true;
            }
            return false;
        }


        public static async Task<LG.Data.Models.Doctors.SearchResults> Search(
            LG.Data.Models.Doctors.Search entity)
        {
            var result = new LG.Data.Models.Doctors.SearchResults();
            var client = LG.Services.ClientConnection.GetMpmsConnection();
            try
            {
                client.Open();
                entity.Request.MessageGuid = entity.MessageGuid;
                var response = await client.GetMedicalPractitionersAsync(entity.Request);
                client.Close();
                result.Results = response.ListOfMedicalPractitionerInfo;
                result.IsError = false;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.ToString();

            }
            return result;
        }
        public static async Task<LG.Data.Models.Doctors.OperationResponses> Save(
            LG.Data.Models.Doctors.Inputs eInputs)
        {
            switch (eInputs.EventAction)
            {
                case DoctorActions.DoctorAdd:
                    return await SavePractioner(eInputs);
                    break;
                case DoctorActions.DoctorUpdate:
                    return await SavePractioner(eInputs);
                    break;
                case DoctorActions.DoctorUpdatePrintedName:
                    return await SavePractioner(eInputs);
                    break;
                case DoctorActions.DoctorUpdateMedicalInformation:
                    return await SavePractioner(eInputs);
                    break;
                case DoctorActions.LicenseAdd:
                    return await SaveLicense(eInputs);
                    break;
                case DoctorActions.LicenseUpdate:
                    return await SaveLicense(eInputs);
                    break;
                default:
                    return null;
            }
        }
        private static async Task<LG.Data.Models.Doctors.OperationResponses> SavePractioner(
            LG.Data.Models.Doctors.Inputs eInputs)
        {
            var result = new LG.Data.Models.Doctors.OperationResponses();
            var client = LG.Services.ClientConnection.GetMpmsConnection();
            switch (eInputs.EventAction)
            {
                case DoctorActions.DoctorAdd:
                    try
                    {
                        client.Open();
                        result.InsertPractioner = await client.InsertMedicalPractitionerAsync(
                            new InsertMedicalPractitionerRequest()
                            {
                                MedicalPractitioner = eInputs.MedicalPractitionerInsertInput,
                                MessageGuid = eInputs.MessageGuid,
                                PropBag = eInputs.PropBag
                            });
                        client.Close();
                        result.IsError = false;
                        result.Message = "Success";
                    }
                    catch (Exception ex)
                    {
                        client.Close();
                        result.IsError = true;
                        result.Message = ex.ToString();
                    }
                    break;
                case DoctorActions.DoctorUpdate:
                    try
                    {
                        client.Open();

                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_PersonInfo = true;
                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_PrintedName = true;

                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_IsActive = true;
                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_IsTesting = true;

                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_IsVideoCapable = true;
                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_IsConsultingOutOfState = true;
                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_IsOKToCallWhenNotScheduled = true;
                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_IsObservingDaylightSavings = true;
                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_TimeZoneID = true;

                       
                        result.UpdatePractioner = await client.UpdateMedicalPractitionerAsync(
                            new UpdateMedicalPractitionerRequest()
                            {
                                RID = eInputs.RID,
                                MedicalPractitionerUpdateInput = eInputs.MedicalPractitionerUpdateInput,
                                MessageGuid = eInputs.MessageGuid,
                                PropBag = eInputs.PropBag
                            });
                        client.Close();
                        result.IsError = false;
                        result.Message = "Success";
                    }
                    catch (Exception ex)
                    {
                        client.Close();
                        result.IsError = true;
                        result.Message = ex.ToString();
                    }
                    break;
                case DoctorActions.DoctorUpdatePrintedName:
                    try
                    {
                        client.Open();
                        result.UpdatePractioner = await client.UpdateMedicalPractitionerAsync(
                            new UpdateMedicalPractitionerRequest()
                            {
                                RID = eInputs.RID,
                                MedicalPractitionerUpdateInput = eInputs.MedicalPractitionerUpdateInput,
                                MessageGuid = eInputs.MessageGuid,
                                PropBag = eInputs.PropBag
                            });
                        client.Close();
                        result.IsError = false;
                        result.Message = "Success";
                    }
                    catch (Exception ex)
                    {
                        client.Close();
                        result.IsError = true;
                        result.Message = ex.ToString();
                    }
                    break;
                case DoctorActions.DoctorUpdateMedicalInformation:
                    try
                    {
                       
                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_DEA = true;
                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_D_DEAExpiration = true;
                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_NPI = true;
                        eInputs.MedicalPractitionerUpdateInput.IsUpdating_PIN = true;

                        client.Open();
                        result.UpdatePractioner = await client.UpdateMedicalPractitionerAsync(
                            new UpdateMedicalPractitionerRequest()
                            {
                                RID = eInputs.RID,
                                MedicalPractitionerUpdateInput = eInputs.MedicalPractitionerUpdateInput,
                                MessageGuid = eInputs.MessageGuid,
                                PropBag = eInputs.PropBag
                            });
                        client.Close();
                        result.IsError = false;
                        result.Message = "Success";
                    }
                    catch (Exception ex)
                    {
                        client.Close();
                        result.IsError = true;
                        result.Message = ex.ToString();
                    }
                    break;
            }
            return result;
        }

        private static async Task<LG.Data.Models.Doctors.OperationResponses> SaveLicense(
           LG.Data.Models.Doctors.Inputs eInputs)
        {
            var result = new LG.Data.Models.Doctors.OperationResponses();
            var client = LG.Services.ClientConnection.GetMpmsConnection();
            switch (eInputs.EventAction)
            {
                case DoctorActions.LicenseAdd:
                    try
                    {
                        client.Open();
                        result.InsertLicense = await client.StoreMedicalLicenseAsync(
                            new StoreMedicalLicense_Request()
                            {
                                MedicalLicense = eInputs.LicenseStoreInput,
                                MessageGuid = eInputs.MessageGuid,
                                PropBag = eInputs.PropBag
                            });
                        client.Close();
                        result.IsError = false;
                        result.Message = "Success";
                    }
                    catch (Exception ex)
                    {
                        client.Close();
                        result.IsError = true;
                        result.Message = ex.ToString();
                    }
                    break;
                case DoctorActions.LicenseUpdate:
                    try
                    {
                        client.Open();
                        result.UpdateLicense = await client.UpdateMedicalLicenseAsync(
                            new UpdateMedicalLicenseRequest()
                            {
                                ID = eInputs.MedicalLicenseID,
                                MedicalLicenseUpdateInput = new MedicalLicense_UpdateInput()
                                {
                                    ID = eInputs.LicenseUpdateInput.ID,
                                    RID = eInputs.LicenseUpdateInput.RID,
                                    D_Expires = eInputs.LicenseUpdateInput.D_Expires,
                                    IsPrimaryState = eInputs.LicenseUpdateInput.IsPrimaryState,
                                    IsVoid = eInputs.LicenseUpdateInput.IsVoid,
                                    IsUpdating_D_Expires = true,
                                    IsUpdating_IsPrimaryState = true,
                                    IsUpdating_IsVoid = true,
                                    IsUpdating_LicenseNumber = true,
                                    IsUpdating_StateCode = true,
                                    LicenseNumber = eInputs.LicenseUpdateInput.LicenseNumber,
                                    StateCode = eInputs.LicenseUpdateInput.StateCode,
                                },
                                MessageGuid = eInputs.MessageGuid,

                                PropBag = eInputs.PropBag,
                            });
                        client.Close();
                        result.IsError = false;
                        result.Message = "Success";
                    }
                    catch (Exception ex)
                    {
                        client.Close();
                        result.IsError = true;
                        result.Message = ex.ToString();
                    }
                    break;
            }
            return result;
        }

    }
}
