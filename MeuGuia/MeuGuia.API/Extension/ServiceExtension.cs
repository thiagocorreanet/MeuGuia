using MeuGuia.Infra.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerUI;

using System.Reflection;

namespace MeuGuia.WebAPI.Extension;

public static class ServiceExtension
{
    public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MeuGuiaContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(MeuGuiaContext).Assembly.FullName)));

        return services;
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            // Configuração básica do Swagger
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "WebAPI",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Meu guia",
                    Url = new Uri("https://www.meuguia.com.br/")
                },
                Description = @"Seja muito bem-vindo à nossa documentação do Swagger! Estamos felizes em tê-lo aqui. Nesta documentação, você encontrará uma descrição completa e detalhada de todos os endpoints disponíveis em nossa API. Esse projeto consiste até o momento em organizar de forma pratica nossas finanças pessoais com entrada e saída."
            });

            // Inclui comentários XML para documentação
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            // Configuração da autenticação JWT no Swagger
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Insira o token JWT desta maneira: Bearer {seu token aqui}"
            };

            c.AddSecurityDefinition("Bearer", securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    securityScheme,
                    new List<string>()
                }
            };

            c.AddSecurityRequirement(securityRequirement);
        });
    }

    public static void UseSwaggerUI(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DocumentTitle = "Meu guia Web API";
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");

            c.DefaultModelExpandDepth(2);
            c.DefaultModelRendering(ModelRendering.Model);
            c.DefaultModelsExpandDepth(-1);
            c.DisplayOperationId();
            c.DisplayRequestDuration();
            c.EnableDeepLinking();
            c.EnableFilter();
            c.ShowExtensions();
            c.EnableValidator();
            c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete);
            c.EnableDeepLinking();
        });
    }
}
