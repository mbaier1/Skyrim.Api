using System.ComponentModel.DataAnnotations;
using System;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.AbstractModels
{
    public abstract class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LocationType TypeOfLocation { get; set; }
        public string GeographicalDescription { get; set; }
        public ICollection<Building> Buildings { get; set; }
        public ICollection<Person> People { get; set; }
        public ICollection<Creature> Creatures { get; set; }
    }
}
