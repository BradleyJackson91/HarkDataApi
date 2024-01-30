namespace HarkDataApi.DataTransferObjects.Models
{
    public class TemperatureDto
    {
        public DateTime Date { get; set; }
        public float AverageTemperature { get; set; }
        public float AverageHumidity { get; set; }
    }
}
