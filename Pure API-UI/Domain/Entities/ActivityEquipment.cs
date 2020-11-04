namespace BreakAway.Entities
{
    public class ActivityEquipment
    {
        public int ActivityId { get; set; }
        public int EquipmentId { get; set; }
        public virtual Activity Activity { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}