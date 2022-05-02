using System;
using Microsoft.Data.Sqlite;
using Test.Data.CommandText;

namespace Test.Data;

public class CardTrackerFixture : IDisposable
{
    public SqliteConnection Connection { get; private set; }

    public CardTrackerFixture()
    {
        var conn = new SqliteConnection("Data Source=:memory:");
        Connection = conn;
        conn.Open();
        
        (new SqliteCommand(CreateTable.Cards, conn)).ExecuteNonQuery();
        (new SqliteCommand(CreateTable.CardSets, conn)).ExecuteNonQuery();
        (new SqliteCommand(CreateTable.Grades, conn)).ExecuteNonQuery();
        (new SqliteCommand(CreateTable.SubGrades, conn)).ExecuteNonQuery();
        (new SqliteCommand(CreateTable.CardDescriptors, conn)).ExecuteNonQuery();
        (new SqliteCommand(CreateTable.CardAssesments, conn)).ExecuteNonQuery();
        (new SqliteCommand(CreateTable.AssesmentNotes, conn)).ExecuteNonQuery();
        
        (new SqliteCommand(SeedData.Card_1, conn)).ExecuteNonQuery();
        (new SqliteCommand(SeedData.CardSet_1, conn)).ExecuteNonQuery();
        (new SqliteCommand(SeedData.CardGrade_1, conn)).ExecuteNonQuery();
        (new SqliteCommand(SeedData.SubGrade_1, conn)).ExecuteNonQuery();
        (new SqliteCommand(SeedData.CardDescriptor_1, conn)).ExecuteNonQuery();
        (new SqliteCommand(SeedData.CardAssesment_1, conn)).ExecuteNonQuery();
        (new SqliteCommand(SeedData.AssesmentNote_1, conn)).ExecuteNonQuery();
        (new SqliteCommand(SeedData.AssesmentNote_2, conn)).ExecuteNonQuery();
        (new SqliteCommand(SeedData.AssesmentNote_3, conn)).ExecuteNonQuery();
    }



    public void Dispose()
    {
        if (Connection != null)
        {
            Connection.Dispose();
        }
    }
}