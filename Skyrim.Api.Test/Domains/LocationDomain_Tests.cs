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
            else if (type.TypeOfLocation == LocationType.Fort)
                _mockMapper.Setup(x => x.Map<Fort>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewFort());
            else if (type.TypeOfLocation == LocationType.GiantCamp)
                _mockMapper.Setup(x => x.Map<GiantCamp>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewGiantCamp());
            else if (type.TypeOfLocation == LocationType.Grove)
                _mockMapper.Setup(x => x.Map<Grove>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewGrove());
            else if (type.TypeOfLocation == LocationType.ImperialCamp)
                _mockMapper.Setup(x => x.Map<ImperialCamp>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewImperialCamp());
            else if (type.TypeOfLocation == LocationType.LightHouse)
                _mockMapper.Setup(x => x.Map<LightHouse>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewLightHouse());
            else if (type.TypeOfLocation == LocationType.Mine)
                _mockMapper.Setup(x => x.Map<Mine>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewMine());
            else if (type.TypeOfLocation == LocationType.NordicTower)
                _mockMapper.Setup(x => x.Map<NordicTower>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewNordicTower());
            else if (type.TypeOfLocation == LocationType.OrcStronghold)
                _mockMapper.Setup(x => x.Map<OrcStronghold>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewOrcStronghold());
            else if (type.TypeOfLocation == LocationType.Pass)
                _mockMapper.Setup(x => x.Map<Pass>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewPass());
            else if (type.TypeOfLocation == LocationType.Ruin)
                _mockMapper.Setup(x => x.Map<Ruin>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewRuin());
            else if (type.TypeOfLocation == LocationType.Shack)
                _mockMapper.Setup(x => x.Map<Shack>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewShack());
            else if (type.TypeOfLocation == LocationType.Ship)
                _mockMapper.Setup(x => x.Map<Ship>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewShip());
            else if (type.TypeOfLocation == LocationType.Shipwreck)
                _mockMapper.Setup(x => x.Map<Shipwreck>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewShipwreck());
            else if (type.TypeOfLocation == LocationType.Stable)
                _mockMapper.Setup(x => x.Map<Stable>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewStable());
            else if (type.TypeOfLocation == LocationType.StormcloakCamp)
                _mockMapper.Setup(x => x.Map<StormcloakCamp>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewStormcloakCamp());
            else if (type.TypeOfLocation == LocationType.Tomb)
                _mockMapper.Setup(x => x.Map<Tomb>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewTomb());
            else if (type.TypeOfLocation == LocationType.Watchtower)
                _mockMapper.Setup(x => x.Map<Watchtower>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewWatchtower());
            else if (type.TypeOfLocation == LocationType.WheatMill)
                _mockMapper.Setup(x => x.Map<WheatMill>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewWheatMill());
            else if (type.TypeOfLocation == LocationType.LumberMill)
                _mockMapper.Setup(x => x.Map<LumberMill>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewLumberMill());
            else if (type.TypeOfLocation == LocationType.BodyOfWater)
                _mockMapper.Setup(x => x.Map<BodyOfWater>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewBodyOfWater());
            else if (type.TypeOfLocation == LocationType.InnOrTavern)
                _mockMapper.Setup(x => x.Map<InnOrTavern>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewInnOrTavern());
            else if (type.TypeOfLocation == LocationType.Temple)
                _mockMapper.Setup(x => x.Map<Temple>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewTemple());
            else if (type.TypeOfLocation == LocationType.WordWall)
                _mockMapper.Setup(x => x.Map<WordWall>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewWordWall());

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
            yield return new object[]
            {
                "Valid properties for Fort Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsFort(),
                new Fort
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Fort,
                    GeographicalDescription = "Test"
                },
                new Fort
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Fort,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for GiantCamp Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsGiantCamp(),
                new GiantCamp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.GiantCamp,
                    GeographicalDescription = "Test"
                },
                new GiantCamp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.GiantCamp,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Grove Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsGrove(),
                new Grove
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Grove,
                    GeographicalDescription = "Test"
                },
                new Grove
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Grove,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for ImperialCamp Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsImperialCamp(),
                new ImperialCamp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.ImperialCamp,
                    GeographicalDescription = "Test"
                },
                new ImperialCamp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.ImperialCamp,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for LightHouse Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsLightHouse(),
                new LightHouse
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.LightHouse,
                    GeographicalDescription = "Test"
                },
                new LightHouse
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.LightHouse,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Mine Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsMine(),
                new Mine
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Mine,
                    GeographicalDescription = "Test"
                },
                new Mine
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Mine,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for NordicTower Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsNordicTower(),
                new NordicTower
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.NordicTower,
                    GeographicalDescription = "Test"
                },
                new NordicTower
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.NordicTower,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for OrcStronghold Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsOrcStronghold(),
                new OrcStronghold
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.OrcStronghold,
                    GeographicalDescription = "Test"
                },
                new OrcStronghold
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.OrcStronghold,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Pass Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsPass(),
                new Pass
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Pass,
                    GeographicalDescription = "Test"
                },
                new Pass
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Pass,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Ruin Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsRuin(),
                new Ruin
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Ruin,
                    GeographicalDescription = "Test"
                },
                new Ruin
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Ruin,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Shack Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsShack(),
                new Shack
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Shack,
                    GeographicalDescription = "Test"
                },
                new Shack
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Shack,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Ship Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsShip(),
                new Ship
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Ship,
                    GeographicalDescription = "Test"
                },
                new Ship
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Ship,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Shipwreck Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsShipwreck(),
                new Shipwreck
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Shipwreck,
                    GeographicalDescription = "Test"
                },
                new Shipwreck
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Shipwreck,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Stable Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsStable(),
                new Stable
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Stable,
                    GeographicalDescription = "Test"
                },
                new Stable
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Stable,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for StormcloakCamp Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsStormcloakCamp(),
                new StormcloakCamp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.StormcloakCamp,
                    GeographicalDescription = "Test"
                },
                new StormcloakCamp
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.StormcloakCamp,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Tomb Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsTomb(),
                new Tomb
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Tomb,
                    GeographicalDescription = "Test"
                },
                new Tomb
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Tomb,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Watchtower Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsWatchtower(),
                new Watchtower
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Watchtower,
                    GeographicalDescription = "Test"
                },
                new Watchtower
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Watchtower,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for WheatMill Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsWheatMill(),
                new WheatMill
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.WheatMill,
                    GeographicalDescription = "Test"
                },
                new WheatMill
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.WheatMill,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for LumberMill Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsLumberMill(),
                new LumberMill
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.LumberMill,
                    GeographicalDescription = "Test"
                },
                new LumberMill
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.LumberMill,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for BodyOfWater Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsBodyOfWater(),
                new BodyOfWater
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.BodyOfWater,
                    GeographicalDescription = "Test"
                },
                new BodyOfWater
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.BodyOfWater,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for InnOrTavern Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsInnOrTavern(),
                new InnOrTavern
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.InnOrTavern,
                    GeographicalDescription = "Test"
                },
                new InnOrTavern
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.InnOrTavern,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for Temple Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsTemple(),
                new Temple
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Temple,
                    GeographicalDescription = "Test"
                },
                new Temple
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.Temple,
                    GeographicalDescription = "Test"
                }
            };
            yield return new object[]
            {
                "Valid properties for WordWall Location",
                TestMethodHelpers.CreateNewCreateLocationDtoAsWordWall(),
                new WordWall
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.WordWall,
                    GeographicalDescription = "Test"
                },
                new WordWall
                {
                    Id = 0,
                    Name = "Test",
                    Description = "Test",
                    TypeOfLocation = LocationType.WordWall,
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
            else if (location.TypeOfLocation == LocationType.Fort)
                _mockMapper.Setup(x => x.Map<Fort>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewFort());
            else if (location.TypeOfLocation == LocationType.GiantCamp)
                _mockMapper.Setup(x => x.Map<GiantCamp>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewGiantCamp());
            else if (location.TypeOfLocation == LocationType.Grove)
                _mockMapper.Setup(x => x.Map<Grove>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewGrove());
            else if (location.TypeOfLocation == LocationType.ImperialCamp)
                _mockMapper.Setup(x => x.Map<ImperialCamp>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewImperialCamp());
            else if (location.TypeOfLocation == LocationType.LightHouse)
                _mockMapper.Setup(x => x.Map<LightHouse>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewLightHouse());
            else if (location.TypeOfLocation == LocationType.Mine)
                _mockMapper.Setup(x => x.Map<Mine>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewMine());
            else if (location.TypeOfLocation == LocationType.NordicTower)
                _mockMapper.Setup(x => x.Map<NordicTower>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewNordicTower());
            else if (location.TypeOfLocation == LocationType.OrcStronghold)
                _mockMapper.Setup(x => x.Map<OrcStronghold>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewOrcStronghold());
            else if (location.TypeOfLocation == LocationType.Pass)
                _mockMapper.Setup(x => x.Map<Pass>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewPass());
            else if (location.TypeOfLocation == LocationType.Ruin)
                _mockMapper.Setup(x => x.Map<Ruin>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewRuin());
            else if (location.TypeOfLocation == LocationType.Shack)
                _mockMapper.Setup(x => x.Map<Shack>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewShack());
            else if (location.TypeOfLocation == LocationType.Ship)
                _mockMapper.Setup(x => x.Map<Ship>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewShip());
            else if (location.TypeOfLocation == LocationType.Shipwreck)
                _mockMapper.Setup(x => x.Map<Shipwreck>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewShipwreck());
            else if (location.TypeOfLocation == LocationType.Stable)
                _mockMapper.Setup(x => x.Map<Stable>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewStable());
            else if (location.TypeOfLocation == LocationType.StormcloakCamp)
                _mockMapper.Setup(x => x.Map<StormcloakCamp>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewStormcloakCamp());
            else if (location.TypeOfLocation == LocationType.Tomb)
                _mockMapper.Setup(x => x.Map<Tomb>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewTomb());
            else if (location.TypeOfLocation == LocationType.Watchtower)
                _mockMapper.Setup(x => x.Map<Watchtower>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewWatchtower());
            else if (location.TypeOfLocation == LocationType.WheatMill)
                _mockMapper.Setup(x => x.Map<WheatMill>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewWheatMill());
            else if (location.TypeOfLocation == LocationType.LumberMill)
                _mockMapper.Setup(x => x.Map<LumberMill>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewLumberMill());
            else if (location.TypeOfLocation == LocationType.BodyOfWater)
                _mockMapper.Setup(x => x.Map<BodyOfWater>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewBodyOfWater());
            else if (location.TypeOfLocation == LocationType.InnOrTavern)
                _mockMapper.Setup(x => x.Map<InnOrTavern>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewInnOrTavern());
            else if (location.TypeOfLocation == LocationType.Temple)
                _mockMapper.Setup(x => x.Map<Temple>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewTemple());
            else if (location.TypeOfLocation == LocationType.WordWall)
                _mockMapper.Setup(x => x.Map<WordWall>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewWordWall());

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
            else if (location.TypeOfLocation == LocationType.StandingStone)
                _mockMapper.Verify(x => x.Map<StandingStone>(createLocationDto), Times.Once());
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
            else if (location.TypeOfLocation == LocationType.Fort)
                _mockMapper.Verify(x => x.Map<Fort>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.GiantCamp)
                _mockMapper.Verify(x => x.Map<GiantCamp>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Grove)
                _mockMapper.Verify(x => x.Map<Grove>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.ImperialCamp)
                _mockMapper.Verify(x => x.Map<ImperialCamp>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.LightHouse)
                _mockMapper.Verify(x => x.Map<LightHouse>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Mine)
                _mockMapper.Verify(x => x.Map<Mine>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.NordicTower)
                _mockMapper.Verify(x => x.Map<NordicTower>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.OrcStronghold)
                _mockMapper.Verify(x => x.Map<OrcStronghold>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Pass)
                _mockMapper.Verify(x => x.Map<Pass>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Ruin)
                _mockMapper.Verify(x => x.Map<Ruin>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Shack)
                _mockMapper.Verify(x => x.Map<Shack>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Ship)
                _mockMapper.Verify(x => x.Map<Ship>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Shipwreck)
                _mockMapper.Verify(x => x.Map<Shipwreck>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Stable)
                _mockMapper.Verify(x => x.Map<Stable>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.StormcloakCamp)
                _mockMapper.Verify(x => x.Map<StormcloakCamp>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Tomb)
                _mockMapper.Verify(x => x.Map<Tomb>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Watchtower)
                _mockMapper.Verify(x => x.Map<Watchtower>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.WheatMill)
                _mockMapper.Verify(x => x.Map<WheatMill>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.LumberMill)
                _mockMapper.Verify(x => x.Map<LumberMill>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.BodyOfWater)
                _mockMapper.Verify(x => x.Map<BodyOfWater>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.InnOrTavern)
                _mockMapper.Verify(x => x.Map<InnOrTavern>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.Temple)
                _mockMapper.Verify(x => x.Map<Temple>(createLocationDto), Times.Once());
            else if (location.TypeOfLocation == LocationType.WordWall)
                _mockMapper.Verify(x => x.Map<WordWall>(createLocationDto), Times.Once());
            else
                Assert.True(false);
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
            yield return new object[]
            {
                "Invalid properties for Fort",
                new CreateLocationDto { TypeOfLocation = LocationType.Fort }
            };
            yield return new object[]
            {
                "Invalid properties for GiantCamp",
                new CreateLocationDto { TypeOfLocation = LocationType.GiantCamp }
            };
            yield return new object[]
            {
                "Invalid properties for Grove",
                new CreateLocationDto { TypeOfLocation = LocationType.Grove }
            };
            yield return new object[]
            {
                "Invalid properties for ImperialCamp",
                new CreateLocationDto { TypeOfLocation = LocationType.ImperialCamp }
            };
            yield return new object[]
            {
                "Invalid properties for LightHouse",
                new CreateLocationDto { TypeOfLocation = LocationType.LightHouse }
            };
            yield return new object[]
            {
                "Invalid properties for Mine",
                new CreateLocationDto { TypeOfLocation = LocationType.Mine }
            };
            yield return new object[]
            {
                "Invalid properties for NordicTower",
                new CreateLocationDto { TypeOfLocation = LocationType.NordicTower }
            };
            yield return new object[]
            {
                "Invalid properties for OrcStronghold",
                new CreateLocationDto { TypeOfLocation = LocationType.OrcStronghold }
            };
            yield return new object[]
            {
                "Invalid properties for Pass",
                new CreateLocationDto { TypeOfLocation = LocationType.Pass }
            };
            yield return new object[]
            {
                "Invalid properties for Ruin",
                new CreateLocationDto { TypeOfLocation = LocationType.Ruin }
            };
            yield return new object[]
            {
                "Invalid properties for Shack",
                new CreateLocationDto { TypeOfLocation = LocationType.Shack }
            };
            yield return new object[]
            {
                "Invalid properties for Ship",
                new CreateLocationDto { TypeOfLocation = LocationType.Ship }
            };
            yield return new object[]
            {
                "Invalid properties for Shipwreck",
                new CreateLocationDto { TypeOfLocation = LocationType.Shipwreck }
            };
            yield return new object[]
            {
                "Invalid properties for Stable",
                new CreateLocationDto { TypeOfLocation = LocationType.Stable }
            };
            yield return new object[]
            {
                "Invalid properties for StormcloakCamp",
                new CreateLocationDto { TypeOfLocation = LocationType.StormcloakCamp }
            };
            yield return new object[]
            {
                "Invalid properties for Tomb",
                new CreateLocationDto { TypeOfLocation = LocationType.Tomb }
            };
            yield return new object[]
            {
                "Invalid properties for Watchtower",
                new CreateLocationDto { TypeOfLocation = LocationType.Watchtower }
            };
            yield return new object[]
            {
                "Invalid properties for WheatMill",
                new CreateLocationDto { TypeOfLocation = LocationType.WheatMill }
            };
            yield return new object[]
            {
                "Invalid properties for LumberMill",
                new CreateLocationDto { TypeOfLocation = LocationType.LumberMill }
            };
            yield return new object[]
            {
                "Invalid properties for BodyOfWater",
                new CreateLocationDto { TypeOfLocation = LocationType.BodyOfWater }
            };
            yield return new object[]
            {
                "Invalid properties for InnOrTavern",
                new CreateLocationDto { TypeOfLocation = LocationType.InnOrTavern }
            };
            yield return new object[]
            {
                "Invalid properties for Temple",
                new CreateLocationDto { TypeOfLocation = LocationType.Temple }
            };
            yield return new object[]
            {
                "Invalid properties for WordWall",
                new CreateLocationDto { TypeOfLocation = LocationType.WordWall }
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
            else if (location.TypeOfLocation == LocationType.Fort)
                _mockMapper.Setup(x => x.Map<Fort>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.GiantCamp)
                _mockMapper.Setup(x => x.Map<GiantCamp>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Grove)
                _mockMapper.Setup(x => x.Map<Grove>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.ImperialCamp)
                _mockMapper.Setup(x => x.Map<ImperialCamp>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.LightHouse)
                _mockMapper.Setup(x => x.Map<LightHouse>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Mine)
                _mockMapper.Setup(x => x.Map<Mine>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.NordicTower)
                _mockMapper.Setup(x => x.Map<NordicTower>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.OrcStronghold)
                _mockMapper.Setup(x => x.Map<OrcStronghold>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Pass)
                _mockMapper.Setup(x => x.Map<Pass>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Ruin)
                _mockMapper.Setup(x => x.Map<Ruin>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Shack)
                _mockMapper.Setup(x => x.Map<Shack>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Ship)
                _mockMapper.Setup(x => x.Map<Ship>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Shipwreck)
                _mockMapper.Setup(x => x.Map<Shipwreck>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Stable)
                _mockMapper.Setup(x => x.Map<Stable>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.StormcloakCamp)
                _mockMapper.Setup(x => x.Map<StormcloakCamp>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Tomb)
                _mockMapper.Setup(x => x.Map<Tomb>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Watchtower)
                _mockMapper.Setup(x => x.Map<Watchtower>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.WheatMill)
                _mockMapper.Setup(x => x.Map<WheatMill>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.LumberMill)
                _mockMapper.Setup(x => x.Map<LumberMill>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.BodyOfWater)
                _mockMapper.Setup(x => x.Map<BodyOfWater>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.InnOrTavern)
                _mockMapper.Setup(x => x.Map<InnOrTavern>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.Temple)
                _mockMapper.Setup(x => x.Map<Temple>(It.IsAny<CreateLocationDto>())).Throws(new Exception());
            else if (location.TypeOfLocation == LocationType.WordWall)
                _mockMapper.Setup(x => x.Map<WordWall>(It.IsAny<CreateLocationDto>())).Throws(new Exception());

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
            yield return new object[]
            {
                "Invalid properties for Fort",
                TestMethodHelpers.CreateNewFort(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsFort()
            };
            yield return new object[]
           {
                "Invalid properties for GiantCamp",
                TestMethodHelpers.CreateNewGiantCamp(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsGiantCamp()
           };
            yield return new object[]
           {
                "Invalid properties for Grove",
                TestMethodHelpers.CreateNewGrove(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsGrove()
           };
            yield return new object[]
           {
                "Invalid properties for ImperialCamp",
                TestMethodHelpers.CreateNewImperialCamp(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsImperialCamp()
           };
            yield return new object[]
           {
                "Invalid properties for LightHouse",
                TestMethodHelpers.CreateNewLightHouse(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsLightHouse()
           };
            yield return new object[]
           {
                "Invalid properties for Mine",
                TestMethodHelpers.CreateNewMine(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsMine()
           };
            yield return new object[]
           {
                "Invalid properties for NordicTower",
                TestMethodHelpers.CreateNewNordicTower(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsNordicTower()
           };
            yield return new object[]
           {
                "Invalid properties for OrcStronghold",
                TestMethodHelpers.CreateNewOrcStronghold(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsOrcStronghold()
           };
            yield return new object[]
           {
                "Invalid properties for Pass",
                TestMethodHelpers.CreateNewPass(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsPass()
           };
            yield return new object[]
           {
                "Invalid properties for Ruin",
                TestMethodHelpers.CreateNewRuin(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsRuin()
           };
            yield return new object[]
           {
                "Invalid properties for Shack",
                TestMethodHelpers.CreateNewShack(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsShack()
           };
            yield return new object[]
           {
                "Invalid properties for Ship",
                TestMethodHelpers.CreateNewShip(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsShip()
           };
            yield return new object[]
           {
                "Invalid properties for Shipwreck",
                TestMethodHelpers.CreateNewShipwreck(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsShipwreck()
           };
            yield return new object[]
           {
                "Invalid properties for Stable",
                TestMethodHelpers.CreateNewStable(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsStable()
           };
            yield return new object[]
           {
                "Invalid properties for StormcloakCamp",
                TestMethodHelpers.CreateNewStormcloakCamp(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsStormcloakCamp()
           };
            yield return new object[]
           {
                "Invalid properties for Tomb",
                TestMethodHelpers.CreateNewTomb(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsTomb()
           };
            yield return new object[]
           {
                "Invalid properties for Watchtower",
                TestMethodHelpers.CreateNewWatchtower(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsWatchtower()
           };
            yield return new object[]
           {
                "Invalid properties for WheatMill",
                TestMethodHelpers.CreateNewWheatMill(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsWheatMill()
           };
            yield return new object[]
           {
                "Invalid properties for LumberMill",
                TestMethodHelpers.CreateNewLumberMill(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsLumberMill()
           };
            yield return new object[]
           {
                "Invalid properties for BodyOfWater",
                TestMethodHelpers.CreateNewBodyOfWater(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsBodyOfWater()
           };
            yield return new object[]
           {
                "Invalid properties for InnOrTavern",
                TestMethodHelpers.CreateNewInnOrTavern(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsInnOrTavern()
           };
            yield return new object[]
           {
                "Invalid properties for Temple",
                TestMethodHelpers.CreateNewTemple(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsTemple()
           };
            yield return new object[]
           {
                "Invalid properties for WordWall",
                TestMethodHelpers.CreateNewWordWall(),
                TestMethodHelpers.CreateNewCreateLocationDtoAsWordWall()
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
            else if (type.TypeOfLocation == LocationType.Fort)
                _mockMapper.Setup(x => x.Map<Fort>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewFort());
            else if (type.TypeOfLocation == LocationType.GiantCamp)
                _mockMapper.Setup(x => x.Map<GiantCamp>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewGiantCamp());
            else if (type.TypeOfLocation == LocationType.Grove)
                _mockMapper.Setup(x => x.Map<Grove>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewGrove());
            else if (type.TypeOfLocation == LocationType.ImperialCamp)
                _mockMapper.Setup(x => x.Map<ImperialCamp>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewImperialCamp());
            else if (type.TypeOfLocation == LocationType.LightHouse)
                _mockMapper.Setup(x => x.Map<LightHouse>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewLightHouse());
            else if (type.TypeOfLocation == LocationType.Mine)
                _mockMapper.Setup(x => x.Map<Mine>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewMine());
            else if (type.TypeOfLocation == LocationType.NordicTower)
                _mockMapper.Setup(x => x.Map<NordicTower>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewNordicTower());
            else if (type.TypeOfLocation == LocationType.OrcStronghold)
                _mockMapper.Setup(x => x.Map<OrcStronghold>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewOrcStronghold());
            else if (type.TypeOfLocation == LocationType.Pass)
                _mockMapper.Setup(x => x.Map<Pass>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewPass());
            else if (type.TypeOfLocation == LocationType.Ruin)
                _mockMapper.Setup(x => x.Map<Ruin>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewRuin());
            else if (type.TypeOfLocation == LocationType.Shack)
                _mockMapper.Setup(x => x.Map<Shack>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewShack());
            else if (type.TypeOfLocation == LocationType.Ship)
                _mockMapper.Setup(x => x.Map<Ship>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewShip());
            else if (type.TypeOfLocation == LocationType.Shipwreck)
                _mockMapper.Setup(x => x.Map<Shipwreck>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewShipwreck());
            else if (type.TypeOfLocation == LocationType.Stable)
                _mockMapper.Setup(x => x.Map<Stable>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewStable());
            else if (type.TypeOfLocation == LocationType.StormcloakCamp)
                _mockMapper.Setup(x => x.Map<StormcloakCamp>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewStormcloakCamp());
            else if (type.TypeOfLocation == LocationType.Tomb)
                _mockMapper.Setup(x => x.Map<Tomb>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewTomb());
            else if (type.TypeOfLocation == LocationType.Watchtower)
                _mockMapper.Setup(x => x.Map<Watchtower>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewWatchtower());
            else if (type.TypeOfLocation == LocationType.WheatMill)
                _mockMapper.Setup(x => x.Map<WheatMill>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewWheatMill());
            else if (type.TypeOfLocation == LocationType.LumberMill)
                _mockMapper.Setup(x => x.Map<LumberMill>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewLumberMill());
            else if (type.TypeOfLocation == LocationType.BodyOfWater)
                _mockMapper.Setup(x => x.Map<BodyOfWater>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewBodyOfWater());
            else if (type.TypeOfLocation == LocationType.InnOrTavern)
                _mockMapper.Setup(x => x.Map<InnOrTavern>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewInnOrTavern());
            else if (type.TypeOfLocation == LocationType.Temple)
                _mockMapper.Setup(x => x.Map<Temple>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewTemple());
            else if (type.TypeOfLocation == LocationType.WordWall)
                _mockMapper.Setup(x => x.Map<WordWall>(It.IsAny<CreateLocationDto>())).Returns(TestMethodHelpers.CreateNewWordWall());

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
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Fort with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Fort,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsFort(),
                    TestMethodHelpers.CreateNewFort(),
                    TestMethodHelpers.CreateNewFort()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Fort with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Fort,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsFort(),
                    TestMethodHelpers.CreateNewFort(),
                    TestMethodHelpers.CreateNewFort()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Fort with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Fort,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsFort(),
                    TestMethodHelpers.CreateNewFort(),
                    TestMethodHelpers.CreateNewFort()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a GiantCamp with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.GiantCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsGiantCamp(),
                    TestMethodHelpers.CreateNewGiantCamp(),
                    TestMethodHelpers.CreateNewGiantCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a GiantCamp with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.GiantCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsGiantCamp(),
                    TestMethodHelpers.CreateNewGiantCamp(),
                    TestMethodHelpers.CreateNewGiantCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a GiantCamp with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.GiantCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsGiantCamp(),
                    TestMethodHelpers.CreateNewGiantCamp(),
                    TestMethodHelpers.CreateNewGiantCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Grove with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Grove,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsGrove(),
                    TestMethodHelpers.CreateNewGrove(),
                    TestMethodHelpers.CreateNewGrove()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Grove with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Grove,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsGrove(),
                    TestMethodHelpers.CreateNewGrove(),
                    TestMethodHelpers.CreateNewGrove()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Grove with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Grove,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsGrove(),
                    TestMethodHelpers.CreateNewGrove(),
                    TestMethodHelpers.CreateNewGrove()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a ImperialCamp with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.ImperialCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsImperialCamp(),
                    TestMethodHelpers.CreateNewImperialCamp(),
                    TestMethodHelpers.CreateNewImperialCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a ImperialCamp with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.ImperialCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsImperialCamp(),
                    TestMethodHelpers.CreateNewImperialCamp(),
                    TestMethodHelpers.CreateNewImperialCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a ImperialCamp with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.ImperialCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsImperialCamp(),
                    TestMethodHelpers.CreateNewImperialCamp(),
                    TestMethodHelpers.CreateNewImperialCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a LightHouse with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.LightHouse,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsLightHouse(),
                    TestMethodHelpers.CreateNewLightHouse(),
                    TestMethodHelpers.CreateNewLightHouse()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a LightHouse with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.LightHouse,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsLightHouse(),
                    TestMethodHelpers.CreateNewLightHouse(),
                    TestMethodHelpers.CreateNewLightHouse()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a LightHouse with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.LightHouse,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsLightHouse(),
                    TestMethodHelpers.CreateNewLightHouse(),
                    TestMethodHelpers.CreateNewLightHouse()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Mine with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Mine,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsMine(),
                    TestMethodHelpers.CreateNewMine(),
                    TestMethodHelpers.CreateNewMine()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Mine with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Mine,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsMine(),
                    TestMethodHelpers.CreateNewMine(),
                    TestMethodHelpers.CreateNewMine()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Mine with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Mine,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsMine(),
                    TestMethodHelpers.CreateNewMine(),
                    TestMethodHelpers.CreateNewMine()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a NordicTower with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.NordicTower,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsNordicTower(),
                    TestMethodHelpers.CreateNewNordicTower(),
                    TestMethodHelpers.CreateNewNordicTower()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a NordicTower with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.NordicTower,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsNordicTower(),
                    TestMethodHelpers.CreateNewNordicTower(),
                    TestMethodHelpers.CreateNewNordicTower()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a NordicTower with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.NordicTower,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsNordicTower(),
                    TestMethodHelpers.CreateNewNordicTower(),
                    TestMethodHelpers.CreateNewNordicTower()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a OrcStronghold with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.OrcStronghold,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsOrcStronghold(),
                    TestMethodHelpers.CreateNewOrcStronghold(),
                    TestMethodHelpers.CreateNewOrcStronghold()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a OrcStronghold with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.OrcStronghold,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsOrcStronghold(),
                    TestMethodHelpers.CreateNewOrcStronghold(),
                    TestMethodHelpers.CreateNewOrcStronghold()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a OrcStronghold with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.OrcStronghold,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsOrcStronghold(),
                    TestMethodHelpers.CreateNewOrcStronghold(),
                    TestMethodHelpers.CreateNewOrcStronghold()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Pass with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Pass,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsPass(),
                    TestMethodHelpers.CreateNewPass(),
                    TestMethodHelpers.CreateNewPass()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Pass with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Pass,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsPass(),
                    TestMethodHelpers.CreateNewPass(),
                    TestMethodHelpers.CreateNewPass()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Pass with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Pass,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsPass(),
                    TestMethodHelpers.CreateNewPass(),
                    TestMethodHelpers.CreateNewPass()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Ruin with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Ruin,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsRuin(),
                    TestMethodHelpers.CreateNewRuin(),
                    TestMethodHelpers.CreateNewRuin()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Ruin with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Ruin,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsRuin(),
                    TestMethodHelpers.CreateNewRuin(),
                    TestMethodHelpers.CreateNewRuin()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Ruin with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Ruin,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsRuin(),
                    TestMethodHelpers.CreateNewRuin(),
                    TestMethodHelpers.CreateNewRuin()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Shack with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Shack,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsShack(),
                    TestMethodHelpers.CreateNewShack(),
                    TestMethodHelpers.CreateNewShack()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Shack with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Shack,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsShack(),
                    TestMethodHelpers.CreateNewShack(),
                    TestMethodHelpers.CreateNewShack()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Shack with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Shack,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsShack(),
                    TestMethodHelpers.CreateNewShack(),
                    TestMethodHelpers.CreateNewShack()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Ship with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Ship,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsShip(),
                    TestMethodHelpers.CreateNewShip(),
                    TestMethodHelpers.CreateNewShip()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Ship with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Ship,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsShip(),
                    TestMethodHelpers.CreateNewShip(),
                    TestMethodHelpers.CreateNewShip()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Ship with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Ship,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsShip(),
                    TestMethodHelpers.CreateNewShip(),
                    TestMethodHelpers.CreateNewShip()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Shipwreck with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Shipwreck,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsShipwreck(),
                    TestMethodHelpers.CreateNewShipwreck(),
                    TestMethodHelpers.CreateNewShipwreck()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Shipwreck with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Shipwreck,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsShipwreck(),
                    TestMethodHelpers.CreateNewShipwreck(),
                    TestMethodHelpers.CreateNewShipwreck()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Shipwreck with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Shipwreck,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsShipwreck(),
                    TestMethodHelpers.CreateNewShipwreck(),
                    TestMethodHelpers.CreateNewShipwreck()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Stable with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Stable,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsStable(),
                    TestMethodHelpers.CreateNewStable(),
                    TestMethodHelpers.CreateNewStable()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Stable with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Stable,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsStable(),
                    TestMethodHelpers.CreateNewStable(),
                    TestMethodHelpers.CreateNewStable()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Stable with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Stable,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsStable(),
                    TestMethodHelpers.CreateNewStable(),
                    TestMethodHelpers.CreateNewStable()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a StormcloakCamp with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.StormcloakCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsStormcloakCamp(),
                    TestMethodHelpers.CreateNewStormcloakCamp(),
                    TestMethodHelpers.CreateNewStormcloakCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a StormcloakCamp with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.StormcloakCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsStormcloakCamp(),
                    TestMethodHelpers.CreateNewStormcloakCamp(),
                    TestMethodHelpers.CreateNewStormcloakCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a StormcloakCamp with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.StormcloakCamp,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsStormcloakCamp(),
                    TestMethodHelpers.CreateNewStormcloakCamp(),
                    TestMethodHelpers.CreateNewStormcloakCamp()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Tomb with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Tomb,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsTomb(),
                    TestMethodHelpers.CreateNewTomb(),
                    TestMethodHelpers.CreateNewTomb()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Tomb with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Tomb,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsTomb(),
                    TestMethodHelpers.CreateNewTomb(),
                    TestMethodHelpers.CreateNewTomb()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Tomb with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Tomb,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsTomb(),
                    TestMethodHelpers.CreateNewTomb(),
                    TestMethodHelpers.CreateNewTomb()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Watchtower with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Watchtower,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsWatchtower(),
                    TestMethodHelpers.CreateNewWatchtower(),
                    TestMethodHelpers.CreateNewWatchtower()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Watchtower with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Watchtower,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsWatchtower(),
                    TestMethodHelpers.CreateNewWatchtower(),
                    TestMethodHelpers.CreateNewWatchtower()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Watchtower with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Watchtower,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsWatchtower(),
                    TestMethodHelpers.CreateNewWatchtower(),
                    TestMethodHelpers.CreateNewWatchtower()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a WheatMill with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.WheatMill,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsWheatMill(),
                    TestMethodHelpers.CreateNewWheatMill(),
                    TestMethodHelpers.CreateNewWheatMill()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a WheatMill with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.WheatMill,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsWheatMill(),
                    TestMethodHelpers.CreateNewWheatMill(),
                    TestMethodHelpers.CreateNewWheatMill()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a WheatMill with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.WheatMill,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsWheatMill(),
                    TestMethodHelpers.CreateNewWheatMill(),
                    TestMethodHelpers.CreateNewWheatMill()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a LumberMill with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.LumberMill,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsLumberMill(),
                    TestMethodHelpers.CreateNewLumberMill(),
                    TestMethodHelpers.CreateNewLumberMill()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a LumberMill with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.LumberMill,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsLumberMill(),
                    TestMethodHelpers.CreateNewLumberMill(),
                    TestMethodHelpers.CreateNewLumberMill()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a LumberMill with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.LumberMill,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsLumberMill(),
                    TestMethodHelpers.CreateNewLumberMill(),
                    TestMethodHelpers.CreateNewLumberMill()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a BodyOfWater with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.BodyOfWater,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsBodyOfWater(),
                    TestMethodHelpers.CreateNewBodyOfWater(),
                    TestMethodHelpers.CreateNewBodyOfWater()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a BodyOfWater with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.BodyOfWater,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsBodyOfWater(),
                    TestMethodHelpers.CreateNewBodyOfWater(),
                    TestMethodHelpers.CreateNewBodyOfWater()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a BodyOfWater with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.BodyOfWater,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsBodyOfWater(),
                    TestMethodHelpers.CreateNewBodyOfWater(),
                    TestMethodHelpers.CreateNewBodyOfWater()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a InnOrTavern with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.InnOrTavern,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsInnOrTavern(),
                    TestMethodHelpers.CreateNewInnOrTavern(),
                    TestMethodHelpers.CreateNewInnOrTavern()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a InnOrTavern with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.InnOrTavern,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsInnOrTavern(),
                    TestMethodHelpers.CreateNewInnOrTavern(),
                    TestMethodHelpers.CreateNewInnOrTavern()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a InnOrTavern with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.InnOrTavern,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsInnOrTavern(),
                    TestMethodHelpers.CreateNewInnOrTavern(),
                    TestMethodHelpers.CreateNewInnOrTavern()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a Temple with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.Temple,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsTemple(),
                    TestMethodHelpers.CreateNewTemple(),
                    TestMethodHelpers.CreateNewTemple()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a Temple with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.Temple,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsTemple(),
                    TestMethodHelpers.CreateNewTemple(),
                    TestMethodHelpers.CreateNewTemple()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a Temple with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.Temple,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsTemple(),
                    TestMethodHelpers.CreateNewTemple(),
                    TestMethodHelpers.CreateNewTemple()
            };
            yield return new object[]
            {
                    "CreateLocationDto has a null description so it returns a WordWall with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.WordWall,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsWordWall(),
                    TestMethodHelpers.CreateNewWordWall(),
                    TestMethodHelpers.CreateNewWordWall()
            };
            yield return new object[]
            {
                    "CreateLocationDto has white spaces for description so it returns a WordWall with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "     ",
                        TypeOfLocation = LocationType.WordWall,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsWordWall(),
                    TestMethodHelpers.CreateNewWordWall(),
                    TestMethodHelpers.CreateNewWordWall()
            };
            yield return new object[]
            {
                    "CreateLocationDto has empty description so it returns a WordWall with empty description",
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = "",
                        TypeOfLocation = LocationType.WordWall,
                        GeographicalDescription = "Test"
                    },
                    TestMethodHelpers.CreateNewCreateLocationDtoAsWordWall(),
                    TestMethodHelpers.CreateNewWordWall(),
                    TestMethodHelpers.CreateNewWordWall()
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
            yield return new object[]
            {
                    "CreateLocationDto has a null name",
                    new CreateLocationDto
                    {
                        Description = "Test",
                        GeographicalDescription = "Test",
                        Name = null,
                        TypeOfLocation = LocationType.Fort
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
                        TypeOfLocation = LocationType.Fort
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
                        TypeOfLocation = LocationType.Fort
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
                        TypeOfLocation = LocationType.Fort
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
                        TypeOfLocation = LocationType.Fort
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
                        TypeOfLocation = LocationType.Fort
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
                        TypeOfLocation = LocationType.GiantCamp
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
                        TypeOfLocation = LocationType.GiantCamp
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
                        TypeOfLocation = LocationType.GiantCamp
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
                        TypeOfLocation = LocationType.GiantCamp
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
                        TypeOfLocation = LocationType.GiantCamp
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
                        TypeOfLocation = LocationType.GiantCamp
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
                        TypeOfLocation = LocationType.Grove
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
                        TypeOfLocation = LocationType.Grove
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
                        TypeOfLocation = LocationType.Grove
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
                        TypeOfLocation = LocationType.Grove
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
                        TypeOfLocation = LocationType.Grove
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
                        TypeOfLocation = LocationType.Grove
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
                        TypeOfLocation = LocationType.ImperialCamp
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
                        TypeOfLocation = LocationType.ImperialCamp
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
                        TypeOfLocation = LocationType.ImperialCamp
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
                        TypeOfLocation = LocationType.ImperialCamp
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
                        TypeOfLocation = LocationType.ImperialCamp
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
                        TypeOfLocation = LocationType.ImperialCamp
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
                        TypeOfLocation = LocationType.LightHouse
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
                        TypeOfLocation = LocationType.LightHouse
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
                        TypeOfLocation = LocationType.LightHouse
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
                        TypeOfLocation = LocationType.LightHouse
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
                        TypeOfLocation = LocationType.LightHouse
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
                        TypeOfLocation = LocationType.LightHouse
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
                        TypeOfLocation = LocationType.Mine
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
                        TypeOfLocation = LocationType.Mine
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
                        TypeOfLocation = LocationType.Mine
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
                        TypeOfLocation = LocationType.Mine
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
                        TypeOfLocation = LocationType.Mine
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
                        TypeOfLocation = LocationType.Mine
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
                        TypeOfLocation = LocationType.NordicTower
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
                        TypeOfLocation = LocationType.NordicTower
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
                        TypeOfLocation = LocationType.NordicTower
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
                        TypeOfLocation = LocationType.NordicTower
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
                        TypeOfLocation = LocationType.NordicTower
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
                        TypeOfLocation = LocationType.NordicTower
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
                        TypeOfLocation = LocationType.OrcStronghold
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
                        TypeOfLocation = LocationType.OrcStronghold
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
                        TypeOfLocation = LocationType.OrcStronghold
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
                        TypeOfLocation = LocationType.OrcStronghold
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
                        TypeOfLocation = LocationType.OrcStronghold
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
                        TypeOfLocation = LocationType.OrcStronghold
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
                        TypeOfLocation = LocationType.Pass
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
                        TypeOfLocation = LocationType.Pass
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
                        TypeOfLocation = LocationType.Pass
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
                        TypeOfLocation = LocationType.Pass
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
                        TypeOfLocation = LocationType.Pass
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
                        TypeOfLocation = LocationType.Pass
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
                        TypeOfLocation = LocationType.Ruin
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
                        TypeOfLocation = LocationType.Ruin
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
                        TypeOfLocation = LocationType.Ruin
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
                        TypeOfLocation = LocationType.Ruin
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
                        TypeOfLocation = LocationType.Ruin
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
                        TypeOfLocation = LocationType.Ruin
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
                        TypeOfLocation = LocationType.Shack
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
                        TypeOfLocation = LocationType.Shack
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
                        TypeOfLocation = LocationType.Shack
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
                        TypeOfLocation = LocationType.Shack
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
                        TypeOfLocation = LocationType.Shack
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
                        TypeOfLocation = LocationType.Shack
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
                        TypeOfLocation = LocationType.Ship
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
                        TypeOfLocation = LocationType.Ship
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
                        TypeOfLocation = LocationType.Ship
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
                        TypeOfLocation = LocationType.Ship
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
                        TypeOfLocation = LocationType.Ship
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
                        TypeOfLocation = LocationType.Ship
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
                        TypeOfLocation = LocationType.Shipwreck
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
                        TypeOfLocation = LocationType.Shipwreck
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
                        TypeOfLocation = LocationType.Shipwreck
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
                        TypeOfLocation = LocationType.Shipwreck
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
                        TypeOfLocation = LocationType.Shipwreck
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
                        TypeOfLocation = LocationType.Shipwreck
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
                        TypeOfLocation = LocationType.Stable
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
                        TypeOfLocation = LocationType.Stable
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
                        TypeOfLocation = LocationType.Stable
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
                        TypeOfLocation = LocationType.Stable
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
                        TypeOfLocation = LocationType.Stable
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
                        TypeOfLocation = LocationType.Stable
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
                        TypeOfLocation = LocationType.StormcloakCamp
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
                        TypeOfLocation = LocationType.StormcloakCamp
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
                        TypeOfLocation = LocationType.StormcloakCamp
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
                        TypeOfLocation = LocationType.StormcloakCamp
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
                        TypeOfLocation = LocationType.StormcloakCamp
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
                        TypeOfLocation = LocationType.StormcloakCamp
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
                        TypeOfLocation = LocationType.Tomb
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
                        TypeOfLocation = LocationType.Tomb
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
                        TypeOfLocation = LocationType.Tomb
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
                        TypeOfLocation = LocationType.Tomb
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
                        TypeOfLocation = LocationType.Tomb
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
                        TypeOfLocation = LocationType.Tomb
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
                        TypeOfLocation = LocationType.Watchtower
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
                        TypeOfLocation = LocationType.Watchtower
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
                        TypeOfLocation = LocationType.Watchtower
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
                        TypeOfLocation = LocationType.Watchtower
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
                        TypeOfLocation = LocationType.Watchtower
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
                        TypeOfLocation = LocationType.Watchtower
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
                        TypeOfLocation = LocationType.WheatMill
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
                        TypeOfLocation = LocationType.WheatMill
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
                        TypeOfLocation = LocationType.WheatMill
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
                        TypeOfLocation = LocationType.WheatMill
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
                        TypeOfLocation = LocationType.WheatMill
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
                        TypeOfLocation = LocationType.WheatMill
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
                        TypeOfLocation = LocationType.LumberMill
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
                        TypeOfLocation = LocationType.LumberMill
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
                        TypeOfLocation = LocationType.LumberMill
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
                        TypeOfLocation = LocationType.LumberMill
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
                        TypeOfLocation = LocationType.LumberMill
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
                        TypeOfLocation = LocationType.LumberMill
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
                        TypeOfLocation = LocationType.BodyOfWater
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
                        TypeOfLocation = LocationType.BodyOfWater
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
                        TypeOfLocation = LocationType.BodyOfWater
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
                        TypeOfLocation = LocationType.BodyOfWater
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
                        TypeOfLocation = LocationType.BodyOfWater
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
                        TypeOfLocation = LocationType.BodyOfWater
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
                        TypeOfLocation = LocationType.InnOrTavern
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
                        TypeOfLocation = LocationType.InnOrTavern
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
                        TypeOfLocation = LocationType.InnOrTavern
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
                        TypeOfLocation = LocationType.InnOrTavern
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
                        TypeOfLocation = LocationType.InnOrTavern
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
                        TypeOfLocation = LocationType.InnOrTavern
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
                        TypeOfLocation = LocationType.Temple
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
                        TypeOfLocation = LocationType.Temple
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
                        TypeOfLocation = LocationType.Temple
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
                        TypeOfLocation = LocationType.Temple
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
                        TypeOfLocation = LocationType.Temple
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
                        TypeOfLocation = LocationType.Temple
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
                        TypeOfLocation = LocationType.WordWall
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
                        TypeOfLocation = LocationType.WordWall
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
                        TypeOfLocation = LocationType.WordWall
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
                        TypeOfLocation = LocationType.WordWall
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
                        TypeOfLocation = LocationType.WordWall
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
                        TypeOfLocation = LocationType.WordWall
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
