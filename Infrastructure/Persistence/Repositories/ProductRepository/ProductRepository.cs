

using Application.Core.Exceptions;
using Domain.Entities.ProductEntity;
using Domain.Repositories.ProductRepository;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.ProductRepository
{
    public class ProductRepository : GenericRepository<Product, int>, IProductRepository
    {
        public ProductRepository(BusinessContext context) : base(context)
        {
        }

        public async Task<Product> FindByName(string name)
        {
            // El nombre de la tabla debe coincidir exactamente con el de la base de datos.
            // Por tu configuración, parece ser "Product".
            var tableName = "Product";

            // El nombre de la columna también debe ser exacto.
            // Por tu configuración, parece ser "ProductName".
            var columnName = "ProductName";

            // Creamos la consulta SQL. Usamos ILIKE para la comparación case-insensitive.
            // El @name es un marcador de parámetro para prevenir inyección SQL.
            var sql = $"SELECT * FROM \"{tableName}\" WHERE \"{columnName}\" ILIKE @name LIMIT 1";

            // Creamos el parámetro. NpgsqlParameter es específico de PostgreSQL.
            var nameParam = new Npgsql.NpgsqlParameter("@name", name);

            // Ejecutamos la consulta.
            // FromSqlRaw le dice a EF que ejecute este SQL y que mapee los resultados
            // a la entidad 'Product'. EF usará tus ValueConverters para materializar
            // los objetos correctamente.
            return await _context.Products
                                 .FromSqlRaw(sql, nameParam)
                                 .FirstOrDefaultAsync();
        }
       
        


        public async Task<Product> GetProductByIdWithProvider(int Id)
        => await _context.Products.Include(p => p.Provider).FirstOrDefaultAsync(x => x.Id.Equals(Id))??
            throw new NotFoundException("No se encontró ningun producto con ese id");
           
    }
}
