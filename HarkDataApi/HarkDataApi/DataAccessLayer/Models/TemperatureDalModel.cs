using HarkDataApi.DataTransferObjects.Models;

namespace HarkDataApi.DataAccessLayer.Models
{
    public class TemperatureDalModel
    {
        public DateTime Date { get; set; }
        public float AverageTemperature { get; set; }
        public float AverageHumidity { get; set; }

        public TemperatureDalModel() { }
        public TemperatureDalModel(TemperatureDto dto)
        {
            Date = dto.Date;
            AverageTemperature = dto.AverageTemperature;
            AverageHumidity = dto.AverageHumidity;
        }

        public TemperatureDalModel(string csvLine)
        {
            string[] parts = csvLine.Split(',');

            Date = DateTime.Parse(parts[0].Trim());
            AverageTemperature = float.Parse(parts[1].Trim());
            AverageHumidity = float.Parse(parts[2].Trim());
        }
        public TemperatureDto MapToDto()
        {
            return new TemperatureDto()
            {
                Date = Date,
                AverageTemperature = AverageTemperature,
                AverageHumidity = AverageHumidity
            };
        }

        public string ToCsv()
        {
            return string.Concat(Date, ",", AverageTemperature, ",", AverageHumidity);
        }

    }
}
