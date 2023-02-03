using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.AbstractModels
{
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PersonType TypeOfPerson { get; set; }

        [ForeignKey(nameof(LocationId))]
        public int LocationId { get; set; }
    }
}
