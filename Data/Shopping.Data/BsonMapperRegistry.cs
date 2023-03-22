using LiteDB;
using NMoneys;
using System.Text.Json.Nodes;

namespace Shopping.Data;

internal static class BsonMapperRegistry
{
    public static void Register()
    {
        BsonMapper.Global.RegisterType<Money>
        (
            serialize: (money) => money.Amount,
            deserialize: (bson) => new Money(bson.AsDecimal)
        );

        BsonMapper.Global.RegisterType<JsonNode>
        (
            serialize: (jsonNode) => jsonNode.ToJsonString(),
            deserialize: (bson) => JsonNode.Parse(bson.AsString)!
        );
    }
}