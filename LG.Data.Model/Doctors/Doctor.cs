using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Doctors
{

    public class Inputs : LG.Data.Models.BaseModel
    {
        public System.Int64 RID { get; set; }
        public System.Int32 MedicalLicenseID { get; set; }
        public Guid MessageGuid { get { return Guid.NewGuid(); } }
        public System.String PropBag { get { return "<PropBag>LG.Data.Models.Doctors.Search</PropBag>"; } }
        public LG.Data.Models.Enums.DoctorActions EventAction { get; set; }
        public LG.Services.MPMS.MedicalLicense_StoreInput LicenseStoreInput { get; set; }
        public LG.Services.MPMS.MedicalLicense_UpdateInput LicenseUpdateInput { get; set; }

        public LG.Services.MPMS.MedicalPractitioner_InsertInput MedicalPractitionerInsertInput { get; set; }
        public LG.Services.MPMS.MedicalPractitioner_UpdateInput MedicalPractitionerUpdateInput { get; set; }

    }

    public class OperationResponses:LG.Data.Models.BaseModel
    {
        public LG.Services.MPMS.GetMedicalPractitionersResponse GetListResponse { get; set; }
        public LG.Services.MPMS.StoreMedicalLicense_Response InsertLicense { get; set; }
        public LG.Services.MPMS.UpdateMedicalLicenseResponse UpdateLicense { get; set; }
        public LG.Services.MPMS.InsertMedicalPractitionerResponse InsertPractioner { get; set; }
        public LG.Services.MPMS.UpdateMedicalPractitionerResponse UpdatePractioner { get; set; }

    }


}
