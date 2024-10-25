using InteractiveDashboard.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveDashboard.Application.Handlers.Tickers
{
    public class GetAllTickersHandler : IRequestHandler<GetAllTickersCommand, List<string>>
    {
        private readonly ITickerService _tickerService;

        public GetAllTickersHandler(ITickerService tickerService)
        {
            _tickerService = tickerService;
        }

        Task<List<string>> IRequestHandler<GetAllTickersCommand, List<string>>.Handle(GetAllTickersCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_tickerService.GetAllTickers());
        }
    }
}
