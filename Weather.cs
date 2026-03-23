namespace Weather_API
{
    public class Weather
    {
        public DateTime DateTime { get; set; }
        public decimal temp { get; set; }
        public string? sunrise { get; set; }
        public string? sunset { get; set; }
        public string? conditions { get; set; }
        public string? description { get; set; }

    }
}
