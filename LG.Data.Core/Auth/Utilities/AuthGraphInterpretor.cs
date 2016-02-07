using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using LG.Data.Models.Enums.Applications;
using LG.Data.Models.Enums.AuthGraph;
using LG.Data.Models.Identity.Graph;
using LG.UX.Identity.Utilities.AuthGraph;


namespace LG.Data.Core.Auth.Utilities
{
    public class Interpretor
    {
        /// <summary>
        /// Each set of user access is defined by this node.
        /// </summary>
        /// <returns>Collection=>LG.Entities.Users.AccessConfiguration</returns>
        public static List<AccessConfiguration> LoadData(String data)
        {
            var root = XElement.Parse(HttpUtility.HtmlDecode(data));
            var rid = root.Attribute(AccessConfigurationXNames.XrId);
            var c1 = from el in root.Elements(AccessConfigurationXNames.XAccessConfiguration)
                     select el;
            return c1.Select(el => new AccessConfiguration()
            {
                RId = Convert.ToInt64(rid.Value),
                RoleId = Convert.ToInt32(el.Attribute(AccessConfigurationXNames.XRoleId).Value),
                RoleName = el.Attribute(AccessConfigurationXNames.XRoleName).Value,
                AccessDetails = LoadDetails(el.Elements().FirstOrDefault())
            }).ToList();
        }
        /// <summary>
        /// Under each AccessConfiguration section
        /// there are a list of persmissions defined by Access Node.
        /// </summary>
        /// <param name="child"></param>
        /// <returns>LG.Entities.Users.AccessDetails</returns>
        internal static AccessDetails LoadDetails(XElement child)
        {
            var result = new AccessDetails()
            {
                AccessMode = GetAccessModeEnum(child.Attribute(AccessXNames.XAccessMode).Value),
                AccessType = GetAccessTypeEnum(child.Attribute(AccessXNames.XAccessType).Value),
                ActionMode = GetActionModeEnum(child.Attribute(AccessXNames.XActionMode).Value),
                FeatureName = GetFeatureApplicationPortal(child.Attribute(AccessXNames.XFeatureName).Value),
                Scope = GetScope(child),
                AreaList = GetAccessTag(child)
            };
            return result;
        }

        #region [@  ACCESS PARSING          @]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <returns>LG.Entities.Users.AuthGrap.Access</returns>
        internal static Access ParseAccessTag(XElement child)
        {
            var result = new Access()
            {
                AccessMode = GetAccessModeEnum(child.Attribute(AccessXNames.XAccessMode).Value),
                AccessType = GetAccessTypeEnum(child.Attribute(AccessXNames.XAccessType).Value),
                ActionMode = GetActionModeEnum(child.Attribute(AccessXNames.XActionMode).Value),
                FeatureName = GetAreaEnum(child.Attribute(AccessXNames.XFeatureName).Value),
                Roles = GetRoleTag(child),
                Scope = GetScope2(child)
            };
            return result;
        }
        /// <summary>
        /// This method parses through the 
        /// individual settings of Access Details Node
        /// </summary>
        /// <param name="child">XElement</param>
        /// <returns>Collection=>LG.Entities.Users.AuthGraph.Access</returns>
        internal static List<Access> GetAccessTag(XElement child)
        {
            return (
                from grandChild
                    in child.Elements()
                where grandChild.Name == "Access"
                select ParseAccessTag(grandChild)).ToList();
        }
        #endregion
        #region [@  ROLE PARSING            @]
        /// <summary>
        /// Parses out the individual roles.
        /// </summary>
        /// <param name="child"></param>
        /// <returns>LG.Entities.Users.AuthGraph.Role</returns>
        internal static Role ParseRoleTag(XElement child)
        {
            var result = new Role()
            {
                AccessMode = GetAccessModeEnum(child.Attribute(AccessXNames.XAccessMode).Value),
                AccessType = GetAccessTypeEnum(child.Attribute(AccessXNames.XAccessType).Value),
                ActionMode = GetActionModeEnum(child.Attribute(AccessXNames.XActionMode).Value),
                RoleName = GetAreaEnum(child.Attribute(AccessXNames.XFeatureName).Value)
            };
            // TODO: Adjust when lower level functionality gets exposed.
            // ,Roles = GetAccessTag(child)
            return result;
        }
        /// <summary>
        /// Returns a collection of Roles for Access
        /// </summary>
        /// <param name="child"></param>
        /// <returns>Collection=>LG.Entities.Users.AuthGraph.Role</returns>
        internal static List<Role> GetRoleTag(XElement child)
        {
            return (from grandChild in child.Elements()
                    where grandChild.Name == "Access"
                    select ParseRoleTag(grandChild)).ToList();
        }
        #endregion
        #region [@  SCOPE PARSING           @]
        internal static AccessScope GetScope(XElement child)
        {
            var item = child;
            return (from grandChild in child.Elements()
                    where grandChild.Name == "Scope"
                    select ParseScopeTag(grandChild)).FirstOrDefault();
        }
        internal static AccessScope GetScope2(XElement child)
        {
            return (from grandChild in child.Elements()
                    where grandChild.Name == "Scope"
                    select ParseScopeTag2(grandChild)).FirstOrDefault();
        }
        internal static AccessScope ParseScopeTag(XElement child)
        {
            var result = new AccessScope()
            {
                RId = Convert.ToInt64(child.Value),
                ScopeType = child.Attribute(ScopeXNames.XscopeType).Value,
                Inherit = child.Attribute(ScopeXNames.XisInherit).Value,
                EntityType = new AccessEntity()
                {
                    Id = Convert.ToInt32(child.Attribute(ScopeXNames.XentityTypeId).Value),
                    Name = child.Attribute(ScopeXNames.XentityTypeName).Value
                }
            };
            return result;
        }
        internal static AccessScope ParseScopeTag2(XElement child)
        {
            return new AccessScope()
            {
                Inherit = child.Attribute(
                ScopeXNames.XisInherit).Value
            };
        }
        #endregion
        #region [@  ENUM PARSING            @]
        ///<summary>
        /// This Section has all the Enumeration parse methods.
        /// </summary>
        /// <summary>
        /// This is a method Defines what aspect of Global User
        /// </summary>
        /// <param name="value"></param>
        /// <returns>LG.Entities.Enum.AuthGraph.ApplicationPortal</returns>
        internal static ApplicationPortal
            GetFeatureApplicationPortal(String value)
        {
            return (ApplicationPortal)System.Enum.Parse(typeof(ApplicationPortal), value, true);
        }
        /// <summary>
        /// Will be used for navigation
        /// </summary>
        /// <param name="value"></param>
        /// <returns>LG.Entities.Enum.AuthGraph.AreaEnum</returns>
        internal static AreaEnum
           GetAreaEnum(String value)
        {
            return (AreaEnum)System.Enum.Parse(typeof(AreaEnum), value, true);
        }
        /// <summary>
        /// Controls the actions of user.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>LG.Entities.Enum.AuthGraph.ActionModeEnum</returns>
        internal static ActionModeEnum
            GetActionModeEnum(String value)
        {
            return (ActionModeEnum)System.Enum.Parse(typeof(ActionModeEnum), value, true);
        }
        /// <summary>
        /// Controls the Access Mode of user.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>LG.Entities.Enum.AuthGraph.AccessMode</returns>
        internal static AccessModeEnum
           GetAccessModeEnum(String value)
        {
            return (AccessModeEnum)System.Enum.Parse(typeof(AccessModeEnum), value, true);
        }
        /// <summary>
        /// Controls the Access Type of user.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>LG.Entities.Enum.AuthGraph.AccessTypeEnum</returns>
        internal static AccessTypeEnum
           GetAccessTypeEnum(String value)
        {
            return (AccessTypeEnum)System.Enum.Parse(typeof(AccessTypeEnum), value, true);
        }

