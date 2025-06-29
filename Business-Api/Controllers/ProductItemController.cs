using Application.Core;
using Application.DTOs;
using Application.Features.ProductItems.Commands.CreateProductItem;
using Application.Features.ProductItems.Commands.DeleteProductItem.HardDelete;
using Application.Features.ProductItems.Commands.DeleteProductItem.SoftDelete;
using Application.Features.ProductItems.Queries.GetAllProductsItems;
using Application.Features.ProductItems.Queries.GetProductItemById;
using Business_Api.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Business_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductItemController : ControllerBase
    {
        private readonly ISender _mediator;

        public ProductItemController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<PagedResponse<ProductItemDto>>> GetAll([FromQuery] GetAllProductItemQuery query)
            => Ok(ApiResponse<PagedResponse<ProductItemDto>>.Success(await _mediator.Send(query)));


        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<ProductItemDto>> GetById([FromRoute] GetProductItemByIdQuery query)
            => Ok(ApiResponse<ProductItemDto>.Success(await _mediator.Send(query)));

        [HttpPost("InsertItems/{productId}")]
        public async Task<ActionResult<string>> PostProductsItems([FromRoute]int productId, [FromBody]ProductItemInsertDto itemInsertDto)
        {
            var command = new CreateProductItemCommand(itemInsertDto.productCode,productId, itemInsertDto.itemsAmount);

            return Ok(ApiResponse<string>.Success(await _mediator.Send(command)));
        }

        [HttpDelete("HardDelete/{Id}")]
        public async Task<ActionResult<bool>> HardDelete([FromRoute] HardDeleteProductItemCommand command)
            => Ok(ApiResponse<bool>.Success(await _mediator.Send(command)));

        [HttpDelete("SoftDelete/{Id}")]
        public async Task<ActionResult<bool>> SoftDelete([FromRoute] SoftDeleteProductItemCommand command)
            => Ok(ApiResponse<bool>.Success(await _mediator.Send(command)));

    }
}
