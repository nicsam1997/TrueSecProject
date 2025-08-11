using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using TrueSecProject.Models;

namespace TrueSecProject.Serialization;

public class HashesConverter : JsonConverter<Hashes>
{
    private static readonly Dictionary<string, Regex> ValidationRules = new()
    {
        { "MD5", new Regex("^[a-fA-F0-9]{32}$", RegexOptions.Compiled) },
        { "SHA-1", new Regex("^[a-fA-F0-9]{40}$", RegexOptions.Compiled) },
        { "SHA-256", new Regex("^[a-fA-F0-9]{64}$", RegexOptions.Compiled) },
        { "SHA-512", new Regex("^[a-fA-F0-9]{128}$", RegexOptions.Compiled) },
        { "SHA3-256", new Regex("^[a-fA-F0-9]{64}$", RegexOptions.Compiled) },
        { "SHA3-512", new Regex("^[a-fA-F0-9]{128}$", RegexOptions.Compiled) },
        { "SSDEEP", new Regex("^[a-zA-Z0-9/+:.]{1,128}$", RegexOptions.Compiled) },
        { "TLSH", new Regex("^[a-zA-Z0-9]{70}$", RegexOptions.Compiled) }
    };


    public override Hashes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected start of a JSON object for Hashes.");
        }

        var hashes = new Hashes();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return hashes;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException("Expected property name in Hashes object.");
            }

            var hashAlgorithm = reader.GetString()!;
            reader.Read();
            var hashValue = reader.GetString()!;

            if (ValidationRules.TryGetValue(hashAlgorithm, out var regex))
            {
                if (!regex.IsMatch(hashValue))
                {
                    throw new JsonException($"Hash value for '{hashAlgorithm}' does not match the required pattern '{regex}'.");
                }
                else
                {
                   hashes.Add(hashAlgorithm, hashValue);
                }
            } else {
                throw new JsonException($"Hash key '{hashAlgorithm}' is not a valid standard hash algorithm name.");
            }
        }

        throw new JsonException("Unexpected end of JSON while parsing Hashes object.");
    }

    public override void Write(Utf8JsonWriter writer, Hashes value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (IDictionary<string, string>)value, options);
    }
}