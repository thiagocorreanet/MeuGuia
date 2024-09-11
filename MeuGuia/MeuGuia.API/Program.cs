using MeuGuia.Application.Mapping;
using MeuGuia.CrossCutting;
using MeuGuia.WebAPI.Extension;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwagger();
builder.Services.AddContext(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("Development",
        builder =>
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

    options.AddPolicy("Production",
        builder =>
            builder
                .WithMethods("GET")
                .WithOrigins("http://localhost:5000")
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ResolveDependencies();
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));


//JWT Authentication configuration
//var appSettingsSection = builder.Configuration.GetSection("AppSettingsJwt");
//builder.Services.Configure<JsonWebToken>(appSettingsSection);

//var appSettings = appSettingsSection.Get<JsonWebToken>();
//var key = Encoding.ASCII.GetBytes(appSettings.Secret);


//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x =>
//{
//    x.RequireHttpsMetadata = false;
//    x.SaveToken = true;
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidAudience = appSettings.ValidIn,
//        ValidIssuer = appSettings.Issuer
//    };
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Development");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();