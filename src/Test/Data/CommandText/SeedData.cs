using System;

namespace Test.Data.CommandText;

public static class SeedData
{
    public static readonly string Card_1 =
        @"INSERT INTO Cards(
            Id,
            SetId,
            Name,
            CardNumberInSet,
            InsertName,
            Status)
        VALUES(
            1,
            1, 
            'Ken Griffey Jr.',
            '259',
            'Base',
            5)";

    public static readonly string CardSet_1 =
        @"INSERT INTO CardSets(
            Id,
            Manufacturer,
            SubsetName,
            Year,
            Sport)
        VALUES(
            1,
            'Donruss',
            'Base',
            1990,
            0)";

    public static readonly string CardGrade_1 =
        @"INSERT INTO Grades(
            CardId,
            Value,
            Vender)
        VALUES(
            1,
            10,
            2)";

    public static readonly string SubGrade_1 =
        @"INSERT INTO SubGrades(
            CardId,
            Value,
            Description)
        VALUES(
            1,
            10,
            'Auto')";

    public static readonly string CardDescriptor_1 =
        @"INSERT INTO CardDescriptors(
            CardId,
            Text)
        VALUES(
            1,
            'HOF')";

    public static readonly string CardAssesment_1 =
        @"INSERT INTO CardAssesments(
            Id,
            CardId,
            Date,
            GradeEstimate)
        VALUES(
            1,
            1,
            '2022-02-16 00:00:00',
            10)";

    public static readonly string AssesmentNote_1 =
        @"INSERT INTO AssesmentNotes(
            Id,
            AssesmentId,
            Text)
        VALUES(
            1,
            1,
            '4 sharp corners')";

        public static readonly string AssesmentNote_2 =
        @"INSERT INTO AssesmentNotes(
            Id,
            AssesmentId,
            Text)
        VALUES(
            2,
            1,
            'Sharp edges, no dents or chipping')";

            public static readonly string AssesmentNote_3 =
        @"INSERT INTO AssesmentNotes(
            Id,
            AssesmentId,
            Text)
        VALUES(
            3,
            1,
            'Well centered')";
}