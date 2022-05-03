namespace Data.CommandText;

public static class ReadTable
{
    public static readonly string Cards =
        @"SELECT
        Id, 
        SetId, 
        Name, 
        CardNumberInSet,
        InsertName, 
        Status
        FROM Cards";

    public static readonly string CardSets =
        @"SELECT
        Id,
        Manufacturer,
        SubsetName,
        Year,
        Sport
        FROM CardSets";

    public static readonly string Grades =
        @"SELECT
        CardId,
        Value,
        Vender
        FROM Grades";

    public static readonly string SubGrades =
        @"SELECT
        CardId,
        Value,
        Description
        FROM SubGrades";

    public static readonly string CardDescriptors =
        @"SELECT
        CardId,
        Text
        FROM CardDescriptors";

    public static readonly string CardAssesments =
        @"SELECT
        Id,
        CardId,
        Date,
        GradeEstimate
        From CardAssesments";
    
    public static readonly string AssesmentNotes =
        @"SELECT
        Id,
        AssesmentId,
        Text
        FROM AssesmentNotes";

    public static readonly string CardCommands =
        @"SELECT
        Id,
        PartType,
        Count,
        Command
        FROM CardCommands
        ORDER BY Id";
}