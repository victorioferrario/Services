using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LG.Services.CDMS;

namespace LG.Data.Models.Orders
{
    public class Data
    {
        public Order Order { get; set; }
        public OrderInProcess OrdersInProcess { get; set; }
        public FilesAssociatedWithConsultation Files { get; set; }
    }
    public class OrderInProcess :LG.Data.Models.BaseModel 
    {
        public System.Int32 AccountID { get; set; }
        public LG.Services.OMS.ProcessStateEnum ProcessState { get; set; }

        public List<LG.Services.OMS.ConsultationStatusFound> ConsultationsFound { get; set; }
        public List<ConsultationStatusFoundExpanded> ConsultationsFoundExpanded { get; set; }
       
        public List<LG.Services.OMS.OrderStatusFound> OrdersFound { get; set; }
        public LG.Data.Models.Enums.OrderAction OrderAction { get; set; }
        public LG.Data.Models.Enums.OrderActionResult OrderResult { get; set; }
    }
    public class ConsultationFileAttachment : LG.Data.Models.BaseModel
    {
        public System.Int32 ConsultationID { get; set; }
        public System.String FileFullName { get; set; }
        public System.Byte[] FilePlainBytes { get; set; }
        public System.String Description { get; set; }
    }

    public class FilesAssociatedWithConsultation: LG.Data.Models.BaseModel
    {
        public System.Int32 ConsultationID { get; set; }

        public LG.Services.CDMS.FileAssociatedWithMedicalRecord FileItem { get;set; }
        public List<LG.Services.CDMS.FileAssociatedWithMedicalRecord> Files { get; set; }
       
    }

    public class Order : LG.Data.Models.BaseModel
    {
        public LG.Services.OMS.SingleConsultationOrderInput OrderInput { get; set; }
        public LG.Services.OMS.StartOrderProcessingResponse OrderInputResponse { get; set; }
        public LG.Data.Models.Enums.OrderAction OrderAction { get; set; }
        public LG.Data.Models.Enums.OrderActionResult OrderResult { get; set; }
    }
    public class ManuallyAssignOrder : LG.Data.Models.BaseModel
    {
        public System.Int64 AssignedByRID { get; set; }
        public System.Int32 ConsultationID { get; set; }
        public System.Int64 MedicalPractitionerRID { get; set; }
    }

    public class OrderStatusFoundExpanded: LG.Services.OMS.OrderStatusFound
    {
        public System.String DrName { get; set; }
    }
    public class ConsultationStatusFoundExpanded : LG.Services.OMS.ConsultationStatusFound
    {
        [System.Runtime.Serialization.DataMemberAttribute(Order = 14)]
        public System.String DrName { get; set; }

        public ConsultationStatusFoundExpanded() : base()
        {
            DrName = String.Empty;
            // me.base.MemberwiseClone();
        }
    }


    public class Consultations : LG.Data.Models.BaseModel
    {
        public List<LG.Services.OMS.ConsultationToBeAssignedDetails> ToBeAssigned { get; set; }
        public List<LG.Services.OMS.ConsultationToBeServicedDetails> ToBeServiced { get; set; }

        public System.Boolean IsTesting { get; set; }
        public LG.Data.Models.Enums.OrderAction OrderAction { get; set; }
        public LG.Data.Models.Enums.OrderActionResult OrderResult { get; set; }
    }
}
