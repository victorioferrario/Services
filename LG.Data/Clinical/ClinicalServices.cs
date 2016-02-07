using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Clinical
{
   public static class ClinicalServices
    {
       public static async Task<LG.Data.Models.Clinical.Allergy> Allergy(LG.Data.Models.Clinical.Allergy entity)
       {
           return await LG.Data.Core.Clinical.ClinicalDataService.Allergy(entity);
       }
       public static async Task<LG.Data.Models.Clinical.Allergy> LoadAllergies(LG.Data.Models.Clinical.Allergy entity)
       {
           return await LG.Data.Core.Clinical.ClinicalDataService.LoadAllergies(entity);
       }
       public static async Task<LG.Data.Models.Clinical.Condition> Condition(LG.Data.Models.Clinical.Condition entity)
       {
           return await LG.Data.Core.Clinical.ClinicalDataService.Condition(entity);
       }
       public static async Task<LG.Data.Models.Clinical.Condition> LoadConditions(LG.Data.Models.Clinical.Condition entity)
       {
           return await LG.Data.Core.Clinical.ClinicalDataService.LoadConditions(entity);
       }
       
       public static async Task<LG.Data.Models.Clinical.FamilyCondition> LoadFamilyHistory(LG.Data.Models.Clinical.FamilyCondition entity)
       {
           return await LG.Data.Core.Clinical.ClinicalDataService.FamilyCondition(entity);
       }

       public static async Task<LG.Data.Models.Clinical.AllergyResults> Allergies(
           LG.Data.Models.Clinical.AllergyResults entity)
       {
           return await LG.Data.Core.Clinical.SearchDataService.Allergys(entity);
       }
       public static async Task<LG.Data.Models.Clinical.ConditionsResults> Conditions(
           LG.Data.Models.Clinical.ConditionsResults entity)
       {
           return await LG.Data.Core.Clinical.SearchDataService.Conditions(entity);
       }
       public static async Task<LG.Data.Models.Clinical.MedicationResults> Medications(
           LG.Data.Models.Clinical.MedicationResults entity)
       {
           return await LG.Data.Core.Clinical.SearchDataService.Medications(entity);
       }

       public static async Task<LG.Data.Models.Clinical.MedicationTaken> Medication(
           LG.Data.Models.Clinical.MedicationTaken entity)
       {
           return await LG.Data.Core.Clinical.ClinicalDataService.Medication(entity);
       }

       public static async Task<LG.Data.Models.Clinical.FamilyCondition> FamilyHistory(
           LG.Data.Models.Clinical.FamilyCondition entity)
       {
           return await LG.Data.Core.Clinical.ClinicalDataService.FamilyCondition(entity);
       }

       public static async Task<LG.Data.Models.Clinical.VitalReading> VitalReading(
          LG.Data.Models.Clinical.VitalReading entity)
       {
           return await LG.Data.Core.Clinical.ClinicalDataService.VitalReading(entity);
       }
       
    }
}