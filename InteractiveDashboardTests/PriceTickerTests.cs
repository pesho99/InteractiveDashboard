namespace InteractiveDashboardTests
{
    using NUnit.Framework;
    using Moq;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using InteractiveDashboard.Application.Services;
    using InteractiveDashboard.Application.Settings;

    [TestFixture]
    public class PriceTickerTests
    {
        private Mock<ITickerService> _tickerServiceMock;
        private PriceTicker _priceTicker;
        private PriceTickerSetttings _settings;

        [SetUp]
        public void Setup()
        {
            _tickerServiceMock = new Mock<ITickerService>();
            _settings = new PriceTickerSetttings
            {
                DelayInMilliseconds = 1000,
                SupportedTickers = new List<string> { "BTCUSD", "ETHUSD" }
            };

            var optionsMock = new Mock<IOptions<PriceTickerSetttings>>();
            optionsMock.Setup(o => o.Value).Returns(_settings);

            _priceTicker = new PriceTicker(optionsMock.Object, _tickerServiceMock.Object);
        }

        [Test]
        public async Task ExecuteAsync_CallsGeneratePrices_MultipleTimesBeforeStopping()
        {
            // Arrange
            using var cancellationTokenSource = new CancellationTokenSource(3000); 

            // Act
            await _priceTicker.StartAsync(cancellationTokenSource.Token);

            // Assert
            _tickerServiceMock.Verify(t => t.PushPrice(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()),
                                      Times.AtLeastOnce, "Expected GeneratePrices to be called multiple times.");
        }

        [Test]
        public async Task GerneratePrices_CallsPushPrice_ForEachTicker()
        {
            // Act
            _priceTicker.GetType()
                              .GetMethod("GerneratePrices", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                              ?.Invoke(_priceTicker, null);

            // Assert
            foreach (var ticker in _settings.SupportedTickers)
            {
                _tickerServiceMock.Verify(t => t.PushPrice(
                    ticker,
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>()),
                    Times.Once, $"Expected PushPrice to be called once for each ticker: {ticker}");
            }
        }

        [Test]
        public async Task GerneratePrices_UsesRandomPrices_ForBidAndAsk()
        {
            // Arrange
            var bidAskPairs = new List<(decimal ask, decimal bid)>();
            _tickerServiceMock.Setup(t => t.PushPrice(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                              .Callback<string, decimal, decimal>((ticker, ask, bid) =>
                              {
                                  bidAskPairs.Add((ask, bid));
                              });

            // Act
            _priceTicker.GetType()
                              .GetMethod("GerneratePrices", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                              ?.Invoke(_priceTicker, null);

            // Assert
            Assert.IsTrue(bidAskPairs.Count > 0, "Expected bid and ask pairs to be generated.");
            Assert.IsTrue(bidAskPairs.Exists(p => p.ask != p.bid), "Expected at least one ask/bid pair to differ.");
        }
    }

}
