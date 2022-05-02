using Data.Enums;

namespace Data.Models;

public class CardSet
{
    public int Id { get; set; }
    public string Manufacturer { get; set; } = string.Empty;
    public string SubsetName { get; set; } = "Base";
    public int Year { get; set; }
    public Sport Sport { get; set; }
}