namespace InteractiveDashboardTests
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using InteractiveDashboard.Application.InfrastructureServices;
    using InteractiveDashboard.Application.Services;
    using InteractiveDashboard.Domain.Dtos;

    [TestFixture]
    public class TickerServiceTests
    {
        private TickerService _tickerService;
        private Mock<IHubContext<TickerHub, IPriceTickerClient>> _hubContextMock;
        private Mock<IPriceTickerClient> _priceTickerClientMock;
        private Mock<IDateTimeService> _dateTimeServiceMock;

        [SetUp]
        public void Setup()
        {
            _hubContextMock = new Mock<IHubContext<TickerHub, IPriceTickerClient>>();
            _priceTickerClientMock = new Mock<IPriceTickerClient>();
            _dateTimeServiceMock = new Mock<IDateTimeService>();

            _hubContextMock
                .Setup(h => h.Clients.Group(It.IsAny<string>()))
                .Returns(_priceTickerClientMock.Object);

            _tickerService = new TickerService(_hubContextMock.Object, _dateTimeServiceMock.Object);
        }

        [Test]
        public void GetTicker_ReturnsTicker_WhenTickerExists()
        {
            // Arrange
            string tickerName = "BTCUSD";
            var tickerDto = new TickerDto { SymbolName = tickerName };
            _tickerService.GetType().GetField("_tickers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_tickerService, new ConcurrentDictionary<string, TickerDto> { [tickerName] = tickerDto });

            // Act
            var result = _tickerService.GetTicker(tickerName);

            // Assert
            Assert.That(result.SymbolName, Is.EqualTo(tickerName));
        }

        [Test]
        public async Task PushPrice_UpdatesTickerAndCallsClient_WhenTickerExists()
        {
            // Arrange
            string tickerName = "BTCUSD";
            decimal askPrice = 50000m;
            decimal bidPrice = 49900m;
            var currentTime = System.DateTime.UtcNow;
            _dateTimeServiceMock.Setup(d => d.GetCurrentDateTime()).Returns(currentTime);

            // Act
            await _tickerService.PushPrice(tickerName, askPrice, bidPrice);

            // Assert
            var tickers = (ConcurrentDictionary<string, TickerDto>)_tickerService.GetType()
                .GetField("_tickers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(_tickerService);
            var ticker = tickers[tickerName];

            Assert.That(ticker.Ask, Is.EqualTo(askPrice));
            Assert.That(ticker.Bid, Is.EqualTo(bidPrice));
            Assert.That(ticker.PriceDate, Is.EqualTo(currentTime));

            _priceTickerClientMock.Verify(m => m.PriceUpdate(It.IsAny<TickerDto>()), Times.Once);
        }

        [Test]
        public void GetAllTickers_ReturnsAllTickerNames()
        {
            // Arrange
            var tickers = new ConcurrentDictionary<string, TickerDto>
            {
                ["BTCUSD"] = new TickerDto { SymbolName = "BTCUSD" },
                ["ETHUSD"] = new TickerDto { SymbolName = "ETHUSD" }
            };
            _tickerService.GetType().GetField("_tickers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_tickerService, tickers);

            // Act
            var result = _tickerService.GetAllTickers();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.Contains("BTCUSD", result);
            Assert.Contains("ETHUSD", result);
        }
    }

}
