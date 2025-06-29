
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

var builder = WebApplication.CreateBuilder(args);

// Carga la configuración según el entorno
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
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();


// ---- INICIO DEL CÓDIGO DE DIAGNÓSTICO ----
app.Use(async (context, next) =>
{
    // Imprime en la consola de depuración la información de la petición entrante
    Console.WriteLine($"--> [DIAGNÓSTICO] Petición recibida: {context.Request.Method} {context.Request.Path}");

    // Pasa la petición al siguiente middleware en la cadena
    await next.Invoke();
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


