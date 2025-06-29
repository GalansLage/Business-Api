
using Application.Core.Exceptions;
using Application.DTOs;
using Application.Features.Transactions.Queries;
using Application.Mappers;
using Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Queries.Transaction
{
    public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, TransactionDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTransactionQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TransactionDto> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.ProductTransactionRepository.GetAll()
                .Include(t=>t.Products).ThenInclude(pi=>pi.Product).ThenInclude(p=>p.Provider).FirstOrDefaultAsync(cancellationToken);

            if (transaction == null) throw new NotFoundException("No se esta realizando ninguna transacción. ");

            var transactionDto = TransactionMapper.ToDto(transaction);

            return transactionDto;

        }
    }
}
