using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.Data.Models.Applications
{
    public class MenuItem : LG.Data.Models.Applications.IMenuItem
    {
        public String Icon { get; set; }
        public String HRef { get; set; }
        public String Label { get; set; }
        public String Title { get; set; }
        public System.Boolean? Selected { get; set; }
        public LG.Data.Models.Enums.Applications.AreaEnum Area { get; set; }
        public LG.Data.Models.Enums.Applications.MenuItemType MenuItemType { get; set; }
    }

    public class MenuItemList : List<MenuItem>
    {
        public List<MenuItem> Items { get; set; }

        public MenuItemList()
            : base()
        {
            Items = new List<MenuItem>();
        }
        public MenuItem SelectedItem
        {
            get { return Items.FirstOrDefault(item => item.Selected.HasValue && item.Selected.Value); }

        }
    }
}
