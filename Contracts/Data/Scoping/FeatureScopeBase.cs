using System.Text.Json.Nodes;

namespace Shopping.Contracts.Data.Scoping;

public class FeatureScopeBase
{
    protected readonly IDictionary<string, JsonNode> featureSet;
    public FeatureScopeBase(IDictionary<string, JsonNode> featureSet)
    {
        this.featureSet = featureSet;
    }

    public TValue? TryGet<TValue>(string key, TValue? fallback)
    {
        if (featureSet.TryGetValue(key, out var node))
        {
            return node!.GetValue<TValue>();
        }

        return fallback;
    }

    public void Set(string key, Func<JsonNode> selector)
    {
        featureSet[key] = selector.Invoke();
    }
}