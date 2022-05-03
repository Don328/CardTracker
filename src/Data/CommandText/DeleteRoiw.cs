namespace Data.CommandText;

public static class DeleteRow
{
    public static readonly string Card =
        @"DELETE FROM Cards
        Where Id=@id";
}