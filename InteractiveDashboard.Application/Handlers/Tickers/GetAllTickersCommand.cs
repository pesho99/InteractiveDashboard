using MediatR;

namespace InteractiveDashboard.Application.Handlers.Tickers
{
    public class GetAllTickersCommand : IRequest<List<string>>
    {
    }
}
