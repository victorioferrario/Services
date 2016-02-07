using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums.Applications;

namespace LG.Data.Models.Applications
{
    public static class MenuItemListData
    {
        internal const string UrlDefault = "Default.aspx";
        internal const string UrlClients = "Clients.aspx";
        internal const string UrlMembers = "Members.aspx";
        internal const string UrlPhysicians = "Physicians.aspx";
        internal const string UrlReports = "Reports.aspx";
        internal const string UrlBilling = "Billing.aspx";
        internal const string UrlUsers = "Users.aspx";
        public static 
            List<MenuItem> DefaultMenuItemList()
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Icon = "fa fa-home",
                    HRef = UrlDefault,
                    Label = "Dashboard",
                    Title = "Welcome",
                    Selected = true,
                    Area = LG.Data.Models.Enums.Applications.AreaEnum.Dashboard,
                    MenuItemType = MenuItemType.Dashboard,
                },
                new MenuItem()
                {
                    Icon = "fa fa-users",
                    HRef = UrlClients,
                    Label = "Clients&nbsp;&&nbsp;Groups",
                    Title = "Clients & Groups",
                    Selected = false,
                    Area = LG.Data.Models.Enums.Applications.AreaEnum.ClientManagement,
                    MenuItemType = MenuItemType.Clients
                },
                new MenuItem()
                {
                    Icon = "fa fa-user-md",
                    HRef = UrlPhysicians,
                    Label = "Physician&nbsp;Management",
                    Title = "Physician Management",
                    Selected = false,
                    Area = LG.Data.Models.Enums.Applications.AreaEnum.HealthServicesProviderManagement,
                    MenuItemType = MenuItemType.Physicians,
                },
                new MenuItem()
                {
                    Icon = "fa fa-user",
                    HRef = UrlMembers,
                    Label = "Member&nbsp;Management",
                    Title = "Member Management",
                    Selected = false,
                    Area = LG.Data.Models.Enums.Applications.AreaEnum.MemberManagement,
                    MenuItemType = MenuItemType.Members
                },                
                new MenuItem()
                {
                    Icon = "fa fa-line-chart",
                    HRef = UrlReports,
                    Label = "Reports&nbsp;Viewer",
                    Title = "Reports Viewer",
                    Selected = false,
                    Area = LG.Data.Models.Enums.Applications.AreaEnum.Reports,
                    MenuItemType = MenuItemType.Reports,
                },
                new MenuItem()
                {
                    Icon = "fa fa-inbox",
                    HRef = UrlBilling,
                    Label = "Billing&nbsp;/&nbsp;Invoicing",
                    Title = "Billing / Invoicing",
                    Selected = false,
                    Area = LG.Data.Models.Enums.Applications.AreaEnum.Accounting,
                    MenuItemType = MenuItemType.Billing,
                },
                new MenuItem()
                {
                    Icon = "fa fa-rocket",
                    HRef = UrlUsers,
                    Label = "User&nbsp;Management",
                    Title = "User Management",
                    Selected = false,
                    Area = LG.Data.Models.Enums.Applications.AreaEnum.UserManagement,
                    MenuItemType = MenuItemType.UserManagement,
                }
            };
        }
    }
}
