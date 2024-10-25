using MediatR;

namespace InteractiveDashboard.Application.Handlers.PersonalTickers
{
    public class UpdatePersonalTickersCommand : IRequest<EmptyResponse>
    {
        public List<string> Tickers { get; set; }
        public string UserEmail { get; set; }
    }

}
