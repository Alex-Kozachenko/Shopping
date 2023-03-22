using Shopping.Contracts.Data.Units;

namespace Shopping.Domain.Services.ProductFeaturesExtractor.Units;

internal static class UnitNormalizer
{
    internal static Unit Normalize(Unit unit)
    {
        var samples = new Dictionary<string, decimal>
        {
            { "кг", 1000 },
            { "л", 1000 },
            { "мл", 1 },
            { "г", 1 }
        };

        var sample = samples.FirstOrDefault(x => x.Key == unit.Measure);
        if (sample.Value != 0)
        {
            return new Unit(unit.Value * sample.Value, "г");
        }
        return unit;
    }
}
