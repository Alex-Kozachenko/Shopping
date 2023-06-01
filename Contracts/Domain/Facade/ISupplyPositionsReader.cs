using Shopping.Contracts.Data.Supply;

namespace Shopping.Contracts.Domain.Facade;

public interface ISupplyPositionsReader
{
    SupplyPosition[] Read(Stream htmlStream);
}
