using HarkDataApi.BusinessLayer.Logic;
using HarkDataApi.DataTransferObjects.Exceptions;
using HarkDataApi.DataTransferObjects.Models;

namespace HarkDataApi.ServiceLayer.Models
{
    public interface IEnergyConsumptionService
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
    public class EnergyConsumptionService : IEnergyConsumptionService
    {
        private readonly IEnergyConsumptionLogic _logic;

        public EnergyConsumptionService(IEnergyConsumptionLogic logic)
        {
            _logic = logic;
        }

        public bool CreateEnergyConsumptionRecord(EnergyConsumptionDto energyConsumption)
        {
            try
            {
                return _logic.CreateEnergyConsumptionRecord(energyConsumption);
            }
            catch(DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw new ApiException(500, "Database error occurred.", dalEx);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new ApiException(500, "public server error.", ex);
            }
        }

        public bool DeleteEnergyConsumptionRecord(EnergyConsumptionDto energyConsumption)
        {
            try
            {
                return _logic.DeleteEnergyConsumptionRecord(energyConsumption);
            }
            catch (DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw new ApiException(500, "Database error occurred.", dalEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new ApiException(500, "public server error.", ex);
            }
        }

        public List<EnergyConsumptionDto> GetAllEnergyConsumptionRecords(int? page = null, int? pageSize = null)
        {
            try
            {
                return _logic.GetAllEnergyConsumptionRecords(page, pageSize);
            }
            catch (DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw new ApiException(500, "Database error occurred.", dalEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new ApiException(500, "public server error.", ex);
            }
        }

        public List<ConsumptionWeatherDto> GetEnergyConsumptionAndWeatherRecordsForDate(DateTime date, int? page = null, int? pageSize = null)
        {
            try
            {
                return _logic.GetEnergyConsumptionAndWeatherRecordsForDate(date, page, pageSize);
            }
            catch (DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw new ApiException(500, "Database error occurred.", dalEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new ApiException(500, "public server error.", ex);
            }
        }

        public List<ConsumptionWeatherDto> GetEnergyConsumptionAndWeatherRecordsForDateRange(DateTime startDate, DateTime endDate, int? page = null, int? pageSize = null)
        {
            try
            {
                return _logic.GetEnergyConsumptionAndWeatherRecordsForDateRange(startDate, endDate, page, pageSize);
            }
            catch (DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw new ApiException(500, "Database error occurred.", dalEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new ApiException(500, "public server error.", ex);
            }
        }

        public List<EnergyConsumptionDto> GetEnergyConsumptionRecordsForDate(DateTime date, int? page = null, int? pageSize = null)
        {
            try
            {
                return _logic.GetEnergyConsumptionRecordsForDate(date, page, pageSize);
            }
            catch (DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw new ApiException(500, "Database error occurred.", dalEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new ApiException(500, "public server error.", ex);
            }
        }

        public List<EnergyConsumptionDto> GetEnergyConsumptionRecordsForDateRange(DateTime startDate, DateTime endDate, int? page = null, int? pageSize = null)
        {
            try
            {
                return _logic.GetEnergyConsumptionRecordsForDateRange(startDate, endDate, page, pageSize);
            }
            catch (DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw new ApiException(500, "Database error occurred.", dalEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new ApiException(500, "public server error.", ex);
            }
        }

        public bool UpdateEnergyConsumptionRecord(EnergyConsumptionDto energyConsumption)
        {
            try
            {
                return _logic.UpdateEnergyConsumptionRecord(energyConsumption);
            }
            catch (DalException dalEx)
            {
                Console.WriteLine(dalEx.ToString());
                throw new ApiException(500, "Database error occurred.", dalEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new ApiException(500, "public server error.", ex);
            }
        }
    }
}
