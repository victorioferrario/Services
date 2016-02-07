using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Clients;
using LG.Data.Models.Core;
using LG.Data.Models.Shared;

namespace LG.Data.Models.Identity
{
    public class UserEntityBase : RolodexItemObject,
        IRolodexItemObject
    {
        public UserEntityBase()
            : base()
        {
            Person = new LG.Data.Models.Shared.PersonInfo();
            ListPhones = new List<PhoneBase>();
            ListEmails = new List<EmailAddress>();
            ListAddresses = new List<LG.Data.Models.Shared.Address>();
        }

        public int Id { get; set; }
        public LG.Data.Models.Shared.PersonInfo Person { get; set; }
        public List<PhoneBase> ListPhones { get; set; }
        public List<EmailAddress> ListEmails { get; set; }
        public List<LG.Data.Models.Shared.Address> ListAddresses { get; set; }
    }

    public interface IUserEntity
    {
        int Id { get; set; }
        PersonInfo Person { get; set; }
        List<PhoneBase> ListPhones { get; set; }
        List<EmailAddress> ListEmails { get; set; }
        List<LG.Data.Models.Shared.Address> ListAddresses { get; set; }
        System.Boolean IsActive { get; set; }
        System.Boolean IsTesting { get; set; }
        System.Int64 RolodexItemId { get; set; }
        LG.Data.Models.Enums.RolodexItemType RolodexItemType { get; set; }
    }

    public class UserEntity
        : UserEntityBase, IUserEntity
    {
        public UserEntity()
            : base()
        {
        }
        //public System.String ResultOfAuthGraph { get; set; }
    }
}
