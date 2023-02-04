using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Town : Location
    {
        public Town()
        {
            this.TypeOfLocation = LocationType.Town;
        }
    }
}