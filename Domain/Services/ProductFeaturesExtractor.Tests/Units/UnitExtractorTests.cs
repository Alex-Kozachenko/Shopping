using Shopping.Contracts.Data.Units;
using Shopping.Domain.Services.ProductFeaturesExtractor.Units;

namespace Shopping.Domain.Services.ProductFeaturesExtractor.Tests.Units;

internal class UnitExtractorTests
{
    [TestCase("200 мл   1 мл    0 мл    123.4 мл    12,3 мл")]
    [TestCase("200мл    1мл     0мл     123.4мл     12,3мл")]
    [TestCase("200 мл.  1 мл.   0 мл.   123.4 мл.   12,3 мл.")]
    [TestCase("200мл.   1мл.    0мл.    123.4мл.    12,3мл.")]
    public void Exctract_SingleToken_ShouldReturn_CorrectResult(string line)
    {
        var expected = new Unit[]
        {
            new(200, "мл"),
            new(1, "мл"),
            new(0, "мл"),
            new(123.4m, "мл"),
            new(12.3m, "мл"),
        };

        var actual = UnitExtractor.Extract(line);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [TestCase("200 мл 10 шт")]
    [TestCase("200мл 10шт")]
    [TestCase("200 мл. 10 шт.")]
    [TestCase("200мл. 10шт.")]
    public void Exctract_MultipleTokens_ShouldReturn_CorrectResult(string line)
    {
        var expected = new Unit[]
        {
            new(200, "мл"),
            new(10, "шт")
        };

        var actual = UnitExtractor.Extract(line);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [TestCase("200.0 мл", "200.0")]
    [TestCase("200.5 мл", "200.5")]
    [TestCase("200.5мл", "200.5")]
    [TestCase("200.9 мл", "200.9")]
    [TestCase("200.1 мл", "200.1")]
    [TestCase("200.19 мл", "200.19")]
    public void Exctract_DecimalTokens_ShouldReturn_CorrectResult(string line, string expected)
    {
        var actual = UnitExtractor.Extract(line)
            .ElementAt(0)
            .Value
            .ToString();

        Assert.That(actual, Is.EqualTo(expected));
    }
}
