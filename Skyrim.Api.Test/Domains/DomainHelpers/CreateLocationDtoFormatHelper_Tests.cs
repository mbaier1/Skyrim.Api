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
        }
    }
}
