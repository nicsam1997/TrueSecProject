using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TrueSecProject.Swagger;

public class GranularMarkingSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type != typeof(GranularMarking))
        {
            return;
        }

        var oneOfRule = new OpenApiSchema
        {
            OneOf = new List<OpenApiSchema>
            {
                new()
                {
                    Required = new HashSet<string> { "lang" }
                },
                new()
                {
                    Required = new HashSet<string> { "marking_ref" }
                }
            }
        };

        if (schema.AllOf == null)
        {
            schema.AllOf = new List<OpenApiSchema>();
        }
        schema.AllOf.Add(oneOfRule);
    }
}