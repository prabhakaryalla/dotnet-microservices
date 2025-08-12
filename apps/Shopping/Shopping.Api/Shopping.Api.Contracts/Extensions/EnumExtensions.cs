using System.Reflection;
using System.Runtime.Serialization;

namespace Shopping.Api.Contracts.Extensions;

public static class EnumExtensions
{
    public static string GetEnumValue<TEnum>(this TEnum value) where TEnum : struct
    {
        return Enum.GetName(typeof(TEnum), value);


    }

    public static TEnum? GetEnumValueOrDefault<TEnum>(this string value) where TEnum : struct
    {
        if (value.TryParseWithMemberName(out TEnum result))
            return result;


        return default;
    }


    public static bool TryParseWithMemberName<TEnum>(this string value, out TEnum result) where TEnum : struct
    {
        result = default;
        if (string.IsNullOrEmpty(value)) return false;
        Type enumType = typeof(TEnum);
        foreach (string name in Enum.GetNames(typeof(TEnum)))
        {
            if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = Enum.Parse<TEnum>(name);
                return true;
            }


            EnumMemberAttribute memberAttribute = enumType.GetField(name).GetCustomAttribute(typeof(EnumMemberAttribute)) as EnumMemberAttribute;

            if (memberAttribute is null)
                continue;

            if (memberAttribute.Value.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = Enum.Parse<TEnum>(name);
                return true;
            }
        }
        return false;
    }
}
