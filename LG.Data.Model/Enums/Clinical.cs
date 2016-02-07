using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Enums
{
    public enum ClinicalAction
    {
        None = 0,
        Add = 1,
        Update = 2,
        LoadAll=3,
        LoadDetail = 4,
        ToggleHidden = 5,
        AddHeight = 6,
        LoadHeight = 7,
        UpdateHeight = 8,
        AddWeight = 9, 
        LoadWeight = 10,
        UpdateWeight = 11,
    }
    public enum ClinicalType
    {
        None = 0,
        Allergy = 1,
        Condition = 2,
        Medication = 3,
        FamilyHistory = 4,
        VitalReading = 5,
    }
    public enum ClinicalActionResult
    {
        None =0,
        Failed = 1,
        Success=2,
    }
    public enum OrderAction
    {
        None = 0,
        StartOrder = 1,
        CancelOrder = 2,
        GetOrderStatus = 3,
        RetryProcessPayment = 4,
        ForcePaymentProcessing = 5,
        GetConsultationStatus = 6,
        FindOrdersInProcessState = 7,
        FindConsultationsInProcessState = 8,
        ManuallyAssignMedicalPractitioner = 9,
        GetConsultationsToBeAssigned = 10,
        GetConsultationsToBeServiced = 11,
    }
    public enum OrderActionResult
    {
        None = 0,
        Failed = 1,
        Success = 2,
    }
}
