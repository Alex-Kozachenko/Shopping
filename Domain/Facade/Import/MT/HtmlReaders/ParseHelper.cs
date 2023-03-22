using NMoneys;

namespace Shopping.Cloud.Common.Helpers;

public static class ParseHelper
{
    public static decimal ToDecimal(this string value)
        => Convert.ToDecimal(value);

    public static long ToInt64(this string value)
        => Convert.ToInt64(value);

    public static long ToInt64(this decimal value)
        => Convert.ToInt64(value);

    public static decimal ToSum(this string value)
        => value.Split(' ')
                .First()
                .ToDecimal();

    public static Money ToMoney(this decimal value)
        => new(value);
}
