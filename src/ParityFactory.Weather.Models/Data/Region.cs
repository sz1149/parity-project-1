namespace ParityFactory.Weather.Models.Data
{
    public enum RegionId
    {
        Iowa = 1
    }
    public class Region
    {
        public RegionId Id { get; set; }
        public string Name { get; set; }
    }
}