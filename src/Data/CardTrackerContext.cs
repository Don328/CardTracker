using System.Data.Common;
using Data.CommandText;
using Data.Enums;
using Data.Models;

namespace Data;

public class CardTrackerContext
{
    private DbConnection connection;

    public IEnumerable<Card> Cards { get; private set; }
    public IEnumerable<CardSet> Sets { get; private set; }
    public IEnumerable<Grade> Grades { get; private set; }
    public IEnumerable<SubGrade> SubGrades { get; private set; }
    public IEnumerable<CardDescriptor> CardDescriptors { get; private set; }
    public IEnumerable<CardAssesment> Assesments { get; set; }
    public IEnumerable<AssesmentNote> AssesmentNotes { get; set; }

    public CardTrackerContext(DbConnection conn)
    {
        connection = conn;
        ReadSets();
        ReadSubGrades();
        ReadCardDescriptors();
        ReadAssesmentNotes();

        // Depends on `ReadAssesmentNotes()`
        ReadCardAssesments();


        // Depends on `ReadSubGrades()`
        ReadGrades();
        
        // Depends on all previous `Read()` methods;
        ReadCards();
    }

    private void ReadSets()
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = ReadTable.CardSets;
            using (var reader = command.ExecuteReader())
            {
                var sets = new List<CardSet>();
                Sets = sets;

                while (reader.Read())
                {
                    sets.Add(new CardSet()
                    {
                        Id = reader.GetInt32(0),
                        Manufacturer = reader.GetString(1),
                        SubsetName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        Sport = (Sport)reader.GetInt32(4),
                    });
                }
            }
        }
    }

    private void ReadSubGrades()
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = ReadTable.SubGrades;
            using (var reader = command.ExecuteReader())
            {
                var subGrades = new List<SubGrade>();
                SubGrades = subGrades;

                while (reader.Read())
                {
                    subGrades.Add(new SubGrade(){
                        CardId = reader.GetInt32(0),
                        Value = reader.GetDouble(1),
                        Description = reader.GetString(2)
                    });
                }
            }
        }
    }

    private void ReadGrades()
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = ReadTable.Grades;
            using (var reader = command.ExecuteReader())
            {
                var grades = new List<Grade>();
                Grades = grades;

                while (reader.Read())
                {
                    var grade = new Grade()
                    {
                        CardId = reader.GetInt32(0),
                        Value = reader.GetDouble(1),
                        Vender = (GradingVender)reader.GetInt32(2)
                    };

                    grade.SubGrades = (
                        from g in SubGrades
                        where g.CardId == grade.CardId
                        select g).ToArray();
                    
                    grades.Add(grade);
                }
            }
        }
    }

    private void ReadCardDescriptors()
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = ReadTable.CardDescriptors;
            using (var reader = command.ExecuteReader())
            {
                var descriptors = new List<CardDescriptor>();
                CardDescriptors = descriptors;

                while (reader.Read())
                {
                    descriptors.Add(new CardDescriptor()
                    {
                        CardId = reader.GetInt32(0),
                        Text = reader.GetString(1)
                    });
                }
            }
        }
    }
    
    private void ReadAssesmentNotes()
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = ReadTable.AssesmentNotes;
            using (var reader = command.ExecuteReader())
            {
                var notes = new List<AssesmentNote>();
                AssesmentNotes = notes;

                while(reader.Read())
                {
                    notes.Add(new AssesmentNote()
                    {
                        Id = reader.GetInt32(0),
                        AssesmentId = reader.GetInt32(1),
                        Text = reader.GetString(2)
                    });
                }
            }
        }
    }

    private void ReadCardAssesments()
    {
        using (var command  = connection.CreateCommand())
        {
            command.CommandText = ReadTable.CardAssesments;
            using (var reader = command.ExecuteReader())
            {
                var assesments = new List<CardAssesment>();
                Assesments = assesments;

                while (reader.Read())
                {
                    var assesment = new CardAssesment
                    {
                        Id = reader.GetInt32(0),
                        CardId = reader.GetInt32(1),
                        Date = reader.GetDateTime(2),
                        GradeEstimate = reader.GetDouble(3)
                    };
                    
                    assesment.Notes = (
                        from n in AssesmentNotes
                        where n.AssesmentId == assesment.Id
                        select n).ToArray();

                    assesments.Add(assesment);
                }
            }
        }
    }

    private void ReadCards()
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = ReadTable.Cards;
            using (var reader = command.ExecuteReader())
            {
                var cards = new List<Card>();
                Cards = cards;
                
                while (reader.Read())
                {
                    var card = new Card()
                    {
                        Id = reader.GetInt32(0),
                        SetId = reader.GetInt32(1),
                        Name = reader.GetString(2),
                        CardNumberInSet = reader.GetString(3),
                        InsertName = reader.GetString(4),
                        Status = (CardStatus)reader.GetInt32(5)
                    };

                    card.Set = Sets.Single(s => s.Id == card.SetId);
                    card.Grade = Grades.FirstOrDefault(g => g.CardId == card.Id);
                    
                    card.Descriptors = 
                        (from d in CardDescriptors
                        where d.CardId == card.Id
                        select d).ToArray();

                    card.Assesments =
                        (from a in Assesments
                        where a.CardId == card.Id
                        select a).ToArray();

                    cards.Add(card);                   
                }
            }
        }
    }
}