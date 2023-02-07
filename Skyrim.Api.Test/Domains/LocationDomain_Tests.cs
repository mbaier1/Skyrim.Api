﻿using AutoMapper;
using Moq;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository.Interface;

namespace Skyrim.Api.Test.Domains
{
    public class LocationDomain_Tests
    {
        protected readonly ILocationDomain _locationDomain;
        protected readonly Mock<ILocationRepository> _mockLocationRepository;
        protected readonly Mock<ICreateLocationDtoFormatHelper> _mockCreateLocationDtoFormatHelper;
        protected Mock<IDomainLoggerExtension> _mockLoggerExtension;
        protected Mock<IMapper> _mockMapper;

        public LocationDomain_Tests()
        {
            _mockLocationRepository = new Mock<ILocationRepository>();
            _mockCreateLocationDtoFormatHelper = new Mock<ICreateLocationDtoFormatHelper>();
            _mockLoggerExtension = new Mock<IDomainLoggerExtension>();
            _mockMapper = new Mock<IMapper>();
            _locationDomain = new LocationDomain(_mockLocationRepository.Object, _mockCreateLocationDtoFormatHelper.Object,
                _mockLoggerExtension.Object, _mockMapper.Object);
        }

        protected static City CreateNewCity()
        {
            return new City
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.City,
                GeographicalDescription = "Test"
            };
        }

