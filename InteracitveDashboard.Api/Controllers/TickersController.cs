using InteractiveDashboard.Application.Handlers.Tickers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InteractiveDashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TickersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TickersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{tickerName}")]
        public async Task<IActionResult> GetTicker(string tickerName)
        {
            var command = new GetTckerCommand { TickerName = tickerName };
            var dto = await _mediator.Send(command);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetTickers()
        {
            var command = new GetAllTickersCommand();
            var dto = await _mediator.Send(command);
            return Ok(dto);
        }
    }
}
