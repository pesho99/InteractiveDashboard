using InteractiveDashboard.Application.Repos;
using MediatR;

namespace InteractiveDashboard.Application.Handlers.PersonalTickers
{
    public class UpdatePersonalTickerCommandHandler : IRequestHandler<UpdatePersonalTickersCommand, EmptyResponse>
    {
        private readonly IPersonalTickerRepo _personalTickerRepo;
        public UpdatePersonalTickerCommandHandler(IPersonalTickerRepo personalTickerRepo)
        {
            _personalTickerRepo = personalTickerRepo;
        }

        public async Task<EmptyResponse> Handle(UpdatePersonalTickersCommand request, CancellationToken cancellationToken)
        {
            await _personalTickerRepo.AddTickersForUser(request.UserEmail, request.Tickers);
            return new EmptyResponse();
        }
    }
}
