﻿namespace InteractiveDashboardTests
{
    using NUnit.Framework;
    using Moq;
    using System.Threading;
    using System.Threading.Tasks;
    using InteractiveDashboard.Application.Handlers.Tickers;
    using InteractiveDashboard.Application.Services;
    using InteractiveDashboard.Domain.Dtos;

    [TestFixture]
    public class GetTIckerHandlerTests
    {
        private Mock<ITickerService> _tickerServiceMock;
        private GetTIckerHandler _handler;

        [SetUp]
        public void Setup()
        {
            _tickerServiceMock = new Mock<ITickerService>();
            _handler = new GetTIckerHandler(_tickerServiceMock.Object);
        }

        [Test]
        public async Task Handle_ReturnsTickerDto_WhenTickerExists()
        {
            // Arrange
            var tickerName = "BTCUSD";
            var expectedTicker = new TickerDto { SymbolName = tickerName, Ask = 60000m, Bid = 59900m };

            _tickerServiceMock
                .Setup(service => service.GetTicker(tickerName))
                .Returns(expectedTicker);

            var command = new GetTckerCommand { TickerName = tickerName };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.SymbolName, Is.EqualTo(expectedTicker.SymbolName));
            Assert.That(result.Ask, Is.EqualTo(expectedTicker.Ask));
            Assert.That(result.Bid, Is.EqualTo(expectedTicker.Bid));
        }

        [Test]
        public async Task Handle_CallsGetTicker_WithCorrectTickerName()
        {
            // Arrange
            var tickerName = "BTCUSD";
            var command = new GetTckerCommand { TickerName = tickerName };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _tickerServiceMock.Verify(service => service.GetTicker(tickerName), Times.Once,
                "Expected GetTicker to be called once with the correct ticker name.");
        }

        [Test]
        public async Task Handle_ReturnsNull_WhenTickerDoesNotExist()
        {
            // Arrange
            var tickerName = "UNKNOWN";

            _tickerServiceMock
                .Setup(service => service.GetTicker(tickerName))
                .Returns((TickerDto)null);

            var command = new GetTckerCommand { TickerName = tickerName };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNull(result, "Expected null when ticker does not exist.");
        }
    }

}
