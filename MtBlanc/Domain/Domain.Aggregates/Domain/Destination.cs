using System.ComponentModel.DataAnnotations;
using Studentum.Domain;

namespace BreakAway.Domain
{
    public class Destination : IEntity
    {
        public int Id { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }
    }
}