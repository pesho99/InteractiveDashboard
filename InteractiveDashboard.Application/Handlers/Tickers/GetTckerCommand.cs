using InteractiveDashboard.Domain.Dtos;
using MediatR;

namespace InteractiveDashboard.Application.Handlers.Tickers
{
    public class GetTckerCommand : IRequest<TickerDto>
    {
        public string TickerName { get; set; } = string.Empty;
    }
}
