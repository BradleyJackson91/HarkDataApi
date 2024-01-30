using HarkDataApi.DataTransferObjects.Models;

namespace HarkDataApi.DataAccessLayer.Models
{
    public class EnergyConsumptionAnomaliesDalModel
    {
        public DateTime Timestamp { get; set; }
        public float Consumption { get; set; }

        public EnergyConsumptionAnomaliesDalModel() { }
        public EnergyConsumptionAnomaliesDalModel(EnergyConsumptionAnomaliesDto dto)
        {
            Timestamp = dto.Timestamp;
            Consumption = dto.Consumption;
        }

        public EnergyConsumptionAnomaliesDalModel(string csvLine)
        {
            string[] parts = csvLine.Split(',');

            Timestamp = DateTime.Parse(parts[0].Trim());
            Consumption = float.Parse(parts[1].Trim());
        }
        public EnergyConsumptionAnomaliesDto MapToDto()
        {
            return new EnergyConsumptionAnomaliesDto()
            {
                Timestamp = Timestamp,
                Consumption = Consumption
            };
        }

        public string ToCsv()
        {
            return string.Concat(Timestamp, ",", Consumption);
        }

    }
}
