

namespace Domain.Repositories
{
    public interface IGenericRepository<T,Tid>
    {
        IQueryable<T> GetAll();
        Task<T?> GetById(Tid Id);
        Task<T> Insert(T value);
        Task<bool> SoftDelete(Tid Id);
        Task<bool> HardDelete(Tid Id);
        bool Update(T value);
    }
}
