using System.Diagnostics.CodeAnalysis;

namespace Shopping.Contracts.Data.Units;

public readonly record struct Unit
{
    private const string GramsMeasure = "г";
    private const string PiecesMeasure = "шт";

    public required decimal Value { get; init; }
    public required string Measure { get; init; }

    [SetsRequiredMembers]
    public Unit(decimal value, string measure)
    {
        Value = value;
        Measure = measure;
    }

    public static Unit CreateGrams(decimal value)
        => new(value, GramsMeasure);

    public static Unit CreatePieces(decimal value)
        => new(value, PiecesMeasure);

    public bool IsGrams()
        => Measure.Equals(GramsMeasure, StringComparison.InvariantCultureIgnoreCase);

    public bool IsPieces()
        => Measure.Equals(PiecesMeasure, StringComparison.InvariantCultureIgnoreCase);
};