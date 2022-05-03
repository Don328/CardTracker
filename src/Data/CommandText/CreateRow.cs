namespace Data.CommandText;

public static class CreateRow
{
    public static readonly string Card =
        @"INSERT INTO Cards(
            SetId, Name, CardNumberInSet,
            InsertName, Status)
        VALUES(
            @setId, @name, @cardNumberInSet,
            @insertName, @status);
        SELECT last_insert_rowid();";

    public static readonly string Set =
        @"INSERT INTO CardSets(
            Manufacturer, SubsetName,
            Year, Sport)
        VALUES(
            @manufacturer, @subsetName,
            @year, @sport);
        SELECT last_insert_rowid();";

    public static readonly string Grade =
        @"INSERT INTO Grades(
            CardId, Value, Vender)
        VALUES(
            @cardId, @value, @vender);";

    public static readonly string SubGrade =
        @"INSERT INTO SubGrades(
            CardId, Value, Description)
        VALUES(
            @cardId, @value, @description);
        SELECT last_insert_rowid();";

    public static readonly string Descriptor =
        @"INSERT INTO CardDescriptors(
            CardId, Text)
        VALUES(
            @cardId, @text);
        SELECT last_insert_rowid();";


    public static readonly string Assesment =
        @"INSERT INTO CardAssesments(
            CardId, Date, GradeEstimate)
        VALUES(
            @cardId, @date, @gradeEstimate);
        SELECT last_insert_rowid();";

    public static readonly string Note =
        @"INSERT INTO AssesmentNotes(
            AssesmentId, CardId, Text)
        VALUES(
            @assesmentId, @cardId, @text);
        SELECT last_insert_rowid();";
}