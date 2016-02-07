using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Applications
{
    public class Navigation
    {
        public PageManagement PageManager { get; set; }

        public Navigation()
        {
            this.PageManager = new PageManagement();
            this._Default = LG.Data.Models.Applications.MenuItemListData.DefaultMenuItemList();
        }

        public MenuItemList NavList
        {
            get
            {
                return this.PageManager.NavList;
            }
        }

        private List<MenuItem> _Default;
        public List<MenuItem> Default
        {
            get { return this._Default; }
        }


        public System.Boolean Select(LG.Data.Models.Enums.Applications.MenuItemType page)
        {
            this.PageManager.CurrentPage = page;
            return this.PageManager.Select(true);
        }


    }
    public class PageManagement
    {
        public PageManagement()
        {
            this.NavList = new MenuItemList();

        }
        public MenuItem SelectedItem { get; set; }
        public MenuItem PreviousItem { get; set; }
        public System.Boolean Select(Boolean reset)
        {
            if (this.NavList.SelectedItem != null)
            {
                if (this.NavList.SelectedItem.Selected.HasValue) this.NavList.SelectedItem.Selected = false;
            }
            foreach (var t in this.NavList.Items.Where(t => t.MenuItemType == CurrentPage))
            {
                t.Selected = true; SelectedItem = t;
            }
            return true;
        }


        public LG.Data.Models.Enums.Applications.MenuItemType CurrentPage { get; set; }

       // public LG.Data.Models.Applications.MenuItemListData
        public LG.Data.Models.Applications.MenuItemList NavList { get; set; }
        public System.Boolean Populate(LG.Data.Models.Identity.Application applicationInstance)
        {
            this.NavList = new MenuItemList();
            var defaultMenu = LG.Data.Models.Applications.MenuItemListData.DefaultMenuItemList();
            foreach (var area in applicationInstance.Areas)
            {
                this.NavList.Items.Add(defaultMenu.Find(x => x.Area == area.Name));
            }
            return true;
        }
    }
}
