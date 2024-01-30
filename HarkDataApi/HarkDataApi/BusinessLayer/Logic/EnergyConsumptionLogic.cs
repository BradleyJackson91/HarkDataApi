using HarkDataApi.DataAccessLayer.Repositories;
using HarkDataApi.DataTransferObjects.Exceptions;
using HarkDataApi.DataTransferObjects.Models;
using System.Reflection.Metadata;

namespace HarkDataApi.BusinessLayer.Logic
{
    public interface IEnergyConsumptionLogic
    {
        public List<EnergyConsumptionDto> GetAllEnergyConsumptionRecords(
            int? page = null, int? pageSize = null);
        public List<EnergyConsumptionDto> GetEnergyConsumptionRecordsForDateRange(
            DateTime startDate, DateTime endDate, int? page = null, int? pageSize = null);
        public List<EnergyConsumptionDto> GetEnergyConsumptionRecordsForDate(
            DateTime date, int? page = null, int? pageSize = null);

        public bool CreateEnergyConsumptionRecord(EnergyConsumptionDto energyConsumption);
        public bool UpdateEnergyConsumptionRecord(EnergyConsumptionDto energyConsumption);
        public bool DeleteEnergyConsumptionRecord(EnergyConsumptionDto energyConsumption);

        public List<ConsumptionWeatherDto> GetEnergyConsumptionAndWeatherRecordsForDate(
            DateTime date, int? page = null, int? pageSize = null);
        public List<ConsumptionWeatherDto> GetEnergyConsumptionAndWeatherRecordsForDateRange(
            DateTime startDate, DateTime endDate, int? page = null, int? pageSize = null);

    }

    public class EnergyConsumptionLogic : IEnergyConsumptionLogic
    {
        private readonly IEnergyConsumptionRepository _energyConsumptionRepository;
        private readonly ITemperatureRepository _temperatureRepository;

        public EnergyConsumptionLogic(
            IEnergyConsumptionRepository energyConsumptionRepository,
            ITemperatureRepository temperatureRepository)
        {
            _energyConsumptionRepository = energyConsumptionRepository;
            _temperatureRepository = temperatureRepository;
        }

        public bool CreateEnergyConsumptionRecord(EnergyConsumptionDto energyConsumption)
        {
            try
            {
                _energyConsumptionRepository.Add(energyConsumption);
                return true;
            }
            catch(DalException dalEx)
            {
                Console.WriteLine(dalEx?.ToString());
                throw;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString()); //TODO: Replace with real logging.
                throw;
            }
        }

        public bool DeleteEnergyConsumptionRecord(EnergyConsumptionDto energyConsumption)
        {
            try
            {
                _energyConsumptionRepository.Remove(energyConsumption);
                return true;
            }
            catch(DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw dalEx;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<EnergyConsumptionDto> GetAllEnergyConsumptionRecords(int? page = null, int? pageSize = null)
        {
            try
            {
                return _energyConsumptionRepository.GetAll()
                    .Skip(page ?? 0 * pageSize ?? 0)
                    .Take(pageSize ?? int.MaxValue)
                    .ToList();
            }
            catch(DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw dalEx;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<ConsumptionWeatherDto> GetEnergyConsumptionAndWeatherRecordsForDate(DateTime date, int? page = null, int? pageSize = null)
        {
            try
            {
                List<ConsumptionWeatherDto> result = 
                    _energyConsumptionRepository.GetByDate(date)
                    .Skip(page ?? 0 * pageSize ?? 0)
                    .Take(pageSize ?? int.MaxValue)
                    .Select(r => new ConsumptionWeatherDto() { TimeStamp = r.Timestamp, Consumption = r.Consumption })
                    .ToList();

                foreach(ConsumptionWeatherDto item in result)
                {
                    TemperatureDto? tempHumidity = _temperatureRepository.GetByDateTime(item.TimeStamp).FirstOrDefault();
                    if(tempHumidity == null)
                    {
                        continue;
                    }

                    item.AverageTemperature = tempHumidity.AverageTemperature;
                    item.AverageHumidity = tempHumidity.AverageHumidity;
                }

                return result;
            }
            catch(DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw dalEx;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<ConsumptionWeatherDto> GetEnergyConsumptionAndWeatherRecordsForDateRange(DateTime startDate, DateTime endDate, int? page = null, int? pageSize = null)
        {
            try
            {
                List<ConsumptionWeatherDto> result =
                    _energyConsumptionRepository.GetByDateRange(startDate, endDate)
                    .Skip(page ?? 0 * pageSize ?? 0)
                    .Take(pageSize ?? int.MaxValue)
                    .Select(r => new ConsumptionWeatherDto() { TimeStamp = r.Timestamp, Consumption = r.Consumption })
                    .ToList();

                foreach (ConsumptionWeatherDto item in result)
                {
                    TemperatureDto? tempHumidity = _temperatureRepository.GetByDateTime(item.TimeStamp).FirstOrDefault();
                    if (tempHumidity == null)
                    {
                        continue;
                    }

                    item.AverageTemperature = tempHumidity.AverageTemperature;
                    item.AverageHumidity = tempHumidity.AverageHumidity;
                }

                return result;
            }
            catch(DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw dalEx;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public List<EnergyConsumptionDto> GetEnergyConsumptionRecordsForDate(DateTime date, int? page = null, int? pageSize = null)
        {
            try
            {
                return _energyConsumptionRepository.GetByDate(date).Skip(page ?? 0 * pageSize ?? 0).Take(page ?? int.MaxValue).ToList();
            }
            catch(DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw dalEx;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<EnergyConsumptionDto> GetEnergyConsumptionRecordsForDateRange(DateTime startDate, DateTime endDate, int? page = null, int? pageSize = null)
        {
            try
            {
                return _energyConsumptionRepository.GetByDateRange(startDate, endDate)
                    .Skip(page ?? 0 * pageSize ?? 0)
                    .Take(pageSize ?? int.MaxValue)
                    .ToList();
            }
            catch(DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw dalEx;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool UpdateEnergyConsumptionRecord(EnergyConsumptionDto energyConsumption)
        {
            try
            {
                _energyConsumptionRepository.Update(energyConsumption);
                return true;
            }
            catch(DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw dalEx;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
