using Data;
using Data.Enums;
using Data.Models;
using System;
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
    public void TestAddAndRemoveSet()
    {
        context.CreateSet(new CardSet()
        {
            Manufacturer = "Topps",
            Year = 1993,
            Sport = Sport.Basketball
        });
        var sets = context.Sets;
        Assert.Equal(2, sets.Count());
        var set = sets.Where(s => s.Id == 2).First();
        Assert.Equal("Topps", set.Manufacturer);
        Assert.Equal(1993, set.Year);
        Assert.Equal("Base", set.SubsetName);
        Assert.Equal(Sport.Basketball, set.Sport);

        context.DeleteSet(set.Id);
        sets = context.Sets;
        Assert.Equal(1, sets.Count());
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
    public void TestAddAndRemoveSubGrade()
    {
        context.CreateSubGrade(new SubGrade()
        {
            CardId = 1,
            Value = 10,
            Description = "Corners"
        });
        
        var subGrades = context.SubGrades;
        Assert.Equal(2, subGrades.Count());
        var subGrade = subGrades.Where(g => g.Id == 2).First();
        Assert.Equal(1, subGrade.CardId);
        Assert.Equal(10, subGrade.Value);
        Assert.Equal("Corners", subGrade.Description);

        context.DeleteSubGrade(subGrade.Id);
        subGrades = context.SubGrades;
        Assert.Equal(1, subGrades.Count());

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
    public void TestAddAndRemoveGrade()
    {
        context.CreateGrade(new Grade()
        {
            CardId = 1,
            Value = 9,
            Vender = GradingVender.SGC
        });
        var grades = context.Grades;
        Assert.Equal(1, grades.Count());
        var grade = context.Cards.First().Grade;
        Assert.Equal(9, grade?.Value);
        Assert.Equal(GradingVender.SGC, grade?.Vender);
        
        // Return to original state
        context.CreateGrade(new Grade()
        {
            CardId = 1,
            Value = 10,
            Vender = GradingVender.PSA
        });
        grades = context.Grades;
        Assert.Equal(1, grades.Count());
        grade = context.Cards.First().Grade;
        Assert.Equal(10, grade?.Value);
        Assert.Equal(GradingVender.PSA, grade?.Vender);

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
    public void TestAddAndRemoveDescriptor()
    {
        context.CreateDescriptor(new CardDescriptor()
        {
            CardId = 1,
            Text = "Rookie"
        });
        var descriptors = context.CardDescriptors;
        Assert.Equal(2, descriptors.Count());
        var descriptor = descriptors.Where(d =>
            d.Id == 2).First();
        Assert.Equal(1, descriptor.CardId);
        Assert.Equal("Rookie", descriptor.Text);

        context.DeleteDescriptor(descriptor.Id);
        descriptors = context.CardDescriptors;
        Assert.Equal(1, descriptors.Count());

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
    public void TestAddAndRemoveAssesment()
    {
        context.CreateAssesment(new CardAssesment()
        {
            CardId = 1,
            Date = DateTime.Today,
            GradeEstimate = 9.5
        });
        var assesments = context.Assesments;
        Assert.Equal(2, assesments.Count());
        var assesment = assesments.Where(
            a => a.Id == 2).First();
        Assert.Equal(1, assesment.CardId);
        Assert.Equal(DateTime.Today, assesment.Date);

        context.DeleteAssesment(assesment.Id);
        assesments = context.Assesments;
        Assert.Equal(1, assesments.Count());
    }

    [Fact]
    public void TestAssesmentNotes()
    {
        var assesmentNotes = context.AssesmentNotes;
        var note = (from n in assesmentNotes
            where n.Id == 1
            select n).First();        

        Assert.Equal(3, assesmentNotes.Count());
        Assert.Equal(1, note.CardId);
        Assert.Equal(1, note.AssesmentId);
        Assert.Equal("4 sharp corners", note.Text);

    }

    [Fact]
    public void TestAddAndRemoveNote()
    {
        context.CreateAssesmentNote(new AssesmentNote()
        {
            AssesmentId = 1,
            CardId = 1,
            Text = "test"
        });

        var notes = context.AssesmentNotes;
        Assert.Equal(4, notes.Count());
        var note = notes.Where(n => n.Id == 4).First();
        Assert.Equal("test", note.Text);

        context.DeleteAssesmentNote(note.Id);
        notes = context.AssesmentNotes;
        Assert.Equal(3, notes.Count());
    }

    [Fact]
    public void TestCards()
    {
        var cards = context.Cards;
        var card = cards.First();

        Assert.Equal(1, cards.Count());
        Assert.Equal(1, card.Id);
        Assert.Equal(1, card.SetId);
        Assert.Equal("Ken Griffey Jr.", card.Name);
        Assert.Equal("259", card.CardNumberInSet);
        Assert.Equal("Base", card.InsertName);
        Assert.Equal(CardStatus.Graded, card.Status);
    }

    [Fact]
    public void TestCardSets()
    {
        var card = context.Cards.First();
        Assert.Equal(1, card.Set.Id);
        Assert.Equal("Donruss", card.Set.Manufacturer);
        Assert.Equal("Base", card.Set.SubsetName);
        Assert.Equal(1990, card.Set.Year);
        Assert.Equal(Sport.Baseball, card.Set.Sport);
    }

    [Fact]
    public void TestCardGrades()
    {
        var card = context.Cards.First();
        Assert.NotNull(card.Grade);
        Assert.Equal(1, card.Grade?.CardId);
        Assert.Equal(10, card.Grade?.Value);
        Assert.Equal(GradingVender.PSA, card.Grade?.Vender);
    }

    [Fact]
    public void TestCardSubGrades()
    {
        var card = context.Cards.First();
        Assert.Equal(1, card.Grade?.SubGrades?[0].CardId);
        Assert.Equal(10, card.Grade?.SubGrades?[0].Value);
        Assert.Equal("Auto", card.Grade?.SubGrades?[0].Description);
    }

    [Fact]
    public void TestCardDescriptors()
    {
        var card = context.Cards.First();
        Assert.Equal(1, card.Descriptors?[0].CardId);
        Assert.Equal("HOF", card.Descriptors?[0].Text);
    }

    [Fact]
    public void TestCardAssesments()
    {
        var card = context.Cards.First();
        Assert.Equal(1, card.Assesments?[0].Id);
        Assert.Equal(1, card.Assesments?[0].CardId);
        Assert.Equal("2022-02-16", card.Assesments?[0].Date.ToString("yyyy-MM-dd"));
        Assert.Equal(10.0, card.Assesments?[0].GradeEstimate);
    }

    [Fact]
    public void TestCardAssesmentNotes()
    {
        var card = context.Cards.First();
        Assert.Equal(3, card.Assesments?[0].Notes?.Count());
    }

    [Fact]
    public void TestAddRemoveAndUpdateCard()
    {
        // Test Add Card
        context.CreateCard(new Card(){
            SetId = 1,
            Name = "Bo Jackson",
            CardNumberInSet = "150",
            Status = CardStatus.Pending
        });

        var cards = context.Cards;

        Assert.Equal(2, cards.Count());
        var card = cards.Where(c => c.Id == 2).First();
        Assert.Equal("Bo Jackson", card.Name);
        Assert.Equal(CardStatus.Pending, card.Status);
        
        // Test Update Card Status
        context.UpdateCardStatus(2, CardStatus.Rejected);
        cards = context.Cards;
        card = cards.Where(c => c.Id == 2).First();
        Assert.Equal(CardStatus.Rejected, card.Status);

        // Test Delete Card
        context.DeleteCard(2);
        cards = context.Cards;
        Assert.Equal(1, cards.Count());
    }
}