using MediatR;

namespace InteractiveDashboard.Application.Handlers.PersonalTickers
{
    public class UpdatePersonalTickersCommand : IRequest<EmptyResponse>
    {
        public List<string> Tickers { get; set; } = new();
        public string UserEmail { get; set; } = string.Empty;
    }

}
