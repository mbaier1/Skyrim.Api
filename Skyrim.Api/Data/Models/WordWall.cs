using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class WordWall : Location
    {
        public WordWall()
        {
            this.TypeOfLocation = LocationType.WordWall;
        }
    }
}
