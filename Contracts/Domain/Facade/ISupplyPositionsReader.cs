using System.Collections.ObjectModel;
using System.IO.Pipelines;
using Shopping.Contracts.Data.Supply;

namespace Shopping.Contracts.Domain.Facade;

public interface ISupplyPositionsReader
{
    Task<ReadOnlyCollection<SupplyPosition>> Read(PipeReader htmlPipeReader);
}
