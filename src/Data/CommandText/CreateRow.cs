namespace Data.CommandText;

public static class CreateRow
{
    public static readonly string Card =
        @"INSERT INTO Cards(
            SetId, Name, CardNumberInSet,
            InsertName, Status)
        VALUES(
            @setId, @name, @cardNumberInSet,
            @insertName, @status);
        SELECT last_insert_rowid();";
}