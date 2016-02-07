using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using LG.Data.Models.Clinical;
using LG.Data.Models.Enums;
using LG.Services;
using LG.Services.CDMS;
namespace LG.Data.Core.Clinical
{
    public static class ClinicalDataService
    {
        public static string Propbag = "<PropBag>LG.Data.Core.Clinical.ClinicalDataService</PropBag>";
        public static async Task<LG.Data.Models.Clinical.Allergy> Allergy(LG.Data.Models.Clinical.Allergy entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                switch (entity.ActionHelper.ClincalAction)
                {
                    case ClinicalAction.Add:
                        #region [@  Method     @]
                        var response = await client.AddAllergyAsync(new AddAllergyRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            Allergy = entity.InsertInput,
                            PropBag = Propbag
                        });
                        client.Close();
                        entity.NewAllergyID = response.NewAllergyID;
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                        #endregion
                        break;
                    case ClinicalAction.Update:
                        #region [@  Method     @]
                        var response2 = await client.UpdateAllergyAsync(new UpdateAllergyRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            Allergy = entity.UpdateInput,
                            PropBag = Propbag
                        });
                        client.Close();
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                        #endregion
                        break;
                    case ClinicalAction.ToggleHidden:
                        #region [@  Method     @]
                        var response3 = await client.ToggleIsHiddenAllergyAsync(new ToggleIsHiddenAllergyRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            ID = entity.UpdateInput.AllergyID,
                            IsHidden = entity.UpdateInput.IsHidden,
                            PropBag = Propbag
                        });
                        client.Close();
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                        #endregion
                        break;
                    case ClinicalAction.LoadAll:
                        #region [@  Method     @]
                        
