using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TrueSecProject.Models;

namespace TrueSecProject.Swagger;

public class ExternalReferenceSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type != typeof(ExternalReference))
        {
            return;
        }

        var anyOfRule = new OpenApiSchema
        {
            AnyOf = new List<OpenApiSchema>
            {
                new() { Required = new HashSet<string> { "description" } },
                new() { Required = new HashSet<string> { "url" } },
                new() { Required = new HashSet<string> { "external_id" } }
            }
        };

        var cveRule = new OpenApiSchema
        {
            OneOf = new List<OpenApiSchema>
            {
                new()
                {
                    Not = new OpenApiSchema
                    {
                        Properties = new Dictionary<string, OpenApiSchema>
                        {
                            ["source_name"] = new() { Enum = new List<IOpenApiAny> { new OpenApiString("cve") } }
                        },
                        Required = new HashSet<string> { "source_name" }
                    }
                },
                new()
                {
                    Properties = new Dictionary<string, OpenApiSchema>
                    {
                        ["source_name"] = new() { Enum = new List<IOpenApiAny> { new OpenApiString("cve") } },
                        ["external_id"] = new() { Pattern = @"^CVE-\d{4}-\d{4,}$" }
                    },
                    Required = new HashSet<string> { "source_name", "external_id" }
                }
            }
        };

        if (schema.AllOf == null)
        {
            schema.AllOf = new List<OpenApiSchema>();
        }
        schema.AllOf.Add(anyOfRule);
        schema.AllOf.Add(cveRule);
    }
}