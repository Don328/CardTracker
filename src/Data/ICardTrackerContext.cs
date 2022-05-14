using Data.Enums;
using Data.Models;

namespace Data;

public interface ICardTrackerContext
{
    IEnumerable<Card> Cards { get; }
    IEnumerable<CardSet> Sets { get; }
    IEnumerable<Grade> Grades { get; }
    IEnumerable<SubGrade> SubGrades { get; }
    IEnumerable<CardDescriptor> CardDescriptors { get; }
    IEnumerable<CardAssesment> Assesments { get; set; }
    IEnumerable<AssesmentNote> AssesmentNotes { get; set; }

    void CreateAssesment(CardAssesment assesment);
    void CreateAssesmentNote(AssesmentNote note);
    void CreateCard(Card card);
    void CreateDescriptor(CardDescriptor descriptor);
    void CreateGrade(Grade grade);
    void CreateSet(CardSet set);
    void CreateSubGrade(SubGrade subGrade);
    void DeleteAssesment(int assesmentId);
    void DeleteAssesmentNote(int noteId);
    void DeleteCard(int cardId);
    void DeleteDescriptor(int descriptorId);
    void DeleteGrade(int cardId);
    void DeleteSet(int setId);
    void DeleteSubGrade(int subGradeId);
    void UpdateCardStatus(int cardId, CardStatus status);
}