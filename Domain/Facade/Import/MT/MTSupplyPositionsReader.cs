using Shopping.Contracts.Data.Supply;
using Shopping.Domain.Facade.Import.MT.HtmlReaders;
using SoftCircuits.HtmlMonkey;

namespace Shopping.Domain.Facade.Import;

public class MTSupplyPositionsReader : ISupplyPositionsReader
{
    public SupplyPosition[] Read(Stream htmlStream)
    {
        // HACK:
        using var reader = new StreamReader(htmlStream);
        var html = reader.ReadToEnd();
        var document = HtmlDocument.FromHtml(html);
        return SupplyReader.Parse(document);
    }
}
