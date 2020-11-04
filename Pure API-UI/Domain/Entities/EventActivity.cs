namespace BreakAway.Entities
{
    public class EventActivity
    {
        public int EventId { get; set; }
        public int ActivityId { get; set; }
        public virtual Event Event { get; set; }
        public virtual Activity Activity { get; set; }
    }
}