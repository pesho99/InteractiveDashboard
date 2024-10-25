

using InteractiveDashboard.Domain.Dtos;

namespace InteractiveDashboard.Application.Services
{
    public interface ITickerService
    {
        List<string> GetAllTickers();
        TickerDto GetTicker(string tickerName);
        Task PushPrice(string tickerNamne, decimal askPrice, decimal bidPrice);
    }
}