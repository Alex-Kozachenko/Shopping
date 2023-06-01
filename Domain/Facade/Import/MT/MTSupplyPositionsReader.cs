using System.Collections.ObjectModel;
using System.IO.Pipelines;
using System.Text;
using Shopping.Contracts.Data.Supply;
using Shopping.Domain.Facade.Import.MT.HtmlReaders;
using SoftCircuits.HtmlMonkey;

namespace Shopping.Domain.Facade.Import;

public class MTSupplyPositionsReader : ISupplyPositionsReader
{
    public async Task<ReadOnlyCollection<SupplyPosition>> Read(PipeReader htmlPipeReader)
    {
        // HACK:        
        var sb = new StringBuilder();
        while (true)
        {
            var readResult = await htmlPipeReader.ReadAsync();
            var text = Encoding.UTF8.GetString(readResult.Buffer);
            sb.Append(text);

            htmlPipeReader.AdvanceTo(
                readResult.Buffer.Start, 
                readResult.Buffer.End);

            if (readResult.IsCompleted)
            {
                break;
            }
        }

        var html = sb.ToString();
        var document = HtmlDocument.FromHtml(html);
        return SupplyReader
            .Parse(document)
            .AsReadOnly();
    }
}
