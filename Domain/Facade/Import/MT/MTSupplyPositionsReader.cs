using Shopping.Contracts.Data.Supply;
using Shopping.Domain.Facade.Import.MT.HtmlReaders;
using SoftCircuits.HtmlMonkey;

namespace Shopping.Domain.Facade.Import;

public class MTSupplyPositionsReader : ISupplyPositionReader
{
    public SupplyPosition[] Read(string html)
        => SupplyReader.Parse(HtmlDocument.FromHtml(html));
}
