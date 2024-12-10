namespace WeatherApi.Models
{
    public class WeatherRecord
    {
        public int Id { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
        public double TemperatureC { get; set; }
        public string Condition { get; set; }
    }
}
