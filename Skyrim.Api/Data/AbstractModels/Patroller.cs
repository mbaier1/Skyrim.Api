using System;

namespace Skyrim.Api.Data.AbstractModels
{
    public abstract class Patroller : Person
    {
        public ICollection<Location>? PatrolledLocations { get; set; }
        public ICollection<Building>? PatrolledBuildings { get; set; }
        public ICollection<Person>? PatrolledPeople { get; set; }
        public ICollection<Creature>? PatrolledCreatures { get; set; }
    }
}