                        var response4 = await client.GetAllergiesAsync(new GetAllergiesRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            RID = entity.RID
                        });
                        client.Close();
                        entity.List = response4.Allergies;
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 

                        #endregion
                        break;
                    case ClinicalAction.LoadDetail:
                        #region [@  Method     @]
                        var response5 = await client.GetAllergyAsync(new GetAllergyRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            ID = entity.UpdateInput.ID
                        });
                        client.Close();
                        entity.AllergyItem = response5.Allergy;
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                        #endregion
                        break;
                }
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Failed }; ;
                return entity;
            }
        }
        public static async Task<LG.Data.Models.Clinical.Allergy> LoadAllergies(LG.Data.Models.Clinical.Allergy entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response = await client.GetAllergiesAsync(new GetAllergiesRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    RID = entity.RID
                });
                client.Close();
                entity.List = response.Allergies;
                entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; ;
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Failed }; ;
            
                return entity;
            }
        }
        public static async Task<LG.Data.Models.Clinical.Condition> Condition(LG.Data.Models.Clinical.Condition entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                switch (entity.ActionHelper.ClincalAction)
                {
                    case ClinicalAction.Add:
                        #region [@  Method     @]
                        var response = await client.AddMedicalConditionAsync(new AddMedicalConditionRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            MedicalCondition = entity.InsertInput,
                            PropBag = Propbag
                        });
                        client.Close();
                        entity.NewMedicalConditionID = response.NewMedicalConditionID;
                        entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; ;
                        #endregion
                        break;
                    case ClinicalAction.Update:
                        #region [@  Method     @]
                        var response2 = await client.UpdateMedicalConditionAsync(
                            new UpdateMedicalConditionRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            MedicalCondition = entity.UpdateInput,
                            PropBag = Propbag
                        });
                        client.Close();
                        entity.ActionHelper = new ActionHelper
                        {
                            ClincalActionResult = ClinicalActionResult.Success
                        }; ;
                        entity.Message = response2.ExitCode.ToString();
                        #endregion
                        break;
                    case ClinicalAction.ToggleHidden:
                        #region [@  Method     @]
                        var response3 = await client.ToggleIsHiddenMedicalConditionAsync(
                            new ToggleIsHiddenMedicalConditionRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            ID = entity.UpdateInput.ID,
                            IsHidden = entity.UpdateInput.IsHidden,
                            PropBag = Propbag
                        });
                        client.Close();
                        entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; ;
                        #endregion
                        break;
                    case ClinicalAction.LoadAll:
                        #region [@  Method     @]
                        var response4 = await client.GetMedicalConditionsAsync(new GetMedicalConditionsRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            RID = entity.RID
                        });
                        client.Close();
                        entity.List = response4.MedicalConditions;
                        entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; ;
                        #endregion
                        break;
                    case ClinicalAction.LoadDetail:
                        #region [@  Method     @]
                        var response5 = await client.GetMedicalConditionAsync(new GetMedicalConditionRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            ID = entity.UpdateInput.ID
                        });
                        client.Close();
                        entity.MedicalConditionItem = response5.MedicalCondition;
                        entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; ;
                        #endregion
                        break;
                }
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Failed }; ;
                return entity;
            }
        }
        public static async Task<LG.Data.Models.Clinical.Condition> LoadConditions(
            LG.Data.Models.Clinical.Condition entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response = await client.GetMedicalConditionsAsync(
                    new GetMedicalConditionsRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    RID = entity.RID
                });
                client.Close();
                entity.List = response.MedicalConditions;
                  entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
          
                entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Failed }; ;
                return entity;
            }
        }
        public static async Task<LG.Data.Models.Clinical.VitalReading> VitalReading(
            LG.Data.Models.Clinical.VitalReading entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                switch (entity.ActionHelper.ClincalAction)
                {
                    case ClinicalAction.AddHeight:
                        #region [@  Method     @]

                        var response = await client.InsertVitalReading_HeightInInchesAsync(new InsertVitalReading_HeightInInchesRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            VitalReading = entity.InsertInput,
                            PropBag = Propbag
                        });
                        client.Close();
                        entity.NewVitalReadingID = response.NewVitalReadingID;
                  
                        entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; ;

                        #endregion
                        break;
                    case ClinicalAction.UpdateHeight:
                        #region [@  Method     @]
                        var response2 = await client.UpdateVitalReading_HeightInInchesAsync(new UpdateVitalReading_HeightInInchesRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            VitalReading = entity.UpdateInput,
                            PropBag = Propbag
                        });
                        client.Close();
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                 
                        entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success };
                        #endregion
                        break;
                    case ClinicalAction.AddWeight:
                        #region [@  Method     @]

                        var response6 = await client.InsertVitalReading_WeightInPoundsAsync(new InsertVitalReading_WeightInPoundsRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            VitalReading = entity.InsertInput,
                            PropBag = Propbag
                        });
                        client.Close();
                        entity.NewVitalReadingID = response6.NewVitalReadingID;
                        entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; ;
                        #endregion
                        break;
                    case ClinicalAction.UpdateWeight:
                        #region [@  Method     @]
                        var response7 = await client.UpdateVitalReading_WeightInPoundsAsync(new UpdateVitalReading_WeightInPoundsRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            VitalReading = entity.UpdateInput,
                            PropBag = Propbag
                        });
                        client.Close();
                        entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; ;
                        #endregion
                        break;
                    case ClinicalAction.ToggleHidden:
                        #region [@  Method     @]
                      
                        //var response3 = await client.ToggleIsHiddenMedicalConditionAsync(
                        //    new ToggleIsHiddenMedicalConditionRequest()
                        //    {
                        //        MessageGuid = Guid.NewGuid(),
                        //        ID = entity.UpdateInput.ID,
                        //        IsHidden = entity.UpdateInput.IsHidden,
                        //        PropBag = Propbag
                        //    });
                        //client.Close();
                        //  entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 

                        #endregion
                        break;
                    case ClinicalAction.LoadHeight:
                        #region [@  Method     @]
                      
                        var response4 = await client.GetVitalReadingsAsync(new GetVitalReadingsRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            RID = entity.RID,
                            VitalStatistics = VitalStatisticsEnum.Height
                        });
                        client.Close();
                        entity.List = response4.VitalReadings;
                        entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; ;

                        #endregion
                        break;
                    case ClinicalAction.LoadWeight:
                        #region [@  Method     @]
                        
                        var response5 = await client.GetVitalReadingsAsync(new GetVitalReadingsRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            RID =entity.RID,
                            VitalStatistics = VitalStatisticsEnum.Weight
                        });
                        client.Close();
                        entity.List.AddRange(response5.VitalReadings); ;
                        entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; ;
                        #endregion
                        break;
                    case ClinicalAction.LoadAll:
                        #region [@  Method     @]

                        var height = await VitalReading(
                            new LG.Data.Models.Clinical.VitalReading()
                            {
                                RID = entity.RID,
                                Action = ClinicalAction.LoadHeight
                            });

                        var weight = await VitalReading(
                            new LG.Data.Models.Clinical.VitalReading()
                            {
                                RID = entity.RID,
                                Action = ClinicalAction.LoadWeight
                            });
                        client.Close();

                        entity.List.AddRange(
                            height.List); ;
                        entity.List.AddRange(
                            weight.List); ;
                        entity.ActionHelper = new ActionHelper
                        {
                            ClincalActionResult = ClinicalActionResult.Success
                        }; ;
                        #endregion
                        break;
                }
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Failed }; ;
                return entity;
            }
        }
        public static async Task<LG.Data.Models.Clinical.MedicationTaken> Medication(LG.Data.Models.Clinical.MedicationTaken entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                switch (entity.ActionHelper.ClincalAction)
                {
                    case ClinicalAction.Add:
                        #region [@  Method     @]
                        var response = await client.AddMedicationTakenAsync(new AddMedicationTakenRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            MedicationTaken = entity.InsertInput,
                            PropBag = Propbag
                        });
                        client.Close();
                        entity.NewMedicationTakenID = response.NewMedicationTakenID;
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                        #endregion
                        break;
                    case ClinicalAction.Update:
                        #region [@  Method     @]
                        var response2 = await client.UpdateMedicationTakenAsync(new UpdateMedicationTakenRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            MedicationTaken = entity.UpdateInput,
                            PropBag = Propbag
                        });
                        client.Close();
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                        #endregion
                        break;
                    case ClinicalAction.ToggleHidden:
                        #region [@  Method     @]

                        //var response3 = await client.ToggleIsHiddenMedicalConditionAsync(
                        //    new ToggleIsHiddenMedicalConditionRequest()
                        //    {
                        //        MessageGuid = Guid.NewGuid(),
                        //        ID = entity.UpdateInput.ID,
                        //        IsHidden = entity.UpdateInput.IsHidden,
                        //        PropBag = Propbag
                        //    });
                        //client.Close();
                        //  entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 

                        #endregion
                        break;
                    case ClinicalAction.LoadAll:
                        #region [@  Method     @]
                        var response4 = await client.GetMedicationsTakenAsync(new GetMedicationsTakenRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            RID = entity.RID
                        });
                        client.Close();
                        entity.List = response4.MedicationsTaken;
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                        #endregion
                        break;
                    case ClinicalAction.LoadDetail:
                        #region [@  Method     @]

                        var response5 = await client.GetMedicationTakenAsync(new GetMedicationTakenRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            ID = entity.UpdateInput.ID
                        });
                        client.Close();
                        entity.MedicationTakenItem = response5.MedicationTaken;
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                        #endregion
                        break;
                }
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Failed }; ;
                return entity;
            }
        }
        public static async Task<LG.Data.Models.Clinical.FamilyCondition> FamilyCondition(LG.Data.Models.Clinical.FamilyCondition entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                switch (entity.ActionHelper.ClincalAction)
                {
                    case ClinicalAction.Add:
                        #region [@  Method     @]
                        var response = await client.AddFamilyHistoryRecordAsync(new AddFamilyHistoryRecordRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            FamilyHistoryRecord = entity.InsertInput,
                            PropBag = Propbag
                        });
                        client.Close();
                        entity.NewFamilyHistoryRecordID = response.NewFamilyHistoryRecordID;
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                        #endregion
                        break;
                    case ClinicalAction.Update:
                        #region [@  Method     @]
                        var response2 = await client.UpdateFamilyHistoryRecordAsync(new UpdateFamilyHistoryRecordRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            FamilyHistoryRecord = entity.UpdateInput,
                            PropBag = Propbag
                        });
                        client.Close();
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                        #endregion
                        break;
                    case ClinicalAction.ToggleHidden:
                        #region [@  Method     @]

                        //var response3 = await client.ToggleIsHiddenMedicalConditionAsync(
                        //    new ToggleIsHiddenMedicalConditionRequest()
                        //    {
                        //        MessageGuid = Guid.NewGuid(),
                        //        ID = entity.UpdateInput.ID,
                        //        IsHidden = entity.UpdateInput.IsHidden,
                        //        PropBag = Propbag
                        //    });
                        //client.Close();
                        //  entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 

                        #endregion
                        break;
                    case ClinicalAction.LoadAll:
                        #region [@  Method     @]
                        var response4 = await client.GetFamilyHistoryRecordsAsync(new GetFamilyHistoryRecordsRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            RID = entity.RID
                        });
                        client.Close();
                        entity.List = response4.FamilyHistoryRecords;
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                        #endregion
                        break;
                    case ClinicalAction.LoadDetail:
                        #region [@  Method     @]

                        var response5 = await client.GetFamilyHistoryRecordAsync(new GetFamilyHistoryRecordRequest()
                        {
                            MessageGuid = Guid.NewGuid(),
                            ID = entity.UpdateInput.ID
                        });
                        client.Close();
                        entity.FamilyHistoryRecordItem = response5.FamilyHistoryRecord;
                      
                          entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Success }; 
                        #endregion
                        break;
                }
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                entity.ActionHelper = new ActionHelper { ClincalActionResult = ClinicalActionResult.Failed }; ;
                return entity;
            }
        }
    
    }
    public static class SearchDataService
    {
        public static async Task<LG.Data.Models.Clinical.AllergyResults> Allergys(
            LG.Data.Models.Clinical.AllergyResults entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response = await client.FindAllergyStandardizedAsync(new FindAllergyStandardizedRequest()
                {
                    MessageGuid = new Guid(),
                    SearchString = entity.SearchText,
                    ResultSetSize = entity.ResultSetSize
                });
                client.Close();
                entity.Results = response.ListOfAllergiesStandardized;
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;

                entity.Message = ex.ToString();
                return entity;
            }
        }
        public static async Task<LG.Data.Models.Clinical.ConditionsResults> Conditions(
            LG.Data.Models.Clinical.ConditionsResults entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response = await client.FindMedicalConditionStandardizedAsync(new FindMedicalConditionStandardizedRequest()
                {
                    MessageGuid = new Guid(),
                    SearchString = entity.SearchText,
                    ResultSetSize = entity.ResultSetSize
                });
                client.Close();
                entity.Results = response.ListOfConditionStandardized;
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                return entity;
            }
        }
        public static async Task<LG.Data.Models.Clinical.MedicationResults> Medications(
           LG.Data.Models.Clinical.MedicationResults entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response = await client.FindMedicationStandardizedAsync(new FindMedicationStandardizedRequest()
                {
                    MessageGuid = new Guid(),
                    SearchString = entity.SearchText,
                    ResultSetSize = entity.ResultSetSize
                });
                client.Close();
                entity.Results = response.ListOfMedicationsStandardized;
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                return entity;
            }
        }
        public static async Task<LG.Data.Models.Clinical.FamilyHistoryResults> FamilyHistory(
           LG.Data.Models.Clinical.FamilyHistoryResults entity)
        {
            var client = ClientConnection.GetCDMS_Connection();
            try
            {
                client.Open();
                var response = await client.GetFamilyHistoryConditionsListAsync(new GetFamilyHistoryConditionsListRequest()
                {
                    MessageGuid = new Guid(),
                });
                client.Close();
                entity.Results = response.ListOfFamilyHistoryConditions;
                return entity;
            }
            catch (Exception ex)
            {
                client.Close();
                entity.IsError = true;
                entity.Message = ex.ToString();
                return entity;
            }
        }
    }
}