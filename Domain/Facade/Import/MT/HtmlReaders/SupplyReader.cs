using SoftCircuits.HtmlMonkey;
using Shopping.Contracts.Data.Supply;

namespace Shopping.Domain.Facade.Import.MT.HtmlReaders;

internal class SupplyReader
{
    internal static SupplyPosition[] Parse(HtmlDocument doc)
        => doc.Find(".history-order")
            .Select(node => new
            {
                Node = node,
                Date = ParseDate(node) ?? DateOnly.MinValue,
            })
            .SelectMany(x => ProductReader.Read(x.Node, x.Date))
            .ToArray();

    private static DateOnly? ParseDate(HtmlElementNode node)
    {
        const int dateElementHtmlIndex = 1;
        var dateEntry = GetOrderDataEntry(node, dateElementHtmlIndex);
        if (string.IsNullOrWhiteSpace(dateEntry))
        {
            return null;
        }

        return DateOnly.ParseExact(dateEntry, "dd-MM-yyyy");
    }

    private static string? GetOrderDataEntry(HtmlElementNode node, int index)
    {
        var orderData = ParseOrderData(node);
        var entry = orderData?.Skip(index).FirstOrDefault();
        return entry?.Split(' ').FirstOrDefault();
    }

    private static string[] ParseOrderData(HtmlElementNode node)
    {
        var orderDataRow = node.Children
            .Find(".order-data")
            .First();

        return orderDataRow.Children
            .Find("div")
            .Select(x => x.Text.Trim())
            .ToArray();
    }
}
