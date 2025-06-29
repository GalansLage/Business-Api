using Application.DTOs;
using Application.Features.Transactions.Commands.AddNewProducts;
using Application.Features.Transactions.Commands.CancelTransaction;
using Application.Features.Transactions.Commands.ConfirmTransaction;
using Application.Features.Transactions.Commands.DeleteProducts;
using Application.Features.Transactions.Commands.StartTransaction;
using Application.Features.Transactions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Business_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTransactionController:ControllerBase
    {
        private readonly ISender _mediator;

        public ProductTransactionController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetTransaction")]
        public async Task<ActionResult<TransactionDto>> GetTransaction()
        {
            var query = new GetTransactionQuery();

            return  Ok(ApiResponse<TransactionDto>.Success(await _mediator.Send(query)));
        }


        [HttpPost("StartTransaction")]
        public async Task<ActionResult<bool>> StartTransaction([FromBody] StartTransactionCommand command)
            => Ok(ApiResponse<bool>.Success(await _mediator.Send(command)));

        [HttpDelete("CancelTransaction")]
        public async Task<ActionResult<bool>> CancelTransaction()
        {
            var command = new CancelTransactionCommand();

            return Ok(ApiResponse<bool>.Success(await _mediator.Send(command)));
        }

        [HttpDelete("ConfirmTransaction")]
        public async Task<ActionResult<decimal>> ConfirmTransaction()
        {
            var command = new ConfirmTransactionCommand();

            return Ok(ApiResponse<decimal>.Success(await _mediator.Send(command)));
        }

        [HttpPut("AddNewProducts")]
        public async Task<ActionResult<bool>> AddNewProducts([FromBody] AddNewProductsCommand command)
            => Ok(ApiResponse<bool>.Success(await _mediator.Send(command)));

        [HttpPut("DeleteProducts")]
        public async Task<ActionResult<bool>> DeleteProducts([FromBody] DeleteProductsCommand command)
            => Ok(ApiResponse<bool>.Success(await _mediator.Send(command)));
    }
}
