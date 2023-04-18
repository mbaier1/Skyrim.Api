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
                LocationId = LocationType.City,
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
                LocationId = LocationType.Town,
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
                LocationId = LocationType.Homestead,
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
                LocationId = LocationType.Settlement,
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
                LocationId = LocationType.DaedricShrine,
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
                LocationId = LocationType.StandingStone,
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
                LocationId = LocationType.Landmark,
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
                LocationId = LocationType.Camp,
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
                LocationId = LocationType.Cave,
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
                LocationId = LocationType.Clearing,
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
                LocationId = LocationType.Dock,
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
                LocationId = LocationType.DragonLair,
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
                LocationId = LocationType.DwarvenRuin,
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
                LocationId = LocationType.Farm,
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
                LocationId = LocationType.Fort,
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
                LocationId = LocationType.GiantCamp,
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
                LocationId = LocationType.Grove,
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
                LocationId = LocationType.ImperialCamp,
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
                LocationId = LocationType.LightHouse,
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
                LocationId = LocationType.Mine,
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
                LocationId = LocationType.NordicTower,
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
                LocationId = LocationType.OrcStronghold,
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
                LocationId = LocationType.Pass,
                GeographicalDescription = "Test"
            };
        }

        public static Ruin CreateNewRuin()
        {
            return new Ruin
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.Ruin,
                GeographicalDescription = "Test"
            };
        }

        public static Shack CreateNewShack()
        {
            return new Shack
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.Shack,
                GeographicalDescription = "Test"
            };
        }

        public static Ship CreateNewShip()
        {
            return new Ship
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.Ship,
                GeographicalDescription = "Test"
            };
        }

        public static Shipwreck CreateNewShipwreck()
        {
            return new Shipwreck
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.Shipwreck,
                GeographicalDescription = "Test"
            };
        }

        public static Stable CreateNewStable()
        {
            return new Stable
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.Stable,
                GeographicalDescription = "Test"
            };
        }

        public static StormcloakCamp CreateNewStormcloakCamp()
        {
            return new StormcloakCamp
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.StormcloakCamp,
                GeographicalDescription = "Test"
            };
        }

        public static Tomb CreateNewTomb()
        {
            return new Tomb
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.Tomb,
                GeographicalDescription = "Test"
            };
        }

        public static Watchtower CreateNewWatchtower()
        {
            return new Watchtower
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.Watchtower,
                GeographicalDescription = "Test"
            };
        }

        public static WheatMill CreateNewWheatMill()
        {
            return new WheatMill
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.WheatMill,
                GeographicalDescription = "Test"
            };
        }

        public static LumberMill CreateNewLumberMill()
        {
            return new LumberMill
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.LumberMill,
                GeographicalDescription = "Test"
            };
        }

        public static BodyOfWater CreateNewBodyOfWater()
        {
            return new BodyOfWater
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.BodyOfWater,
                GeographicalDescription = "Test"
            };
        }

        public static InnOrTavern CreateNewInnOrTavern()
        {
            return new InnOrTavern
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.InnOrTavern,
                GeographicalDescription = "Test"
            };
        }

        public static Temple CreateNewTemple()
        {
            return new Temple
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.Temple,
                GeographicalDescription = "Test"
            };
        }

        public static WordWall CreateNewWordWall()
        {
            return new WordWall
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.WordWall,
                GeographicalDescription = "Test"
            };
        }

        public static Data.Models.Castle CreateNewCastle()
        {
            return new Data.Models.Castle
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.Castle,
                GeographicalDescription = "Test"
            };
        }

        public static GuildHeadquarter CreateNewGuildHeadquarter()
        {
            return new GuildHeadquarter
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.GuildHeadquarter,
                GeographicalDescription = "Test"
            };
        }

        public static UnmarkedLocation CreateNewUnmarkedLocation()
        {
            return new UnmarkedLocation
            {
                Id = 0,
                Name = "Test",
                Description = "Test",
                LocationId = LocationType.UnmarkedLocation,
                GeographicalDescription = "Test"
            };
        }

        public static LocationDto CreateNewLocationDtoAsCity()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.City
            };
        }

        public static LocationDto CreateNewLocationDtoAsTown()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Town
            };
        }

        public static LocationDto CreateNewLocationDtoAsHomestead()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Homestead
            };
        }

        public static LocationDto CreateNewLocationDtoAsSettlement()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Settlement
            };
        }

        public static LocationDto CreateNewLocationDtoAsDaedricShrine()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.DaedricShrine
            };
        }

        public static LocationDto CreateNewLocationDtoAsStandingStone()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.StandingStone
            };
        }

        public static LocationDto CreateNewLocationDtoAsLandmark()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Landmark
            };
        }

        public static LocationDto CreateNewLocationDtoAsCamp()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Camp
            };
        }

        public static LocationDto CreateNewLocationDtoAsCave()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Cave
            };
        }

        public static LocationDto CreateNewLocationDtoAsClearing()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Clearing
            };
        }

        public static LocationDto CreateNewLocationDtoAsDock()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Dock
            };
        }

        public static LocationDto CreateNewLocationDtoAsDragonLair()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.DragonLair
            };
        }

        public static LocationDto CreateNewLocationDtoAsDwarvenRuin()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.DwarvenRuin
            };
        }

        public static LocationDto CreateNewLocationDtoAsFarm()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Farm
            };
        }

        public static LocationDto CreateNewLocationDtoAsFort()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Fort
            };
        }

        public static LocationDto CreateNewLocationDtoAsGiantCamp()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.GiantCamp
            };
        }

        public static LocationDto CreateNewLocationDtoAsGrove()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Grove
            };
        }

        public static LocationDto CreateNewLocationDtoAsImperialCamp()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.ImperialCamp
            };
        }

        public static LocationDto CreateNewLocationDtoAsLightHouse()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.LightHouse
            };
        }

        public static LocationDto CreateNewLocationDtoAsMine()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Mine
            };
        }

        public static LocationDto CreateNewLocationDtoAsNordicTower()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.NordicTower
            };
        }

        public static LocationDto CreateNewLocationDtoAsOrcStronghold()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.OrcStronghold
            };
        }

        public static LocationDto CreateNewLocationDtoAsPass()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Pass
            };
        }

        public static LocationDto CreateNewLocationDtoAsRuin()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Ruin
            };
        }

        public static LocationDto CreateNewLocationDtoAsShack()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Shack
            };
        }

        public static LocationDto CreateNewLocationDtoAsShip()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Ship
            };
        }

        public static LocationDto CreateNewLocationDtoAsShipwreck()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Shipwreck
            };
        }

        public static LocationDto CreateNewLocationDtoAsStable()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Stable
            };
        }

        public static LocationDto CreateNewLocationDtoAsStormcloakCamp()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.StormcloakCamp
            };
        }

        public static LocationDto CreateNewLocationDtoAsTomb()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Tomb
            };
        }

        public static LocationDto CreateNewLocationDtoAsWatchtower()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Watchtower
            };
        }

        public static LocationDto CreateNewLocationDtoAsWheatMill()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.WheatMill
            };
        }

        public static LocationDto CreateNewLocationDtoAsLumberMill()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.LumberMill
            };
        }

        public static LocationDto CreateNewLocationDtoAsBodyOfWater()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.BodyOfWater
            };
        }

        public static LocationDto CreateNewLocationDtoAsInnOrTavern()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.InnOrTavern
            };
        }

        public static LocationDto CreateNewLocationDtoAsTemple()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Temple
            };
        }

        public static LocationDto CreateNewLocationDtoAsWordWall()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.WordWall
            };
        }

        public static LocationDto CreateNewLocationDtoAsCastle()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.Castle
            };
        }

        public static LocationDto CreateNewLocationDtoAsGuildHeadquarter()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.GuildHeadquarter
            };
        }

        public static LocationDto CreateNewLocationDtoAsUnmarkedLocation()
        {
            return new LocationDto
            {
                Name = "Test",
                Description = "",
                GeographicalDescription = "Test",
                LocationId = LocationType.UnmarkedLocation
            };
        }
    }
}
