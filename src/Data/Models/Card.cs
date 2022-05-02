using Data.Enums;

namespace Data.Models;

public class Card
{
    public int Id { get; set; }
    public int SetId { get; set; }
    public int GradeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CardNumberInSet { get; set; } = string.Empty;
    public string InsertName { get; set; } = "Base";
    public Grade? Grade { get; set; } = new();
    public CardSet Set { get; set; } = new();
    public CardStatus Status { get; set; }
    public CardDescriptor[]? Descriptors { get; set; }
    public CardAssesment[]? Assesments { get; set; }
}