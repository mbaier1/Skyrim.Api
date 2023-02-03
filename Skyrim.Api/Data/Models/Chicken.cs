using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Chicken : Creature
    {
        public Chicken()
        {
            this.TypeOfCreature = CreatureType.Chicken;
        }
    }
}
