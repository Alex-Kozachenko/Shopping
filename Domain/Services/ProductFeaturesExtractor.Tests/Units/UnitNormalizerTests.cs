using Shopping.Domain.Services.ProductFeaturesExtractor.Units;
using Shopping.Contracts.Data.Units;

namespace Shopping.Domain.Services.ProductFeaturesExtractor.Tests.Units;

internal class UnitNormalizerTests
{
    [TestCase("мл", 200, ExpectedResult = 200)]
    [TestCase("л", 0.2, ExpectedResult = 200)]
    [TestCase("л", 2, ExpectedResult = 2000)]
    [TestCase("л", 2.2, ExpectedResult = 2200)]
    [TestCase("кг", 1, ExpectedResult = 1_000)]
    [TestCase("кг", 0, ExpectedResult = 0)]
    [TestCase("кг", 0.1, ExpectedResult = 100)]
    [TestCase("кг", 100.500, ExpectedResult = 100_500)]
    public decimal Normalize_ShouldReturn_Gramms(string measure, decimal value)
        => Normalize(value, measure).Value;

    private Unit Normalize(decimal value, string measure)
        => UnitNormalizer.Normalize(new Unit(value, measure));
}
