using System.ComponentModel.DataAnnotations;
using Studentum.Domain;
using Studentum.Infrastructure.Caching;

namespace BreakAway.Domain
{
    public class Equipment : ICachedEntity
    {
        public int Id { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        public int Key
        {
            get { return Id; }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}