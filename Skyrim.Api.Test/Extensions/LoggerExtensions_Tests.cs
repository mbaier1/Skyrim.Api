using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Extensions;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerExtensions = Skyrim.Api.Extensions.LoggerExtensions;

namespace Skyrim.Api.Test.Extensions
{
    public class LoggerExtensions_Tests
    {
        protected readonly LoggerExtensions _loggerExtensions;
        protected readonly Mock<ILogger<LocationRepository>> _partialMockLocationRepositoryLogger;
        protected Mock<ILoggerExtension> _mockLoggerExtension;

        public LoggerExtensions_Tests()
        {
            _partialMockLocationRepositoryLogger = new Mock<ILogger<LocationRepository>>();
            _mockLoggerExtension = new Mock<ILoggerExtension>();
            _loggerExtensions = new LoggerExtensions(_partialMockLocationRepositoryLogger.Object);
        }
    }

    public class LogFatalError : LoggerExtensions_Tests
    {
        [Theory]
        [MemberData(nameof(FatalErrorsForEachLocationType))]
        public void WhenLoggerIsCalled_ErrorIsLoggedOnce(string description, Exception exception, CreateLocationDto createLocationDto)
        {
            // Arrange

            // Act
            _mockLoggerExtension.Object.LogFatalError(exception, createLocationDto);

            // Assert
            _mockLoggerExtension.Verify(x => x.LogFatalError(It.IsAny<Exception>(), It.IsAny<CreateLocationDto>()), Times.Once());
        }
        public static IEnumerable<object[]> FatalErrorsForEachLocationType()
        {
            yield return new object[]
            {
                "Fatal Error for City location",
                new Exception(),
                new CreateLocationDto
                {
                    Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.City
                }
            };
            yield return new object[]
            {
                "Fatal Error for Town location",
                new Exception(),
                new CreateLocationDto
                {
                    Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Town
                }
            };
        }
    }
}
