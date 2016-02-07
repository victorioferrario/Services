using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Owin.Identity.Models
{
    public class MedicalRecords
    {
        public List<LG.Services.CDMS.VitalReading> Vitals { get; set; }
        public List<LG.Services.CDMS.Allergy> Allergies { get; set; }
        public LG.Data.Models.Clinical.ActiveConsultation Details { get; set; }
        public List<LG.Services.CDMS.MedicalCondition> Conditions { get; set; }
        public List<LG.Services.CDMS.MedicationTaken> MedicationTaken { get; set; }
        public List<LG.Services.CDMS.FamilyHistoryRecord> FamilyHistory { get; set; }
    }
}
