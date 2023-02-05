using Moq;
using Skyrim.Api.Domain.Interfaces;
using Skyrim.Api.Domain;
using Skyrim.Api.Repository.Interface;
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

    public class CreateLocation : LocationDomain_Tests
    {
        [Theory]
        [MemberData(nameof(ValidPropertiesForEachLocationType))]
        public async void WhenCreateLocationDtoHasValidCityProperties_ReturnsExpectedLocationAsCity(string description, CreateLocationDto createLocationDto,
           Location taskType, Location type)
        {
            // Arrange
            var completedCreateTask = Task<Location>.FromResult(taskType);
            if(type.TypeOfLocation == LocationType.City)
            {
                _mockLocationRepository.Setup(x => x.SaveLocationAsCity(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            }
            else if (type.TypeOfLocation == LocationType.Town)
            {
                _mockLocationRepository.Setup(x => x.SaveLocationAsTown(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            }
            else if (type.TypeOfLocation == LocationType.Homestead)
            {
                _mockLocationRepository.Setup(x => x.SaveLocationAsHomestead(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            }
            else if (type.TypeOfLocation == LocationType.Settlement)
            {
                _mockLocationRepository.Setup(x => x.SaveLocationAsSettlement(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            }

            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(createLocationDto);

            // Act
            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Equal(type.Id, result.Id);
            Assert.Equal(type.Name, result.Name);
            Assert.Equal(type.Description, result.Description);
            Assert.Equal(type.TypeOfLocation, result.TypeOfLocation);
            Assert.Equal(type.GeographicalDescription, result.GeographicalDescription);
        }
        public static IEnumerable<object[]> ValidPropertiesForEachLocationType()
        {
            yield return new object[]
            {
                "Valid properties for City Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.City,
                    GeographicalDescription = "Test"
                },
                new City
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.City,
                    GeographicalDescription = "Test"
                },
                new City
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.City,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Town Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Town,
                    GeographicalDescription = "Test"
                },
                new Town
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Town,
                    GeographicalDescription = "Test"
                },
                new Town
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Town,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Homestead Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Homestead,
                    GeographicalDescription = "Test"
                },
                new Homestead
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Homestead,
                    GeographicalDescription = "Test"
                },
                new Homestead
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Homestead,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Settlement Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Settlement,
                    GeographicalDescription = "Test"
                },
                new Settlement
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Settlement,
                    GeographicalDescription = "Test"
                },
                new Settlement
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Settlement,
                    GeographicalDescription = "Test"
                }
            };
        }

        [Theory]
        [MemberData(nameof(WhiteSpaceProperties))]
        public async void CreateLocationDtoContainsEmpty_WhiteSpace_OrNullDescription(string description,
            CreateLocationDto createLocationDto, CreateLocationDto formatedCreateLocationDto, 
            Location taskType, Location type)
        {
            // Arrange
            var completedCreateTask = Task<Location>.FromResult(taskType);
            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(formatedCreateLocationDto);
            if (type.TypeOfLocation == LocationType.City)
            {
                _mockLocationRepository.Setup(x => x.SaveLocationAsCity(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            }
            else if (type.TypeOfLocation == LocationType.Town)
            {
                _mockLocationRepository.Setup(x => x.SaveLocationAsTown(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            }
            else if (type.TypeOfLocation == LocationType.Homestead)
            {
                _mockLocationRepository.Setup(x => x.SaveLocationAsHomestead(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            }
            else if (type.TypeOfLocation == LocationType.Settlement)
            {
                _mockLocationRepository.Setup(x => x.SaveLocationAsSettlement(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            }

            // Act
            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Equal(type.Name, result.Name);
            Assert.Equal(type.Description, result.Description);
            Assert.Equal(type.TypeOfLocation, result.TypeOfLocation);
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
                    "CreateLocationDto has empty description so it returns a City with empty description", 
                    new CreateLocationDto
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
                    "CreateLocationDto has a null description so it returns a Town with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Town,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Town,
                        GeographicalDescription = "Test"
                    },
                    new Town
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Town,
                        GeographicalDescription = "Test"
                    },
                    new Town
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Town,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Town with empty description",
                new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Town,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Town,
                        GeographicalDescription = "Test"
                    },
                    new Town
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Town,
                        GeographicalDescription = "Test"
                    },
                    new Town
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Town,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Town with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Town,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Town,
                        GeographicalDescription = "Test"
                    },
                    new Town
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Town,
                        GeographicalDescription = "Test"
                    },
                    new Town
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Town,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Homestead with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    },
                    new Homestead
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    },
                    new Homestead
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Homestead with empty description",
                new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    },
                    new Homestead
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    },
                    new Homestead
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Homestead with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    },
                    new Homestead
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    },
                    new Homestead
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Homestead,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Settlement with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    },
                    new Settlement
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    },
                    new Settlement
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Settlement with empty description",
                new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    },
                    new Settlement
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    },
                    new Settlement
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Settlement with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    },
                    new Settlement
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    },
                    new Settlement
                    {
                        Id = 0,
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Settlement,
                        GeographicalDescription = "Test"
                    }
            };
        }

        [Theory]
        [MemberData(nameof(UnallowedNull_Invalid_OrWhiteSpaceProperties))]
        public async void CreateLocationDtoContainsInvalidEmpty_WhiteSpace_OrNullProperties(string description,
            CreateLocationDto createLocationDto, CreateLocationDto badFormatedCreateLocationDto,
            Location taskType, Location type)
        {
            // Arrange
            var completedCreateTask = Task<Location>.FromResult(taskType);
            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(badFormatedCreateLocationDto);
            if (type.TypeOfLocation == LocationType.City)
            {
                _mockLocationRepository.Setup(x => x.SaveLocationAsCity(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            }
            else if (type.TypeOfLocation == LocationType.Town)
            {
                _mockLocationRepository.Setup(x => x.SaveLocationAsTown(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            }
            else if (type.TypeOfLocation == LocationType.Homestead)
            {
                _mockLocationRepository.Setup(x => x.SaveLocationAsHomestead(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            }
            else if (type.TypeOfLocation == LocationType.Settlement)
            {
                _mockLocationRepository.Setup(x => x.SaveLocationAsHomestead(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            }

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
                    (City)null,
                    new City
                    {
                        TypeOfLocation = LocationType.City,
                    }
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
                    (City)null,
                    new City
                    {
                        TypeOfLocation = LocationType.City,
                    }
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
                    (City)null,
                    new City
                    {
                        TypeOfLocation = LocationType.City,
                    }
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
                    (City)null,
                    new City
                    {
                        TypeOfLocation = LocationType.City,
                    }
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
                    (City)null,
                    new City
                    {
                        TypeOfLocation = LocationType.City,
                    }
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
                    (City)null,
                    new City
                    {
                        TypeOfLocation = LocationType.City,
                    }
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
                    (City)null,
                    new City
                    {
                        TypeOfLocation = LocationType.City,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        TypeOfLocation = LocationType.Town
                    },
                    (CreateLocationDto)null,
                    (Town)null,
                    new Town
                    {
                        TypeOfLocation = LocationType.Town,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        TypeOfLocation = LocationType.Town
                    },
                    (CreateLocationDto)null,
                    (Town)null,
                    new Town
                    {
                        TypeOfLocation = LocationType.Town,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        TypeOfLocation = LocationType.Town
                    },
                    (CreateLocationDto)null,
                    (Town)null,
                    new Town
                    {
                        TypeOfLocation = LocationType.Town,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        TypeOfLocation = LocationType.Town
                    },
                    (CreateLocationDto)null,
                    (Town)null,
                    new Town
                    {
                        TypeOfLocation = LocationType.Town,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        TypeOfLocation = LocationType.Town
                    },
                    (CreateLocationDto)null,
                    (Town)null,
                    new Town
                    {
                        TypeOfLocation = LocationType.Town,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        TypeOfLocation = LocationType.Town
                    },
                    (CreateLocationDto)null,
                    (Town)null,
                    new Town
                    {
                        TypeOfLocation = LocationType.Town,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        TypeOfLocation = LocationType.Homestead
                    },
                    (CreateLocationDto)null,
                    (Homestead)null,
                    new Homestead
                    {
                        TypeOfLocation = LocationType.Homestead,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        TypeOfLocation = LocationType.Homestead
                    },
                    (CreateLocationDto)null,
                    (Homestead)null,
                    new Homestead
                    {
                        TypeOfLocation = LocationType.Homestead,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        TypeOfLocation = LocationType.Homestead
                    },
                    (CreateLocationDto)null,
                    (Homestead)null,
                    new Homestead
                    {
                        TypeOfLocation = LocationType.Homestead,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        TypeOfLocation = LocationType.Homestead
                    },
                    (CreateLocationDto)null,
                    (Homestead)null,
                    new Homestead
                    {
                        TypeOfLocation = LocationType.Homestead,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        TypeOfLocation = LocationType.Homestead
                    },
                    (CreateLocationDto)null,
                    (Homestead)null,
                    new Homestead
                    {
                        TypeOfLocation = LocationType.Homestead,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        TypeOfLocation = LocationType.Homestead
                    },
                    (CreateLocationDto)null,
                    (Homestead)null,
                    new Homestead
                    {
                        TypeOfLocation = LocationType.Homestead,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        TypeOfLocation = LocationType.Settlement
                    },
                    (CreateLocationDto)null,
                    (Settlement)null,
                    new Settlement
                    {
                        TypeOfLocation = LocationType.Settlement,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        TypeOfLocation = LocationType.Settlement
                    },
                    (CreateLocationDto)null,
                    (Settlement)null,
                    new Homestead
                    {
                        TypeOfLocation = LocationType.Settlement,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        TypeOfLocation = LocationType.Settlement
                    },
                    (CreateLocationDto)null,
                    (Settlement)null,
                    new Homestead
                    {
                        TypeOfLocation = LocationType.Settlement,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        TypeOfLocation = LocationType.Settlement
                    },
                    (CreateLocationDto)null,
                    (Settlement)null,
                    new Homestead
                    {
                        TypeOfLocation = LocationType.Settlement,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        TypeOfLocation = LocationType.Settlement
                    },
                    (CreateLocationDto)null,
                    (Settlement)null,
                    new Homestead
                    {
                        TypeOfLocation = LocationType.Settlement,
                    }
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        TypeOfLocation = LocationType.Settlement
                    },
                    (CreateLocationDto)null,
                    (Settlement)null,
                    new Homestead
                    {
                        TypeOfLocation = LocationType.Settlement,
                    }
            };
        }

        [Fact]
        public async void CreateLocationDtoDoesNotContainValidLocationType_ReturnsNull()
        {
            // Arrange
            var createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test"
            };

            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(createLocationDto);

            // Act
            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Null(result);
        }
    }
}
