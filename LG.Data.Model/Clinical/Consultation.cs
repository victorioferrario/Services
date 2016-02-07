using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.CDMS;

namespace LG.Data.Models.Clinical
{
    public class ActiveConsultation : LG.Data.Models.BaseModel
    {
        public System.Int32 ConsultationID { get; set; }
        public OrderData OrderDetails { get; set; }
        public ClinicalData EncounterData { get; set; }
        
    }
    public class ConsultationHistoryItem : ClinicalDataHistoryItem
    {
        public System.Int32 ConsultationID { get; set; }
        public OrderData OrderDetails { get; set; }
        public ClinicalData EncounterData { get; set; }

    }
    public class ClinicalDataHistoryItem : ClinicalData
    {
        public LG.Services.CDMS.PlanOfCareLegal PlanOfCare { get; set; }
        public IEnumerable<LG.Services.CDMS.Diagnosis> ListDiagnoses { get; set; }
        

    }
    public class ClinicalData : LG.Data.Models.BaseModel
    {
        public ClinicalData()
        {
            ChiefComplaints = new List<ChiefComplaint>();
            Files = new List<FileAssociatedWithMedicalRecord>();
        }
       public List<LG.Services.CDMS.ChiefComplaint> ChiefComplaints { get; set; }
       public  List<LG.Services.CDMS.FileAssociatedWithMedicalRecord> Files { get; set; }
       public  HealthRecords HealthChart { get; set; }
    }

    public class OrderData : LG.Data.Models.BaseModel
    {
        public OrderData()
        {
            OrderStatus = new LG.Services.OMS.OrderStatusDetails();
            ConsultStatus = new LG.Services.OMS.ConsultationStatusDetails_GetStatus();
        }
        public LG.Services.OMS.OrderStatusDetails OrderStatus { get; set; }
        public LG.Services.OMS.ConsultationStatusDetails_GetStatus ConsultStatus { get; set; }
    }
}
