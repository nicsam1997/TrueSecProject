using System.Text.Json;
using System.Text.Json.Serialization;
using TrueSecProject.Models;

namespace TrueSecProject.Serialization;

public class MarkingDefinitionConverter : JsonConverter<MarkingDefinition>
{
    public override MarkingDefinition? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected a string for MarkingDefinition.");
        }
        var value = reader.GetString();
        return value is null ? null : new MarkingDefinition(value);
    }

    public override void Write(Utf8JsonWriter writer, MarkingDefinition value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}