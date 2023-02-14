using AutoMapper;
using Moq;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository.Interface;
using Skyrim.Api.Test.TestHelpers;

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
                _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewCity());
            else if (type.TypeOfLocation == LocationType.Town)
                _mockMapper.Setup(x => x.Map<Town>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewTown());
            else if (type.TypeOfLocation == LocationType.Homestead)
                _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewHomestead());
            else if (type.TypeOfLocation == LocationType.Settlement)
                _mockMapper.Setup(x => x.Map<Settlement>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewSettlement());
            else if (type.TypeOfLocation == LocationType.DaedricShrine)
                _mockMapper.Setup(x => x.Map<DaedricShrine>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewDaedricShrine());
            else if (type.TypeOfLocation == LocationType.StandingStone)
                _mockMapper.Setup(x => x.Map<StandingStone>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewStandingStone());
            else if (type.TypeOfLocation == LocationType.Landmark)
                _mockMapper.Setup(x => x.Map<Landmark>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewLandmark());
            else if (type.TypeOfLocation == LocationType.Camp)
                _mockMapper.Setup(x => x.Map<Camp>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewCamp());
            else if (type.TypeOfLocation == LocationType.Cave)
                _mockMapper.Setup(x => x.Map<Cave>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewCave());
            else if (type.TypeOfLocation == LocationType.Clearing)
                _mockMapper.Setup(x => x.Map<Clearing>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewClearing());
            else if (type.TypeOfLocation == LocationType.Dock)
                _mockMapper.Setup(x => x.Map<Dock>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewDock());
            else if (type.TypeOfLocation == LocationType.DragonLair)
                _mockMapper.Setup(x => x.Map<DragonLair>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewDragonLair());
            else if (type.TypeOfLocation == LocationType.DwarvenRuin)
                _mockMapper.Setup(x => x.Map<DwarvenRuin>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewDwarvenRuin());
            else if (type.TypeOfLocation == LocationType.Farm)
                _mockMapper.Setup(x => x.Map<Farm>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewFarm());

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
                TestMethodHelpers.CreateNewCreateLocationDtoAsCity(),
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
                TestMethodHelpers.CreateNewCreateLocationDtoAsTown(),
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
                TestMethodHelpers.CreateNewCreateLocationDtoAsHomestead(),
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
                TestMethodHelpers.CreateNewCreateLocationDtoAsSettlement(),
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
                TestMethodHelpers.CreateNewCreateLocationDtoAsDaedricShrine(),
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
            yield return new object[]
            {
                "Valid properties for StandingStone Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsStandingStone(),
                new StandingStone
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.StandingStone,
                    GeographicalDescription = "Test"
                },
                new StandingStone
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.StandingStone,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Landmark Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsLandmark(),
                new Landmark
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Landmark,
                    GeographicalDescription = "Test"
                },
                new Landmark
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Landmark,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Camp Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsCamp(),
                new Camp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Camp,
                    GeographicalDescription = "Test"
                },
                new Camp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Camp,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Cave Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsCave(),
                new Cave
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Cave,
                    GeographicalDescription = "Test"
                },
                new Cave
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Cave,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Clearing Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsClearing(),
                new Clearing
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Clearing,
                    GeographicalDescription = "Test"
                },
                new Clearing
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Clearing,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Dock Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsDock(),
                new Dock
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Dock,
                    GeographicalDescription = "Test"
                },
                new Dock
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Dock,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for DragonLair Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsDragonLair(),
                new DragonLair
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.DragonLair,
                    GeographicalDescription = "Test"
                },
                new DragonLair
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.DragonLair,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for DwarvenRuin Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsDwarvenRuin(),
                new DwarvenRuin
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.DwarvenRuin,
                    GeographicalDescription = "Test"
                },
                new DwarvenRuin
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.DwarvenRuin,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Farm Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsFarm(),
                new Farm
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Farm,
                    GeographicalDescription = "Test"
                },
                new Farm
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Farm,
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
                _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewCity());
            else if (location.TypeOfLocation == LocationType.Town)
                _mockMapper.Setup(x => x.Map<Town>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewTown());
            else if (location.TypeOfLocation == LocationType.Homestead)
                _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewHomestead());
            else if (location.TypeOfLocation == LocationType.Settlement)
                _mockMapper.Setup(x => x.Map<Settlement>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewSettlement());
            else if (location.TypeOfLocation == LocationType.DaedricShrine)
                _mockMapper.Setup(x => x.Map<DaedricShrine>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewDaedricShrine());
            else if (location.TypeOfLocation == LocationType.StandingStone)
                _mockMapper.Setup(x => x.Map<StandingStone>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewStandingStone());
            else if (location.TypeOfLocation == LocationType.Landmark)
                _mockMapper.Setup(x => x.Map<Landmark>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewLandmark());
            else if (location.TypeOfLocation == LocationType.Camp)
                _mockMapper.Setup(x => x.Map<Camp>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewCamp());
            else if (location.TypeOfLocation == LocationType.Cave)
                _mockMapper.Setup(x => x.Map<Cave>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewCave());
            else if (location.TypeOfLocation == LocationType.Clearing)
                _mockMapper.Setup(x => x.Map<Clearing>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewClearing());
            else if (location.TypeOfLocation == LocationType.Dock)
                _mockMapper.Setup(x => x.Map<Dock>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewDock());
            else if (location.TypeOfLocation == LocationType.DragonLair)
                _mockMapper.Setup(x => x.Map<DragonLair>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewDragonLair());
            else if (location.TypeOfLocation == LocationType.DwarvenRuin)
                _mockMapper.Setup(x => x.Map<DwarvenRuin>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewDwarvenRuin());
            else if (location.TypeOfLocation == LocationType.Farm)
                _mockMapper.Setup(x => x.Map<Farm>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewFarm());

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
            else if (location.TypeOfLocation == LocationType.Landmark)
                _mockMapper.Verify(x => x.Map<Landmark>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Camp)
                _mockMapper.Verify(x => x.Map<Camp>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Cave)
                _mockMapper.Verify(x => x.Map<Cave>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Clearing)
                _mockMapper.Verify(x => x.Map<Clearing>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Dock)
                _mockMapper.Verify(x => x.Map<Dock>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.DragonLair)
                _mockMapper.Verify(x => x.Map<DragonLair>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.DwarvenRuin)
                _mockMapper.Verify(x => x.Map<DwarvenRuin>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Farm)
                _mockMapper.Verify(x => x.Map<Farm>(createLocationDto), Times.Once());
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
            yield return new object[]
            {
                "Invalid properties for StandingStone",
                new CreateLocationDto { TypeOfLocation = LocationType.StandingStone }
            };
            yield return new object[]
            {
                "Invalid properties for Landmark",
                new CreateLocationDto { TypeOfLocation = LocationType.Landmark }
            };
            yield return new object[]
            {
                "Invalid properties for Camp",
                new CreateLocationDto { TypeOfLocation = LocationType.Camp }
            };
            yield return new object[]
            {
                "Invalid properties for Cave",
                new CreateLocationDto { TypeOfLocation = LocationType.Cave }
            };
            yield return new object[]
            {
                "Invalid properties for Clearing",
                new CreateLocationDto { TypeOfLocation = LocationType.Clearing }
            };
            yield return new object[]
            {
                "Invalid properties for Dock",
                new CreateLocationDto { TypeOfLocation = LocationType.Dock }
            };
            yield return new object[]
            {
                "Invalid properties for DragonLair",
                new CreateLocationDto { TypeOfLocation = LocationType.DragonLair }
            };
            yield return new object[]
            {
                "Invalid properties for DwarvenRuin",
                new CreateLocationDto { TypeOfLocation = LocationType.DwarvenRuin }
            };
            yield return new object[]
            {
                "Invalid properties for Farm",
                new CreateLocationDto { TypeOfLocation = LocationType.Farm }
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
            else if (location.TypeOfLocation == LocationType.StandingStone)
                _mockMapper.Setup(x => x.Map<StandingStone>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Landmark)
                _mockMapper.Setup(x => x.Map<Landmark>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Camp)
                _mockMapper.Setup(x => x.Map<Camp>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Cave)
                _mockMapper.Setup(x => x.Map<Cave>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Clearing)
                _mockMapper.Setup(x => x.Map<Clearing>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Dock)
                _mockMapper.Setup(x => x.Map<Dock>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.DragonLair)
                _mockMapper.Setup(x => x.Map<DragonLair>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.DwarvenRuin)
                _mockMapper.Setup(x => x.Map<DwarvenRuin>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Farm)
                _mockMapper.Setup(x => x.Map<Farm>(It.IsAny<CreateLocationDto>())).Throws(new Exception());

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
                TestMethodHelpers.CreateNewCity(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsCity(),
            };
            yield return new object[]
            {
                "Invalid properties for Town",
                TestMethodHelpers.CreateNewTown(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsTown()
            };
            yield return new object[]
            {
                "Invalid properties for Homestead",
                TestMethodHelpers.CreateNewHomestead(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsHomestead()
            };
            yield return new object[]
            {
                "Invalid properties for Settlement",
                TestMethodHelpers.CreateNewSettlement(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsSettlement()
            };
            yield return new object[]
            {
                "Invalid properties for DaedricShrine",
                TestMethodHelpers.CreateNewDaedricShrine(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsDaedricShrine()
            };
            yield return new object[]
            {
                "Invalid properties for StandingStone",
                TestMethodHelpers.CreateNewStandingStone(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsStandingStone()
            };
            yield return new object[]
            {
                "Invalid properties for Landmark",
                TestMethodHelpers.CreateNewLandmark(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsLandmark()
            };
            yield return new object[]
            {
                "Invalid properties for Camp",
                TestMethodHelpers.CreateNewCamp(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsCamp()
            };
            yield return new object[]
            {
                "Invalid properties for Cave",
                TestMethodHelpers.CreateNewCave(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsCave()
            };
            yield return new object[]
            {
                "Invalid properties for Clearing",
                TestMethodHelpers.CreateNewClearing(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsClearing()
            };
            yield return new object[]
            {
                "Invalid properties for Dock",
                TestMethodHelpers.CreateNewDock(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsDock()
            };
            yield return new object[]
            {
                "Invalid properties for DragonLair",
                TestMethodHelpers.CreateNewDragonLair(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsDragonLair()
            };
            yield return new object[]
            {
                "Invalid properties for DwarvenRuin",
                TestMethodHelpers.CreateNewDwarvenRuin(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsDwarvenRuin()
            };
            yield return new object[]
            {
                "Invalid properties for Farm",
                TestMethodHelpers.CreateNewFarm(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsFarm()
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
                _mockMapper.Setup(x => x.Map<City>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewCity());
            else if (type.TypeOfLocation == LocationType.Town)
                _mockMapper.Setup(x => x.Map<Town>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewTown());
            else if (type.TypeOfLocation == LocationType.Homestead)
                _mockMapper.Setup(x => x.Map<Homestead>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewHomestead());
            else if (type.TypeOfLocation == LocationType.Settlement)
                _mockMapper.Setup(x => x.Map<Settlement>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewSettlement());
            else if (type.TypeOfLocation == LocationType.DaedricShrine)
                _mockMapper.Setup(x => x.Map<DaedricShrine>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewDaedricShrine());
            else if (type.TypeOfLocation == LocationType.StandingStone)
                _mockMapper.Setup(x => x.Map<StandingStone>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewStandingStone());
            else if (type.TypeOfLocation == LocationType.Landmark)
                _mockMapper.Setup(x => x.Map<Landmark>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewLandmark());
            else if (type.TypeOfLocation == LocationType.Camp)
                _mockMapper.Setup(x => x.Map<Camp>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewCamp());
            else if (type.TypeOfLocation == LocationType.Cave)
                _mockMapper.Setup(x => x.Map<Cave>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewCave());
            else if (type.TypeOfLocation == LocationType.Clearing)
                _mockMapper.Setup(x => x.Map<Clearing>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewClearing());
            else if (type.TypeOfLocation == LocationType.Dock)
                _mockMapper.Setup(x => x.Map<Dock>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewDock());
            else if (type.TypeOfLocation == LocationType.DragonLair)
                _mockMapper.Setup(x => x.Map<DragonLair>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewDragonLair());
            else if (type.TypeOfLocation == LocationType.DwarvenRuin)
                _mockMapper.Setup(x => x.Map<DwarvenRuin>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewDwarvenRuin());
            else if (type.TypeOfLocation == LocationType.Farm)
                _mockMapper.Setup(x => x.Map<Farm>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewFarm());

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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsCity(),
                    TestMethodHelpers.CreateNewCity(),
                    TestMethodHelpers.CreateNewCity()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsCity(),
                    TestMethodHelpers.CreateNewCity(),
                    TestMethodHelpers.CreateNewCity()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsCity(),
                    TestMethodHelpers.CreateNewCity(),
                    TestMethodHelpers.CreateNewCity()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsTown(),
                    TestMethodHelpers.CreateNewTown(),
                    TestMethodHelpers.CreateNewTown()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsTown(),
                    TestMethodHelpers.CreateNewTown(),
                    TestMethodHelpers.CreateNewTown()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsTown(),
                    TestMethodHelpers.CreateNewTown(),
                    TestMethodHelpers.CreateNewTown()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsHomestead(),
                    TestMethodHelpers.CreateNewHomestead(),
                    TestMethodHelpers.CreateNewHomestead()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsHomestead(),
                    TestMethodHelpers.CreateNewHomestead(),
                    TestMethodHelpers.CreateNewHomestead()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsHomestead(),
                    TestMethodHelpers.CreateNewHomestead(),
                    TestMethodHelpers.CreateNewHomestead()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsSettlement(),
                    TestMethodHelpers.CreateNewSettlement(),
                    TestMethodHelpers.CreateNewSettlement()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsSettlement(),
                    TestMethodHelpers.CreateNewSettlement(),
                    TestMethodHelpers.CreateNewSettlement()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsSettlement(),
                    TestMethodHelpers.CreateNewSettlement(),
                    TestMethodHelpers.CreateNewSettlement()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsDaedricShrine(),
                    TestMethodHelpers.CreateNewDaedricShrine(),
                    TestMethodHelpers.CreateNewDaedricShrine()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsDaedricShrine(),
                    TestMethodHelpers.CreateNewDaedricShrine(),
                    TestMethodHelpers.CreateNewDaedricShrine()
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
                    TestMethodHelpers.CreateNewCreateLocationDtoAsDaedricShrine(),
                    TestMethodHelpers.CreateNewDaedricShrine(),
                    TestMethodHelpers.CreateNewDaedricShrine()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a StandingStone with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.StandingStone,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsStandingStone(),
                    TestMethodHelpers.CreateNewStandingStone(),
                    TestMethodHelpers.CreateNewStandingStone()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a StandingStone with empty description",
                new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.StandingStone,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsStandingStone(),
                    TestMethodHelpers.CreateNewStandingStone(),
                    TestMethodHelpers.CreateNewStandingStone()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a StandingStone with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.StandingStone,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsStandingStone(),
                    TestMethodHelpers.CreateNewStandingStone(),
                    TestMethodHelpers.CreateNewStandingStone()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Landmark with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Landmark,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsLandmark(),
                    TestMethodHelpers.CreateNewLandmark(),
                    TestMethodHelpers.CreateNewLandmark()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Landmark with empty description",
                new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Landmark,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsLandmark(),
                    TestMethodHelpers.CreateNewLandmark(),
                    TestMethodHelpers.CreateNewLandmark()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Landmark with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Landmark,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsLandmark(),
                    TestMethodHelpers.CreateNewLandmark(),
                    TestMethodHelpers.CreateNewLandmark()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Camp with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Camp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsCamp(),
                    TestMethodHelpers.CreateNewCamp(),
                    TestMethodHelpers.CreateNewCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Camp with empty description", 
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Camp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsCamp(),
                    TestMethodHelpers.CreateNewCamp(),
                    TestMethodHelpers.CreateNewCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Camp with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Camp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsCamp(),
                    TestMethodHelpers.CreateNewCamp(),
                    TestMethodHelpers.CreateNewCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Cave with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Cave,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsCave(),
                    TestMethodHelpers.CreateNewCave(),
                    TestMethodHelpers.CreateNewCave()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Cave with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Cave,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsCave(),
                    TestMethodHelpers.CreateNewCave(),
                    TestMethodHelpers.CreateNewCave()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Cave with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Cave,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsCave(),
                    TestMethodHelpers.CreateNewCave(),
                    TestMethodHelpers.CreateNewCave()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Clearing with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Clearing,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsClearing(),
                    TestMethodHelpers.CreateNewClearing(),
                    TestMethodHelpers.CreateNewClearing()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Clearing with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Clearing,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsClearing(),
                    TestMethodHelpers.CreateNewClearing(),
                    TestMethodHelpers.CreateNewClearing()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Clearing with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Clearing,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsClearing(),
                    TestMethodHelpers.CreateNewClearing(),
                    TestMethodHelpers.CreateNewClearing()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Dock with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Dock,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsDock(),
                    TestMethodHelpers.CreateNewDock(),
                    TestMethodHelpers.CreateNewDock()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Dock with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Dock,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsDock(),
                    TestMethodHelpers.CreateNewDock(),
                    TestMethodHelpers.CreateNewDock()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Dock with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Dock,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsDock(),
                    TestMethodHelpers.CreateNewDock(),
                    TestMethodHelpers.CreateNewDock()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a DragonLair with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.DragonLair,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsDragonLair(),
                    TestMethodHelpers.CreateNewDragonLair(),
                    TestMethodHelpers.CreateNewDragonLair()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a DragonLair with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.DragonLair,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsDragonLair(),
                    TestMethodHelpers.CreateNewDragonLair(),
                    TestMethodHelpers.CreateNewDragonLair()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a DragonLair with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.DragonLair,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsDragonLair(),
                    TestMethodHelpers.CreateNewDragonLair(),
                    TestMethodHelpers.CreateNewDragonLair()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a DwarvenRuin with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.DwarvenRuin,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsDwarvenRuin(),
                    TestMethodHelpers.CreateNewDwarvenRuin(),
                    TestMethodHelpers.CreateNewDwarvenRuin()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a DwarvenRuin with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.DwarvenRuin,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsDwarvenRuin(),
                    TestMethodHelpers.CreateNewDwarvenRuin(),
                    TestMethodHelpers.CreateNewDwarvenRuin()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a DwarvenRuin with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.DwarvenRuin,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsDwarvenRuin(),
                    TestMethodHelpers.CreateNewDwarvenRuin(),
                    TestMethodHelpers.CreateNewDwarvenRuin()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Farm with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Farm,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsFarm(),
                    TestMethodHelpers.CreateNewFarm(),
                    TestMethodHelpers.CreateNewFarm()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Farm with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Farm,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsFarm(),
                    TestMethodHelpers.CreateNewFarm(),
                    TestMethodHelpers.CreateNewFarm()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Farm with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Farm,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsFarm(),
                    TestMethodHelpers.CreateNewFarm(),
                    TestMethodHelpers.CreateNewFarm()
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
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        TypeOfLocation = LocationType.StandingStone
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
                        TypeOfLocation = LocationType.StandingStone
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
                        TypeOfLocation = LocationType.StandingStone
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
                        TypeOfLocation = LocationType.StandingStone
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
                        TypeOfLocation = LocationType.StandingStone
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
                        TypeOfLocation = LocationType.StandingStone
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
                        TypeOfLocation = LocationType.Landmark
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
                        TypeOfLocation = LocationType.Landmark
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
                        TypeOfLocation = LocationType.Landmark
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
                        TypeOfLocation = LocationType.Landmark
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
                        TypeOfLocation = LocationType.Landmark
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
                        TypeOfLocation = LocationType.Landmark
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
                        TypeOfLocation = LocationType.Camp
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
                        TypeOfLocation = LocationType.Camp
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
                        TypeOfLocation = LocationType.Camp
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
                        TypeOfLocation = LocationType.Camp
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
                        TypeOfLocation = LocationType.Camp
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
                        TypeOfLocation = LocationType.Camp
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
                        TypeOfLocation = LocationType.Cave
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
                        TypeOfLocation = LocationType.Cave
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
                        TypeOfLocation = LocationType.Cave
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
                        TypeOfLocation = LocationType.Cave
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
                        TypeOfLocation = LocationType.Cave
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
                        TypeOfLocation = LocationType.Cave
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
                        TypeOfLocation = LocationType.Clearing
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
                        TypeOfLocation = LocationType.Clearing
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
                        TypeOfLocation = LocationType.Clearing
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
                        TypeOfLocation = LocationType.Clearing
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
                        TypeOfLocation = LocationType.Clearing
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
                        TypeOfLocation = LocationType.Clearing
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
                        TypeOfLocation = LocationType.Dock
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
                        TypeOfLocation = LocationType.Dock
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
                        TypeOfLocation = LocationType.Dock
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
                        TypeOfLocation = LocationType.Dock
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
                        TypeOfLocation = LocationType.Dock
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
                        TypeOfLocation = LocationType.Dock
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
                        TypeOfLocation = LocationType.DragonLair
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
                        TypeOfLocation = LocationType.DragonLair
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
                        TypeOfLocation = LocationType.DragonLair
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
                        TypeOfLocation = LocationType.DragonLair
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
                        TypeOfLocation = LocationType.DragonLair
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
                        TypeOfLocation = LocationType.DragonLair
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
                        TypeOfLocation = LocationType.DwarvenRuin
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
                        TypeOfLocation = LocationType.DwarvenRuin
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
                        TypeOfLocation = LocationType.DwarvenRuin
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
                        TypeOfLocation = LocationType.DwarvenRuin
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
                        TypeOfLocation = LocationType.DwarvenRuin
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
                        TypeOfLocation = LocationType.DwarvenRuin
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
                        TypeOfLocation = LocationType.Farm
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
                        TypeOfLocation = LocationType.Farm
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
                        TypeOfLocation = LocationType.Farm
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
                        TypeOfLocation = LocationType.Farm
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
                        TypeOfLocation = LocationType.Farm
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
                        TypeOfLocation = LocationType.Farm
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
