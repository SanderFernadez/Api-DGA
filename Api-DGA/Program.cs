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
        Description = "API para gestión de productos y ventas",
        Contact = new OpenApiContact
        {
            Name = "API-DGA Team",
            Email = "support@api-dga.com"
        }
    });
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
               .AllowAnyHeader());
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

app.Run();
