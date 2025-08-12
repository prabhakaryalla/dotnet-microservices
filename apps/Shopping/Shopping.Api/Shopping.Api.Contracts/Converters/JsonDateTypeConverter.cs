using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Shopping.Api.Contracts.Converters;

public class JsonDateTypeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.TryParse(reader.GetString(), out var date) ? date : DateTime.MinValue;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value != DateTime.MinValue
            ? value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            : null);
    }
}
