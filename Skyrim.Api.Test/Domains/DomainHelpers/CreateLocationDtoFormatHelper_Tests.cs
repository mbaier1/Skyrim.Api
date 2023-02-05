using Skyrim.Api.Data.Enums;
using Skyrim.Api.Domain.DomainHelpers;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
