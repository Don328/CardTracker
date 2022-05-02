namespace Test.Data.CommandText;

public static class CreateTable
{
    public static readonly string Cards =
        @"CREATE TABLE Cards (
            Id                  INTEGER PRIMARY KEY,
            SetId               INTEGER NOT NULL,
            Name                VARCHAR(255) NOT NULL,
            CardNumberInSet     VARCHAR(255),
            InsertName          VARCHAR(255),
            Status              INTEGER)";

    public static readonly string CardSets =
        @"CREATE TABLE CardSets (
            Id              INTEGER PRIMARY KEY,
            Manufacturer    VARCHAR(255),
            SubsetName      VARCHAR(255),
            Year            INTEGER NOT NULL,
            Sport           INTEGER)";

    public static readonly string Grades =
        @"CREATE TABLE Grades (
            CardId      INTEGER PRIMARY KEY,
            Value       REAL NOT NULL,
            Vender      INTEGER NOT NULL)";

    public static readonly string SubGrades =
        @"CREATE TABLE SubGrades (
            CardId          INTEGER PRIMARY KEY,
            Value           REAL NOT NULL,
            Description     VARCHAR(255) NOT NULL)";

    public static readonly string CardDescriptors =
        @"CREATE TABLE CardDescriptors (
            CardId      INTEGER PRIMARY KEY,
            Text        VARCHAR(255) NOT NULL)";

    public static readonly string CardAssesments =
        @"CREATE TABLE CardAssesments (
            Id                  INTEGER PRIMARY KEY,
            CardId              INTEGER NOT NULL,
            Date                DATE NOT NULL,           
            GradeEstimate       REAL NOT NULL)";

    public static readonly string AssesmentNotes =
        @"CREATE TABLE AssesmentNotes (
            Id                  INTEGER PRIMARY KEY,
            AssesmentId         INTEGER NOT NULL,
            Text                VARCHAR NOT NULL)";

}