using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.CMS;

namespace LG.Data.Models.Clients
{
    public class Client
    {
        public Int64 RID { get; set; }
        public string Name { get; set; }
        public System.Boolean IsActive { get; set; }
        public System.Boolean IsTesting { get; set; }
        public System.String WebsiteUrl { get; set; }
        public List<LG.Services.CMS.Phone> PhoneList { get; set; }
        public List<LG.Services.CMS.Address> AddressList { get; set; }
        public List<LG.Services.CMS.EmailAddress> EmailList { get; set; }

    }


    public class EligibilitySettings:BaseModel
    {
        public Int64 ClientRID { get; set; }
        public Guid MessageGuid { get { return Guid.NewGuid(); } }
        public System.Boolean HavingFMEN { get; set; }
        public System.Boolean HavingPMEN { get; set; }
        public System.Boolean IsSendingFMData { get; set; }
        public System.String PropBag { get { return "<PropBag>LG.Data.Models.Clients.EligibilitySettings</PropBag>"; } }

    }

    public class NewClient
    {

        public Int64 RID { get; set; }
        public Contact Contact { get; set; }
        public ClientInstance Client { get; set; }
        public LG.Services.CMS.AddressInput Address { get; set; }

    }

    public class ClientInstance
    {
        public string Name { get; set; }
        public System.Boolean IsActive { get; set; }
        public System.Boolean IsTesting { get; set; }
        public System.String Website { get; set; }
        public System.Boolean HavingFn { get; set; }
        public System.Boolean HavingPn { get; set; }
        public System.Boolean IsSendingFData { get; set; }
        //     HavingFn: boolean;
        //HavingPn: boolean;
        //IsSendingFData: boolean;
    }

    public class Contact
    {
        public String Phone { get; set; }
        public String Fax { get; set; }
        public String Email { get; set; }
    }

    public class MediaItem
    {
        public Int64 RID { get; set; }
        public string Value { get; set; }
    }
    public class BitItem
    {
        public Int64 RID { get; set; }
        public Boolean Value { get; set; }
    }
    public class Address
    {
        public System.Int64 RID { get; set; }
        public int ID { get; set; }
        public System.String AddressLine1 { get; set; }
        public System.String AddressLine2 { get; set; }
        public System.String City { get; set; }
        public System.String State { get; set; }
        public System.String ZipCode { get; set; }
        public System.String CountryCode { get; set; }
        public System.Boolean IsValid { get; set; }
        public System.Boolean IsPrimary { get; set; }
        public System.Int32 AddressUsageEnum { get; set; }
        public LG.Services.CMS.AddressUsageEnum AddressUsageEnumValue { get; set; }
    }

    public class Phone
    {
        public System.Int64 RID { get; set; }
        public int Id { get; set; }
        public System.String PhoneNumber { get; set; }
        public System.String PhoneExtenstion { get; set; }
        public System.String PhoneCountryCode { get; set; }
        public System.Boolean IsValid { get; set; }
        public System.Boolean IsPrimary { get; set; }
        public System.Int32 PhoneUsageEnum { get; set; }
        public System.String PropBag { get { return "<PropBag></PropBag>"; } }
    }

    public class Email
    {
        public System.Int64 RID { get; set; }
        public int Id { get; set; }
        public System.String Value { get; set; }
        public System.Boolean IsValid { get; set; }
        public System.Boolean IsPrimary { get; set; }
        public System.Int32 EmailUsageEnum { get; set; }
        public System.String PropBag { get { return "<PropBag></PropBag>"; } }
    }
    public class UpdateClient
    {

        public string Name { get; set; }
        public Int64 RID { get; set; }
        public System.Boolean IsActive { get; set; }
        public System.Boolean IsTesting { get; set; }

        public System.String WebsiteURL { get; set; }

        public System.String PathToLogoImage { get; set; }
    }

    public class ResponseClient : LG.Data.Models.BaseModel
    {
        public ResponseClient()
            : base()
        {

        }

        public GetClientInfoResponse ServiceResponse { get; set; }
    }

    public class AddressTransport
    {
        public LG.Services.CMS.ReturnStatus Status { get; set; }
        public List<LG.Services.CMS.Address> List { get; set; }
    }
}
