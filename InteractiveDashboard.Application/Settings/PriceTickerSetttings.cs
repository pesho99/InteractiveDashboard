using System.Reflection.Emit;

namespace InteractiveDashboard.Application.Settings
{
    public class PriceTickerSetttings
    {
        public int DelayInMilliseconds { get; set; }
        public List<string> SupportedTickers { get; set; } = new();
    }
}
