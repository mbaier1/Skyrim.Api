using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Temple : Location
    {
        public Temple()
        {
            this.TypeOfLocation = LocationType.Temple;
        }
    }
}
