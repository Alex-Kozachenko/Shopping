using NMoneys;

namespace Shopping.Domain.Facade.Import.MT.HtmlReaders;

internal static class ParseHelper
{
    internal static decimal ToDecimal(this string value)
        => Convert.ToDecimal(value);

    internal static long ToInt64(this string value)
        => Convert.ToInt64(value);

    internal static long ToInt64(this decimal value)
        => Convert.ToInt64(value);

    internal static decimal ToSum(this string value)
        => value.Split(' ')
                .First()
                .ToDecimal();

    internal static Money ToMoney(this decimal value)
        => new(value);
}
