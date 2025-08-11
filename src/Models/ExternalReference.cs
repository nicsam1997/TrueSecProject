using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using TrueSecProject.Exceptions;

namespace TrueSecProject.Models;

public class ExternalReference
{

    private static readonly Regex CvePattern = new(@"^CVE-\d{4}-\d{4,}$", RegexOptions.Compiled);

    [JsonPropertyName("source_name")]
    [Required]
    public string SourceName { get; }

    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; }

    [JsonPropertyName("url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [RegularExpression(@"^(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\?([^#]*))?(#(.*))?", ErrorMessage = "The URL must conform to RFC 3986.")]
    public string? Url { get; }

    [JsonPropertyName("hashes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Hashes? Hashes { get; }

    [JsonPropertyName("external_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExternalId { get; }

    [JsonConstructor]
    public ExternalReference(
        string sourceName,
        Hashes? hashes = null,
        string? description = null,
        string? url = null,
        string? externalId = null
    )
    {
        if (string.IsNullOrWhiteSpace(description) && string.IsNullOrWhiteSpace(url) && string.IsNullOrWhiteSpace(externalId))
        {
            throw new InvalidStructureException("In an external_reference, in addition to source_name, at least one of description, url, or external_id must be provided.");
        }

        if (sourceName?.ToLower() == "cve" && (string.IsNullOrWhiteSpace(externalId) || !CvePattern.IsMatch(externalId)))
        {
            throw new InvalidStructureException("In an external_reference,, when source_name is 'cve', the external_id must be a valid CVE identifier  matching the format 'CVE-YYYY-NNNN...'.");
        }
        SourceName = sourceName;
        Hashes = hashes;
        Description = description;
        Url = url;
        ExternalId = externalId;
    }
}

public enum HashType
{
    [JsonStringEnumMemberName("MD5")]
    MD5,
    [JsonStringEnumMemberName("SHA-1")]
    SHA1,
    [JsonStringEnumMemberName("SHA-256")]
    SHA256,
    [JsonStringEnumMemberName("SHA-512")]
    SHA512,
    [JsonStringEnumMemberName("SHA3-256")]
    SHA3_256,
    [JsonStringEnumMemberName("SHA3-512")]
    SHA3_512,
    [JsonStringEnumMemberName("SSDEEP")]
    SSDEEP,
    [JsonStringEnumMemberName("TLSH")]
    TLSH
}