        #endregion
        #region [@  STATIC STRINGS          @]
        //ParseChildElements(el.Elements().FirstOrDefault())
        private static String authgraphString =
         @"&lt;AuthGraph RID='20006'&gt;&lt;AccessConfiguration roleID='2' roleName='CorpAdmin'&gt;&lt;Access featureName='AdministrativeDashboard' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='0' entityTypeID='2' entityTypeName='RType_Corporation' scopeType='Selected'&gt;&lt;RID&gt;4&lt;/RID&gt;&lt;/Scope&gt;&lt;Access featureName='ClientManagement' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;Access featureName='Administrator' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;/Access&gt;&lt;Access featureName='User' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;/Access&gt;&lt;/Access&gt;&lt;Access featureName='MemberManagement' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;Access featureName='Administrator' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;/Access&gt;&lt;Access featureName='User' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;/Access&gt;&lt;/Access&gt;&lt;Access featureName='HealthServicesProviderManagement' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;Access featureName='Administrator' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;/Access&gt;&lt;Access featureName='User' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;/Access&gt;&lt;/Access&gt;&lt;Access featureName='UserManagement' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;Access featureName='Administrator' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;/Access&gt;&lt;Access featureName='User' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;/Access&gt;&lt;/Access&gt;&lt;Access featureName='Reports' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;Access featureName='Administrator' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;/Access&gt;&lt;Access featureName='User' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;/Access&gt;&lt;/Access&gt;&lt;Access featureName='Accounting' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;Access featureName='Administrator' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;/Access&gt;&lt;Access featureName='User' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='1' /&gt;&lt;/Access&gt;&lt;/Access&gt;&lt;/Access&gt;&lt;/AccessConfiguration&gt;&lt;AccessConfiguration roleID='4' roleName='Client'&gt;&lt;Access featureName='ClientManagement' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All'&gt;&lt;Scope isInherit='0' entityTypeID='5' entityTypeName='RType_Client' scopeType='Selected'&gt;&lt;RID&gt;20007&lt;/RID&gt;&lt;/Scope&gt;&lt;Access featureName='User' accessMode='Granted' accessType='DescendantsInheritCurrentPermissions' actionMode='All' /&gt;&lt;/Access&gt;&lt;/AccessConfiguration&gt;&lt;/AuthGraph&gt;";
        #endregion


         }
}
