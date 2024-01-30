using HarkDataApi.DataTransferObjects.Models;

namespace HarkDataApi.DataAccessLayer.Models
{
    public class EnergyConsumptionDalModel
    {
        public DateTime Timestamp { get; set; }
        public float Consumption { get; set; }

        public EnergyConsumptionDalModel() { }
        public EnergyConsumptionDalModel(EnergyConsumptionDto dto)
        {
            Timestamp = dto.Timestamp;
            Consumption = dto.Consumption;
        }

        public EnergyConsumptionDalModel(string csvLine)
        {
            string[] parts = csvLine.Split(',');

            Timestamp = DateTime.Parse(parts[0].Trim());
            Consumption = float.Parse(parts[1].Trim());
        }
        public EnergyConsumptionDto MapToDto()
        {
            return new EnergyConsumptionDto()
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
