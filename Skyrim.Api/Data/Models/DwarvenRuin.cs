using Skyrim.Api.Data.AbstractModels;

namespace Skyrim.Api.Data.Models
{
    public class DwarvenRuin : Location
    {
        public DwarvenRuin()
        {
            this.TypeOfLocation = Enums.LocationType.DwarvenRuin;
        }
    }
}
