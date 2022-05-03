namespace Data.Models;

public class SubGrade
{
    public int Id { get; set; }
    public int CardId { get; set; }
    public double Value { get; set; }
    public string Description { get; set; } = string.Empty;
}