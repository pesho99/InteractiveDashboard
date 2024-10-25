using InteractiveDashboard.Application.Repos;
using Microsoft.EntityFrameworkCore;

namespace InteractiveDashboard.Infrastructure.Database
{
    public class PersonalTickerRepo : IPersonalTickerRepo
    {
        private readonly ApplicationContext _context;

        public PersonalTickerRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddTickersForUser(string userEmail, List<string> tickers)
        {
            var oldTickers = _context.PersonalTickers.Where(p => p.UserEmail == userEmail).ToList();
            _context.PersonalTickers.RemoveRange(oldTickers);
            await _context.SaveChangesAsync();
            foreach(var ticker in tickers)
            {
                _context.PersonalTickers.Add(new Domain.Models.PersonalTicker
                {
                    TickerName = ticker,
                    UserEmail = userEmail,
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetTickersForUser(string userEmail)
        {
            return await _context.PersonalTickers.Where(p => p.UserEmail == userEmail).Select(p => p.TickerName).ToListAsync();
        }
    }
}
