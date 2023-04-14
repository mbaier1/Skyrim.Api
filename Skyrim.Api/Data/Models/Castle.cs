using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Castle : Location
    {
        public Castle()
        {
            this.TypeOfLocation = LocationType.Castle;
        }
    }
}
