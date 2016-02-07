using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.CDMS;

namespace LG.Data.Models.Doctors.ConsultWizard
{
    public class SearchDiagnosisEntity:BaseModel
    {
        public string InputString { get; set; }
        public List<LG.Services.CDMS.DiagnosisLookup> Result { get; set; }

    }
}
