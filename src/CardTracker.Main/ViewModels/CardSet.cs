using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CardTracker.Main.ViewModels;

public class CardSet
{
    private string manufacturer = string.Empty;
    private string? setName = null;
    private string? subsetName = null;

    public int Id { get; set; }

    [Range(1900, 2030)]
    public int Season { get; set; }

    [MinLength(5)]
    public string Manufacturer 
    { 
        get => manufacturer; 
        set => manufacturer = NormalizeName(value)?? string.Empty; 
    }
    
    public string? SetName 
    {
        get => setName;
        set => setName = NormalizeName(value); 
    }
    
    public string? SubsetName
    { 
        get => subsetName; 
        set => setName = NormalizeName(value); 
    }

    private string? NormalizeName(string? name)
    {
        if (string.IsNullOrEmpty(name)) return null;    

        var lower = name.ToLower();
        var builder = new StringBuilder();
        var upper = true;
        for (var i = 0; i < lower.Length -1; i++)
        {
            if (char.IsWhiteSpace(lower[i])) 
            {
                builder.Append(lower[i]);
                upper = true;
                continue;
            }
            
            if (upper)
            {
                var capitalized = lower[i].ToString().ToUpper();
                builder.Append(capitalized);
                upper = false;
                continue;
            }

            builder.Append(lower[i]);
        }

        return builder.ToString();
    }
}
