using Api_DGA.Application;
using APi_DGA_Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "API-DGA", 
        Version = "v1",
        Description = "API REST para gestión de productos y ventas. Desarrollada con .NET 8, Entity Framework Core y SQL Server.",
        Contact = new OpenApiContact
        {
            Name = "API-DGA Team",
            Email = "support@api-dga.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
    
    // Configurar esquemas de respuesta
    c.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date-time" });
    c.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" });
});

// Registrar servicios de infraestructura y aplicación
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("Content-Disposition")); // Para descargas de archivos
    
    // Política específica para desarrollo
    options.AddPolicy("Development", builder =>
        builder.WithOrigins("http://localhost:3000", "http://localhost:8080", "http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());
});

builder.Services.AddHealthChecks();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API-DGA v1");
        c.RoutePrefix = "swagger";
    });
}

// Configurar manejo de errores global
app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

// Configurar CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

// Ejecutar seeding automático
await app.Services.RunAsyncSeed();

app.Run();
