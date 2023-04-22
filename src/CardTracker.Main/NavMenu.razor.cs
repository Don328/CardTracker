using CardTracker.Main.ViewModels;
using Microsoft.AspNetCore.Components;

namespace CardTracker.Main;

public partial class NavMenu : ComponentBase
{
    public NavMenu()
    {
        MenuItems.Add(new NavMenuItem("/", "Home"));
        MenuItems.Add(new NavMenuItem("/collection", "Collection"));
        MenuItems.Add(new NavMenuItem("/addcard", "Add Card"));
    }

    public List<NavMenuItem> MenuItems { get; set; }
        = new List<NavMenuItem>();
}
