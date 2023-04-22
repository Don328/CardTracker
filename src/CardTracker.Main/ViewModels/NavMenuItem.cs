namespace CardTracker.Main.ViewModels;

public record NavMenuItem(
    string Href, 
    string Text, 
    string CssClass = "nav-menu-item");
