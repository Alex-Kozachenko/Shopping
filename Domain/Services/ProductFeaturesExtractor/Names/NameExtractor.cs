namespace Shopping.Domain.Services.ProductFeaturesExtractor.Names;

internal static class NameExtractor
{
    private static readonly TokenExtractor[] filters = new[]
    {
        TokenExtractor.Exclusive(@"^.{0,2}$"), // smallWords
        TokenExtractor.Exclusive(@"\w+(?:ая)"), // adjectives
        TokenExtractor.Exclusive(@"\w+ньон"), // foreignWords
        TokenExtractor.Exclusive(@"\(.+?\)"), // parentesed (words)
        TokenExtractor.Exclusive(@"^[a-zA-Z]+?$"), // english
    };

    private static readonly string[] specialWords = new string[]
    {
        "корень",
        "Крупа",
        "Масло",
        "Хлопья",
        "Тушка",
        "Печень",
    };

    private static readonly string[] knownGroups = new string[]
    {
        "Пена для бритья"
    };

    private static readonly Dictionary<string, string> replacers = new Dictionary<string, string>()
    {
        { "цыпленка-бройлера", "куриная" }
    };

    /// <summary>
    /// Extracts a name as single word.
    /// </summary>
    /// <remarks>
    /// If failed to recognize a single word - returns multiple words instead.
    /// </remarks>
    internal static string? Extract(string line)
    {
        foreach (var item in replacers)
        {
            line = line.Replace(item.Key, item.Value);
        }

        return ExtractWithKnownGroups(line)
            ?? ExtractWithSpecialWords(line)
            ?? ExtractWithFilters(line);
    }

    private static string? ExtractWithKnownGroups(string line)
        => knownGroups.FirstOrDefault(x => line.Contains(x, StringComparison.InvariantCultureIgnoreCase));

    private static string? ExtractWithFilters(string line)
    {
        var tokens = line.Split(' ')
            .Apply(filters);

        return tokens.FirstOrDefault();
    }

    private static string? ExtractWithSpecialWords(string line) 
    {
        var tokens = line.Split(' ');
        if (!tokens.Any())
        {
            return null;
        }

        if (specialWords.Any(x => x.Equals(tokens[0], StringComparison.InvariantCultureIgnoreCase)))
        {
            return string.Join(' ', tokens[0], tokens[1]);
        }

        return null;
    }
}
