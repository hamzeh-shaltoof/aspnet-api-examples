using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace M01.BaselineAPIProjectController.OpenApi.Transformers
{
    public sealed class BearerSecuritySchemeDocumentTransformer : IOpenApiDocumentTransformer
    {
        private const string SchemeId = JwtBearerDefaults.AuthenticationScheme;
        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            document.Components ??= new();
            document.Components.SecuritySchemes = new Dictionary<string,OpenApiSecurityScheme>();

            document.Components.SecuritySchemes[SchemeId] = new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT Bearer Token",
                Name = "Authorization",
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = SchemeId,
                }
            };

            return Task.CompletedTask;
        }
    }
}
