namespace Data.CommandText;

public static class UpdateEntry
{
    public static readonly string CardStatus =
        @"UPDATE Cards
        SET Status=@status
        WHERE Id=@id";
}