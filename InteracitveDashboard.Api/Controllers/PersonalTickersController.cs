using InteractiveDashboard.Application.Extensions;
using InteractiveDashboard.Application.Handlers.PersonalTickers;
using InteractiveDashboard.Application.Handlers.Tickers;
using InteractiveDashboard.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveDashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PersonalTickersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonalTickersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var command = new GetPersonalTickersCommand { UserEmail = User.GetEmail() };
            var dto = await _mediator.Send(command);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UpdatePersonalTickersDto dto)
        {
            var command = new UpdatePersonalTickersCommand { UserEmail = User.GetEmail(), Tickers = dto.Tickers };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
