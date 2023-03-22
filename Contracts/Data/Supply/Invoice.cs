using NMoneys;
using Shopping.Contracts.Data;

namespace Shopping.Contracts.Data.Supply;

public readonly record struct Invoice
{
    public DateOnly Date { get; init; }

    public Vendor Vendor { get; init; }

    public long Quantity { get; init; }

    public Money Price { get; init; }

    public Money TotalPrice => Price.Times(Quantity);
}