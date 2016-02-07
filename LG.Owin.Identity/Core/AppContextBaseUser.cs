using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LG.Data.Models.Clinical;
using LG.Data.Models.Core;
using LG.Data.Models.Doctors.ConsultWizard;
using LG.Data.Models.Enums;
using LG.Owin.Identity.Enums;
using LG.Owin.Identity.Models;
using LG.Services.ACS;
using LG.Services.EMS;
using LG.Services.PDMS;
using Entity = LG.Data.Models.Members.Entity;
using LG.Services.CMS;
using LG.Services.OMS;
namespace LG.Owin.Identity.Core
{
    public class AppContextUserBase : AppContextUserProperties
    {
        #region [@   CONSTRUCTOR            @]

        public AppContextUserBase()
        {
            RolodexItemID = 0; IsAuthenticated = false; ContextPortal = PortalType.MEMBERS;
            CreditCartList = new List<CreditCardInfo>();
        }

        public bool StartUp(PortalType portal)
        {
            if (ContextPortal != portal)
            {
                ContextPortal = portal;
            }
            return true;
        }

        #endregion

        #region [@   MEMBER DATA            @]

        public async Task<bool> Load()
        {
            if (Identity == null && RolodexItemID == 0) return false;
            if (Identity != null)
            {

                IsAuthenticated = true;

                RolodexItemID = Identity.RID;

                Entity = await LG.Data.Members.MemberService.LoadDetail(
                    Identity.RID);

                PersonInfo = await LG.Data.Shared.EntityService.LoadPersonInfo(
                    Identity.RID);

                AccountInfo = await LG.Data.Members.MemberService.GetAccountInfo(
                    Identity.RID);

                var o = new Entity()
                {
                    RID = Identity.RID,
                    AccountAction = AccountAction.LoadCreditCard,
                    Events = new Events()
                    {
                        AccountAction = AccountAction.LoadCreditCard,
                        AccountActionResult = ActionResult.None
                    },
                    AccountInfo = new AccountInfo()
                    {
                        AccountID = AccountInfo.AccountInfo.AccountID
                    }
                };


                CreditCartList = await LG.Data.Members.AccountService.LoadCreditCards(
                    AccountInfo.AccountInfo.AccountID);

                AccountInfoCCInfo = await LG.Data.Members.AccountService.Load(o);

                MembershipPlan = await LG.Data.Clients.MembershipService.GetInfo(AccountInfo.AccountInfo.MembershipPlanID);

                GroupInfo = await LG.Data.Clients.GroupService.Get(MembershipPlan.GroupID);

                ClientInfo = await LG.Data.Clients.ClientService.GetClientInfo(GroupInfo.ClientRID);

                Products = await LG.Data.Products.ProductService.GetProductsByAccountID(AccountInfo.AccountInfo.AccountID);

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Load(long id)
        {


            RolodexItemID = id;

            Entity = await LG.Data.Members.MemberService.LoadDetail(
                id);

            PersonInfo = await LG.Data.Shared.EntityService.LoadPersonInfo(
                id);

            AccountInfo = await LG.Data.Members.MemberService.GetAccountInfo(
                id);


            var o = new Entity()
            {
                RID = id,
                AccountAction = AccountAction.LoadCreditCard,
                Events = new Events()
                {
                    AccountAction = AccountAction.LoadCreditCard,
                    AccountActionResult = ActionResult.None
                },
                AccountInfo = new AccountInfo()
                {
                    AccountID = AccountInfo.AccountInfo.AccountID
                }

            };
            CreditCartList = await LG.Data.Members.AccountService.LoadCreditCards(
                  AccountInfo.AccountInfo.AccountID);

            AccountInfoCCInfo = await LG.Data.Members.AccountService.Load(o);

            MembershipPlan = await LG.Data.Clients.MembershipService.GetInfo(
                AccountInfo.AccountInfo.MembershipPlanID);

            GroupInfo = await LG.Data.Clients.GroupService.Get(
                MembershipPlan.GroupID);

            ClientInfo = await LG.Data.Clients.ClientService.GetClientInfo(
                GroupInfo.ClientRID);

            Products = await LG.Data.Products.ProductService.GetProductsByAccountID(
                AccountInfo.AccountInfo.AccountID);

            return true;
        }

        public async Task<List<AccountInfoExtended>> LoadDependents()
        {
            DependentAccounts = await LG.Data.Members.AccountService.LoadDependents(AccountInfo.AccountInfo.AccountID);
            return DependentAccounts;
        }

        #endregion

        #region [@   CLINICAL DATA          @]

        public async Task<bool> LoadConsultHealthRecords()
        {
            HealthRecords = new MedicalRecords();
            if (RolodexItemID != 0)
            {
                var allergies = LG.Data.Clinical.ClinicalServices.LoadAllergies(new Allergy()
                {
                    RID = RolodexItemID,
                    Message = "",
                    ClincalAction = ClinicalAction.LoadAll
                });
                var conditions = LG.Data.Clinical.ClinicalServices.LoadConditions(new Condition()
                {
                    RID = RolodexItemID,
                    Message = "",
                    ActionHelper = new ActionHelper { ClincalAction = ClinicalAction.LoadAll }
                });
                var medications = LG.Data.Clinical.ClinicalServices.Medication(new MedicationTaken()
                {
                    RID = RolodexItemID,
                    Message = "",
                    ActionHelper = new ActionHelper { ClincalAction = ClinicalAction.LoadAll }
                });
                var fhistory = LG.Data.Clinical.ClinicalServices.LoadFamilyHistory(new FamilyCondition()
                {
                    RID = RolodexItemID,
                    Message = "",
                    ActionHelper = new ActionHelper { ClincalAction = ClinicalAction.LoadAll }
                });
                var vitals = LG.Data.Clinical.ClinicalServices.VitalReading(new VitalReading()
                {
                    RID = RolodexItemID,
                    Message = "",
                    ActionHelper = new ActionHelper { ClincalAction = ClinicalAction.LoadAll }
                });

                await vitals;
                await fhistory;
                await allergies;
                await conditions;
                await medications;

                HealthRecords.Vitals = vitals.Result.List;
                HealthRecords.Allergies = allergies.Result.List;
                HealthRecords.Conditions = conditions.Result.List;
                HealthRecords.FamilyHistory = fhistory.Result.List;
                HealthRecords.MedicationTaken = medications.Result.List;

                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> LoadConsultHealthRecords(long id)
        {
            var allergies = LG.Data.Clinical.ClinicalServices.LoadAllergies(new Allergy()
            {
                RID = id,
                Message = "",
                ClincalAction = ClinicalAction.LoadAll
            });
            var conditions = LG.Data.Clinical.ClinicalServices.LoadConditions(new Condition()
            {
                RID = id,
                Message = "",
                ActionHelper = new ActionHelper { ClincalAction = ClinicalAction.LoadAll }
            });
            var medications = LG.Data.Clinical.ClinicalServices.Medication(new MedicationTaken()
            {
                RID = id,
                Message = "",
                ActionHelper = new ActionHelper { ClincalAction = ClinicalAction.LoadAll }
            });
            var fhistory = LG.Data.Clinical.ClinicalServices.LoadFamilyHistory(new FamilyCondition()
            {
                RID = id,
                Message = "",
                ActionHelper = new ActionHelper { ClincalAction = ClinicalAction.LoadAll }
            });
            var vitals = LG.Data.Clinical.ClinicalServices.VitalReading(new VitalReading()
            {
                RID = id,
                Message = "",
                ActionHelper = new ActionHelper
                {
                    ClincalAction = ClinicalAction.LoadAll
                }
            });

            await vitals;
            await fhistory;
            await allergies;
            await conditions;
            await medications;

            HealthRecords.Vitals = vitals.Result.List;
            HealthRecords.Allergies = allergies.Result.List;
            HealthRecords.Conditions = conditions.Result.List;
            HealthRecords.FamilyHistory = fhistory.Result.List;
            HealthRecords.MedicationTaken = medications.Result.List;

            return true;
        }

        #endregion

        #region [@   CONSULT DATA           @]

        public async Task<ConsultationHistoryItem> LoadConsult(int id)
        {

            return await LG.Data.Clinical.Disposition.GetDisposition(id);

        }
        public static async Task<ConsultationHistoryItem> LoadConsultDisposition(int Id)
        {
            return await LG.Data.Clinical.Disposition.GetDisposition(Id);
        }

        public async Task<List<LG.Data.Models.Orders.OrderInProcess>> LoadConsultHistory()
        {
            var service = new LG.Data.Orders.OrderDetails();
            var entity = await service.FindItemsInProcess(new LG.Data.Models.Orders.OrderInProcess
            {
                AccountID = AccountInfo.AccountInfo.AccountID,
                ProcessState = ProcessStateEnum.Completed,
                OrderAction = OrderAction.FindConsultationsInProcessState
            });
            var entity2 = await service.FindItemsInProcess(new LG.Data.Models.Orders.OrderInProcess
            {
                AccountID = AccountInfo.AccountInfo.AccountID,
                ProcessState = ProcessStateEnum.Running,
                OrderAction = OrderAction.FindConsultationsInProcessState
            });
            OrderList = new List<LG.Data.Models.Orders.OrderInProcess> { entity, entity2 };
            return OrderList;
        }

        public async Task<AudioCallList> LoadConsultCallHistroy(int consultID)
        {
            return await LG.Data.Clinical.AudioService.GetCallAssociatedWithConsultation(consultID);
        }
        public async Task<LG.Data.Models.Orders.Order> ProcessOrder(LG.Data.Models.Orders.Order entity)
        {
            entity.OrderAction = OrderAction.StartOrder;
            entity.CorporationRID = 10;

            var result = await LG.Data.Orders.OrderService.StartOrder(entity);
            return result;
        }

        protected void SelectConsultData(ConsultationStatusFound item)
        {
            if (item.ConsultationID == SelectedConsultItem.ConsultationID)
            {
                SelectedConsultationFound = item;
            }
        }

        public async Task<MessageConsultation> GetAllMessagesByConsultID(int id)
        {
            SelectedConsultItem
                = await LoadConsult(id);

            OrderList[1].ConsultationsFound.ForEach(
                SelectConsultData);

            return await LG.Data.Doctors.ConsultServicing.GetMessageExchange(new MessageConsultation()
            {
                ConsultationID = id
            });
        }

        public ConsultationHistoryItem SelectedConsultItem { get; set; }
        public ConsultationStatusFound SelectedConsultationFound { get; set; }



        public async Task<MessageConsultation> SendMessage(string message)
        {
            var d = await LG.Data.Doctors.ConsultServicing.SendMessage(
                new MessageConsultation()
                {
                    SendByRID = Identity.RID,
                    PatientRID = Identity.RID,
                    ConsultationID = SelectedConsultItem.ConsultationID,
                    MedicalPractionerRID = SelectedConsultationFound.AssignedToRID,
                    Message = message,
                    EConsultationMessage = message,
                    CorporationRID = 10
                });
            return await GetAllMessages();
        }

        public async Task<MessageConsultation> GetAllMessages()
        {
            var d = await LG.Data.Doctors.ConsultServicing.GetMessageExchange(new MessageConsultation()
            {
                ConsultationID = SelectedConsultItem.ConsultationID
            });
            return d;
        }

        public async Task<List<CreditCardInfo>> GetCreditCards()
        {
            CreditCartList = await LG.Data.Members.AccountService.LoadCreditCards(
                 AccountInfo.AccountInfo.AccountID);
            return CreditCartList;
        }

        #endregion

    }
    public class AppContextUserExtensions {
        public IdentityUser Identity
        {
            get;
            set;
        }
        public PortalType ContextPortal
        {
            get;
            set;
        }
        public AppContextUserExtensions()
        {
            DateTimeCreated = DateTime.Now;
        }
        public bool IsSessionNew { get; set; }
        public DateTime DateTimeCreated { get; }
        public bool IsAuthenticated { get; set; }
        public DateTime DTCreatedInstance { get; set; }

        public static string ModuleName = "AppContextUser";
        public static string ContextID => string.Concat(ModuleName, HttpContext.Current.Session.SessionID);
    }
    public class AppContextUserProperties : AppContextUserExtensions
    {
        public BEntity Entity { get; set; }
        public long RolodexItemID { get; set; }
        public Entity EntityInstance { get; set; }
        public Entity AccountInfoCCInfo { get; set; }
        public List<ProductInfo> Products { get; set; }
        public MedicalRecords HealthRecords { get; set; }
        public ActiveConsultation ActiveConsult { get; set; }
        public Services.EMS.PersonInfo PersonInfo { get; set; }
        public List<CreditCardInfo> CreditCartList { get; set; }
        public LG.Data.Models.Clients.Group GroupInfo { get; set; }
        public LG.Data.Models.Members.Account AccountInfo { get; set; }
        public List<AccountInfoExtended> DependentAccounts { get; set; }
        public LG.Data.Models.Clients.ResponseClient ClientInfo { get; set; }
        public List<LG.Data.Models.Orders.OrderInProcess> OrderList { get; set; }
        public LG.Data.Models.Clients.MembershipPlan MembershipPlan { get; set; }
    }
}