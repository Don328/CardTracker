using Data;
using Data.Enums;
using Data.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Test.Data;

public class Seed : IClassFixture<CardTrackerFixture>
{
    private CardTrackerFixture fixture;
    private CardTrackerContext context;

    public Seed(CardTrackerFixture fixture)
    {
        this.fixture = fixture;
        this.context = new 
            CardTrackerContext(fixture.Connection);
        
    }

    [Fact]
    public void TestSets()
    {
        var sets = context.Sets;
        var set = sets.First();

        Assert.Equal(1, sets.Count());
        Assert.Equal(1, set.Id);
        Assert.Equal("Donruss", set.Manufacturer);
        Assert.Equal("Base", set.SubsetName);
        Assert.Equal(1990, set.Year);
        Assert.Equal(Sport.Baseball, set.Sport);
    }

    [Fact]
    public void TestSubGrades()
    {
        var subGrades = context.SubGrades;
        var subGrade = subGrades.First();

        Assert.Equal(1, subGrades.Count());
        Assert.Equal(1, subGrade.CardId);
        Assert.Equal(10, subGrade.Value);
        Assert.Equal("Auto", subGrade.Description);
    }

    [Fact]
    public void TestGrades()
    {
        var grades = context.Grades;
        var grade = grades.First();

        Assert.Equal(1, grades.Count());
        Assert.Equal(1, grade.CardId);
        Assert.Equal(10, grade.Value);
        Assert.Equal(GradingVender.PSA, grade.Vender);
        Assert.Equal(1, grade.SubGrades?[0].CardId);
        Assert.Equal(10, grade.SubGrades?[0].Value);
        Assert.Equal("Auto", grade.SubGrades?[0].Description);
    }

    [Fact]
    public void TestDescriptors()
    {
        var descriptors = context.CardDescriptors;
        var descriptor = descriptors.First();

        Assert.Equal(1, descriptors.Count());
        Assert.Equal(1, descriptor.CardId);
        Assert.Equal("HOF", descriptor.Text);
    }

    [Fact]
    public void TestAssesments()
    {
        var assesments = context.Assesments;
        var assesment = assesments.First();

        Assert.Equal(1, assesments.Count());
        Assert.Equal(1, assesment.Id);
        Assert.Equal(1, assesment.CardId);
        Assert.Equal("2022-02-16", assesment.Date.ToString("yyyy-MM-dd"));
        Assert.Equal(10.0, assesment.GradeEstimate);
        Assert.Equal(3, assesment.Notes?.Count());
    }

    [Fact]
    public void TestAssesmentNotes()
    {
        var assesmentNotes = context.AssesmentNotes;
        Assert.Equal(3, assesmentNotes.Count());
    }

    [Fact]
    public void TestCards()
    {
        var cards = context.Cards;
        var card = cards.First();

        Assert.Equal(1, cards.Count());

        // Test Add Card

        Assert.Equal(1, card.Id);
        Assert.Equal(1, card.SetId);
        Assert.Equal("Ken Griffey Jr.", card.Name);
        Assert.Equal("259", card.CardNumberInSet);
        Assert.Equal("Base", card.InsertName);
        Assert.Equal(CardStatus.Graded, card.Status);

        // Test Card Set data
        Assert.Equal(1, card.Set.Id);
        Assert.Equal("Donruss", card.Set.Manufacturer);
        Assert.Equal("Base", card.Set.SubsetName);
        Assert.Equal(1990, card.Set.Year);
        Assert.Equal(Sport.Baseball, card.Set.Sport);

        // Test Card Grade data
        Assert.NotNull(card.Grade);
        Assert.Equal(1, card.Grade?.CardId);
        Assert.Equal(10, card.Grade?.Value);
        Assert.Equal(GradingVender.PSA, card.Grade?.Vender);
 
        // Test Card SubGrade data
        Assert.Equal(1, card.Grade?.SubGrades?[0].CardId);
        Assert.Equal(10, card.Grade?.SubGrades?[0].Value);
        Assert.Equal("Auto", card.Grade?.SubGrades?[0].Description);
        
        // Test Card Descriptors
        Assert.Equal(1, card.Descriptors?[0].CardId);
        Assert.Equal("HOF", card.Descriptors?[0].Text);
        
        // Test Card Assesments
        Assert.Equal(1, card.Assesments?[0].Id);
        Assert.Equal(1, card.Assesments?[0].CardId);
        Assert.Equal("2022-02-16", card.Assesments?[0].Date.ToString("yyyy-MM-dd"));
        Assert.Equal(10.0, card.Assesments?[0].GradeEstimate);
        
        // Test Card Assesment Notes
        Assert.Equal(3, card.Assesments?[0].Notes?.Count());

        // Test Create Card
        context.CreateCard(new Card(){
            SetId = 1,
            Name = "Bo Jackson",
            CardNumberInSet = "150",
            Status = CardStatus.Pending
        });

        cards = context.Cards;

        Assert.Equal(2, cards.Count());
        var card2 = cards.Where(c => c.Id == 2).First();
        Assert.Equal("Bo Jackson", card2.Name);
        Assert.Equal(CardStatus.Pending, card2.Status);
        
        // Test Update Card Status
        context.UpdateCardStatus(2, CardStatus.Rejected);
        cards = context.Cards;
        card2 = cards.Where(c => c.Id == 2).First();
        Assert.Equal(CardStatus.Rejected, card2.Status);

        // Test Delete Card
        context.DeleteCard(2);
        cards = context.Cards;
        Assert.Equal(1, cards.Count());
    }
}