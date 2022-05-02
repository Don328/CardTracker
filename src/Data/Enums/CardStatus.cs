namespace Data.Enums;

public enum CardStatus {
    Pending, // Inspection not completed
    Inspected, // Inspection and grade estimate completed
    Packaged, // Final inspection and cleaning done. Awaiting shipping
    Rejected, // Rejected on inspection
    Sent, // Sent to grading vender
    Graded, // Has returned from grader
    Returned_Ungraded, // Grading vender declined to grade
}