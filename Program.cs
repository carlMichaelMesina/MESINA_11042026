
using MESINA_11042026.Middleware;
using MESINA_11042026.Services;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Enter your API Key. The key will be sent using the X-API-KEY header.",
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("ApiKey", document)] = []
        });
});

builder.Services.AddSingleton<IFileTrackingService, FileTrackingService>();
builder.Services.AddTransient<IFileProcessor, JsonFileProcessor>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyAuthMiddleware>();

app.MapControllers();

app.Run();
