
using Application.Core.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T, TId> : IGenericRepository<T, TId>
        where T:Entity<TId>
        where TId:IEquatable<TId>
    {
        protected readonly BusinessContext _context;
        protected DbSet<T> Entities => _context.Set<T>();

        protected GenericRepository(BusinessContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        => Entities;


        public async Task<T?> GetById(TId Id)
        => await Entities.FirstOrDefaultAsync(e => e.Id.Equals(Id));

        public async Task<bool> HardDelete(TId Id)
        {
            var entity = await GetById(Id);
            if (entity == null) throw new NotFoundException(ProductMessages.ProductByIdNotFound());

            Entities.Remove(entity);

            return true;
        }

        public async Task<T> Insert(T value)
        {
            var result = await Entities.AddAsync(value);

            return result.Entity;
        }

        public async Task<bool> SoftDelete(TId Id)
        {
            var entity = await GetById(Id);
            if (entity == null) throw new NotFoundException(ProductMessages.ProductByIdNotFound());
            entity.IsDeletedChange();
            return Update(entity);
        }

        public bool Update(T value)
        {
            value.LastUpdate();
            Entities.Update(value);
            return true;
        }
    }
}
