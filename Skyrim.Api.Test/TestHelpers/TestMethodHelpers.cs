using Skyrim.Api.Data.Enums;
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

        public static Camp CreateNewCamp()
        {
            return new Camp
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Camp,
                GeographicalDescription = "Test"
            };
        }

        public static Cave CreateNewCave()
        {
            return new Cave
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Cave,
                GeographicalDescription = "Test"
            };
        }

        public static Clearing CreateNewClearing()
        {
            return new Clearing
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Clearing,
                GeographicalDescription = "Test"
            };
        }

        public static Dock CreateNewDock()
        {
            return new Dock
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Dock,
                GeographicalDescription = "Test"
            };
        }

        public static DragonLair CreateNewDragonLair()
        {
            return new DragonLair
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.DragonLair,
                GeographicalDescription = "Test"
            };
        }

        public static DwarvenRuin CreateNewDwarvenRuin()
        {
            return new DwarvenRuin
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.DwarvenRuin,
                GeographicalDescription = "Test"
            };
        }

        public static Farm CreateNewFarm()
        {
            return new Farm
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Farm,
                GeographicalDescription = "Test"
            };
        }

        public static Fort CreateNewFort()
        {
            return new Fort
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Fort,
                GeographicalDescription = "Test"
            };
        }

        public static GiantCamp CreateNewGiantCamp()
        {
            return new GiantCamp
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.GiantCamp,
                GeographicalDescription = "Test"
            };
        }

        public static Grove CreateNewGrove()
        {
            return new Grove
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Grove,
                GeographicalDescription = "Test"
            };
        }

        public static ImperialCamp CreateNewImperialCamp()
        {
            return new ImperialCamp
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.ImperialCamp,
                GeographicalDescription = "Test"
            };
        }

        public static LightHouse CreateNewLightHouse()
        {
            return new LightHouse
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.LightHouse,
                GeographicalDescription = "Test"
            };
        }

        public static Mine CreateNewMine()
        {
            return new Mine
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Mine,
                GeographicalDescription = "Test"
            };
        }

        public static NordicTower CreateNewNordicTower()
        {
            return new NordicTower
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.NordicTower,
                GeographicalDescription = "Test"
            };
        }

        public static OrcStronghold CreateNewOrcStronghold()
        {
            return new OrcStronghold
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.OrcStronghold,
                GeographicalDescription = "Test"
            };
        }

        public static Pass CreateNewPass()
        {
            return new Pass
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Pass,
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

        public static CreateLocationDto CreateNewCreateLocationDtoAsCamp()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Camp
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsCave()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Cave
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsClearing()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Clearing
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsDock()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Dock
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsDragonLair()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.DragonLair
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsDwarvenRuin()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.DwarvenRuin
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsFarm()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Farm
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsFort()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Fort
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsGiantCamp()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.GiantCamp
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsGrove()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Grove
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsImperialCamp()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.ImperialCamp
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsLightHouse()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.LightHouse
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsMine()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Mine
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsNordicTower()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.NordicTower
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsOrcStronghold()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.OrcStronghold
            };
        }

        public static CreateLocationDto CreateNewCreateLocationDtoAsPass()
        {
            return new CreateLocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                TypeOfLocation = LocationType.Pass
            };
        }
    }
}
