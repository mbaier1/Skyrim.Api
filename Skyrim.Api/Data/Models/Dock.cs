using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Dock : Location
    {
        public Dock()
        {
            this.TypeOfLocation = LocationType.Dock;
        }
    }
}
