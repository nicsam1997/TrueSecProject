using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TrueSecProject.Models;

namespace TrueSecProject.Swagger
{
    public class DefaultValuesSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(LoginAttempt))
            {
                var usernameProp = schema.Properties.Keys
                    .FirstOrDefault(k => k.Equals("username", StringComparison.OrdinalIgnoreCase));

                if (usernameProp != null)
                {
                    schema.Properties[usernameProp].Example = new OpenApiString("admin");
                }

                var passwordProp = schema.Properties.Keys
                    .FirstOrDefault(k => k.Equals("password", StringComparison.OrdinalIgnoreCase));

                if (passwordProp != null)
                {
                    schema.Properties[passwordProp].Example = new OpenApiString("AdminPassword123!");
                }
            }
            else if (context.Type == typeof(Vulnerability))
            {
                schema.Example = CreateVulnerabilityExample();
            }
        }
        private static OpenApiObject CreateVulnerabilityExample()
        {
            return new OpenApiObject
            {
                ["type"] = new OpenApiString("vulnerability"),
                ["id"] = new OpenApiString("vulnerability--c3AdBcda-e4a7-2352-AeA7-84ebabba4ac5"),
                ["name"] = new OpenApiString("string"),
                ["description"] = new OpenApiString("string"),
                ["spec_version"] = new OpenApiString("2.1"),
                ["created_by_ref"] = new OpenApiString("identity--f7B51670-605a-4A04-959f-A8adfe617727"),
                ["labels"] = new OpenApiArray { new OpenApiString("string") },
                ["created"] = new OpenApiString("3199-09-30T01:55:00Z"),
                ["modified"] = new OpenApiString("2490-11-05T17:12:59Z"),
                ["revoked"] = new OpenApiBoolean(true),
                ["confidence"] = new OpenApiInteger(100),
                ["lang"] = new OpenApiString("en"),
                ["external_references"] = new OpenApiArray
                        {
                            new OpenApiObject
                            {
                                ["source_name"] = new OpenApiString("cve"),
                                ["hashes"] = new OpenApiObject
                                {
                                    ["SHA-256"] = new OpenApiString("ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad")
                                },
                                ["description"] = new OpenApiString("string"),
                                ["url"] = new OpenApiString("https://www.google.se"),
                                ["external_id"] = new OpenApiString("CVE-2017-0144")
                            }
                        },
                ["object_marking_refs"] = new OpenApiArray
                        {
                            new OpenApiString("marking-definition--7038c0fe-3B8a-5234-bAd1-d1E1379D2fF5")
                        },
                ["granular_markings"] = new OpenApiArray
                        {
                            new OpenApiObject
                            {
                                ["marking_ref"] = new OpenApiString("marking-definition--7038c0fe-3B8a-5234-bAd1-d1E1379D2fF5"),
                                ["selectors"] = new OpenApiArray
                                {
                                    new OpenApiString("labels[0]")
                                }
                            }
                        }
            };
        }
    }
}