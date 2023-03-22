using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;

namespace Shopping.Contracts.Data;

public readonly record struct Product
{
    public bool IsProcessed => !string.IsNullOrEmpty(ProcessorVersion);
    public string ProcessorVersion { get; init; }
    public required string Info { get; init; }
    public required string Category { get; init; }
    public readonly IDictionary<string, JsonNode> Features { get; init; }
}