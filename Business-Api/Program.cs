
using Application.Core.Behaviors;
using Application.UnitOfWork;
using Business_Api.Middleware;
using Domain.Repositories;
using Domain.Repositories.ProductItemRepository;
using Domain.Repositories.ProductRepository;
using Domain.Repositories.ProductTransactionRepository;
using Domain.Repositories.ProviderRepository;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.ProductItemRepository;
using Infrastructure.Persistence.Repositories.ProductTransactionRepository;
using Infrastructure.Persistence.Repositories.ProviderRepository;
using Infrastructure.Persistence.UnitOfWork;
using Infrastructure.Persistence.Repositories.ProductRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories.DailySalesReportRepository;
using Infrastructure.Persistence.Repositories.DailySalesReportRepository;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de CORS para Render y desarrollo local
var corsAllowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? new[] {
        "https://business-api-1xlx.onrender.com",
        "http://localhost:5173",
         "https://localhost:5173"
    };

builder.Services.AddCors(options =>
{
    options.AddPolicy("RenderCorsPolicy", policy =>
    {
        policy.WithOrigins(corsAllowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();

        // Espec�fico para PostgreSQL y Swagger`
        policy.WithExposedHeaders("Content-Disposition");
    });
});

// Carga la configuraci�n seg�n el entorno
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BusinessContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("BusinessConnection") ?? throw new NotImplementedException()));


//Injection
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductItemRepository, ProductItemRepository>();
builder.Services.AddScoped<IProductTransactionRepository, ProductTransactionRepository>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IDailySalesReportRepository, DailySalesReportRepository>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// --- Aplicar migraciones al inicio ---
// Esto es crucial para que Render pueda crear el esquema de la base de datos.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<BusinessContext>();
        // Si la base de datos no es la de en memoria (para tests), aplica la migraci�n.
        if (dbContext.Database.IsNpgsql())
        {
            await dbContext.Database.MigrateAsync();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurri� un error durante la migraci�n de la base de datos.");
        // Considera no iniciar la app si la migraci�n falla.
        // throw; 
    }
}


// ---- INICIO DEL C�DIGO DE DIAGN�STICO ----
app.Use(async (context, next) =>
{
    // Imprime en la consola de depuraci�n la informaci�n de la petici�n entrante
    Console.WriteLine($"--> [DIAGN�STICO] Petici�n recibida: {context.Request.Method} {context.Request.Path}");

    // Pasa la petici�n al siguiente middleware en la cadena
    await next.Invoke();
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Esto asegura que Swagger funcione correctamente detr�s de un proxy.
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Business API V1");
    c.RoutePrefix = string.Empty; // Para que Swagger UI est� en la ra�z (opcional)
});

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


