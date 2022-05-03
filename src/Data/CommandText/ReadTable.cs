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
        Id,
        CardId,
        Value,
        Description
        FROM SubGrades";

    public static readonly string CardDescriptors =
        @"SELECT
        Id,
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
        CardId,
        AssesmentId,
        Text
        FROM AssesmentNotes";
}