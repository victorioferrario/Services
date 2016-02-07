using System;
namespace LG.Data.Models.Applications
{
    interface IMenuItem
    {
        LG.Data.Models.Enums.Applications.AreaEnum Area { get; set; }
        string HRef { get; set; }
        string Icon { get; set; }
        string Label { get; set; }
        LG.Data.Models.Enums.Applications.MenuItemType MenuItemType { get; set; }
        bool? Selected { get; set; }
        string Title { get; set; }
    }
}
