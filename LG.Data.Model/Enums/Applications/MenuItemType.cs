using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Enums.Applications
{
    public enum MenuItemType
    {
        None = 0,
        Dashboard = 1,
        Clients = 2,
        Members = 3,
        Physicians = 4,
        Reports = 5,
        Billing = 6,
        UserManagement = 7,
        Extra = 8,
    }

    public enum GroupMenuType
    {
        None = 0,
        Welcome = 1,
        Client = 2,
        Member = 3,
        Practioner = 4,
        Administative = 5,
    }
}
