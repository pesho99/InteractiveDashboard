namespace InteractiveDashboard.Domain.Dtos
{
    public class TickerDto
    {
        public string SymbolName { get; set; }
        public decimal? Ask {  get; set; }
        public decimal? Bid { get; set;}
        public DateTime? PriceDate { get; set; }
    }
}
