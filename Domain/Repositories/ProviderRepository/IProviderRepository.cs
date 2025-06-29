

using Domain.Entities.ProviderEntity;

namespace Domain.Repositories.ProviderRepository
{
    public interface IProviderRepository:IGenericRepository<Provider,int>
    {
        IQueryable<Provider> FilterByName(string name);
    }
}
