namespace ParityFactory.Weather.Models.Data
{
    public class Location
    {
        public long Id { get; set; }
        public RegionId RegionId { get; set; }
        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
