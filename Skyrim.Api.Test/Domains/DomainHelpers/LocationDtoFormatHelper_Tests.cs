using Skyrim.Api.Data.Enums;
using Skyrim.Api.Domain.DomainHelpers;
using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Test.Domains.DomainHelpers
{
    public class LocationDtoFormatHelper_Tests
    {
        protected readonly LocationDtoFormatHelper _locationDtoFormatHelper;

        public LocationDtoFormatHelper_Tests()
        {
            _locationDtoFormatHelper = new LocationDtoFormatHelper();
        }
    }

    public class FormatEntity : LocationDtoFormatHelper_Tests
    {
        [Theory]
        [MemberData(nameof(ValidPropertiesForEachLocationType))]
        public void WhenLocationDtoHasValidProperties_ReturnsDtoUnaltered(string description, LocationDto locationDto)
        {
            // Arrange

            // Act
            var result = _locationDtoFormatHelper.FormatEntity(locationDto);

            // Assert
            Assert.Equal(locationDto.Name, result.Name);
            Assert.Equal(locationDto.Description, result.Description);
            Assert.Equal(locationDto.GeographicalDescription, result.GeographicalDescription);
            Assert.Equal(locationDto.LocationId, result.LocationId);
        }
        public static IEnumerable<object[]> ValidPropertiesForEachLocationType()
        {
            yield return new object[]
            {
                "Valid properties for a City Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.City
                }
            };
            yield return new object[]
            {
                "Valid properties for a Town Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Town
                }
            };
            yield return new object[]
            {
                "Valid properties for a Homestead Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Homestead
                }
            };
            yield return new object[]
            {
                "Valid properties for a Settlement Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Settlement
                }
            };
            yield return new object[]
            {
                "Valid properties for a DaedricShrine Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.DaedricShrine
                }
            };
            yield return new object[]
            {
                "Valid properties for a StandingStone Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.StandingStone
                }
            };
            yield return new object[]
            {
                "Valid properties for a Landmark Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Landmark
                }
            };
            yield return new object[]
            {
                "Valid properties for a Camp Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Camp
                }
            };
            yield return new object[]
            {
                "Valid properties for a Cave Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Cave
                }
            };
            yield return new object[]
            {
                "Valid properties for a Clearing Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Clearing
                }
            };
            yield return new object[]
            {
                "Valid properties for a Dock Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Dock
                }
            };
            yield return new object[]
            {
                "Valid properties for a DragonLair Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.DragonLair
                }
            };
            yield return new object[]
            {
                "Valid properties for a DwarvenRuin Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.DwarvenRuin
                }
            };
            yield return new object[]
            {
                "Valid properties for a Farm Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Farm
                }
            };
            yield return new object[]
            {
                "Valid properties for a Fort Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Fort
                }
            };
            yield return new object[]
            {
                "Valid properties for a GiantCamp Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.GiantCamp
                }
            };
            yield return new object[]
            {
                "Valid properties for a Grove Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Grove
                }
            };
            yield return new object[]
            {
                "Valid properties for a ImperialCamp Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.ImperialCamp
                }
            };
            yield return new object[]
            {
                "Valid properties for a LightHouse Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.LightHouse
                }
            };
            yield return new object[]
            {
                "Valid properties for a Mine Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Mine
                }
            };
            yield return new object[]
            {
                "Valid properties for a NordicTower Location",
                new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.NordicTower
                }
            };
        }

        [Theory]
        [MemberData(nameof(Allowed_Null_WhiteSpace_OrEmptyProperties_ForeachLocationType))]
        public void CreateLocationDtoContainsEmpty_WhiteSpace_OrNullDescription(string description, LocationDto locationDto,
            LocationDto formattedLocationDto)
        {
            // Arrange

            // Act
            var result = _locationDtoFormatHelper.FormatEntity(locationDto);

            // Assert
            Assert.Equal(formattedLocationDto.Name, result.Name);
            Assert.Equal(formattedLocationDto.Description, result.Description);
            Assert.Equal(formattedLocationDto.GeographicalDescription, result.GeographicalDescription);
            Assert.Equal(formattedLocationDto.LocationId, result.LocationId);
        }
        public static IEnumerable<object[]> Allowed_Null_WhiteSpace_OrEmptyProperties_ForeachLocationType()
        {
            yield return new object[]
            {
                "CreateLocationDto has a null description for city so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.City,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.City,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for city so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.City,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.City,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for city so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.City,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.City,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for city so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.City,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.City,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for city so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.City,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.City,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for city so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.City,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.City,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for town so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.Town,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Town,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Town so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.Town,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Town,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Town so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Town,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Town,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Town so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.Town,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Town,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Town so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.Town,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Town,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Town so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Town,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Town,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Homestead so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Homestead so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Homestead so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Homestead so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Homestead so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Homestead so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Homestead,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Settlement so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Settlement so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Settlement so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Settlement so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Settlement so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Settlement so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Settlement,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for DaedricShrine so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for DaedricShrine so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for DaedricShrine so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for DaedricShrine so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for DaedricShrine so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for DaedricShrine so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.DaedricShrine,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for StandingStone so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for StandingStone so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for StandingStone so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for StandingStone so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for StandingStone so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for StandingStone so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.StandingStone,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Landmark so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Landmark so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Landmark so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Landmark so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Landmark so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Landmark so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Landmark,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Camp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.Camp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Camp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Camp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.Camp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Camp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Camp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Camp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Camp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Camp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.Camp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Camp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Camp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.Camp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Camp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Camp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Camp,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Camp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Cave so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.Cave,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Cave,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Cave so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.Cave,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Cave,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Cave so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Cave,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Cave,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Cave so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.Cave,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Cave,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Cave so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.Cave,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Cave,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Cave so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Cave,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Cave,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Clearing so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Clearing so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Clearing so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Clearing so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Clearing so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Clearing so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Clearing,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Dock so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.Dock,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Dock,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Dock so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.Dock,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Dock,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Dock so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Dock,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Dock,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Dock so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.Dock,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Dock,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Dock so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.Dock,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Dock,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Dock so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Dock,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Dock,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for DragonLair so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for DragonLair so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for DragonLair so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for DragonLair so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for DragonLair so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for DragonLair so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.DragonLair,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for DwarvenRuin so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for DwarvenRuin so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for DwarvenRuin so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for DwarvenRuin so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for DwarvenRuin so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for DwarvenRuin so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Farm so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.Farm,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Farm,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Farm so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.Farm,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Farm,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Farm so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Farm,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Farm,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Farm so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.Farm,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Farm,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Farm so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.Farm,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Farm,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Farm so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Farm,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Farm,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Fort so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.Fort,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Fort,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Fort so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.Fort,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Fort,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Fort so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Fort,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Fort,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Fort so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.Fort,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Fort,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Fort so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.Fort,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Fort,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Fort so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Fort,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Fort,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for GiantCamp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for GiantCamp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for GiantCamp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for GiantCamp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for GiantCamp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for GiantCamp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.GiantCamp,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Grove so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.Grove,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Grove,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Grove so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.Grove,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Grove,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Grove so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Grove,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Grove,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Grove so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.Grove,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Grove,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Grove so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.Grove,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Grove,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Grove so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Grove,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Grove,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for ImperialCamp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for ImperialCamp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for ImperialCamp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for ImperialCamp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for ImperialCamp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for ImperialCamp so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.ImperialCamp,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for LightHouse so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.LightHouse,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.LightHouse,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for LightHouse so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.LightHouse,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.LightHouse,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for LightHouse so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.LightHouse,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.LightHouse,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for LightHouse so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.LightHouse,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.LightHouse,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for LightHouse so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.LightHouse,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.LightHouse,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for LightHouse so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.LightHouse,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.LightHouse,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Mine so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.Mine,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Mine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Mine so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.Mine,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Mine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Mine so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Mine,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.Mine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Mine so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.Mine,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Mine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Mine so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.Mine,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Mine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Mine so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Mine,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.Mine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for NordicTower so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = null,
                     LocationId = LocationType.NordicTower,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.NordicTower,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for NordicTower so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     LocationId = LocationType.NordicTower,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.NordicTower,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for NordicTower so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.NordicTower,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "",
                     LocationId = LocationType.NordicTower,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for NordicTower so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     LocationId = LocationType.NordicTower,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.NordicTower,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for NordicTower so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     LocationId = LocationType.NordicTower,
                     GeographicalDescription = "Test"
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.NordicTower,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for NordicTower so it returns with an empty description",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.NordicTower,
                     GeographicalDescription = "Test    "
                 },
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     LocationId = LocationType.NordicTower,
                     GeographicalDescription = "Test"
                 }
            };
        }

        [Theory]
        [MemberData(nameof(UnallowedNull_Invalid_orWhiteSpaceProperties))]
        public void CreateLocationDtoContainsInvalid_Empty_Whitespace_OrNullProperties(string description, LocationDto locationDto,
            LocationDto formattedLocationDto)
        {
            // Arrange

            // Act
            var result = _locationDtoFormatHelper.FormatEntity(locationDto);

            // Assert
            Assert.Equal(formattedLocationDto, result);
        }
        public static IEnumerable<object[]> UnallowedNull_Invalid_orWhiteSpaceProperties()
        {
            yield return new object[]
            {
                "CreateLocationDto has a null name for City",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.City
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for City",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.City
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for City",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.City
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for City",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.City
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for City",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.City
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for City",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.City
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Town",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Town
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Town",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Town
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Town",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Town
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Town",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.Town
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Town",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.Town
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Town",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.Town
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Homestead",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Homestead
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Homestead",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Homestead
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Homestead",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Homestead
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Homestead",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.Homestead
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Homestead",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.Homestead
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Homestead",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.Homestead
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Settlement",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Settlement
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Settlement",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Settlement
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Settlement",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Settlement
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Settlement",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.Settlement
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Settlement",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.Settlement
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Settlement",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.Settlement
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for DaedricShrine",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.DaedricShrine
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for DaedricShrine",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.DaedricShrine
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for DaedricShrine",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.DaedricShrine
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for DaedricShrine",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.DaedricShrine
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for DaedricShrine",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.DaedricShrine
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for DaedricShrine",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.DaedricShrine
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for StandingStone",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.StandingStone
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for StandingStone",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.StandingStone
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for StandingStone",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.StandingStone
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for StandingStone",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.StandingStone
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for StandingStone",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.StandingStone
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for StandingStone",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.StandingStone
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Landmark",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Landmark
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Landmark",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Landmark
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Landmark",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Landmark
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Landmark",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.Landmark
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Landmark",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.Landmark
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Landmark",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.Landmark
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Camp",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Camp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Camp",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Camp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Camp",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Camp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Camp",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.Camp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Camp",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.Camp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Camp",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.Camp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Cave",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Cave
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Cave",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Cave
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Cave",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Cave
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Cave",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.Cave
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Cave",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.Cave
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Cave",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.Cave
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Clearing",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Clearing
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Clearing",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Clearing
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Clearing",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Clearing
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Clearing",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.Clearing
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Clearing",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.Clearing
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Clearing",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.Clearing
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Dock",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Dock
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Dock",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Dock
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Dock",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Dock
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Dock",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.Dock
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Dock",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.Dock
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Dock",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.Dock
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for DragonLair",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.DragonLair
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for DragonLair",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.DragonLair
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for DragonLair",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.DragonLair
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for DragonLair",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.DragonLair
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for DragonLair",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.DragonLair
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for DragonLair",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.DragonLair
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for DwarvenRuin",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.DwarvenRuin
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for DwarvenRuin",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.DwarvenRuin
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for DwarvenRuin",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.DwarvenRuin
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for DwarvenRuin",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.DwarvenRuin
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for DwarvenRuin",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.DwarvenRuin
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for DwarvenRuin",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.DwarvenRuin
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Farm",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Farm
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Farm",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Farm
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Farm",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Farm
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Farm",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.Farm
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Farm",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.Farm
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Farm",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.Farm
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Fort",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Fort
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Fort",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Fort
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Fort",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Fort
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Fort",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.Fort
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Fort",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.Fort
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Fort",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.Fort
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for GiantCamp",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.GiantCamp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for GiantCamp",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.GiantCamp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for GiantCamp",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.GiantCamp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for GiantCamp",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.GiantCamp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for GiantCamp",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.GiantCamp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for GiantCamp",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.GiantCamp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Grove",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Grove
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Grove",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Grove
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Grove",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Grove
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Grove",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.Grove
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Grove",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.Grove
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Grove",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.Grove
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for ImperialCamp",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.ImperialCamp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for ImperialCamp",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.ImperialCamp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for ImperialCamp",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.ImperialCamp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for ImperialCamp",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.ImperialCamp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for ImperialCamp",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.ImperialCamp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for ImperialCamp",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.ImperialCamp
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for LightHouse",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.LightHouse
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for LightHouse",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.LightHouse
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for LightHouse",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.LightHouse
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for LightHouse",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.LightHouse
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for LightHouse",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.LightHouse
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for LightHouse",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.LightHouse
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Mine",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Mine
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Mine",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Mine
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Mine",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.Mine
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Mine",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.Mine
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Mine",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.Mine
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Mine",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.Mine
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for NordicTower",
                 new LocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.NordicTower
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for NordicTower",
                 new LocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.NordicTower
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for NordicTower",
                 new LocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     LocationId = LocationType.NordicTower
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for NordicTower",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     LocationId = LocationType.NordicTower
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for NordicTower",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     LocationId = LocationType.NordicTower
                 },
                 (LocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for NordicTower",
                 new LocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     LocationId = LocationType.NordicTower
                 },
                 (LocationDto)null
            };
        }

        [Theory]
        [MemberData(nameof(DifferentNameFormats))]
        public void WithDifferentFormattedNames_ReturnsExpectedLocationFormattedCorrectly(string description, LocationDto locationDto,
            LocationDto formattedLocationDto)
        {
            // Arrange

            // Act
            var result = _locationDtoFormatHelper.FormatEntity(locationDto);

            // Assert
            Assert.Equal(formattedLocationDto.Name, result.Name);
        }
        public static IEnumerable<object[]> DifferentNameFormats()
        {
            yield return new object[]
            {
                "All lowercase name",
                    new LocationDto
                    {
                        Name = "test",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                "All uppercase name",
                    new LocationDto
                    {
                        Name = "TEST",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                "Upper and lowercase name",
                    new LocationDto
                    {
                        Name = "tEsT",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new LocationDto
                    {
                        Name = "Test",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                "multiple lowercase words in name",
                    new LocationDto
                    {
                        Name = "test test test t",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new LocationDto
                    {
                        Name = "Test Test Test T",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                "multiple uppercase words in name",
                    new LocationDto
                    {
                        Name = "TEST TEST TEST T",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new LocationDto
                    {
                        Name = "Test Test Test T",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                "Mixed upper and lowercase words in name",
                    new LocationDto
                    {
                        Name = "tEsT TesT TESt t",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new LocationDto
                    {
                        Name = "Test Test Test T",
                        Description = null,
                        LocationId = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
        }

        [Fact]
        public void WithCorrectlyFormattedName_ReturnsExpectedName()
        {
            // Arrange
            var createLocationDto = new LocationDto
            {
                Name = "Test Test T D",
                LocationId = LocationType.City,
                GeographicalDescription = "Test"
            };

            // Act
            var result = _locationDtoFormatHelper.FormatEntity(createLocationDto);

            // Assert
            Assert.Equal(createLocationDto.Name, result.Name);
        }
    }
}
