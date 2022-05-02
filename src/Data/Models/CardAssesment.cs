namespace Data.Models;

public class CardAssesment
{
    public int Id { get; set; }
    public int CardId { get; set; }
    public DateTime Date { get; set; }
    public double GradeEstimate { get; set; }
    public AssesmentNote[]? Notes { get; set; }
}