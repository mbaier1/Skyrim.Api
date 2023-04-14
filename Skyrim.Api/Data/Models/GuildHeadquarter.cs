using Skyrim.Api.Data.AbstractModels;

namespace Skyrim.Api.Data.Models
{
    public class GuildHeadquarter : Location
    {
        public GuildHeadquarter()
        {
            this.TypeOfLocation = Enums.LocationType.GuildHeadquarter;
        }
    }
}
