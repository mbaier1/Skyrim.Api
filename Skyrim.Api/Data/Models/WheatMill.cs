using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class WheatMill : Location
    {
        public WheatMill()
        {
            this.TypeOfLocation = LocationType.WheatMill;
        }
    }
}
