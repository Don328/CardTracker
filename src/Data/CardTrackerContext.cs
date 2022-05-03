using System.Data;
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

#region Create Row Methods
    public void CreateCard(Card card)
    {
        var command = connection.CreateCommand();
        command.CommandText = CreateRow.Card;
        AddParameter(command, "@setId", card.SetId);
        AddParameter(command, "@name", card.Name);
        AddParameter(
            command, "@cardNumberInSet", card.CardNumberInSet);
        AddParameter(command, "@insertName", card.InsertName);
        AddParameter(command, "@status", (int)card.Status);
        long cardId = (long)command.ExecuteScalar();
        card.Id = (int)cardId;

        ReadCards();
    }

    public void CreateSet(CardSet set)
    {
        var command = connection.CreateCommand();
        command.CommandText = CreateRow.Set;
        AddParameter(command, "@manufacturer", set.Manufacturer);
        AddParameter(command, "@subsetName", set.SubsetName);
        AddParameter(command, "@year", set.Year);
        AddParameter(command, "@sport", set.Sport);
        long setId = (long)command.ExecuteScalar();
        set.Id = (int)setId;

        ReadSets();
        ReadCards();
    }

    public void CreateGrade(Grade grade)
    {
        var existing = Grades.Where(g =>
            g.CardId == grade.CardId);

        if (existing != null)
        {
            DeleteGrade(grade.CardId);
        }

        var command = connection.CreateCommand();
        command.CommandText = CreateRow.Grade;
        AddParameter(command,"@cardId", grade.CardId);
        AddParameter(command,"@value", grade.Value);
        AddParameter(command,"@vender", grade.Vender);
        command.ExecuteNonQuery();

        ReadGrades();
        ReadCards();
    }

    public void CreateSubGrade(SubGrade subGrade)
    {
        var command = connection.CreateCommand();
        command.CommandText = CreateRow.SubGrade;
        AddParameter(command, "@id", subGrade.Id);
        AddParameter(command, "@cardId", subGrade.CardId);
        AddParameter(command, "@value", subGrade.Value);
        AddParameter(command, "@description", subGrade.Description);

        long subGradeId = (long)command.ExecuteScalar();
        subGrade.Id = (int)subGradeId;
        
        ReadSubGrades();
        ReadGrades();
        ReadCards();
    }

    public void CreateDescriptor(CardDescriptor descriptor)
    {
        var command = connection.CreateCommand();
        command.CommandText = CreateRow.Descriptor;
        AddParameter(command, "@cardId", descriptor.CardId);
        AddParameter(command, "@text", descriptor.Text);

        long descriptorId = (long)command.ExecuteScalar();
        descriptor.Id = (int)descriptorId;
        
        ReadCardDescriptors();
        ReadCards();
    }
    
    public void CreateAssesment(CardAssesment assesment)
    {
        var command = connection.CreateCommand();
        command.CommandText = CreateRow.Assesment;
        AddParameter(command, "@cardId", assesment.CardId);
        AddParameter(command, "@date", DateTime.Today);
        AddParameter(command, "@gradeEstimate", assesment.GradeEstimate);

        long assesmentId = (long)command.ExecuteScalar();
        assesment.Id = (int)assesmentId;
        
        ReadCardAssesments();
        ReadCards();
    }
    
    public void CreateAssesmentNote(AssesmentNote note)
    {
        var command = connection.CreateCommand();
        command.CommandText = CreateRow.Note;
        AddParameter(command,"@assesmentId", note.AssesmentId);
        AddParameter(command,"@cardId", note.CardId);
        AddParameter(command,"@text", note.Text);

        long noteId = (long)command.ExecuteScalar();
        note.Id = (int)noteId;

        ReadAssesmentNotes();
        ReadCardAssesments();
    }

#endregion

#region Delete Row methods
    public void DeleteCard(int cardId)
    {
        var command = connection.CreateCommand();
        command.CommandText = DeleteRow.Card;
        AddParameter(command, "@id", cardId);
        command.ExecuteNonQuery();

        ReadCards();
    }

    public void DeleteSet(int setId)
    {
        var command = connection.CreateCommand();
        command.CommandText = DeleteRow.Set;
        AddParameter(command, "@id", setId);
        command.ExecuteNonQuery();

        ReadSets();
        ReadCards();
    }
    
    public void DeleteGrade(int cardId)
    {
        var command = connection.CreateCommand();
        command.CommandText = DeleteRow.Grade;
        AddParameter(command, "@cardId", cardId);
        command.ExecuteNonQuery();

        ReadGrades();
        ReadCards();
    }

    public void DeleteSubGrade(int subGradeId)
    {
        var command = connection.CreateCommand();
        command.CommandText = DeleteRow.SubGrade;
        AddParameter(command, "@id", subGradeId);
        command.ExecuteNonQuery();

        ReadSubGrades();
        ReadGrades();
        ReadCards();
    }

    public void DeleteDescriptor(int descriptorId)
    {
        var command = connection.CreateCommand();
        command.CommandText = DeleteRow.Descriptor;
        AddParameter(command, "@id", descriptorId);
        command.ExecuteNonQuery();

        ReadCardDescriptors();
        ReadCards();
    }

    public void DeleteAssesment(int assesmentId)
    {
        var command = connection.CreateCommand();
        command.CommandText = DeleteRow.Assesment;
        AddParameter(command, "@id", assesmentId);
        command.ExecuteNonQuery();

        ReadCardAssesments();
        ReadCards();
    }

    public void DeleteAssesmentNote(int noteId)
    {
        var command = connection.CreateCommand();
        command.CommandText = DeleteRow.Note;
        AddParameter(command, "@id", noteId);
        command.ExecuteNonQuery();

        ReadAssesmentNotes();
        ReadCardAssesments();
        ReadCards();
    }
#endregion

    public void UpdateCardStatus(
        int cardId, CardStatus status)
    {
        var command = connection.CreateCommand();
        command.CommandText = UpdateEntry.CardStatus;
        AddParameter(command, "@id", cardId);
        AddParameter(command, "@status", status);
        command.ExecuteNonQuery();

        ReadCards();
    }

#region Table Readers
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
                        Id = reader.GetInt32(0),
                        CardId = reader.GetInt32(1),
                        Value = reader.GetDouble(2),
                        Description = reader.GetString(3)
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
                        Id = reader.GetInt32(0),
                        CardId = reader.GetInt32(1),
                        Text = reader.GetString(2)
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
                        CardId = reader.GetInt32(1),
                        AssesmentId = reader.GetInt32(2),
                        Text = reader.GetString(3)
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
#endregion

    private void AddParameter(
        DbCommand cmd,
        string name,
        object value)
    {
        var p = cmd.CreateParameter();
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }

        var type = value.GetType();

        if (type == typeof(int))
            p.DbType = DbType.Int32;
        else if (type == typeof(double))
            p.DbType = DbType.Double;
        else if (type == typeof(string))
            p.DbType = DbType.String;
        else if (type == typeof(CardStatus))
            p.DbType = DbType.Int32;
        else if (type == typeof(GradingVender))
            p.DbType = DbType.Int32;
        else if (type == typeof(Sport))
            p.DbType = DbType.Int32;
        else if (type == typeof(DateTime))
            p.DbType = DbType.DateTime;
        else if (type == typeof(DateOnly))
            p.DbType = DbType.DateTime;
        else
            throw new ArgumentException(
                $"Unrecognized Type: {type}");

        p.Direction = ParameterDirection.Input;
        p.ParameterName = name;
        p.Value = value;
        cmd.Parameters.Add(p);
    }
}