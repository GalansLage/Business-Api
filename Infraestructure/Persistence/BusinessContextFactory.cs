
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class BusinessContextFactory: IDesignTimeDbContextFactory<BusinessContext>
    {
        public BusinessContext CreateDbContext(string[] args)
        {
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Business-Api");

            // Si la ruta no funciona, prueba con una ruta más explícita
            // string basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "/Business-Api";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BusinessContext>();
            var connectionString = configuration.GetConnectionString("BusinessConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("No se encontró la cadena de conexión 'BusinessConnection' en appsettings.json.");
            }

            builder.UseNpgsql(connectionString, b =>
                b.MigrationsAssembly(typeof(BusinessContext).Assembly.GetName().Name));

            return new BusinessContext(builder.Options);
        }
        }
}
