using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Doctors.ConsultWizard;

namespace LG.Owin.Identity.Data
{
    public class Messaging
    {
        public long RID { get; set; }
        public int ConsultationID { get; set; }
        public long MedicalPractiniorRID { get; set; }

        public Messaging(long memberRid, long doctorRid, int consultId)
        {
            RID = memberRid;
            ConsultationID = consultId;
            MedicalPractiniorRID = doctorRid;
        }

        public async Task<MessageConsultation> GetAllMessages()
        {
            return  await LG.Data.Doctors.ConsultServicing.GetMessageExchange(new MessageConsultation()
            {
                ConsultationID = ConsultationID
            });
        }
        public async Task<MessageConsultation> SendMessage(string message)
        {
            return await LG.Data.Doctors.ConsultServicing.SendMessage(new MessageConsultation()
            {
                PatientRID = RID,
                EConsultationMessage = message,
                ConsultationID = ConsultationID,
                MedicalPractionerRID = MedicalPractiniorRID
            });
        }
    }
   
}
