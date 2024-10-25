using InteractiveDashboard.Application.InfrastructureServices;
using InteractiveDashboard.Domain.Dtos;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace InteractiveDashboard.Application.Services
{
    public class TickerService : ITickerService
    {
        ConcurrentDictionary<string, TickerDto> _tickers = new();
        private readonly IDateTimeService _dateTimeService;
        private readonly IHubContext<TickerHub, IPriceTickerClient> _hubContext;

        public TickerService(IHubContext<TickerHub, IPriceTickerClient> hubContext, IDateTimeService dateTimeService)
        {
            _hubContext = hubContext;
            _dateTimeService = dateTimeService;
        }

        public TickerDto GetTicker(string tickerName)
        {
            return _tickers[tickerName];
        }
     
        public async Task PushPrice(string tickerNamne, decimal askPrice, decimal bidPrice)
        {
            var dto = _tickers.GetOrAdd(tickerNamne, new TickerDto { SymbolName = tickerNamne });
            dto.Ask = askPrice;
            dto.Bid = bidPrice;
            dto.PriceDate = _dateTimeService.GetCurrentDateTime();
            await _hubContext.Clients.Group(tickerNamne).PriceUpdate(dto);
        }

        public List<string> GetAllTickers()
        {
            return _tickers.Keys.ToList();
        }
    }
}
