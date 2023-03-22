using System.Text.Json.Nodes;

namespace Shopping.Contracts.Data.Scoping;

public class CapacityFeatureScope : FeatureScopeBase
{
    public CapacityFeatureScope(IDictionary<string, JsonNode> featureSet) : base(featureSet)
    {
    }

    public long MassGramms
    {
        get => TryGet(nameof(MassGramms), 0L);
        set => Set(nameof(MassGramms), () => value);
    }

    public long Pieces
    {
        get => TryGet(nameof(Pieces), 0L);
        set => Set(nameof(Pieces), () => value);
    }
}