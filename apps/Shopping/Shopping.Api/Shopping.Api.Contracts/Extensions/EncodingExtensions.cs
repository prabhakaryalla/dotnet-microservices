using System.Text;

namespace Shopping.Api.Contracts.Extensions;

public static class EncodingExtensions
{
    public static string Base64Encode(this string value)
    {
        return Base64Encode(value, Encoding.UTF8);
    }


    public static string Base64Encode(this string value, Encoding encoder)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        var plainTextBytes = encoder.GetBytes(value);
        return Convert.ToBase64String(plainTextBytes);
    }


    public static string Base64Decode(this string value)
    {
        return Base64Decode(value, Encoding.UTF8);
    }


    public static string Base64Decode(this string value, Encoding encoder)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        var base64EncodedBytes = Convert.FromBase64String(value);
        return encoder.GetString(base64EncodedBytes);
    }
}

