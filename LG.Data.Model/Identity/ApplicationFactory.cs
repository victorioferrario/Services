using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Applications;
using LG.Data.Models.Enums.Applications;
using LG.Data.Models.Enums.AuthGraph;
using LG.Data.Models.Identity.Graph;



namespace LG.Data.Models.Identity
{
    public static class ApplicationFactory
    {
        public static List<Application> Load(List<Graph.AccessConfiguration> data)
        {
            return data.Select(
                ApplicationFactoryWorker.GetInstance).ToList();
        }
    }
    internal static class ApplicationFactoryWorker
    {

        internal static Area GetDashboardArea()
        {
            return new Area()
            {
                Name = AreaEnum.Dashboard,
                Administrator = new AreaRole()
                {
                    AccessMode = AccessModeEnum.Granted,
                    AccessType = AccessTypeEnum.DescendantsInheritCurrentPermissions,
                    ActionMode = ActionModeEnum.All,
                    Role = AreaEnum.Dashboard
                },
                User = new AreaRole()
                {
                    AccessMode = AccessModeEnum.Granted,
                    AccessType = AccessTypeEnum.DescendantsInheritCurrentPermissions,
                    ActionMode = ActionModeEnum.All,
                    Role = AreaEnum.Dashboard
                }
            };
        }
        internal static AccessDetails GetSimpleAccess(AccessDetails simple)
        {
            return new AccessDetails()
            {
                AccessMode = simple.AccessMode,
                AccessType = simple.AccessType,
                ActionMode = simple.ActionMode,
                FeatureName = simple.FeatureName,
                Scope = simple.Scope
            };
        }
        internal static AreaRole GetAreaRole(Role acccess)
        {
            if (acccess != null)
            {
                return new AreaRole()
                {
                    Role = acccess.RoleName,
                    AccessMode = acccess.AccessMode,
                    AccessType = acccess.AccessType,
                    ActionMode = acccess.ActionMode
                };
            }
            return null;
        }
        internal static Application GetInstance(AccessConfiguration data)
        {
            var role = data.AccessDetails.FeatureName;
            var appInstance
                = new LG.Data.Models.Identity.Application()
                {
                    AppPortalType = role,
                    Details = new AccessDetails()
                    {
                        AccessMode = data.AccessDetails.AccessMode,
                        FeatureName = data.AccessDetails.FeatureName,
                        AccessType = data.AccessDetails.AccessType,
                        ActionMode = data.AccessDetails.ActionMode,
                        Scope = data.AccessDetails.Scope
                    }
                };
            switch (role)
            {
                case ApplicationPortal.AdministrativeDashboard:
                    #region @   Bind Area
                    appInstance.Areas = new List<Area> {GetDashboardArea()};
                    foreach (var a in data.AccessDetails.AreaList)
                    {
                        var r = new LG.Data.Models.Applications.Area { Name = a.FeatureName };
                        foreach (var a2 in a.Roles)
                        {
                            if (a2.RoleName == LG.Data.Models.Enums.Applications.AreaEnum.User)
                            {
                                r.User = GetAreaRole(a2);
                            }
                            if (a2.RoleName == LG.Data.Models.Enums.Applications.AreaEnum.Administrator)
                            {
                                r.Administrator = GetAreaRole(a2);
                            }
                        }
                        appInstance.Areas.Add(r);
                    }
                    #endregion
                    break;
                case ApplicationPortal.ClientManagement:
                    #region @   Bind Area
                    var clientArea = new Area()
                   {
                       Name = LG.Data.Models.Enums.Applications.AreaEnum.ClientManagement
                   };
                    foreach (var a in data.AccessDetails.AreaList)
                    {
                        if (a.FeatureName == AreaEnum.User)
                        {
                            clientArea.User = new AreaRole()
                            {
                                AccessMode = a.AccessMode,
                                AccessType = a.AccessType,
                                ActionMode = a.ActionMode,
                                Role = a.FeatureName
                            };
                        }
                        if (a.FeatureName == AreaEnum.Administrator)
                        {
                            clientArea.Administrator = new AreaRole()
                            {
                                AccessMode = a.AccessMode,
                                AccessType = a.AccessType,
                                ActionMode = a.ActionMode,
                                Role = a.FeatureName
                            };
                        }
                    }
                    appInstance.Areas.Add(clientArea);
                    //var clientArea = new Area()
                    //{
                    //    Name = GetAreaEnum(data.AccessDetails.FeatureName.ToString())
                    //};
                    //foreach (var a in data.AccessDetails.AreaList)
                    //{
                    //        clientArea.User = GetAreaRole(a.Roles.First(x => x.RoleName.ToString() == "User"));
                    //        clientArea.Administrator = GetAreaRole(a.Roles.First(x => x.RoleName.ToString() == "Administrator"));
                    //}
                    //appInstance.Areas.Add(clientArea);
                    #endregion
                    break;
                case ApplicationPortal.MemberManagement:
                    break;
                case ApplicationPortal.Accounting:
                    break;
                case ApplicationPortal.Reports:
                    break;
                case ApplicationPortal.UserAdministration:
                    break;
                case ApplicationPortal.HealthServicesProviderManagement:
                    break;
            }
            return appInstance;
        }
        internal static AreaEnum GetAreaEnum(string value)
        {
            var t = System.Enum.GetNames(typeof(AreaEnum)).ToList();
            for (var i = 0; i < t.Count() - 1; i++)
            {
                if (t[i] == value)
                {
                    var re = (AreaEnum)System.Enum.Parse(typeof(AreaEnum), value); return re;
                }
            }
            return AreaEnum.Undefined;
        }
        internal static ApplicationPortal GetPortalEnum(string value)
        {
            var t = System.Enum.GetValues(typeof(ApplicationPortal)).Cast<ApplicationPortal>().ToList();
            for (var i = 0; i < t.Count() - 1; i++)
            {
                if (t[i].ToString() == value)
                {
                    return t[i];
                }
            }
            return ApplicationPortal.None;
        }
    }
}
