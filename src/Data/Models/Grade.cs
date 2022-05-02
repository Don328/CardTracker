using Data.Enums;

namespace Data.Models;

public class Grade
{
    public int CardId { get; set; }
    public double Value { get; set; }
    public GradingVender Vender { get; set; }
    public SubGrade[]? SubGrades { get; set; }
}