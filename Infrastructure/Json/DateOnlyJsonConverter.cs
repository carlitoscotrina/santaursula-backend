using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SantaUrsula.API.Infrastructure.Json;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String &&
            DateOnly.TryParseExact(reader.GetString() ?? string.Empty, Format, out var d))
        {
            return d;
        }

        if (reader.TokenType == JsonTokenType.String && DateOnly.TryParse(reader.GetString(), out d))
        {
            return d;
        }

        throw new JsonException($"Unable to convert token to DateOnly. TokenType: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}
