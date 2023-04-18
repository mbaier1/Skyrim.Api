using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class GuildHeadquarter : Location
    {
        public GuildHeadquarter()
        {
            this.LocationId = LocationType.GuildHeadquarter;
            this.TypeOfLocation = LocationType.GuildHeadquarter.GetDisplayName();
        }
    }
}
