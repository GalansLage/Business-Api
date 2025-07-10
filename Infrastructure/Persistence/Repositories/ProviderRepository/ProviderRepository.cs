
using System.Text;
using Domain.Entities.ProviderEntity;
using Domain.Repositories.ProviderRepository;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastructure.Persistence.Repositories.ProviderRepository
{
    public class ProviderRepository : GenericRepository<Provider, int>, IProviderRepository
    {
        public ProviderRepository(BusinessContext context) : base(context)
        {
        }

        public IQueryable<Provider> FilterByName(string name)
        {
            var parameters = new List<NpgsqlParameter>();
            var whereClause = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(name))
            {
                whereClause.Append(" WHERE \"ProviderName\" ILIKE @searchName");
                parameters.Add(new NpgsqlParameter("@searchName", $"%{name}%"));
            }

            var query = _context.Providers.FromSqlRaw($"SELECT * FROM \"Provider\"{whereClause}", parameters.ToArray());

            return query;
        }

        public async Task<Provider?> GetProviderWithProduct(int Id)
            => await _context.Providers.Include(p => p.Products).FirstOrDefaultAsync(pr => pr.Id == Id);

        
    }
}
