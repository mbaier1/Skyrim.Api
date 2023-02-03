using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Data;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository.Interface;
using Skyrim.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyrim.Api.Test.Repositories
{
    public class LocationRepository_Tests
    {
        protected ILocationRepository _locationRepository;
        protected SkyrimApiDbContext _context;
        protected Mock<IMapper> _mockMapper;
        protected Mock<ILoggerExtension> _mockLoggerExtension;

        protected ILocationRepository GetInMemoryRepository()
        {
            var options = new DbContextOptionsBuilder<SkyrimApiDbContext>()
            .UseInMemoryDatabase(databaseName: "MockSkyrimLocationDb")
            .Options;

            _context = new SkyrimApiDbContext(options);

            _mockMapper = new Mock<IMapper>();
            _mockLoggerExtension = new Mock<ILoggerExtension>();

            return _locationRepository = new LocationRepository(_context, _mockMapper.Object, _mockLoggerExtension.Object);
        }

        public LocationRepository_Tests()
        {
            _locationRepository = GetInMemoryRepository();
        }

        protected City CreateNewCity()
        {
            return new City
            {
                Name = "Test City Name",
                GeographicalDescription = "Test Geographical Location Description",
                Description = "Test Description",
                TypeOfLocation = LocationType.City
            };
        }

        protected CreateLocationDto CreateNewCreateLocationDto()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.City
            };
        }
    }

    public class SaveLocationAsCity : LocationRepository_Tests
    {
        [Fact]
        public async void WithValidLocationDto_SavesExpectedCity()
        {
            // Arrange
            _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Returns(CreateNewCity());

            //Act
            var result = await _locationRepository.SaveLocationAsCity(new CreateLocationDto());

            //Assert
            Assert.Equal(_context.Cities.FirstOrDefault().Name, result.Name);
        }

        [Fact]
        public async void WithValidLocationDto_MapsLocationAsCity()
        {
            // Arrange
            var createLocationDto = CreateNewCreateLocationDto();
            _mockMapper.Setup(x => x.Map<City>(createLocationDto)).Returns(CreateNewCity());

            // Act
            var result = await _locationRepository.SaveLocationAsCity(createLocationDto);

            // Assert
            _mockMapper.Verify(x => x.Map<City>(createLocationDto), Times.Once());
        }

        [Fact]
        public async void WithValidLocationDto_ReturnsExpectedCity()
        {
            // Arrange
            var city = CreateNewCity();
            _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Returns(city);

            // Act
            var result = await _locationRepository.SaveLocationAsCity(new CreateLocationDto());

            // Assert
            Assert.Equal(city.Name, result.Name);
            Assert.Equal(city.Description, result.Description);
            Assert.Equal(city.GeographicalDescription, result.GeographicalDescription);
            Assert.Equal(city.TypeOfLocation, result.TypeOfLocation);
            Assert.Equal(city.Id, result.Id);
        }

        [Fact]
        public async void WithInvalidLocationDto_WhenMapping_ReturnsNullWhichThrowsErrorInSavingToDatabase()
        {
            // Arrange
            _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Throws(new Exception());

            // Act
            var result = await _locationRepository.SaveLocationAsCity(CreateNewCreateLocationDto());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void WithInvalidLocationDto_LogsError()
        {
            // Arrange
            var createLocationDto = CreateNewCreateLocationDto();

            // Act
            await _locationRepository.SaveLocationAsCity(createLocationDto);

            // Assert
            _mockLoggerExtension.Verify(x => x.LogFatalError(It.IsAny<Exception>(), It.IsAny<CreateLocationDto>()), Times.Once);
        }

        [Fact]
        public async void WithInvalidLocationDto_ReturnsExpectedNullLocation()
        {
            // Arrange
            var exception = new Exception();
            _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Throws(exception);

            // Act
            var result = await _locationRepository.SaveLocationAsCity(new CreateLocationDto());

            //Assert
            Assert.Null(result);
        }
    }
}
