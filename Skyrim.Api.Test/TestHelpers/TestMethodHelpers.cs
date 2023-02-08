﻿using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain.DTOs;

namespace Skyrim.Api.Test.TestHelpers
{
    public static class TestMethodHelpers
    {
        public static City CreateNewCity()
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

        public static Town CreateNewTown()
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

        public static Homestead CreateNewHomestead()
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

        public static Settlement CreateNewSettlement()
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

        public static DaedricShrine CreateNewDaedricShrine()
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

        public static StandingStone CreateNewStandingStone()
        {
            return new StandingStone
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.StandingStone,
                GeographicalDescription = "Test"
            };
        }

        public static Landmark CreateNewLandmark()
        {
            return new Landmark
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Landmark,
                GeographicalDescription = "Test"
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsCity()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.City
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsTown()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Town
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsHomestead()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Homestead
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsSettlement()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Settlement
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsDaedricShrine()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.DaedricShrine
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsStandingStone()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.StandingStone
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsLandmark()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Landmark
            };
        }
    }
}