using InteractiveDashboard.Application.Repos;
using MediatR;

namespace InteractiveDashboard.Application.Handlers.PersonalTickers
{
    public class GetPersonalTickersHandler : IRequestHandler<GetPersonalTickersCommand, List<string>>
    {
        private readonly IPersonalTickerRepo _personalTickerRepo;

        public GetPersonalTickersHandler(IPersonalTickerRepo personalTickerRepo)
        {
            _personalTickerRepo = personalTickerRepo;
        }

        public Task<List<string>> Handle(GetPersonalTickersCommand request, CancellationToken cancellationToken)
        {
            return _personalTickerRepo.GetTickersForUser(request.UserEmail);
        }
    }
}
