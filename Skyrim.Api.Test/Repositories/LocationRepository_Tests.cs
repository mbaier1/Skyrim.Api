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

        protected Town CreateNewTown()
        {
            return new Town
            {
                Name = "Test City Name",
                GeographicalDescription = "Test Geographical Location Description",
                Description = "Test Description",
                TypeOfLocation = LocationType.Town
            };
        }

        protected Homestead CreateNewHomestead()
        {
            return new Homestead
            {
                Name = "Test Homestead Name",
                GeographicalDescription = "Test Geographical Location Description",
                Description = "Test Description",
                TypeOfLocation = LocationType.Homestead
            };
        }

        protected CreateLocationDto CreateNewCreateLocationDtoAsCity()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.City
            };
        }

        protected CreateLocationDto CreateNewCreateLocationDtoAsTown()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Town
            };
        }

        protected CreateLocationDto CreateNewCreateLocationDtoAsHomestead()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Homestead
            };
        }
    }

    public class SaveLocationAsCity : LocationRepository_Tests
    {
        [Fact]
        public async void WithValidCreateLocationDto_SavesExpectedCity()
        {
            // Arrange
            _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Returns(CreateNewCity());

            //Act
            var result = await _locationRepository.SaveLocationAsCity(CreateNewCreateLocationDtoAsCity());

            //Assert
            Assert.Equal(_context.Cities.FirstOrDefault().Name, result.Name);
        }

        [Fact]
        public async void WithValidCreateLocationDto_MapsLocationAsCity()
        {
            // Arrange
            var createLocationDto = CreateNewCreateLocationDtoAsCity();
            _mockMapper.Setup(x => x.Map<City>(createLocationDto)).Returns(CreateNewCity());

            // Act
            var result = await _locationRepository.SaveLocationAsCity(createLocationDto);

            // Assert
            _mockMapper.Verify(x => x.Map<City>(createLocationDto), Times.Once());
        }

        [Fact]
        public async void WithValidCreateLocationDto_ReturnsExpectedCity()
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
        public async void WithInvalidCreateLocationDto_WhenMapping_ReturnsNullWhichThrowsErrorInSavingToDatabase()
        {
            // Arrange
            _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Throws(new Exception());

            // Act
            var result = await _locationRepository.SaveLocationAsCity(CreateNewCreateLocationDtoAsCity());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void WithInvalidCreateLocationDto_LogsError()
        {
            // Arrange
            var createLocationDto = CreateNewCreateLocationDtoAsCity();

            // Act
            await _locationRepository.SaveLocationAsCity(createLocationDto);

            // Assert
            _mockLoggerExtension.Verify(x => x.LogFatalError(It.IsAny<Exception>(), It.IsAny<CreateLocationDto>()), Times.Once);
        }

        [Fact]
        public async void WithInvalidCreateLocationDto_ReturnsExpectedNullLocation()
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

    public class SaveLocationAsTown : LocationRepository_Tests
    {
        [Fact]
        public async void WithValidCreateLocationDto_SavesExpectedTown()
        {
            // Arrange
            _mockMapper.Setup(x => x.Map<Town>(It.IsAny<CreateLocationDto>())).Returns(CreateNewTown());

            //Act
            var result = await _locationRepository.SaveLocationAsTown(CreateNewCreateLocationDtoAsCity());

            //Assert
            Assert.Equal(_context.Cities.FirstOrDefault().Name, result.Name);
        }

        [Fact]
        public async void WithValidCreateLocationDto_MapsLocationAsTown()
        {
            // Arrange
            var createLocationDto = CreateNewCreateLocationDtoAsTown();
            _mockMapper.Setup(x => x.Map<Town>(createLocationDto)).Returns(CreateNewTown());

            // Act
            var result = await _locationRepository.SaveLocationAsTown(createLocationDto);

            // Assert
            _mockMapper.Verify(x => x.Map<Town>(createLocationDto), Times.Once());
        }

        [Fact]
        public async void WithValidCreateLocationDto_ReturnsExpectedTown()
        {
            // Arrange
            var town = CreateNewTown();
            _mockMapper.Setup(x => x.Map<Town>(It.IsAny<CreateLocationDto>())).Returns(town);

            // Act
            var result = await _locationRepository.SaveLocationAsTown(CreateNewCreateLocationDtoAsTown());

            // Assert
            Assert.Equal(town.Name, result.Name);
            Assert.Equal(town.Description, result.Description);
            Assert.Equal(town.GeographicalDescription, result.GeographicalDescription);
            Assert.Equal(town.TypeOfLocation, result.TypeOfLocation);
            Assert.Equal(town.Id, result.Id);
        }

        [Fact]
        public async void WithInvalidCreateLocationDto_WhenMapping_ReturnsNullWhichThrowsErrorInSavingToDatabase()
        {
            // Arrange
            _mockMapper.Setup(x => x.Map<Town>(It.IsAny<CreateLocationDto>())).Throws(new Exception());

            // Act
            var result = await _locationRepository.SaveLocationAsTown(CreateNewCreateLocationDtoAsTown());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void WithInvalidCreateLocationDto_LogsError()
        {
            // Arrange
            var createLocationDto = CreateNewCreateLocationDtoAsTown();

            // Act
            await _locationRepository.SaveLocationAsTown(createLocationDto);

            // Assert
            _mockLoggerExtension.Verify(x => x.LogFatalError(It.IsAny<Exception>(), It.IsAny<CreateLocationDto>()), Times.Once);
        }

        [Fact]
        public async void WithInvalidCreateLocationDto_ReturnsExpectedNullLocation()
        {
            // Arrange
            var exception = new Exception();
            _mockMapper.Setup(x => x.Map<Town>(It.IsAny<CreateLocationDto>())).Throws(exception);

            // Act
            var result = await _locationRepository.SaveLocationAsTown(new CreateLocationDto());

            //Assert
            Assert.Null(result);
        }
    }

    public class SaveLocationAsHomestead : LocationRepository_Tests
    {
        [Fact]
        public async void WithValidCreateLocationDto_SavesExpectedHomestead()
        {
            // Arrange
            _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<CreateLocationDto>())).Returns(CreateNewHomestead());

            //Act
            var result = await _locationRepository.SaveLocationAsHomestead(CreateNewCreateLocationDtoAsHomestead());

            //Assert
            Assert.Equal(_context.Homesteads.FirstOrDefault().Name, result.Name);
        }

        [Fact]
        public async void WithValidCreateLocationDto_MapsLocationAsHomestead()
        {
            // Arrange
            var createLocationDto = CreateNewCreateLocationDtoAsHomestead();
            _mockMapper.Setup(x => x.Map<Homestead>(createLocationDto)).Returns(CreateNewHomestead());

            // Act
            var result = await _locationRepository.SaveLocationAsHomestead(createLocationDto);

            // Assert
            _mockMapper.Verify(x => x.Map<Homestead>(createLocationDto), Times.Once());
        }

        [Fact]
        public async void WithValidCreateLocationDto_ReturnsExpectedHomestead()
        {
            // Arrange
            var homestead = CreateNewHomestead();
            _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<CreateLocationDto>())).Returns(homestead);

            // Act
            var result = await _locationRepository.SaveLocationAsHomestead(CreateNewCreateLocationDtoAsHomestead());

            // Assert
            Assert.Equal(homestead.Name, result.Name);
            Assert.Equal(homestead.Description, result.Description);
            Assert.Equal(homestead.GeographicalDescription, result.GeographicalDescription);
            Assert.Equal(homestead.TypeOfLocation, result.TypeOfLocation);
            Assert.Equal(homestead.Id, result.Id);
        }

        [Fact]
        public async void WithInvalidCreateLocationDto_WhenMapping_ReturnsNullWhichThrowsErrorInSavingToDatabase()
        {
            // Arrange
            _mockMapper.Setup(x => x.Map<Town>(It.IsAny<CreateLocationDto>())).Throws(new Exception());

            // Act
            var result = await _locationRepository.SaveLocationAsHomestead(CreateNewCreateLocationDtoAsHomestead());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void WithInvalidCreateLocationDto_LogsError()
        {
            // Arrange
            var createLocationDto = CreateNewCreateLocationDtoAsTown();

            // Act
            await _locationRepository.SaveLocationAsHomestead(createLocationDto);

            // Assert
            _mockLoggerExtension.Verify(x => x.LogFatalError(It.IsAny<Exception>(), It.IsAny<CreateLocationDto>()), Times.Once);
        }

        [Fact]
        public async void WithInvalidCreateLocationDto_ReturnsExpectedNullLocation()
        {
            // Arrange
            var exception = new Exception();
            _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<CreateLocationDto>())).Throws(exception);

            // Act
            var result = await _locationRepository.SaveLocationAsHomestead(new CreateLocationDto());

            //Assert
            Assert.Null(result);
        }
    }
}
