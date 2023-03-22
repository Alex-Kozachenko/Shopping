using Shopping.Contracts.Data.Supply;

namespace Shopping.Contracts.Domain.Facade;

public interface ISupplyPositionReader
{
    SupplyPosition[] Read(string html);
}
