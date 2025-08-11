using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TrueSecProject.Exceptions;
using TrueSecProject.Models;

public class GranularMarking
{
    [JsonPropertyName("lang")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [RegularExpression("^([a-zA-Z]{2,3}(-[a-zA-Z]{3}(-[a-zA-Z]{3}){0,2})?(-[a-zA-Z]{4})?(-([a-zA-Z]{2}|[0-9]{3}))?(-([a-zA-Z0-9]{5,8}|[0-9][a-zA-Z0-9]{3}))*([0-9A-WY-Za-wy-z](-[a-zA-Z0-9]{2,8}){1,})*(x-[a-zA-Z0-9]{2,8})?)|(x-[a-zA-Z0-9]{2,8})|(en-GB-oed)|(i-ami)|(i-bnn)|(i-default)|(i-enochian)|(i-hak)|(i-klingon)|(i-lux)|(i-mingo)|(i-navajo)|(i-pwn)|(i-tao)|(i-tay)|(i-tsu)|(sgn-BE-FR)|(sgn-BE-NL)|(sgn-CH-DE)|(art-lojban)|(cel-gaulish)|(no-bok)|(no-nyn)|(zh-guoyu)|(zh-hakka)|(zh-min)|(zh-min-nan)|(zh-xiang)$")]
    public string? Lang { get; }

    [JsonPropertyName("marking_ref")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MarkingDefinition? MarkingRef { get; }

    [JsonPropertyName("selectors")]
    [Required]
    [MinLength(1)]
    // TODO: Note that we should verify that each selector is applied to a field in the Stix object, this is currently not done
    public IReadOnlyList<string> Selectors { get; }

    [JsonConstructor]
    public GranularMarking(
        string? lang,
        MarkingDefinition? markingRef,
        IReadOnlyList<string> selectors
    )
    {
        // Note that this validation is aligned with the specification, however the json schema
        // in (https://github.com/oasis-open/cti-stix2-json-schemas)
        // seems to require the presence of a marking_ref which is not aligned with the specification
        if (!(!string.IsNullOrWhiteSpace(lang) ^ markingRef != null))
        {
            throw new InvalidStructureException("In granular_marking, exactly one of lang or marking_ref must be provided.");
        }
        Lang = lang;
        MarkingRef = markingRef;
        Selectors = selectors;
    }
}