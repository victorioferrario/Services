using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Doctors
{
    public class Search
    {
        public System.Int64 PracticeID { get { return 100; } }
        public Guid MessageGuid { get { return Guid.NewGuid(); } }
        public System.String PropBag { get { return "<PropBag>LG.Data.Models.Doctors.Search</PropBag>"; } }
        public LG.Services.MPMS.GetMedicalPractitionersRequest Request { get; set; }
            
    }
    public class SearchResults : LG.Data.Models.BaseModel
    {
        public List<LG.Services.MPMS.MedicalPractitionerInfo> Results
        {
            get;
            set;
        }
    }
}
