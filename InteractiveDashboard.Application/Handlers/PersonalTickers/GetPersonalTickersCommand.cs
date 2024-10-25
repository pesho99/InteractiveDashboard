using MediatR;

namespace InteractiveDashboard.Application.Handlers.PersonalTickers
{
    public class GetPersonalTickersCommand : IRequest<List<string>>
    {
        public string UserEmail { get; set; } = string.Empty;
    }
}
