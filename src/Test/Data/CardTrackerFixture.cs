using System;
using System.Collections.Generic;
using System.IO;
using Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Data.CommandText;

namespace Test.Data;

public class CardTrackerFixture
{
    const string ConnStrKey = "ConnectionString";
    const string FallbackConnStr = "Data Source=:memory:";

    static Dictionary<string, string> Config { get;} =
        new Dictionary<string, string>()
        {
            [ConnStrKey] = FallbackConnStr
        };

    public IServiceProvider Services { get; private set; }

    public CardTrackerFixture()
    {
        var configBuilder = new ConfigurationBuilder();
        configBuilder
            .AddInMemoryCollection(Config)
            .AddJsonFile("config.json", true);
        var configRoot = configBuilder.Build();
        var connStr = configRoot[ConnStrKey];
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<ICardTrackerContext>(provider => {
            var conn = new SqliteConnection(connStr);
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
                
            return new SqliteSchemaContext(conn);
        });

        Services = serviceCollection.BuildServiceProvider();
    }
}