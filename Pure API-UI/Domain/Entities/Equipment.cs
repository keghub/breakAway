using System.ComponentModel.DataAnnotations;

namespace BreakAway.Entities
{
    public class Equipment
    {
        public int Id { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }
    }
}