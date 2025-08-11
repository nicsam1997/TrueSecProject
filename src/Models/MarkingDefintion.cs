using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TrueSecProject.Serialization;

namespace TrueSecProject.Models;

[JsonConverter(typeof(MarkingDefinitionConverter))]
public record MarkingDefinition
{
    [Required]
    [RegularExpression("^marking-definition--[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[1-5][0-9a-fA-F]{3}-[89abAB][0-9a-fA-F]{3}-[0-9a-fA-F]{12}$",
        ErrorMessage = "Marking Definition must be a valid STIX Identifier for a marking definition.")]
    public string Value { get; }

    public MarkingDefinition(string value)
    {
        Value = value;
    }
}