using InteractiveDashboard.Application.Services;
using MediatR;

namespace InteractiveDashboard.Application.Handlers.Tickers
{
    public class GetAllTickersHandler : IRequestHandler<GetAllTickersCommand, List<string>>
    {
        private readonly ITickerService _tickerService;

        public GetAllTickersHandler(ITickerService tickerService)
        {
            _tickerService = tickerService;
        }

        public Task<List<string>> Handle(GetAllTickersCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
