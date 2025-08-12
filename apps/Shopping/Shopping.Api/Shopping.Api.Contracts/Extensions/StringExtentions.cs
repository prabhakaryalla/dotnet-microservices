namespace Shopping.Api.Contracts.Extensions;

public static class StringExtentions
{
    public static bool IsNumeric(this string value)
    {
        return Double.TryParse(value, out double number);
    }
}
