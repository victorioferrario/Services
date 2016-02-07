using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums;
using LG.Services.CDMS;

namespace LG.Data.Models.Clinical
{

    public class Entity : LG.Data.Models.BaseModel
    {
        public System.Int64 RID { get; set; }
        public LG.Data.Models.Enums.ClinicalAction Action { get; set; }
        public LG.Data.Models.Enums.ClinicalType ActionType { get; set; }
        public LG.Data.Models.Enums.ClinicalActionResult ActionResult { get; set; }
        public LG.Data.Models.Clinical.ActionHelper ActionHelper { get; set; }

        public Entity()
        {

        }
    }

    public class HealthRecords
    {
        public IEnumerable<Allergy> Allergies { get; set; }
        public IEnumerable<Condition> Conditions { get; set; }
        public IEnumerable<FamilyCondition> FamilyConditions { get; set; }
        public IEnumerable<MedicationTaken> MedicationsTaken { get; set; }
        public IEnumerable<VitalReading> VitalReadings { get; set; }
    }

    public class Data
    {
        public Allergy AllergyEntitiy { get; set; }
        public Condition ConditionEntitiy { get; set; }
        public FamilyCondition FamilyConditionEntitiy { get; set; }
        public MedicationTaken MedicationTakenEntitiy { get; set; }
        public VitalReading VitalReadingEntitiy { get; set; }

    }
    public class ActionHelper
    {
        public LG.Data.Models.Enums.ClinicalAction ClincalAction { get; set; }
        public LG.Data.Models.Enums.ClinicalType ClincalActionType { get; set; }
        public LG.Data.Models.Enums.ClinicalActionResult ClincalActionResult { get; set; }
    }

    public class AllergyResults : LG.Data.Models.BaseModel
    {
        public String SearchText { get; set; }
        public Int32 ResultSetSize { get; set; }
        public List<LG.Services.CDMS.AllergyStandardized> Results { get; set; }
    }
    public class Allergy : LG.Data.Models.BaseModel
    {
        public Allergy()
            : base()
        {

        }

        public Int32 NewAllergyID { get; set; }
        public LG.Services.CDMS.Allergy AllergyItem { get; set; }
        public Allergy_Insert InsertInput { get; set; }
        public Allergy_Update UpdateInput { get; set; }
        public List<LG.Services.CDMS.Allergy> List { get; set; }
        public System.Int64 RID { get; set; }
        public LG.Data.Models.Enums.ClinicalAction ClincalAction { get; set; }
        public LG.Data.Models.Enums.ClinicalType ClincalActionType { get; set; }
        public LG.Data.Models.Enums.ClinicalActionResult ClincalActionResult { get; set; }
        public LG.Data.Models.Clinical.ActionHelper ActionHelper { get; set; }

    }
    public class ConditionsResults : LG.Data.Models.BaseModel
    {
        public String SearchText { get; set; }
        public Int32 ResultSetSize { get; set; }
        public List<LG.Services.CDMS.MedicalConditionStandardized> Results { get; set; }
    }
    public class Condition : LG.Data.Models.BaseModel
    {
        public Int32 NewMedicalConditionID { get; set; }
        public List<LG.Services.CDMS.MedicalCondition> List { get; set; }
        public MedicalCondition MedicalConditionItem { get; set; }
        public MedicalCondition_Insert InsertInput { get; set; }
        public MedicalCondition_Update UpdateInput { get; set; }
        public System.Int64 RID { get; set; }
        public LG.Data.Models.Enums.ClinicalAction Action { get; set; }
        public LG.Data.Models.Enums.ClinicalType ActionType { get; set; }
        public LG.Data.Models.Enums.ClinicalActionResult ActionResult { get; set; }
        public LG.Data.Models.Clinical.ActionHelper ActionHelper { get; set; }

    }
    public class MedicationResults : LG.Data.Models.BaseModel
    {
        public String SearchText { get; set; }
        public Int32 ResultSetSize { get; set; }
        public List<LG.Services.CDMS.MedicationStandardized> Results { get; set; }
    }

    public class FamilyHistoryResults : LG.Data.Models.BaseModel
    {
        public String SearchText { get; set; }
        public Int32 ResultSetSize { get; set; }
        public List<LG.Services.CDMS.FamilyHistoryCondition> Results { get; set; }
    }
    public class VitalReading : LG.Data.Models.BaseModel
    {
        public Int32 NewVitalReadingID { get; set; }
        public System.Int64 RID { get; set; }
        public LG.Data.Models.Enums.ClinicalAction Action { get; set; }
        public LG.Data.Models.Enums.ClinicalType ActionType { get; set; }
        public LG.Data.Models.Clinical.ActionHelper ActionHelper { get; set; }
        public LG.Data.Models.Enums.ClinicalActionResult ActionResult { get; set; }
        public List<LG.Services.CDMS.VitalReading> List { get; set; }

        public LG.Services.CDMS.VitalReading VitalReadingItem { get; set; }
        public LG.Services.CDMS.VitalReading_Input InsertInput { get; set; }
        public LG.Services.CDMS.VitalReading_Update UpdateInput { get; set; }
    }

    public class MedicationTaken : LG.Data.Models.BaseModel
    {
        public Int32 NewMedicationTakenID { get; set; }
        public System.Int64 RID { get; set; }
        public LG.Data.Models.Enums.ClinicalAction Action { get; set; }
        public LG.Data.Models.Enums.ClinicalType ActionType { get; set; }
        public LG.Data.Models.Clinical.ActionHelper ActionHelper { get; set; }
        public LG.Data.Models.Enums.ClinicalActionResult ActionResult { get; set; }
        public List<LG.Services.CDMS.MedicationTaken> List { get; set; }
        public LG.Services.CDMS.MedicationTaken MedicationTakenItem { get; set; }
        public LG.Services.CDMS.MedicationTaken_Insert InsertInput { get; set; }
        public LG.Services.CDMS.MedicationTaken_Update UpdateInput { get; set; }
    }
    public class FamilyCondition : LG.Data.Models.BaseModel
    {
        public Int32 NewFamilyHistoryRecordID { get; set; }
        public System.Int64 RID { get; set; }
        public LG.Data.Models.Enums.ClinicalAction Action { get; set; }
        public LG.Data.Models.Enums.ClinicalType ActionType { get; set; }
        public LG.Data.Models.Clinical.ActionHelper ActionHelper { get; set; }
        public LG.Data.Models.Enums.ClinicalActionResult ActionResult { get; set; }
        public List<LG.Services.CDMS.FamilyHistoryRecord> List { get; set; }
        public LG.Services.CDMS.FamilyHistoryRecord FamilyHistoryRecordItem { get; set; }
        public LG.Services.CDMS.FamilyHistoryRecord_Insert InsertInput { get; set; }
        public LG.Services.CDMS.FamilyHistoryRecord_Update UpdateInput { get; set; }
    }
}