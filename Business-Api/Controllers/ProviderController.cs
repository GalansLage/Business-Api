using Application.Core;
using Application.DTOs;
using Application.Features.Providers.Commands.CreateProvider;
using Application.Features.Providers.Commands.HardDeleteProvider;
using Application.Features.Providers.Commands.SoftDelete;
using Application.Features.Providers.Commands.UpdateProvider;
using Application.Features.Providers.Queries.FilterByName;
using Application.Features.Providers.Queries.GetAllProviders;
using Application.Features.Providers.Queries.GetProviderById;
using Business_Api.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Business_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController:ControllerBase
    {
        private readonly ISender _mediator;

        public ProviderController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<ProviderWithStockDto>>> GetAllProviders([FromQuery] GetAllProvidersQuery getAllProvidersQuery)
            => Ok(ApiResponse<PagedResponse<ProviderWithStockDto>>.Success(await _mediator.Send(getAllProvidersQuery)));

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProviderDto>> GetProviderById([FromRoute] GetProviderByIdQuery getProviderByIdQuery)
            => Ok(ApiResponse<ProviderDto>.Success(await _mediator.Send(getProviderByIdQuery)));

        [HttpGet("FilterByName")]
        public async Task<ActionResult<ProviderDto>> FilterByName([FromQuery] FilterProviderByNameQuery query)
            => Ok(ApiResponse<PagedResponse<ProviderDto>>.Success(await _mediator.Send(query)));
        
        [HttpPost]
        public async Task<ActionResult<ProviderDto>> InsertProvider([FromBody] CreateProviderCommand command)
        {
            var provider = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProviderById), new { provider.Id }, provider);
        }
        [HttpDelete("HardDelete/{Id}")]
        public async Task<ActionResult<bool>> HardDelete([FromRoute] HardDeleteProviderCommand command)
            => Ok(ApiResponse<bool>.Success(await _mediator.Send(command)));

        [HttpDelete("SoftDelete/{Id}")]
        public async Task<ActionResult<bool>> SoftDelete([FromRoute] SoftDeleteProviderCommand command)
            => Ok(ApiResponse<bool>.Success(await _mediator.Send(command)));

        [HttpPut("Update/{Id}")]
        public async Task<ActionResult<bool>> Update([FromRoute]int Id, [FromBody]ProviderUpdateDto updateDto)
        {
            var command = new UpdateProviderCommand(
                Id, updateDto.providerName
                , updateDto.providerLastName
                ,updateDto.cI,updateDto.number);

            return Ok(ApiResponse<bool>.Success(await _mediator.Send(command)));
        }

    }
}
