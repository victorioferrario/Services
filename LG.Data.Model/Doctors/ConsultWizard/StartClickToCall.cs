using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.OMS;

namespace LG.Data.Models.Doctors.ConsultWizard
{
    public class StartClickToCall : BaseModel
    {
        public string PinCode { get; set; }
        public string CallSid { get; set; }
        public Int32 ConsultationID { get; set; }
        public long MedicalPractionerRID { get; set; }
        public string MedicalPractionerPhoneNumber { get; set; }
    }

    public class CallOutcome : BaseModel
    {
        public Int32 ConsultationID { get; set; }

        public long MedicalPractionerRID { get; set; }

        public MedicalConsultationProcessOutcomeEnum Outcome { get; set; }
    }
    public class ClickToCallLight
    {
        public string PhoneNumber { get; set; }
    }
    public class CallInstance : BaseModel
    {
        public string CallSid { get; set; }
        public string PinCode { get; set; }
        public Int32 ConsultationID { get; set; }
        public LG.Services.OMS.IVRCallStatusEnum CallStatus { get; set; }
    }

    

}
