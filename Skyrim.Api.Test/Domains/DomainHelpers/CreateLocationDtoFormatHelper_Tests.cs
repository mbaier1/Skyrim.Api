using Skyrim.Api.Data.Enums;
using Skyrim.Api.Domain.DomainHelpers;
using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Test.Domains.DomainHelpers
{
    public class CreateLocationDtoFormatHelper_Tests
    {
        protected readonly CreateLocationDtoFormatHelper _createLocationDtoFormatHelper;

        public CreateLocationDtoFormatHelper_Tests()
        {
            _createLocationDtoFormatHelper = new CreateLocationDtoFormatHelper();
        }
    }

    public class FormatEntity : CreateLocationDtoFormatHelper_Tests
    {
        [Theory]
        [MemberData(nameof(ValidPropertiesForEachLocationType))]
        public void WhenCreateLocationDtoHasValidProperties_ReturnsDtoUnaltered(string description, CreateLocationDto createLocationDto)
        {
            // Arrange

            // Act
            var result = _createLocationDtoFormatHelper.FormatEntity(createLocationDto);

            // Assert
            Assert.Equal(createLocationDto.Name, result.Name);
            Assert.Equal(createLocationDto.Description, result.Description);
            Assert.Equal(createLocationDto.GeographicalDescription, result.GeographicalDescription);
            Assert.Equal(createLocationDto.TypeOfLocation, result.TypeOfLocation);
        }
        public static IEnumerable<object[]> ValidPropertiesForEachLocationType()
        {
            yield return new object[]
            {
                "Valid properties for a City Location",
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
                "Valid properties for a Town Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Town
                }
            };
            yield return new object[]
            {
                "Valid properties for a Homestead Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Homestead
                }
            };
            yield return new object[]
            {
                "Valid properties for a Settlement Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Settlement
                }
            };
            yield return new object[]
            {
                "Valid properties for a DaedricShrine Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.DaedricShrine
                }
            };
            yield return new object[]
            {
                "Valid properties for a StandingStone Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.StandingStone
                }
            };
            yield return new object[]
            {
                "Valid properties for a Landmark Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Landmark
                }
            };
            yield return new object[]
            {
                "Valid properties for a Camp Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Camp
                }
            };
            yield return new object[]
            {
                "Valid properties for a Cave Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Cave
                }
            };
            yield return new object[]
            {
                "Valid properties for a Clearing Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Clearing
                }
            };
            yield return new object[]
            {
                "Valid properties for a Dock Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Dock
                }
            };
            yield return new object[]
            {
                "Valid properties for a DragonLair Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.DragonLair
                }
            };
            yield return new object[]
            {
                "Valid properties for a DwarvenRuin Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.DwarvenRuin
                }
            };
            yield return new object[]
            {
                "Valid properties for a Farm Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Farm
                }
            };
            yield return new object[]
            {
                "Valid properties for a Fort Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Fort
                }
            };
            yield return new object[]
            {
                "Valid properties for a GiantCamp Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.GiantCamp
                }
            };
            yield return new object[]
            {
                "Valid properties for a Grove Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.Grove
                }
            };
            yield return new object[]
            {
                "Valid properties for a ImperialCamp Location",
                new CreateLocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    TypeOfLocation = LocationType.ImperialCamp
                }
            };
        }

        [Theory]
        [MemberData(nameof(Allowed_Null_WhiteSpace_OrEmptyProperties_ForeachLocationType))]
        public void CreateLocationDtoContainsEmpty_WhiteSpace_OrNullDescription(string description, CreateLocationDto createLocationDto,
            CreateLocationDto formattedCreateLocationDto)
        {
            // Arrange

            // Act
            var result = _createLocationDtoFormatHelper.FormatEntity(createLocationDto);

            // Assert
            Assert.Equal(formattedCreateLocationDto.Name, result.Name);
            Assert.Equal(formattedCreateLocationDto.Description, result.Description);
            Assert.Equal(formattedCreateLocationDto.GeographicalDescription, result.GeographicalDescription);
            Assert.Equal(formattedCreateLocationDto.TypeOfLocation, result.TypeOfLocation);
        }
        public static IEnumerable<object[]> Allowed_Null_WhiteSpace_OrEmptyProperties_ForeachLocationType()
        {
            yield return new object[]
            {
                "CreateLocationDto has a null description for city so it returns with an empty description",
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
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for city so it returns with an empty description",
                 new CreateLocationDto
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
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for city so it returns with an empty description",
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
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for city so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.City,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.City,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for city so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.City,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.City,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for city so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.City,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.City,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for town so it returns with an empty description",
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
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Town so it returns with an empty description",
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
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Town so it returns with an empty description",
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
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Town so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.Town,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Town,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Town so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.Town,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Town,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Town so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Town,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Town,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Homestead so it returns with an empty description",
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
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Homestead so it returns with an empty description",
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
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Homestead so it returns with an empty description",
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
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Homestead so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Homestead so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Homestead so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Homestead,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Homestead,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Settlement so it returns with an empty description",
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
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Settlement so it returns with an empty description",
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
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Settlement so it returns with an empty description",
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
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Settlement so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Settlement so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Settlement so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Settlement,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Settlement,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for DaedricShrine so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for DaedricShrine so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for DaedricShrine so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for DaedricShrine so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for DaedricShrine so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for DaedricShrine so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.DaedricShrine,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.DaedricShrine,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for StandingStone so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for StandingStone so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for StandingStone so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for StandingStone so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for StandingStone so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for StandingStone so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.StandingStone,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.StandingStone,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Landmark so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Landmark so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Landmark so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Landmark so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Landmark so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Landmark so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Landmark,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Landmark,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Camp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.Camp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Camp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Camp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.Camp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Camp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Camp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Camp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Camp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Camp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.Camp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Camp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Camp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.Camp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Camp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Camp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Camp,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Camp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Cave so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.Cave,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Cave,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Cave so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.Cave,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Cave,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Cave so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Cave,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Cave,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Cave so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.Cave,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Cave,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Cave so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.Cave,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Cave,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Cave so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Cave,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Cave,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Clearing so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Clearing so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Clearing so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Clearing so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Clearing so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Clearing so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Clearing,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Clearing,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Dock so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.Dock,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Dock,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Dock so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.Dock,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Dock,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Dock so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Dock,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Dock,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Dock so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.Dock,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Dock,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Dock so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.Dock,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Dock,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Dock so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Dock,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Dock,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for DragonLair so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for DragonLair so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for DragonLair so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for DragonLair so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for DragonLair so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for DragonLair so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.DragonLair,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.DragonLair,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for DwarvenRuin so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for DwarvenRuin so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for DwarvenRuin so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for DwarvenRuin so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for DwarvenRuin so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for DwarvenRuin so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.DwarvenRuin,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Farm so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.Farm,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Farm,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Farm so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.Farm,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Farm,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Farm so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Farm,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Farm,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Farm so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.Farm,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Farm,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Farm so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.Farm,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Farm,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Farm so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Farm,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Farm,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Fort so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.Fort,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Fort,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Fort so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.Fort,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Fort,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Fort so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Fort,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Fort,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Fort so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.Fort,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Fort,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Fort so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.Fort,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Fort,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Fort so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Fort,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Fort,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for GiantCamp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for GiantCamp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for GiantCamp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for GiantCamp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for GiantCamp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for GiantCamp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.GiantCamp,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.GiantCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for Grove so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.Grove,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Grove,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for Grove so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.Grove,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Grove,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for Grove so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Grove,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.Grove,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for Grove so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.Grove,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Grove,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for Grove so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.Grove,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Grove,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for Grove so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Grove,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.Grove,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has a null description for ImperialCamp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = null,
                     TypeOfLocation = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white spaces for description for ImperialCamp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "     ",
                     TypeOfLocation = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has empty description for ImperialCamp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "",
                     TypeOfLocation = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the name for ImperialCamp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "  Test   ",
                     Description = "Test",
                     TypeOfLocation = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the description for ImperialCamp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "  Test",
                     TypeOfLocation = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 }
            };
            yield return new object[]
            {
                "CreateLocationDto has white space in the geographical description for ImperialCamp so it returns with an empty description",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.ImperialCamp,
                     GeographicalDescription = "Test    "
                 },
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     TypeOfLocation = LocationType.ImperialCamp,
                     GeographicalDescription = "Test"
                 }
            };
        }

        [Theory]
        [MemberData(nameof(UnallowedNull_Invalid_orWhiteSpaceProperties))]
        public void CreateLocationDtoContainsInvalid_Empty_Whitespace_OrNullProperties(string description, CreateLocationDto createLocationDto,
            CreateLocationDto formattedCreateLocationDto)
        {
            // Arrange

            // Act
            var result = _createLocationDtoFormatHelper.FormatEntity(createLocationDto);

            // Assert
            Assert.Equal(formattedCreateLocationDto, result);
        }
        public static IEnumerable<object[]> UnallowedNull_Invalid_orWhiteSpaceProperties()
        {
            yield return new object[]
            {
                "CreateLocationDto has a null name for City",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.City
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for City",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.City
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for City",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.City
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for City",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.City
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for City",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.City
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for City",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.City
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Town",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Town
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Town",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Town
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Town",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Town
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Town",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.Town
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Town",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.Town
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Town",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.Town
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Homestead",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Homestead
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Homestead",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Homestead
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Homestead",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Homestead
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Homestead",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.Homestead
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Homestead",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.Homestead
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Homestead",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.Homestead
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Settlement",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Settlement
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Settlement",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Settlement
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Settlement",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Settlement
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Settlement",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.Settlement
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Settlement",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.Settlement
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Settlement",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.Settlement
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for DaedricShrine",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.DaedricShrine
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for DaedricShrine",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.DaedricShrine
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for DaedricShrine",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.DaedricShrine
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for DaedricShrine",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.DaedricShrine
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for DaedricShrine",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.DaedricShrine
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for DaedricShrine",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.DaedricShrine
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for StandingStone",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.StandingStone
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for StandingStone",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.StandingStone
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for StandingStone",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.StandingStone
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for StandingStone",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.StandingStone
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for StandingStone",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.StandingStone
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for StandingStone",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.StandingStone
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Landmark",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Landmark
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Landmark",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Landmark
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Landmark",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Landmark
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Landmark",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.Landmark
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Landmark",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.Landmark
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Landmark",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.Landmark
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Camp",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Camp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Camp",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Camp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Camp",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Camp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Camp",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.Camp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Camp",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.Camp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Camp",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.Camp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Cave",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Cave
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Cave",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Cave
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Cave",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Cave
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Cave",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.Cave
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Cave",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.Cave
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Cave",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.Cave
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Clearing",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Clearing
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Clearing",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Clearing
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Clearing",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Clearing
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Clearing",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.Clearing
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Clearing",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.Clearing
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Clearing",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.Clearing
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Dock",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Dock
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Dock",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Dock
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Dock",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Dock
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Dock",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.Dock
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Dock",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.Dock
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Dock",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.Dock
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for DragonLair",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.DragonLair
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for DragonLair",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.DragonLair
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for DragonLair",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.DragonLair
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for DragonLair",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.DragonLair
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for DragonLair",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.DragonLair
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for DragonLair",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.DragonLair
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for DwarvenRuin",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.DwarvenRuin
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for DwarvenRuin",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.DwarvenRuin
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for DwarvenRuin",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.DwarvenRuin
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for DwarvenRuin",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.DwarvenRuin
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for DwarvenRuin",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.DwarvenRuin
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for DwarvenRuin",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.DwarvenRuin
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Farm",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Farm
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Farm",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Farm
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Farm",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Farm
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Farm",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.Farm
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Farm",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.Farm
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Farm",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.Farm
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Fort",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Fort
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Fort",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Fort
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Fort",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Fort
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Fort",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.Fort
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Fort",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.Fort
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Fort",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.Fort
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for GiantCamp",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.GiantCamp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for GiantCamp",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.GiantCamp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for GiantCamp",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.GiantCamp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for GiantCamp",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.GiantCamp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for GiantCamp",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.GiantCamp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for GiantCamp",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.GiantCamp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for Grove",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Grove
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for Grove",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Grove
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for Grove",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.Grove
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for Grove",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.Grove
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for Grove",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.Grove
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for Grove",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.Grove
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null name for ImperialCamp",
                 new CreateLocationDto
                 {
                     Name = null,
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.ImperialCamp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has an empty name for ImperialCamp",
                 new CreateLocationDto
                 {
                     Name = "",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.ImperialCamp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has has a white space name for ImperialCamp",
                 new CreateLocationDto
                 {
                     Name = " ",
                     Description = "Test",
                     GeographicalDescription = "Test",
                     TypeOfLocation = LocationType.ImperialCamp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a null Geographic Description for ImperialCamp",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = null,
                     TypeOfLocation = LocationType.ImperialCamp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has an empty Geographic Description for ImperialCamp",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = "",
                     TypeOfLocation = LocationType.ImperialCamp
                 },
                 (CreateLocationDto)null
            };
            yield return new object[]
            {
                "CreateLocationDto has a white space Geographic Description name for ImperialCamp",
                 new CreateLocationDto
                 {
                     Name = "Test",
                     Description = "Test",
                     GeographicalDescription = " ",
                     TypeOfLocation = LocationType.ImperialCamp
                 },
                 (CreateLocationDto)null
            };
        }

        [Theory]
        [MemberData(nameof(DifferentNameFormats))]
        public void WithDifferentFormattedNames_ReturnsExpectedLocationFormattedCorrectly(string description, CreateLocationDto createLocationDto,
            CreateLocationDto formattedCreateLocationDto)
        {
            // Arrange

            // Act
            var result = _createLocationDtoFormatHelper.FormatEntity(createLocationDto);

            // Assert
            Assert.Equal(formattedCreateLocationDto.Name, result.Name);
        }
        public static IEnumerable<object[]> DifferentNameFormats()
        {
            yield return new object[]
            {
                "All lowercase name",
                    new CreateLocationDto
                    {
                        Name = "test",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                "All uppercase name",
                    new CreateLocationDto
                    {
                        Name = "TEST",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                "Upper and lowercase name",
                    new CreateLocationDto
                    {
                        Name = "tEsT",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                "multiple lowercase words in name",
                    new CreateLocationDto
                    {
                        Name = "test test test t",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test Test Test T",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                "multiple uppercase words in name",
                    new CreateLocationDto
                    {
                        Name = "TEST TEST TEST T",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test Test Test T",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
            yield return new object[]
            {
                "Mixed upper and lowercase words in name",
                    new CreateLocationDto
                    {
                        Name = "tEsT TesT TESt t",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    },
                    new CreateLocationDto
                    {
                        Name = "Test Test Test T",
                        Description = null,
                        TypeOfLocation = LocationType.City,
                        GeographicalDescription = "Test"
                    }
            };
        }

        [Fact]
        public void WithCorrectlyFormattedName_ReturnsExpectedName()
        {
            // Arrange
            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Test T D",
                TypeOfLocation = LocationType.City,
                GeographicalDescription = "Test"
            };

            // Act
            var result = _createLocationDtoFormatHelper.FormatEntity(createLocationDto);

            // Assert
            Assert.Equal(createLocationDto.Name, result.Name);
        }
    }
}
