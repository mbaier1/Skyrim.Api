using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Ruin : Location
    {
        public Ruin()
        {
            this.TypeOfLocation = LocationType.Ruin;
        }
    }
}