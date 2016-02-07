using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LG.Owin.Identity.Core;

namespace LG.Owin.Identity
{
    public class AppContextUser : AppContextUserBase
    {
         static volatile AppContextUser instance;
         static readonly object syncRoot = new Object();

        AppContextUser() : base()
        {
            this.IsSessionNew = HttpContext.Current.Session.IsNewSession;
        }

        public static AppContextUser GetCurrentSingleton()
        {
            if (null == HttpContext.Current.Session.Contents[InstanceId])
            {
                lock (syncRoot)
                {
                    instance = new AppContextUser();
                }
                using (var manager = LG.Owin.Identity.Managers.AppContextUserManager.GetCurrentSingleton())
                {
                    HttpContext.Current.Session.Contents[ContextID] = instance;
                    manager.Add(instance);
                }
            }
            else
            {
                instance = (AppContextUser)HttpContext.Current.Session.Contents[InstanceId];
            }
            return instance;
        }
        public static string InstanceId => (ModuleName + HttpContext.Current.Session.SessionID);
    }
}
