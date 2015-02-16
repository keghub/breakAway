namespace BreakAway.Entities
{
    public class Resort : Lodging
    {
        public string ResortChainOwner { get; set; }
        public bool IsLuxuryResort { get; set; }
    }
}