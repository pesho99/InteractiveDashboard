
namespace InteractiveDashboard.Application.Repos
{
    public interface IPersonalTickerRepo
    {
        Task AddTickersForUser(string userEmail, List<string> tickers);
        Task<List<string>> GetTickersForUser(string userEmail);
    }
}
