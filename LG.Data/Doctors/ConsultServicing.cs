using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Doctors
{
    public static class ConsultServicing
    {

        #region [@  Plan Of Care        @]

        public static async Task<LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity> GetPlanOfCare(LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.GetPlanOfCare(entity);
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity> SavePlanOfCare(LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.SavePlanOfCare(entity);
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity> SavePlanOfCareDraft(LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.SavePlanOfCareDraft(entity);
        }

        #endregion

        #region [@  Diagnosis           @]

        public static async Task<LG.Data.Models.Doctors.ConsultWizard.SearchDiagnosisEntity> SearchDiagnosis(
            string input)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.SearchDiagnosis(input);
        }

        public static async Task<LG.Data.Models.Doctors.ConsultWizard.Diagnosis> AddDiagnosis(LG.Data.Models.Doctors.ConsultWizard.Diagnosis entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.AddDiagnosis(entity);
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.DiagnosisList> GetDiagnosis(LG.Data.Models.Doctors.ConsultWizard.DiagnosisList entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.GetDiagnosis(entity);
        }

        #endregion

        #region [@  Messages            @]

        public static async Task<LG.Data.Models.Doctors.ConsultWizard.MessageConsultation> SendMessage(LG.Data.Models.Doctors.ConsultWizard.MessageConsultation entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.SendMessage(entity);
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.MessageConsultation> GetMessageExchange(LG.Data.Models.Doctors.ConsultWizard.MessageConsultation entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.GetMessageExchange(entity);
        }

        #endregion // Messages

        #region [@  Notes               @]

        public static async Task<LG.Data.Models.Doctors.ConsultWizard.Note> InsertGeneralNote(LG.Data.Models.Doctors.ConsultWizard.Note entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.InsertGeneralNote(entity);
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.Note> GetGeneralNotes(LG.Data.Models.Doctors.ConsultWizard.Note entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.GetGeneralNotes(entity);
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.Note> ToggleGeneralNote(LG.Data.Models.Doctors.ConsultWizard.Note entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.ToggleGeneralNote(entity);
        }

        #endregion // Notes

        public static async Task<LG.Data.Models.Doctors.ConsultWizard.CallInstance> StartClickToCall(LG.Data.Models.Doctors.ConsultWizard.StartClickToCall entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.StartClickToCall(entity);
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.CallOutcome> SaveCallOutcome(LG.Data.Models.Doctors.ConsultWizard.CallOutcome entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.SaveCallOutcome(entity);
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.CallOutcomeEntity> AddCallOutcome(LG.Data.Models.Doctors.ConsultWizard.CallOutcomeEntity entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.AddCallOutcome(entity);
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.CallOutcome> SaveStepsOutcome(LG.Data.Models.Doctors.ConsultWizard.CallOutcome entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.SaveStepsOutcome(entity);
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.CallInstance> GetCallStatus(LG.Data.Models.Doctors.ConsultWizard.CallInstance entity)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.GetCallStatus(entity);
        }
        public static async Task<LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity> SaveDisposition(LG.Data.Models.Doctors.ConsultWizard.PlanOfCareEntity pc)
        {
            return await LG.Data.Core.Doctors.ConsultationWizardServicing.SaveDisposition(pc);
        }
    }
}
