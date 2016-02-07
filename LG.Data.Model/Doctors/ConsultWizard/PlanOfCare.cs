using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Doctors.ConsultWizard
{
    public class PlanOfCareEntity : BaseModel
    {
        public System.Int64 PatientRID { get; set; }
        public System.Int32 ConsultationID { get; set; }
        public System.Int64 MedicalPractionerRID { get; set; }
        public LG.Services.CDMS.PlanOfCareDraft Draft { get; set; }
        public LG.Services.CDMS.PlanOfCare Disposition { get; set; }
        public LG.Services.CDMS.PlanOfCareLegal DispositionLegal { get; set; }
        public List<LG.Services.CDMS.LegalDisclaimer> Disclaimers { get; set; }
    }

    public class MessageConsultation : BaseModel
    {
        public System.Int32 ID { get; set; }
        public System.Int64 SendByRID { get; set; }
        public System.Int64 PatientRID { get; set; }
        public System.Int32 ConsultationID { get; set; }
        public System.Int64 MedicalPractionerRID { get; set; }
        public System.String EConsultationMessage { get; set; }
        public List<LG.Services.CDMS.ConsultationMessage> MessageExchange { get; set; }

    }

    public class Note : BaseModel
    {
        public System.Int32 ID { get; set; }
        public bool IsHidden { get; set; }
        public System.String NoteText { get; set; }
        public System.Int64 PatientRID { get; set; }
        public System.Int64 CreatedByRID { get; set; }
        public System.Int32 ConsultationID { get; set; }
        public System.Int64 MedicalPractionerRID { get; set; }

        public List<LG.Services.CDMS.GeneralNote> Notes { get; set; }
    }

    public class Diagnosis : BaseModel
    {
        public System.Int32 ID { get; set; }
        public System.Int32 ConsultationID { get; set; }
        public System.Int64 MedicalPractionerRID { get; set; }
        public LG.Services.CDMS.Diagnosis Result { get; set; }
        public LG.Services.CDMS.Diagnosis_Input Input { get; set; }
    }

    public class DiagnosisList : BaseModel
    {
        public System.Int32 ID { get; set; }
        public System.Int32 ConsultationID { get; set; }
        public System.Int64 MedicalPractionerRID { get; set; }
        public List<LG.Services.CDMS.Diagnosis> Result { get; set; }
       
    }
    public class CallOutcomeEntity : BaseModel{
        public System.String CallSid { get; set; }
        public LG.Services.OMS.IVRProvidersEnum Provider { get; set; }
        public LG.Services.OMS.IVRCallOutcomeEnum Outcome { get; set; }
    }
}
