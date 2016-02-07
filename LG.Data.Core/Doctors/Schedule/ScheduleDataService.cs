using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Doctors.Schedule;
using LG.Services;
using LG.Services.MPMS;
using LG.Services.MPMS.SM;

namespace LG.Data.Core.Doctors.Schedule
{
    public static class Load
    {
        public static async Task<LG.Data.Models.Doctors.Schedule.DoctorsAvailablePerState> ListOfDoctorsPerState(string state)
        {
            var client = ClientConnection.GetMpmsConnection();
            var entity = new LG.Data.Models.Doctors.Schedule.DoctorsAvailablePerState()
            {
                StateCode = state
            };
            try
            {
                client.Open();
                var response = await client.GetMedicalPractitionersAsync(new GetMedicalPractitionersRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    IsActiveMedicalPractitionerFilter = IsActiveFilterEnum.ActiveOnly,
                    IsExpiredLicenseFilter = IsExpiredFilterEnum.NonExpired,
                    IsVoidLicenseFilter = IsVoidFilterEnum.NonVoid,
                    IsTestingMedicalPractitionerFilter = IsTestingFilterEnum.All,
                    MSPRID = 100
                });
                client.Close();
                var list = response.ListOfMedicalPractitionerInfo.ToList();
                foreach (var doctor in list)
                {
                    foreach (
                        var license in
                            doctor.ListOfMedicalLicenses.Where(license => license.StateCode == entity.StateCode))
                    {
                        entity.ListOfPractitionerInfo.Add(doctor);
                        entity.ListOfPractitionerAvailablePerState.Add(
                            new MedicalPractitionerAvailabilityOneState()
                            {
                                StateCode = entity.StateCode,
                                FName = doctor.MedicalPractitioner.PersonInfo.FName,
                                LName = doctor.MedicalPractitioner.PersonInfo.LName,
                                PrintedName = doctor.MedicalPractitioner.PrintedName,
                                MedicalPractitionerRID = doctor.MedicalPractitioner.RID,
                                IsPrimary = license.IsPrimaryState,
                                IsTesting = doctor.MedicalPractitioner.IsTesting
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();

            }
            finally
            {
                client.Close();
            }
            return entity;
        }
        public static async Task<LG.Data.Models.Doctors.Schedule.Entity> PractitionerSchedule(LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            var client = ClientConnection.GetMpmsSMConnection();
            try
            {
                client.Open();
                var response = await client.GetPractitionerScheduleAsync(new GetPractitionerScheduleRequest()
                {
                    MSPRID = entity.MSPRID,
                    MessageGuid = entity.InstanceGuid,
                    MedicalPractitionerRID = entity.MedicalPractitionerRID,
                    D_From = entity.D_From,
                    D_To = entity.D_To
                });
                client.Close();
                entity.AvailabilityBlocks = response.AvailabilityList;
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            return null;
        }

        public static async Task<LG.Data.Models.Doctors.Schedule.Entity> PractitionersOneState(LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            var client = ClientConnection.GetMpmsSMConnection();
            try
            {
                client.Open();
                var response = await client.GetScheduledPractitionersOneStateAsync(new GetScheduledPractitionersOneStateRequest()
                {
                    MSPRID = entity.MSPRID,
                    MessageGuid = entity.InstanceGuid,
                    D_From = entity.D_From,
                    D_To = entity.D_To,
                    StateCode = entity.StateCode
                });
                client.Close();
                entity.AvailabilityOneState = response.MedicalPractitionerAvailabilityOneStateList;
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            return null;
        }
       
        public static async Task<LG.Data.Models.Doctors.Schedule.Entity> PractitionerCountInRangeForAllStates(LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            var client = ClientConnection.GetMpmsSMConnection();
            try
            {
                client.Open();
                var response = await client.GetScheduledMPCountInRangeForAllStatesAsync(new GetScheduledMPCountInRangeForAllStatesRequest()
                {
                    MSPRID = entity.MSPRID,
                    MessageGuid = entity.InstanceGuid,
                    D_From = entity.D_From,
                    D_To = entity.D_To
                });
                client.Close();
                entity.AvailabilityAllStates = response.MedicalPractitionerCountOneStateInRangeList;
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
            }
            return null;
        }



    }

    public static class Save
    {

        internal static List<LG.Services.MPMS.SM.MedicalPractitionerAvailability> GetAvailabilities(LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            var result = new List<LG.Services.MPMS.SM.MedicalPractitionerAvailability>();
            foreach (var availibility in entity.AvailabilityBatch.MedicalPractitionerAvailabilities)
            {
                var item = new MedicalPractitionerAvailability
                {
                    MedicalPractitionerRID = availibility.MedicalPractitionerRID,
                    AvalibilityBlocks = new List<AvailabilityBlockInput>()
                };
                foreach (var time in availibility.AvalibilityBlocks)
                {
                    item.AvalibilityBlocks.Add(
                        new AvailabilityBlockInput()
                        {
                            DTDST_ShiftEnds = time.DTDST_ShiftEnds,
                            DTDST_ShiftStart = time.DTDST_ShiftStart,
                        });
                }
                result.Add(item);
            }
            return result;
        }
        internal static MedicalPractitionerAvailabilityBatch GetDBatch(LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            var result = new MedicalPractitionerAvailabilityBatch()
            {
                MSPRID = entity.AvailabilityBatch.MSPRID,
                MedicalPractitionerAvailabilities = GetAvailabilities(entity)
            };
            return result;
        }

        public static async Task<LG.Data.Models.BaseModel> Batch(LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            var client = ClientConnection.GetMpmsSMConnection();
            try
            {
                client.Open();
                entity.InstanceGuid = Guid.NewGuid();
                var response = await client.StorePractitionerAvailabilityBatchAsync(new StorePractitionerAvailabilityBatchRequest()
                {
                    MessageGuid = entity.InstanceGuid,
                    MedicalPractitionerAvailabilityBatch = GetDBatch(entity),
                    PropBag = "<PropBag></PropBag>"
                });
                client.Close();
                return new LG.Data.Models.BaseModel()
                {
                    IsError = false,
                    Message = "Test"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                return new LG.Data.Models.BaseModel()
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
            return null;
        }


     
        //MedicalPractitionerAvailabilities = new List<MedicalPractitionerAvailability>()
        //               {
        //                   new MedicalPractitionerAvailability()
        //                   {
        //                       MedicalPractitionerRID = 11111,
        //                       AvalibilityBlocks = new List<AvailabilityBlockInput>()
        //                       {
        //                          new AvailabilityBlockInput()
        //                          {

        //                              DTDST_ShiftEnds = DateTime.UtcNow.AddDays(1),
        //                              DTDST_ShiftStart = DateTime.UtcNow
        //                          }
        //                       }
        //                   }
        //               }
        public static async Task<LG.Data.Models.BaseModel> OneDayBlock(LG.Data.Models.Doctors.Schedule.Entity entity)
        {
            var client = ClientConnection.GetMpmsSMConnection();
            try
            {
                client.Open();
                var response = await client.StorePractitionerAvailabilityOneDayBlockAsync(new StorePractitionerAvailabilityOneDayBlockRequest()
                {
                    MessageGuid = entity.InstanceGuid,
                    MedicalPractitionerRID = entity.MedicalPractitionerRID,
                    MSPRID = entity.MSPRID,
                    D_Shift = new DateTime(),
                    PropBag = "<PropBag></PropBag>"
                });
                client.Close();
                return new LG.Data.Models.BaseModel()
                {
                    IsError = false,
                    Message = String.Format("{0},{1}", response.ExitCode, response.ReturnStatus.GeneralMessage)
                };
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                return new LG.Data.Models.BaseModel()
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
            return null;
        }

        public static async Task<LG.Data.Models.BaseModel> SingleBlock(LG.Data.Models.Doctors.Schedule.Entity entity)
        {

            var client = ClientConnection.GetMpmsSMConnection();
            try
            {
                client.Open();
                var response = await client.StorePractitionerAvailabilitySingleBlockAsync(new StorePractitionerAvailabilitySingleBlockRequest()
                {
                    MessageGuid = entity.InstanceGuid,
                    MedicalPractitionerRID = entity.MedicalPractitionerRID,
                    MSPRID = entity.MSPRID,
                    DTDST_ShiftEnds = entity.D_To,
                    DTDST_ShiftStarts = entity.D_From,
                    PropBag = "<PropBag></PropBag>"
                });
                client.Close();
                return new LG.Data.Models.BaseModel()
                {
                    IsError = false,
                    Message = String.Format("{0},{1}", response.ExitCode, response.ReturnStatus.GeneralMessage)
                };
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                return new LG.Data.Models.BaseModel()
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
            return null;
        }
    }

    public static class Deactivate
    {
        public static async Task<bool> SingleBlock()
        {

            return false;
        }
        public static async Task<bool> SingleBlockByID()
        {

            return false;
        }


    }



}
