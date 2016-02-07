using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Enums
{
    public enum Action
    {
        None = 0,
        Add = 1,
        Update = 2,
        Remove = 3
    }

    public enum ActionResult
    {
        None = 0,
        Failed = 1,
        Success = 2,
    }

    public enum AccountAction
    {
        None = 0,
        IsActive = 1,
        ParentAccount = 2,
        IsSelfManaged = 3,
        IsAutorenewal = 4,
        IsTesting = 5,
        MembershipPlan = 6,
        ExpirationDate = 7,
        CreditCard = 8,
        AccountInfo = 9,
        LoadCreditCard = 10,
        LoadDependents = 11
    }

    public enum DoctorActions
    {
        None = 0,
        DoctorAdd = 1,
        DoctorUpdate = 2,
        LicenseAdd = 3,
        LicenseUpdate = 4,
        DoctorUpdatePrintedName = 5,
        DoctorUpdateMedicalInformation = 6,

    }
    public enum ProductAction
    {
        Get = 0,
        Load = 1,
        Store = 2,
        StoreMultiple = 5,
        StoreClientLevel = 6,
        UpdateLabel = 3,
        ToggleStatus = 4,
    }
    public enum ContactAction
    {
        Get = 0,
        Add = 2,
        Update = 3,
        Load = 1,
        ToggleStatus = 4,
    }
}
