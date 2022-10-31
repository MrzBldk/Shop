using Microsoft.Extensions.Logging;
using Moq;
using Store.Application.Common.Behaviours;
using Store.Application.Common.Interfaces;
using Store.Application.StoreSections.Commands.CreateStoreSection;

namespace Store.Application.UnitTests.Common.Behaviours
{
    public class RequestLoggerTests
    {
        private Mock<ILogger<CreateStoreSectionCommand>> _logger = null!;
        private Mock<ICurrentUserService> _currentUserService = null!;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<CreateStoreSectionCommand>>();
            _currentUserService = new Mock<ICurrentUserService>();
        }

        [Test]
        public async Task ShouldCallUserIdOnce()
        {
            _currentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

            var requestLogger = new LoggingBehaviour<CreateStoreSectionCommand>(_logger.Object, _currentUserService.Object);

            await requestLogger.Process(new CreateStoreSectionCommand { StoreId = Guid.Empty, Name = "Name" }, new CancellationToken());

            _currentUserService.Verify(i => i.UserId, Times.Once);
        }
    }
}
