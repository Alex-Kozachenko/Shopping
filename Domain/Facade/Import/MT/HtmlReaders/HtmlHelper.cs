using SoftCircuits.HtmlMonkey;

namespace Shopping.Domain.Facade.Import.MT.HtmlReaders;

internal static class HtmlHelper
{
    internal static string GetHrefValue(
        this IEnumerable<HtmlNode> children,
        string selector)
            => children.GetAttributeValue(selector, "href");

    internal static string GetAttributeValue(
        this IEnumerable<HtmlNode> children,
        string selector,
        string name)
            => children.Find(selector).First().Attributes[name]?.Value
                ?? string.Empty;

    internal static string GetText(this IEnumerable<HtmlNode> children, string selector)
        => children.Find(selector)
            .First()
            .Text
            .Trim();
}
