namespace Data.Models;

public class AssesmentNote
{
    public int Id { get; set; }
    public int CardId { get; set; }
    public int AssesmentId { get; set; }
    public string Text { get; set; } = string.Empty;
}