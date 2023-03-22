using Shopping.Domain.Services.ProductFeaturesExtractor.Names;
using Shopping.Domain.Services.ProductFeaturesExtractor.Units;
using Shopping.Contracts.Domain.Services;
using Shopping.Contracts.Data;
using Shopping.Contracts.Data.Scoping;
using System.Text.Json.Nodes;

namespace Shopping.Domain.Services.ProductFeaturesExtractor;

public sealed class ProductFeaturesExtractorService : IProductService
{
    public Product Process(Product product)
    {
        var sharedFeatureSet = new Dictionary<string, JsonNode>();

        _ = new CapacityFeatureScope(sharedFeatureSet)
        {
            MassGramms = GetUnit(product.Info, "г"),
            Pieces = GetUnit(product.Info, "шт"),
        };

        _ = new NamingFeatureScope(sharedFeatureSet)
        {
            GroupingName = NameExtractor.Extract(product.Info)
        };

        return product with 
        {
            ProcessorVersion = "v0.0001a", 
            Features = sharedFeatureSet 
        };
    }

    private static long GetUnit(string line, string measure)
    {
        var units = UnitExtractor.Extract(line)
            .Select(UnitNormalizer.Normalize)
            .ToArray();

        return UnitExtractor.GetUnit(units, measure);
    }
}
