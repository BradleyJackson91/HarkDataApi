namespace HarkDataApi.DataTransferObjects.Models
{
    public class ConsumptionWeatherDto
    {
        public DateTime TimeStamp { get; set; }
        public float Consumption { get; set; }
        public float? AverageTemperature { get; set; }
        public float? AverageHumidity { get; set; }
    }
}
