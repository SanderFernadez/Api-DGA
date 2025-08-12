# 📋 Pasos para Implementar la API-DGA

## 🎯 Objetivo
Este documento detalla los pasos necesarios para completar la implementación de la API de gestión de productos y ventas, siguiendo las mejores prácticas y la arquitectura establecida.

## 📋 Checklist de Implementación

### ✅ **FASE 1: Configuración Inicial (COMPLETADO)**
- [x] Estructura de capas configurada
- [x] Entidades del dominio creadas
- [x] DTOs implementados
- [x] Repositorios genéricos y específicos creados
- [x] Servicios genéricos y específicos implementados
- [x] Interfaces definidas

### ✅ **FASE 2: Configuración de Base de Datos (COMPLETADO)**
- [x] **Configurar Entity Framework Core**
  ```bash
  # Instalar paquetes necesarios en APi-DGA-Infrastructure
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer
  dotnet add package Microsoft.EntityFrameworkCore.Tools
  dotnet add package Microsoft.EntityFrameworkCore.Design
  ```

- [x] **Crear Migrations**
  ```bash
  cd APi-DGA-Infrastructure
  dotnet ef migrations add InitialCreate --startup-project ../Api-DGA
  dotnet ef database update --startup-project ../Api-DGA
  ```

- [x] **Configurar cadena de conexión en appsettings.json**
  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ApiDGA;Trusted_Connection=true;MultipleActiveResultSets=true"
    }
  }
  ```

### ✅ **FASE 3: Configuración de AutoMapper (COMPLETADO)**
- [x] **Instalar AutoMapper**
  ```bash
  cd Api-DGA.Application
  dotnet add package AutoMapper
  dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
  ```

- [x] **Crear perfiles de mapeo en Api-DGA.Application/Mappings/**
  - [x] `ClientMappingProfile.cs`
  - [x] `ProductMappingProfile.cs`
  - [x] `SaleMappingProfile.cs`
  - [x] `SaleProductMappingProfile.cs`

- [x] **Registrar AutoMapper en Program.cs**
  ```csharp
  builder.Services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
  ```

### ✅ **FASE 4: Implementación de Controllers (COMPLETADO)**
- [x] **Crear Controllers en Api-DGA/Controllers/**
  - [x] `ProductsController.cs`
  - [x] `ClientsController.cs`
  - [x] `SalesController.cs`
  - [ ] `SaleProductsController.cs`

- [x] **Implementar endpoints CRUD básicos**
  ```csharp
  [HttpGet]
  [HttpGet("{id}")]
  [HttpPost]
  [HttpPut("{id}")]
  [HttpDelete("{id}")]
  ```

- [x] **Implementar endpoints de búsqueda y filtros**
  ```csharp
  [HttpGet("search")]
  [HttpGet("low-stock")]
  [HttpGet("client/{clientId}")]
  ```

### 🔄 **FASE 5: Validaciones y Manejo de Errores**
- [ ] **Instalar FluentValidation**
  ```bash
  cd Api-DGA.Application
  dotnet add package FluentValidation.AspNetCore
  ```

- [ ] **Crear validadores en Api-DGA.Application/Validators/**
  - [ ] `CreateProductDtoValidator.cs`
  - [ ] `UpdateProductDtoValidator.cs`
  - [ ] `CreateClientDtoValidator.cs`
  - [ ] `UpdateClientDtoValidator.cs`
  - [ ] `CreateSaleDtoValidator.cs`
  - [ ] `UpdateSaleDtoValidator.cs`

- [ ] **Implementar middleware de manejo de errores**
  ```csharp
  // En Program.cs
  app.UseExceptionHandler("/error");
  ```

### ✅ **FASE 6: Configuración de Swagger (COMPLETADO)**
- [x] **Instalar Swagger**
  ```bash
  cd Api-DGA
  dotnet add package Swashbuckle.AspNetCore
  ```

- [x] **Configurar Swagger en Program.cs**
  ```csharp
  builder.Services.AddSwaggerGen(c =>
  {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "API-DGA", Version = "v1" });
  });
  ```

- [x] **Configurar Swagger UI**
  ```csharp
  app.UseSwagger();
  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API-DGA v1"));
  ```

### ✅ **FASE 7: Registro de Servicios (COMPLETADO)**
- [x] **Completar ServiceRegistration.cs en Api-DGA.Application**
  ```csharp
  public static class ServiceRegistration
  {
      public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
      {
          // Registrar AutoMapper con los perfiles de mapeo
          services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

          #region Services
          services.AddTransient(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));
          services.AddTransient<IClientService, ClientService>();
          services.AddTransient<IProductService, ProductService>();
          services.AddTransient<ISaleService, SaleService>();
          services.AddTransient<ISaleProductService, SaleProductService>();
          #endregion
      }
  }
  ```

- [x] **Completar ServiceRegistration.cs en APi-DGA-Infrastructure**
  ```csharp
  public static class ServiceRegistration
  {
      public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
      {
          #region Contexts
          services.AddDbContext<InfrastructureContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
          #endregion

          #region Repositories
          services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
          services.AddTransient<IClientRepository, ClientRepository>();
          services.AddTransient<IProductRepository, ProductRepository>();
          services.AddTransient<ISaleRepository, SaleRepository>();
          services.AddTransient<ISaleProductRepository, SaleProductRepository>();
          #endregion
      }
  }
  ```

### 🔄 **FASE 8: Pruebas Unitarias**
- [ ] **Crear proyecto de pruebas**
  ```bash
  dotnet new xunit -n Api-DGA.Tests
  ```

- [ ] **Instalar paquetes de testing**
  ```bash
  cd Api-DGA.Tests
  dotnet add package Moq
  dotnet add package FluentAssertions
  dotnet add package Microsoft.NET.Test.Sdk
  ```

- [ ] **Crear pruebas para servicios**
  - [ ] `ClientServiceTests.cs`
  - [ ] `ProductServiceTests.cs`
  - [ ] `SaleServiceTests.cs`

- [ ] **Crear pruebas para validadores**
  - [ ] `CreateProductDtoValidatorTests.cs`
  - [ ] `CreateClientDtoValidatorTests.cs`

### 🔄 **FASE 9: Configuración de Logging**
- [ ] **Configurar Serilog**
  ```bash
  cd Api-DGA
  dotnet add package Serilog.AspNetCore
  dotnet add package Serilog.Sinks.File
  ```

- [ ] **Configurar logging en Program.cs**
  ```csharp
  Log.Logger = new LoggerConfiguration()
      .WriteTo.Console()
      .WriteTo.File("logs/api-dga-.txt", rollingInterval: RollingInterval.Day)
      .CreateLogger();
  ```

### 🔄 **FASE 10: Optimizaciones y Mejoras**
- [ ] **Implementar caché**
  ```bash
  dotnet add package Microsoft.Extensions.Caching.Memory
  ```

- [ ] **Configurar CORS**
  ```csharp
  builder.Services.AddCors(options =>
  {
      options.AddPolicy("AllowAll", builder =>
          builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
  });
  ```

- [ ] **Implementar rate limiting**
  ```bash
  dotnet add package Microsoft.AspNetCore.RateLimiting
  ```

## 🚀 Comandos de Ejecución

### Desarrollo
```bash
# Restaurar dependencias
dotnet restore

# Ejecutar migraciones
cd Api-DGA
dotnet ef database update

# Ejecutar la aplicación
dotnet run

# Ejecutar pruebas
dotnet test
```

### Producción
```bash
# Publicar la aplicación
dotnet publish -c Release

# Ejecutar migraciones en producción
dotnet ef database update --environment Production
```

## 📁 Estructura de Archivos Esperada

```
Api-DGA/
├── Api-DGA.Core/
│   ├── Entities/
│   │   ├── Client.cs ✅
│   │   ├── Product.cs ✅
│   │   ├── Sale.cs ✅
│   │   └── SaleProducts.cs ✅
│   └── Common/
│       └── CommontFields.cs ✅
├── Api-DGA.Application/
│   ├── Dtos/
│   │   ├── ClientDtos.cs ✅
│   │   ├── ProductDtos.cs ✅
│   │   ├── SaleDtos.cs ✅
│   │   ├── SaleProductDtos.cs ✅
│   │   └── CommonDtos.cs ✅
│   ├── Interfaces/
│   │   ├── Repositories/ ✅
│   │   └── Services/ ✅
│   ├── Services/ ✅
│   ├── Mappings/ 🔄
│   └── Validators/ 🔄
├── APi-DGA-Infrastructure/
│   ├── Contexts/
│   │   └── InfrastructureContext.cs ✅
│   ├── Repositories/ ✅
│   └── ServiceRegistration.cs 🔄
├── Api-DGA/
│   ├── Controllers/ 🔄
│   ├── Program.cs 🔄
│   └── appsettings.json 🔄
└── Api-DGA.Tests/ 🔄
```

## 🔍 Verificación de Implementación

### Checklist de Verificación
- [ ] La aplicación inicia sin errores
- [ ] Swagger UI es accesible en `/swagger`
- [ ] Los endpoints responden correctamente
- [ ] Las validaciones funcionan
- [ ] Los errores se manejan apropiadamente
- [ ] Las pruebas unitarias pasan
- [ ] La base de datos se crea correctamente
- [ ] Los logs se generan

### Endpoints de Prueba
```bash
# Probar endpoints básicos
curl -X GET "https://localhost:7001/api/products"
curl -X GET "https://localhost:7001/api/clients"
curl -X GET "https://localhost:7001/api/sales"

# Probar creación
curl -X POST "https://localhost:7001/api/products" \
  -H "Content-Type: application/json" \
  -d '{"name":"Producto Test","description":"Descripción","price":100.00,"stock":10}'
```

## 📝 Notas Importantes

### Comentarios en Español
- Todos los comentarios deben estar en español
- Documentar métodos públicos con XML comments
- Incluir ejemplos de uso en la documentación

### Principios SOLID
- Mantener separación de responsabilidades
- Usar inyección de dependencias
- Evitar acoplamiento entre capas
- Implementar interfaces para abstracción

### Manejo de Errores
- No usar fallbacks por defecto sin especificación del usuario
- Proporcionar mensajes de error claros y descriptivos
- Implementar logging para debugging

---

**Estado Actual**: ✅ Fase 1-7 Completadas | 🔄 Fase 8 en Progreso
