using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.UMS;

namespace LG.Data.Models.Users
{
    public class UserListModel : BaseModel
    {
        public UserListModel() : base() { }
        public List<UserInfo> List { get; set; }
    }
}
