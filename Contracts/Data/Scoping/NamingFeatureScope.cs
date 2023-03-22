using System.Text.Json.Nodes;

namespace Shopping.Contracts.Data.Scoping;

public class NamingFeatureScope : FeatureScopeBase
{
    public NamingFeatureScope(IDictionary<string, JsonNode> featureSet) : base(featureSet)
    {
    }

    public string? GroupingName
    {
        get => TryGet(nameof(GroupingName), (string?)null);
        set => Set(nameof(GroupingName), () => value!);
    }
}