        protected static Town CreateNewTown()
        {
            return new Town
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Town,
                GeographicalDescription = "Test"
            };
        }

        protected static Homestead CreateNewHomestead()
        {
            return new Homestead
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Homestead,
                GeographicalDescription = "Test"
            };
        }

        protected static Settlement CreateNewSettlement()
        {
            return new Settlement
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Settlement,
                GeographicalDescription = "Test"
            };
        }

        protected static DaedricShrine CreateNewDaedricShrine()
        {
            return new DaedricShrine
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.DaedricShrine,
                GeographicalDescription = "Test"
            };
        }

        protected static CreateLocationDto CreateNewCreateLocationDtoAsCity()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.City
            };
        }

        protected static CreateLocationDto CreateNewCreateLocationDtoAsTown()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Town
            };
        }

        protected static CreateLocationDto CreateNewCreateLocationDtoAsHomestead()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Homestead
            };
        }

        protected static CreateLocationDto CreateNewCreateLocationDtoAsSettlement()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Settlement
            };
        }

        protected static CreateLocationDto CreateNewCreateLocationDtoAsDaedricShrine()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.DaedricShrine
            };
        }
    }

    public class CreateLocation : LocationDomain_Tests
    {
        [Theory]
        [MemberData(nameof(ValidPropertiesForEachLocationType))]
        public async void WhenCreateLocationDtoHasValidProperties_ReturnsExpectedLocation(string description, CreateLocationDto createLocationDto,
           Location taskType, Location type)
        {
            // Arrange
            if (type.TypeOfLocation == LocationType.City)
                _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Returns(CreateNewCity());
            else if (type.TypeOfLocation == LocationType.Town)
                _mockMapper.Setup(x => x.Map<Town>(It.IsAny<CreateLocationDto>())).Returns(CreateNewTown());
            else if (type.TypeOfLocation == LocationType.Homestead)
                _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<CreateLocationDto>())).Returns(CreateNewHomestead());
            else if (type.TypeOfLocation == LocationType.Settlement)
                _mockMapper.Setup(x => x.Map<Settlement>(It.IsAny<CreateLocationDto>())).Returns(CreateNewSettlement());
            else if (type.TypeOfLocation == LocationType.DaedricShrine)
                _mockMapper.Setup(x => x.Map<DaedricShrine>(It.IsAny<CreateLocationDto>())).Returns(CreateNewDaedricShrine());

            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(createLocationDto);
            var completedCreateTask = Task<Location>.FromResult(taskType);
            _mockLocationRepository.Setup(x => x.SaveLocation(It.IsAny<Location>()))
                .ReturnsAsync((Location)completedCreateTask.Result);

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
                CreateNewCreateLocationDtoAsCity(),
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
                CreateNewCreateLocationDtoAsTown(),
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
                CreateNewCreateLocationDtoAsHomestead(),
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
                CreateNewCreateLocationDtoAsSettlement(),
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
            yield return new object[]
            {
                "Valid properties for DaedricShrine Location",
                CreateNewCreateLocationDtoAsDaedricShrine(),
                new DaedricShrine
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.DaedricShrine,
                    GeographicalDescription = "Test"
                },
                new DaedricShrine
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.DaedricShrine,
                    GeographicalDescription = "Test"
                }
            };
        }

        [Theory]
        [MemberData(nameof(ValidPropertiesForEachLocationType))]
        public async void WithValidProperties_MapsToCorrectLocation(string description, CreateLocationDto createLocationDto, Location taskType,
            Location location)
        {
            // Arrange
            if (location.TypeOfLocation == LocationType.City)
                _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Returns(CreateNewCity());
            else if (location.TypeOfLocation == LocationType.Town)
                _mockMapper.Setup(x => x.Map<Town>(It.IsAny<CreateLocationDto>())).Returns(CreateNewTown());
            else if (location.TypeOfLocation == LocationType.Homestead)
                _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<CreateLocationDto>())).Returns(CreateNewHomestead());
            else if (location.TypeOfLocation == LocationType.Settlement)
                _mockMapper.Setup(x => x.Map<Settlement>(It.IsAny<CreateLocationDto>())).Returns(CreateNewSettlement());
            else if (location.TypeOfLocation == LocationType.DaedricShrine)
                _mockMapper.Setup(x => x.Map<DaedricShrine>(It.IsAny<CreateLocationDto>())).Returns(CreateNewDaedricShrine());

            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(createLocationDto);
            var completedCreateTask = Task<Location>.FromResult(taskType);
            _mockLocationRepository.Setup(x => x.SaveLocation(It.IsAny<Location>()))
                .ReturnsAsync((Location)completedCreateTask.Result);

            // Act
            await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            if (location.TypeOfLocation == LocationType.City)
                _mockMapper.Verify(x => x.Map<City>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Town)
                _mockMapper.Verify(x => x.Map<Town>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Homestead)
                _mockMapper.Verify(x => x.Map<Homestead>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Settlement)
                _mockMapper.Verify(x => x.Map<Settlement>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.DaedricShrine)
                _mockMapper.Verify(x => x.Map<DaedricShrine>(createLocationDto), Times.Once());
        }

        [Theory]
        [MemberData(nameof(InvalidProperties))]
        public async void WithInvalidProperties_MappingFails_AndReturnsNull(string description, CreateLocationDto createLocationDto)
        {
            // Arrange
            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(createLocationDto);

            // Act
            var result = _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Equal(null, result.Result);
        }
        public static IEnumerable<object[]> InvalidProperties()
        {
            yield return new object[]
            {
                "Invalid properties for City",
                new CreateLocationDto { TypeOfLocation = LocationType.City }
            };
            yield return new object[]
            {
                "Invalid properties for Town",
                new CreateLocationDto { TypeOfLocation = LocationType.Town }
            };
            yield return new object[]
            {
                "Invalid properties for Homestead",
                new CreateLocationDto { TypeOfLocation = LocationType.Homestead }
            };
            yield return new object[]
            {
                "Invalid properties for Settlement",
                new CreateLocationDto { TypeOfLocation = LocationType.Settlement }
            };
            yield return new object[]
            {
                "Invalid properties for DaedricShrine",
                new CreateLocationDto { TypeOfLocation = LocationType.DaedricShrine }
            };
        }

        [Theory]
        [MemberData(nameof(InvalidProperteriesCausesLoggingError))]
        public async void WithInvalidProperties_LogsExpectedError(string description, Location location, CreateLocationDto createLocationDto)
        {
            // Arrange
            if (location.TypeOfLocation == LocationType.City)
                _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Town)
                _mockMapper.Setup(x => x.Map<Town>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Homestead)
                _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Settlement)
                _mockMapper.Setup(x => x.Map<Settlement>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.DaedricShrine)
                _mockMapper.Setup(x => x.Map<DaedricShrine>(It.IsAny<CreateLocationDto>())).Throws(new Exception());

            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(createLocationDto);

            // Act
            await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            _mockLoggerExtension.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<CreateLocationDto>()), Times.Once);

        }
        public static IEnumerable<object[]> InvalidProperteriesCausesLoggingError()
        {
            yield return new object[]
            {
                "Invalid properties for City",
                CreateNewCity(),
                CreateNewCreateLocationDtoAsCity(),
            };
            yield return new object[]
            {
                "Invalid properties for Town",
                CreateNewTown(),
                CreateNewCreateLocationDtoAsTown()
            };
            yield return new object[]
            {
                "Invalid properties for Homestead",
                CreateNewHomestead(),
                CreateNewCreateLocationDtoAsHomestead()
            };
            yield return new object[]
            {
                "Invalid properties for Settlement",
                CreateNewSettlement(),
                CreateNewCreateLocationDtoAsSettlement()
            };
            yield return new object[]
            {
                "Invalid properties for DaedricShrine",
                CreateNewDaedricShrine(),
                CreateNewCreateLocationDtoAsDaedricShrine()
            };
        }

        [Theory]
        [MemberData(nameof(WhiteSpaceProperties))]
        public async void CreateLocationDtoContainsEmpty_WhiteSpace_OrNullDescription_ReturnsExpectedLocation(string description,
            CreateLocationDto createLocationDto, CreateLocationDto formatedCreateLocationDto, 
            Location taskType, Location type)
        {
            // Arrange
            if (type.TypeOfLocation == LocationType.City)
                _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Returns(CreateNewCity());
            else if (type.TypeOfLocation == LocationType.Town)
                _mockMapper.Setup(x => x.Map<Town>(It.IsAny<CreateLocationDto>())).Returns(CreateNewTown());
            else if (type.TypeOfLocation == LocationType.Homestead)
                _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<CreateLocationDto>())).Returns(CreateNewHomestead());
            else if (type.TypeOfLocation == LocationType.Settlement)
                _mockMapper.Setup(x => x.Map<Settlement>(It.IsAny<CreateLocationDto>())).Returns(CreateNewSettlement());
            else if (type.TypeOfLocation == LocationType.DaedricShrine)
                _mockMapper.Setup(x => x.Map<DaedricShrine>(It.IsAny<CreateLocationDto>())).Returns(CreateNewDaedricShrine());

            var completedCreateTask = Task<Location>.FromResult(taskType);
            _mockLocationRepository.Setup(x => x.SaveLocation(It.IsAny<Location>()))
                .ReturnsAsync((Location)completedCreateTask.Result);
            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(formatedCreateLocationDto);

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
                    CreateNewCreateLocationDtoAsCity(),
                    CreateNewCity(),
                    CreateNewCity()
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
                    CreateNewCreateLocationDtoAsCity(),
                    CreateNewCity(),
                    CreateNewCity()
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
                    CreateNewCreateLocationDtoAsCity(),
                    CreateNewCity(),
                    CreateNewCity()
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
                    CreateNewCreateLocationDtoAsTown(),
                    CreateNewTown(),
                    CreateNewTown()
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
                    CreateNewCreateLocationDtoAsTown(),
                    CreateNewTown(),
                    CreateNewTown()
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
                    CreateNewCreateLocationDtoAsTown(),
                    CreateNewTown(),
                    CreateNewTown()
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
                    CreateNewCreateLocationDtoAsHomestead(),
                    CreateNewHomestead(),
                    CreateNewHomestead()
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
                    CreateNewCreateLocationDtoAsHomestead(),
                    CreateNewHomestead(),
                    CreateNewHomestead()
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
                    CreateNewCreateLocationDtoAsHomestead(),
                    CreateNewHomestead(),
                    CreateNewHomestead()
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
                    CreateNewCreateLocationDtoAsSettlement(),
                    CreateNewSettlement(),
                    CreateNewSettlement()
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
                    CreateNewCreateLocationDtoAsSettlement(),
                    CreateNewSettlement(),
                    CreateNewSettlement()
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
                    CreateNewCreateLocationDtoAsSettlement(),
                    CreateNewSettlement(),
                    CreateNewSettlement()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a DaedricShrine with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.DaedricShrine,
                        GeographicalDescription = "Test"
                    },
                    CreateNewCreateLocationDtoAsDaedricShrine(),
                    CreateNewDaedricShrine(),
                    CreateNewDaedricShrine()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a DaedricShrine with empty description",
                new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.DaedricShrine,
                        GeographicalDescription = "Test"
                    },
                    CreateNewCreateLocationDtoAsDaedricShrine(),
                    CreateNewDaedricShrine(),
                    CreateNewDaedricShrine()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a DaedricShrine with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.DaedricShrine,
                        GeographicalDescription = "Test"
                    },
                    CreateNewCreateLocationDtoAsDaedricShrine(),
                    CreateNewDaedricShrine(),
                    CreateNewDaedricShrine()
            };
        }

        [Theory]
        [MemberData(nameof(UnallowedNull_Invalid_OrWhiteSpaceProperties))]
        public async void CreateLocationDtoContainsInvalidEmpty_WhiteSpace_OrNullProperties_ReturnsExpectedNull(string description,
            CreateLocationDto createLocationDto, CreateLocationDto badFormatedCreateLocationDto)
        {
            // Arrange
            _mockCreateLocationDtoFormatHelper.Setup(x => x.FormatEntity(It.IsAny<CreateLocationDto>())).Returns(badFormatedCreateLocationDto);

            // Act
            var result = await _locationDomain.CreateLocation(createLocationDto);

            // Assert
            Assert.Equal(null, result);
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
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
                    (CreateLocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        TypeOfLocation = LocationType.DaedricShrine
                    },
                    (CreateLocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "",
                        TypeOfLocation = LocationType.DaedricShrine
                    },
                    (CreateLocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = "   ",
                        TypeOfLocation = LocationType.DaedricShrine
                    },
                    (CreateLocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = null,
                        Name = "Test",
                        TypeOfLocation = LocationType.DaedricShrine
                    },
                    (CreateLocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has an empty Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "",
                        Name = "Test",
                        TypeOfLocation = LocationType.DaedricShrine
                    },
                    (CreateLocationDto)null
            };
            yield return new object[]
            {
                    "CreateLocationDto has a white space Geographic Description",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = " ",
                        Name = "Test",
                        TypeOfLocation = LocationType.DaedricShrine
                    },
                    (CreateLocationDto)null
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
            Assert.Equal(null, result);
        }
    }
}
