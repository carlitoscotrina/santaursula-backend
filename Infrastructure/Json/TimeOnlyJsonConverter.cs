using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SantaUrsula.API.Infrastructure.Json;

public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    private const string Format = "HH:mm:ss";

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String &&
            TimeOnly.TryParseExact(reader.GetString() ?? string.Empty, Format, out var t))
        {
            return t;
        }

        if (reader.TokenType == JsonTokenType.String && TimeOnly.TryParse(reader.GetString(), out t))
        {
            return t;
        }

        throw new JsonException($"Unable to convert token to TimeOnly. TokenType: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}
