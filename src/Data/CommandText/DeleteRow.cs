namespace Data.CommandText;

public static class DeleteRow
{
    public static readonly string Card =
        @"DELETE FROM Cards
        WHERE Id=@id";

    public static readonly string Set =
        @"DELETE FROM CardSets
        WHERE Id=@id";

    public static readonly string Grade =
        @"DELETE FROM Grades
        WHERE CardId=@cardId";

    public static readonly string SubGrade =
        @"DELETE FROM SubGrades
        WHERE Id=@id";

    public static readonly string Descriptor =
        @"DELETE FROM CardDescriptors
        WHERE Id=@id";

    public static readonly string Assesment =
        @"DELETE FROM CardAssesments
        WHERE Id=@id";

    public static readonly string Note =
        @"DELETE FROM AssesmentNotes
        WHERE Id=@id";
}