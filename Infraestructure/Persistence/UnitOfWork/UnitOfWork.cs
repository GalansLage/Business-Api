using System.Data;
using System.Transactions;
using Application.UnitOfWork;
using Domain.Repositories.ProductItemRepository;
using Domain.Repositories.ProductRepository;
using Domain.Repositories.ProductTransactionRepository;
using Domain.Repositories.ProviderRepository;
using Infrastructure.Core.InfrastructureExceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BusinessContext _context;
        private IDbContextTransaction _transaction;
        private readonly ILogger<UnitOfWork> _logger;
        public IProductRepository ProductRepository { get; }
        public IProductItemRepository ProductItemRepository { get; }
        public IProductTransactionRepository ProductTransactionRepository { get; }
        public IProviderRepository ProviderRepository { get; }


        public UnitOfWork(ILogger<UnitOfWork> logger, IProductItemRepository productItemRepository,IProviderRepository providerRepository, BusinessContext context, IProductRepository productRepository, IProductTransactionRepository productTransactionRepository)
        {
            _context = context;
            ProductRepository = productRepository;
            ProductTransactionRepository = productTransactionRepository;
            _logger = logger;
            ProviderRepository = providerRepository;
            ProductItemRepository = productItemRepository;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }

        public async Task RollBackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction = null;
            }
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            try
            {
                await Save(cancellationToken);
                await _transaction.CommitAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx)
            {
                await RollBackAsync();

                switch (pgEx.SqlState)
                {
                    case "23505": throw new ConflictException("Dato duplicado", pgEx);
                    case "23503": throw new ConflictException("Referencia invalida", pgEx);
                    default: throw new DataException("Error en base de datos", ex);
                }
            }
            catch (TransactionAbortedException ex)
            {
                _logger.LogWarning(ex, "Transacción abortada");
                throw new OperationCanceledException("Operación Capturada", ex);
            }
            catch (Exception ex)
            {
                await RollBackAsync();
                _logger.LogError(ex, "Error no controlado en transacción");
                throw;
            }
        }

        public void Dispose()
        => _context.Dispose();

        public async Task<int> Save(CancellationToken cancellation)
        {
            try
            {
                await _context.SaveChangesAsync(cancellation);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Ha ocurrido error de concurrencia");
            }

            return 0;
        }

        public async Task RollBackAsync(CancellationToken cancellationToken)
        {
            await _transaction.RollbackAsync(cancellationToken);

        }
    }
}
