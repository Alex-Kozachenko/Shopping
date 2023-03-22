namespace Shopping.Contracts.Data.Supply;

public readonly record struct SupplyPosition
{
    public Guid Id { get; init; }
    public Invoice Invoice { get; init; }
    public Product Product { get; init; }
}
