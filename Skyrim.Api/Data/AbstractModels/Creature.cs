using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.AbstractModels
{
    public abstract class Creature
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CreatureType TypeOfCreature { get; set; }

        [ForeignKey(nameof(locationId))]
        public int locationId { get; set; }
    }
}
