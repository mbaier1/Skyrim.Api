using Moq;
using Skyrim.Api.Domain.Interfaces;
using Skyrim.Api.Domain;
using Skyrim.Api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Data.AbstractModels;

namespace Skyrim.Api.Test.Domains
{
    public class LocationDomain_Tests
    {
        protected readonly ILocationDomain _locationDomain;
        protected readonly Mock<ILocationRepository> _mockLocationRepository;
        protected readonly Mock<ICreateLocationDtoFormatHelper> _mockCreateLocationDtoFormatHelper;

        public LocationDomain_Tests()
        {
            _mockLocationRepository = new Mock<ILocationRepository>();
            _mockCreateLocationDtoFormatHelper = new Mock<ICreateLocationDtoFormatHelper>();
            _locationDomain = new LocationDomain(_mockLocationRepository.Object, _mockCreateLocationDtoFormatHelper.Object);
        }
    }

    public class Create : LocationDomain_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasValidCityProperties_ReturnsExpectedLocationAsCity()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.City,
                GeographicalDescription = "Test"
            };

            var city = new City()
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.City,
                GeographicalDescription = "Test"
            };

            var completedCreateTask = Task<Location>.FromResult(city);

            _mockLocationRepository.Setup(x => x.SaveLocationAsCity(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(createLocationDto);

            // Act

            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert

            Assert.Equal(city.Id, result.Id);
            Assert.Equal(city.Name, result.Name);
            Assert.Equal(city.Description, result.Description);
            Assert.Equal(city.TypeOfLocation, result.TypeOfLocation);
            Assert.Equal(city.GeographicalDescription, result.GeographicalDescription);
        }

        [Theory]
        [MemberData(nameof(WhiteSpaceProperties))]
        public async void CreateLocationDtoContainsEmpty_WhiteSpace_OrNullDescription(string description,
            CreateLocationDto createLocationDto, CreateLocationDto formatedCreateLocationDto, City city)
        {
            // Arrange
            var completedCreateTask = Task<Location>.FromResult(city);
            _mockLocationRepository.Setup(x => x.SaveLocationAsCity(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(formatedCreateLocationDto);

            // Act
            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Equal(city.Name, result.Name);
            Assert.Equal(city.Description, result.Description);
            Assert.Equal(city.TypeOfLocation, result.TypeOfLocation);
        }

        public static IEnumerable<object[]> WhiteSpaceProperties()
        {
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a City with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new City
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a City with empty description", new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new City
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a City with empty description", new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new City
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
        }

        [Theory]
        [MemberData(nameof(UnallowedNull_Invalid_OrWhiteSpaceProperties))]
        public async void CreateLocationDtoContainsInvalidEmpty_WhiteSpace_OrNullProperties(string description,
            CreateLocationDto createLocationDto, CreateLocationDto badFormatedCreateLocationDto, City city)
        {
            // Arrange
            var completedCreateTask = Task<Location>.FromResult(city);
            _mockLocationRepository.Setup(x => x.SaveLocationAsCity(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(badFormatedCreateLocationDto);

            // Act
            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Null(result);
        }

        public static IEnumerable<object[]> UnallowedNull_Invalid_OrWhiteSpaceProperties()
        {
            yield return new object[]
            {
                    "CreateLocationDto has no Location Type",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "Test"
                    },
                    (CreateLocationDto)null,
                    (City)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        TypeOfLocation = LocationType.City
                    },
                    (CreateLocationDto)null,
                    (City)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        TypeOfLocation = LocationType.City
                    },
                    (CreateLocationDto)null,
                    (City)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        TypeOfLocation = LocationType.City
                    },
                    (CreateLocationDto)null,
                    (City)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        TypeOfLocation = LocationType.City
                    },
                    (CreateLocationDto)null,
                    (City)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        TypeOfLocation = LocationType.City
                    },
                    (CreateLocationDto)null,
                    (City)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        TypeOfLocation = LocationType.City
                    },
                    (CreateLocationDto)null,
                    (City)null
            };
        }
    }
}
