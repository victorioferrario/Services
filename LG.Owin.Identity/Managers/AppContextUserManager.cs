using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LG.Owin.Identity.Managers
{
    public class AppContextUserManager : Component
    {
        public static string ModuleName = "AppContextUserManager";
        public int Count => this.Items.Count;

        public static string InstanceID
        {
            get
            {
                var str = string.Concat(
                    AppContextUserManager.ModuleName,
                    HttpContext.Current.Session.SessionID); return str;
            }
        }

        public Collection<AppContextUser> Items { get; private set; }

        static AppContextUserManager()
        {
            AppContextUserManager.ModuleName = "AppContextUserManager";
        }
        AppContextUserManager()
        {
            this.Items = new Collection<AppContextUser>();
        }

        public bool Add(AppContextUser item)
        {
            this.Items.Add(item);
            return true;
        }

        public bool Clear()
        {
            for (var i = 0; i < this.Items.Count; i++)
            {
                this.Items.RemoveAt(i);
            }
            return true;
        }

        public static AppContextUserManager GetCurrentSingleton()
        {
            AppContextUserManager oSingleton;
            if (null != HttpContext.Current.Session.Contents[AppContextUserManager.InstanceID])
            {
                oSingleton = (AppContextUserManager)HttpContext.Current.Session.Contents[AppContextUserManager.InstanceID];
            }
            else
            {
                oSingleton = new AppContextUserManager();
                HttpContext.Current.Session.Contents[AppContextUserManager.InstanceID] = oSingleton;
            }
            return oSingleton;
        }
    }
}
