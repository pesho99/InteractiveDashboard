using InteractiveDashboard.Application.Services;
using InteractiveDashboard.Domain.Dtos;
using MediatR;

namespace InteractiveDashboard.Application.Handlers.Tickers
{
    public class GetTIckerHandler : IRequestHandler<GetTckerCommand, TickerDto>
    {
        private readonly ITickerService _tickerService;

        public GetTIckerHandler(ITickerService tickerService)
        {
            _tickerService = tickerService;
        }

        public Task<TickerDto> Handle(GetTckerCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_tickerService.GetTicker(request.TickerName));
        }
    }
}
