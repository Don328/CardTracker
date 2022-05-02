using Data;
using Data.Enums;
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
    public void Start()
    {
        var sets = context.Sets;
        Assert.Equal(1, sets.Count());
        
        var set = sets.First();
        Assert.Equal(1, set.Id);
        Assert.Equal("Donruss", set.Manufacturer);
        Assert.Equal("Base", set.SubsetName);
        Assert.Equal(1990, set.Year);
        Assert.Equal(Sport.Baseball, set.Sport);

        var subGrades = context.SubGrades;
        Assert.Equal(1, subGrades.Count());

        var subGrade = subGrades.First();
        Assert.Equal(1, subGrade.CardId);
        Assert.Equal(10, subGrade.Value);
        Assert.Equal("Auto", subGrade.Description);

        var grades = context.Grades;
        Assert.Equal(1, grades.Count());

        var grade = grades.First();
        Assert.Equal(1, grade.CardId);
        Assert.Equal(10, grade.Value);
        Assert.Equal(GradingVender.PSA, grade.Vender);
        Assert.Equal(1, grade.SubGrades?[0].CardId);
        Assert.Equal(10, grade.SubGrades?[0].Value);
        Assert.Equal("Auto", grade.SubGrades?[0].Description);

        var descriptors = context.CardDescriptors;
        Assert.Equal(1, descriptors.Count());

        var descriptor = descriptors.First();
        Assert.Equal(1, descriptor.CardId);
        Assert.Equal("HOF", descriptor.Text);

        var assesmentNotes = context.AssesmentNotes;
        Assert.Equal(3, assesmentNotes.Count());

        var assesments = context.Assesments;
        Assert.Equal(1, assesments.Count());

        var assesment = assesments.First();
        Assert.Equal(1, assesment.Id);
        Assert.Equal(1, assesment.CardId);
        Assert.Equal("2022-02-16", assesment.Date.ToString("yyyy-MM-dd"));
        Assert.Equal(10.0, assesment.GradeEstimate);
        Assert.Equal(3, assesment.Notes?.Count());

        var cards = context.Cards;
        Assert.Equal(1, cards.Count());

        var card = cards.First();
        Assert.Equal(1, card.Id);
        Assert.Equal(1, card.SetId);
        Assert.Equal("Ken Griffey Jr.", card.Name);
        Assert.Equal("259", card.CardNumberInSet);
        Assert.Equal("Base", card.InsertName);
        Assert.Equal(CardStatus.Graded, card.Status);
        Assert.Equal(1, card.Set.Id);
        Assert.Equal("Donruss", card.Set.Manufacturer);
        Assert.Equal("Base", card.Set.SubsetName);
        Assert.Equal(1990, card.Set.Year);
        Assert.Equal(Sport.Baseball, card.Set.Sport);
        Assert.NotNull(card.Grade);
        Assert.Equal(1, card.Grade?.CardId);
        Assert.Equal(10, card.Grade?.Value);
        Assert.Equal(GradingVender.PSA, card.Grade?.Vender);
        Assert.Equal(1, card.Grade?.SubGrades?[0].CardId);
        Assert.Equal(10, card.Grade?.SubGrades?[0].Value);
        Assert.Equal("Auto", card.Grade?.SubGrades?[0].Description);
        Assert.Equal(1, card.Descriptors?[0].CardId);
        Assert.Equal("HOF", card.Descriptors?[0].Text);
        Assert.Equal(1, card.Assesments?[0].Id);
        Assert.Equal(1, card.Assesments?[0].CardId);
        Assert.Equal("2022-02-16", card.Assesments?[0].Date.ToString("yyyy-MM-dd"));
        Assert.Equal(10.0, card.Assesments?[0].GradeEstimate);
        Assert.Equal(3, card.Assesments?[0].Notes?.Count());
    }
}