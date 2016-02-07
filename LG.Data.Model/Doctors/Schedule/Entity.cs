using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.MPMS;
using LG.Services.MPMS.SM;

namespace LG.Data.Models.Doctors.Schedule
{
    public class Entity:LG.Data.Models.BaseModel
    {
        public Guid InstanceGuid { get; set; }
        public System.Int64 MSPRID{ get; set; }

        public System.String StateCode { get; set; }

        public System.DateTime D_To { get; set; }
        public System.DateTime D_From { get; set; }

        public System.Int64 MedicalPractitionerRID { get; set; }
        public List<LG.Services.MPMS.SM.AvailabilityBlock> AvailabilityBlocks { get; set; }

        public MedicalPractitionerAvailabilityBatch AvailabilityBatch { get; set; }

        public List<LG.Services.MPMS.SM.MedicalPractitionerAvailabilityOneState> AvailabilityOneState { get; set; }

        public List<LG.Services.MPMS.SM.MedicalPractitionerCountOneStateInRange> AvailabilityAllStates { get; set; }
        
    }
    public class DoctorsAvailablePerState : LG.Data.Models.BaseModel
    {
        public string StateCode { get; set; }
        public List<MedicalPractitionerInfo> ListOfPractitionerInfo { get; set; }
        public List<MedicalPractitionerAvailabilityOneState> ListOfPractitionerAvailablePerState { get; set; }
        public DoctorsAvailablePerState()
        {
            ListOfPractitionerInfo 
                = new List<MedicalPractitionerInfo>();
            ListOfPractitionerAvailablePerState 
                = new List<MedicalPractitionerAvailabilityOneState>();
        }
    }
}