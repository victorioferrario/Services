using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Users;
using LG.Services.MPMS;

namespace LG.Data.Doctors
{
    public static class DoctorService
    {
        public static async Task<LG.Data.Models.Doctors.SearchResults> Search()
        {
            var entity = new LG.Data.Models.Doctors.Search()
            {
                Request = new LG.Services.MPMS.GetMedicalPractitionersRequest()
                {
                    IsActiveMedicalPractitionerFilter = IsActiveFilterEnum.All,
                    IsExpiredLicenseFilter = IsExpiredFilterEnum.All,
                    IsTestingMedicalPractitionerFilter = IsTestingFilterEnum.All,
                    IsVoidLicenseFilter = IsVoidFilterEnum.All,
                    MSPRID =100
                }
            };
            return await LG.Data.Core.Doctors.DoctorDataService.Search(entity);
        }


        public static async Task<bool> SaveContactInfo(UserModel entity)
        {
            return await LG.Data.Core.Doctors.DoctorDataService.SaveContactInfo(entity);
        }

        public static async Task<LG.Data.Models.Doctors.OperationResponses> Save(LG.Data.Models.Doctors.Inputs eInputs)
        {
            return await LG.Data.Core.Doctors.DoctorDataService.Save(eInputs);
        }
    }
}
