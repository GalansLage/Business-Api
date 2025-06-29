

using Domain.Core.Enums;
using Domain.Entities.ProductItemEntity;
using Domain.Repositories.ProductItemRepository;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastructure.Persistence.Repositories.ProductItemRepository
{
    public class ProductItemRepository : GenericRepository<ProductItem, int>, IProductItemRepository
    {
        public ProductItemRepository(BusinessContext context) : base(context)
        {
        }

        public async Task<List<ProductItem>> FilterByIdAndState(int Id, int quantity,CancellationToken cancellationToken)
        {
            var tableName = "ProductItem";
            // Los nombres exactos de las columnas en la BD
            var productIdColumn = "ProductId";
            var stateColumn = "ProductState"; // O "ProductState", revisa tu migración
            var requiredState = PState.InStore.ToString().ToUpper(); // Ej: "INSTORE"
            var isDeletedColumn = "IsDeleted";

            var sql =$@"
            SELECT * 
            FROM ""{tableName}""
            WHERE ""{isDeletedColumn}"" = FALSE 
              AND ""{productIdColumn}"" = @productId
              AND UPPER(""{stateColumn}"") = @state
            LIMIT @quantity";

            var parameters = new[]
            {
                new NpgsqlParameter("@productId",Id),
                new NpgsqlParameter("@state",requiredState),
                new NpgsqlParameter("@quantity",quantity)
            };

            return await _context.ProductItems.FromSqlRaw(sql, parameters).ToListAsync(cancellationToken);
        }
        

        public async Task<ProductItem?> GetProductItemById(int Id)
            => await _context.ProductItems.Include(pi => pi.Product).ThenInclude(p=>p.Provider).FirstOrDefaultAsync(pi => pi.Id == Id);
            
        

        public async Task<bool> InsetProductsItems(List<ProductItem> productItems)
        {
            await _context.AddRangeAsync(productItems);
            return true;
        }
    }
}
