using InteractiveDashboard.Application.Settings;
using InteractiveDashboard.Domain.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace InteractiveDashboard.Application.Services
{
    public class PriceTicker : BackgroundService
    {
        readonly int _delay;
        readonly IEnumerable<string> _tickers;
        private readonly List<decimal> prices = new() { 13, 10.12m, 33.45m, 17.89m, 14.3m, 125.2m, 14.45m, 14.54m, 14.20m, 27.80m, 16.14m, 67.15m, 15.6m, 111, 0.54m };
        Random rnd = new Random();

        private readonly ITickerService _tickerService;
        public PriceTicker(IOptions<PriceTickerSetttings> options, ITickerService tickerService)
        {
            _delay = options.Value.DelayInMilliseconds;
            _tickers = options.Value.SupportedTickers;
            _tickerService = tickerService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await GerneratePrices();
                await Task.Delay(_delay, stoppingToken);
            }
        }

        private async Task GerneratePrices()
        {
            foreach (var ticker in _tickers)
            {
                var bidIndex = rnd.Next(prices.Count);
                var askIndex = rnd.Next(prices.Count);  //not a real random. BEware
                await _tickerService.PushPrice(ticker, prices[askIndex], prices[bidIndex]);
            }
        }
    }
}
