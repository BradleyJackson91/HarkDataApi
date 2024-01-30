using HarkDataApi.Controllers.ApiResponses;
using HarkDataApi.Controllers.Models;
using HarkDataApi.DataTransferObjects.Exceptions;
using HarkDataApi.DataTransferObjects.Models;
using HarkDataApi.ServiceLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HarkDataApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorDataController : ControllerBase
    {
        private readonly ILogger<SensorDataController> _logger;
        private readonly IEnergyConsumptionService _service;

        public SensorDataController(ILogger<SensorDataController> logger, IEnergyConsumptionService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("GetAll", Name = "GetAll")]
        public ApiResponse GetAll(int? page, int? pageSize)
        {
            try
            {
                List<EnergyConsumptionDto> result = _service.GetAllEnergyConsumptionRecords(page, pageSize);

                if (result.Count == 0)
                {
                    return ApiResponse.NotFound();
                }

                return ApiResponse.Success(result);
            }
            catch (ApiException apiEx)
            {
                return ApiResponse.Error(apiEx.Status, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponse.Error(500, "public Server Error");
            }
        }

        [HttpGet("GetForDateRange", Name = "GetForDateRange")]
        public ApiResponse GetForDateRange(DateRangeQueryParams parameters)
        {
            try
            {
                List<EnergyConsumptionDto> result = _service.GetEnergyConsumptionRecordsForDateRange(
                    parameters.StartDate,
                    parameters.EndDate,
                    parameters.Page,
                    parameters.PageSize);

                if (result.Count == 0)
                {
                    return ApiResponse.NotFound();
                }

                return ApiResponse.Success(result);
            }
            catch (ApiException apiEx)
            {
                return ApiResponse.Error(apiEx.Status, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponse.Error(500, "public Server Error");
            }
        }

        [HttpGet("GetForDate", Name = "GetForDate")]
        public ApiResponse GetForDate(DateTime date, int? page, int? pageSize)
        {
            try
            {
                List<EnergyConsumptionDto> result = _service.GetEnergyConsumptionRecordsForDate(date, page, pageSize);

                if (result.Count == 0)
                {
                    return ApiResponse.NotFound();
                }

                return ApiResponse.Success(result);
            }
            catch (ApiException apiEx)
            {
                return ApiResponse.Error(apiEx.Status, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponse.Error(500, "public Server Error");
            }
        }

        [HttpGet("GetAllWithWeatherData", Name = "GetAllWithWeatherData")]
        public ApiResponse GetAllWithWeatherData(DateTime date, int? page, int? pageSize)
        {
            try
            {
                List<ConsumptionWeatherDto> result = _service.GetEnergyConsumptionAndWeatherRecordsForDate(date, page, pageSize);

                if (result.Count == 0)
                {
                    return ApiResponse.NotFound();
                }

                return ApiResponse.Success(result);
            }
            catch (ApiException apiEx)
            {
                return ApiResponse.Error(apiEx.Status, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponse.Error(500, "public Server Error");
            }
        }

        [HttpGet("GetAllWithWeatherDataForDateRange", Name = "GetAllWithWeatherDataForDateRange")]
        public ApiResponse GetAllWithWeatherDataForDateRange(DateRangeQueryParams parameters)
        {
            try
            {
                List<ConsumptionWeatherDto> result = _service.GetEnergyConsumptionAndWeatherRecordsForDateRange(
                    parameters.StartDate,
                    parameters.EndDate,
                    parameters.Page,
                    parameters.PageSize);

                if (result.Count == 0)
                {
                    return ApiResponse.NotFound();
                }

                return ApiResponse.Success(result);
            }
            catch (ApiException apiEx)
            {
                return ApiResponse.Error(apiEx.Status, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponse.Error(500, "public Server Error");
            }
        }

        [HttpPost(Name = "Create")]
        public ApiResponse Create(EnergyConsumptionDto energyConsumptionRecord)
        {
            try
            {
                //TODO: Handle this better.
                _service.CreateEnergyConsumptionRecord(energyConsumptionRecord);
                return ApiResponse.Success(null);
            }
            catch (ApiException apiEx)
            {
                return ApiResponse.Error(apiEx.Status, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponse.Error(500, "public Server Error");
            }
        }

        [HttpPut(Name = "Update")]
        public ApiResponse Update(EnergyConsumptionDto energyConsumptionRecord)
        {
            try
            {
                //TODO: Handle this better.
                _service.UpdateEnergyConsumptionRecord(energyConsumptionRecord);
                return ApiResponse.Success(null);
            }
            catch (ApiException apiEx)
            {
                return ApiResponse.Error(apiEx.Status, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponse.Error(500, "public Server Error");
            }
        }

        [HttpDelete(Name = "Delete")]
        public ApiResponse Delete(EnergyConsumptionDto energyConsumptionRecord)
        {
            try
            {
                //TODO: Handle this better.
                _service.DeleteEnergyConsumptionRecord(energyConsumptionRecord);
                return ApiResponse.Success(null);
            }
            catch (ApiException apiEx)
            {
                return ApiResponse.Error(apiEx.Status, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponse.Error(500, "public Server Error");
            }
        }
    }
}
