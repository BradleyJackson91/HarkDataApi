using HarkDataApi.DataAccessLayer.Repositories;
using HarkDataApi.DataTransferObjects.Models;

namespace HarkDataApi.BusinessLayer.Logic
{
    public interface IAnomaliesLogic
    {
        public bool IsEnergyConsumptionAnAnomaly(EnergyConsumptionDto dto);
        public List<EnergyConsumptionAnomaliesDto> CalculateConsumptionAnomaliesBasedOnProvidedPercentageVariation(float percentage);
    }

    public class AnomaliesLogic : IAnomaliesLogic
    {
        private readonly IEnergyConsumptionAnomaliesRepository _anomaliesRepository;
        private readonly IEnergyConsumptionRepository _energyConsumptionRepository;
        private readonly ITemperatureRepository _temperatureRepository;

        public AnomaliesLogic(IEnergyConsumptionAnomaliesRepository anomaliesRepository,
            IEnergyConsumptionRepository energyConsumptionRepository,
            ITemperatureRepository temperatureRepository)
        {
            _anomaliesRepository = anomaliesRepository;
            _energyConsumptionRepository = energyConsumptionRepository;
            _temperatureRepository = temperatureRepository;
        }

        public bool IsEnergyConsumptionAnAnomaly(EnergyConsumptionDto dto)
        {
            List<EnergyConsumptionAnomaliesDto> anomalies = _anomaliesRepository.GetByDateTime(dto.Timestamp).ToList();

            if (anomalies == null || anomalies.Count == 0)
            {
                return false;
            }

            if (anomalies.Any(a => a.Timestamp == dto.Timestamp && a.Consumption.Equals(dto.Consumption)))
            {
                return true;
            }

            return false;
        }

        public List<EnergyConsumptionAnomaliesDto> CalculateConsumptionAnomaliesBasedOnProvidedPercentageVariation(float percentage)
        {
            List<EnergyConsumptionDto> records = _energyConsumptionRepository.GetAll().ToList();
            float consumptionAverage = records.Average(r => r.Consumption);

            float consumptionMin = GetConsumptionMin(consumptionAverage, percentage);
            float consumptionMax = GetConsumptionMax(consumptionAverage, percentage);

            List<EnergyConsumptionAnomaliesDto> anomalies = records
                .Where(r => r.Consumption <= consumptionMin || r.Consumption >= consumptionMax)
                .Select(r => new EnergyConsumptionAnomaliesDto() { Timestamp = r.Timestamp, Consumption = r.Consumption })
                .ToList();

            return anomalies;
        }

        private float GetConsumptionMin(float consumptionAverage, float percentage)
        {
            float reductionFactor = 1 - (percentage / 100);
            float result = consumptionAverage * reductionFactor;

            return result;
        }

        private float GetConsumptionMax(float consumptionAverage, float percentage)
        {
            float reductionFactor = 1 + (percentage / 100);
            float result = consumptionAverage * reductionFactor;

            return result;
        }

        public ConsumptionWeatherDto GetConsumptionAndWeatherDataForAnomaly(EnergyConsumptionDto consumptionDto)
        {
            if (!IsEnergyConsumptionAnAnomaly(consumptionDto))
            {
                throw new ArgumentException("Energy consumption record provided is not an anomaly.");
            }

            TemperatureDto? temperatureDto = _temperatureRepository.GetByDateTime(consumptionDto.Timestamp).FirstOrDefault();

            return new ConsumptionWeatherDto()
            {
                TimeStamp = consumptionDto.Timestamp,
                Consumption = consumptionDto.Consumption,
                AverageTemperature = temperatureDto?.AverageTemperature ?? null,
                AverageHumidity = temperatureDto?.AverageHumidity ?? null
            };
        }

        public List<ConsumptionWeatherDto> GetConsumptionAndWeatherDataForAnomaliesWithinDateRange(DateTime start, DateTime end)
        {
            List<EnergyConsumptionAnomaliesDto> anomalies = _anomaliesRepository.GetByDateRange(start, end).ToList();
            List<TemperatureDto> weatherData = _temperatureRepository.GetByDateRange(start, end).ToList();
            
            List<ConsumptionWeatherDto> result = new List<ConsumptionWeatherDto>();

            foreach(EnergyConsumptionAnomaliesDto anomaly in anomalies)
            {
                TemperatureDto? weatherRecord = weatherData.Where(w => w.Date == anomaly.Timestamp).FirstOrDefault();

                result.Add(new ConsumptionWeatherDto()
                {
                    TimeStamp = anomaly.Timestamp,
                    Consumption = anomaly.Consumption,
                    AverageTemperature= weatherRecord?.AverageTemperature ?? null,
                    AverageHumidity = weatherRecord?.AverageHumidity ?? null
                });
            }

            return result;
        }
    }
}
