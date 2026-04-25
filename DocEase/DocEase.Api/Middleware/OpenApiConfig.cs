using Microsoft.OpenApi;
namespace DocEase.Api.Middleware
{
    public static class OpenApiConfig
    {
        public static void AddOpenApiConfig(this IServiceCollection services)
        {
            services.AddOpenApi(c =>
            {
                c.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    // Register the bearer scheme
                    var bearerScheme = new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "JWT Authorization header using the Bearer scheme.",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                    };

                    // Use AddComponent instead of directly setting SecuritySchemes
                    document.AddComponent("bearer", bearerScheme);

                    // Build the security requirement referencing the registered scheme
                    var securityRequirement = new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("bearer", document)] = []
                    };

                    // Apply to every operation individually
                    foreach (var path in document.Paths.Values)
                    {
                        foreach (var operation in path.Operations.Values)
                        {
                            operation.Security ??= new List<OpenApiSecurityRequirement>();
                            operation.Security.Add(securityRequirement);
                        }
                    }

                    return Task.CompletedTask;
                });
            });
        }
    }
}