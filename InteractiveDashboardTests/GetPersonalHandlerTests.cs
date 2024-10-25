using InteractiveDashboard.Application.Handlers.PersonalTickers;
using InteractiveDashboard.Application.Repos;
using Moq;

namespace InteractiveDashboardTests
{
    [TestFixture]
    public class GetPersonalTickersHandlerTests
    {
        private Mock<IPersonalTickerRepo> _personalTickerRepoMock;
        private GetPersonalTickersHandler _handler;

        [SetUp]
        public void Setup()
        {
            _personalTickerRepoMock = new Mock<IPersonalTickerRepo>();
            _handler = new GetPersonalTickersHandler(_personalTickerRepoMock.Object);
        }

        [Test]
        public async Task Handle_ReturnsTickersList_WhenUserHasTickers()
        {
            // Arrange
            var userEmail = "test@example.com";
            var expectedTickers = new List<string> { "BTCUSD", "ETHUSD" };

            _personalTickerRepoMock
                .Setup(repo => repo.GetTickersForUser(userEmail))
                .ReturnsAsync(expectedTickers);

            var command = new GetPersonalTickersCommand { UserEmail = userEmail };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedTickers));
        }

        [Test]
        public async Task Handle_CallsGetTickersForUser_WithCorrectEmail()
        {
            // Arrange
            var userEmail = "test@example.com";
            var command = new GetPersonalTickersCommand { UserEmail = userEmail };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _personalTickerRepoMock.Verify(repo => repo.GetTickersForUser(userEmail), Times.Once,
                "Expected GetTickersForUser to be called once with the correct email.");
        }

        [Test]
        public async Task Handle_ReturnsEmptyList_WhenUserHasNoTickers()
        {
            // Arrange
            var userEmail = "test@example.com";
            _personalTickerRepoMock
                .Setup(repo => repo.GetTickersForUser(userEmail))
                .ReturnsAsync(new List<string>());

            var command = new GetPersonalTickersCommand { UserEmail = userEmail };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsEmpty(result, "Expected an empty list when user has no tickers.");
        }
    }
}
