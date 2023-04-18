using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class WordWall : Location
    {
        public WordWall()
        {
            this.LocationId = LocationType.WordWall;
            this.TypeOfLocation = LocationType.WordWall.GetDisplayName();
        }
    }
}
