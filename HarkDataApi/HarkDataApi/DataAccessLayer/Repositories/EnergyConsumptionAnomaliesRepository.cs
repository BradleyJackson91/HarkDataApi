using HarkDataApi.DataAccessLayer.Data;
using HarkDataApi.DataAccessLayer.Models;
using HarkDataApi.DataTransferObjects.Exceptions;
using HarkDataApi.DataTransferObjects.Models;
using System;

namespace HarkDataApi.DataAccessLayer.Repositories
{
    public interface IEnergyConsumptionAnomaliesRepository
    {
        public IEnumerable<EnergyConsumptionAnomaliesDto> GetAll();
        public IEnumerable<EnergyConsumptionAnomaliesDto> GetByDate(DateTime date);
        public IEnumerable<EnergyConsumptionAnomaliesDto> GetByDateTime(DateTime dateTime);
        public IEnumerable<EnergyConsumptionAnomaliesDto> GetByDateRange(DateTime startDate, DateTime endDate);
        public void Add(EnergyConsumptionAnomaliesDto dto);
        public void Remove(EnergyConsumptionAnomaliesDto dto);
        public void Update(EnergyConsumptionAnomaliesDto dto);    
    }

    public class EnergyConsumptionAnomaliesRepository : IEnergyConsumptionAnomaliesRepository
    {
        private readonly IEnergyConsumptionAnomaliesDataSource _dataSource;

        public EnergyConsumptionAnomaliesRepository(IEnergyConsumptionAnomaliesDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<EnergyConsumptionAnomaliesDto> GetAll()
        {
            try
            {
                return _dataSource.Records.Select(r => r.MapToDto());
            }
            catch (Exception ex)
            {
                throw new DalException("Energy consumption anomalies - GetAll", ex);
            }
        }

        public IEnumerable<EnergyConsumptionAnomaliesDto> GetByDate(DateTime date)
        {
            try
            {
                return _dataSource.Records
                    .Where(r => r.Timestamp.Date == date.Date)
                    .Select(r => r.MapToDto());
            }
            catch (Exception ex)
            {
                throw new DalException("Energy consumption anomalies - GetByDate", ex);
            }
        }

        public IEnumerable<EnergyConsumptionAnomaliesDto> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _dataSource.Records
                    .Where(r => r.Timestamp >= startDate && r.Timestamp <= endDate)
                    .Select(r => r.MapToDto());
            }
            catch(Exception ex)
            {
                throw new DalException("Energy consumption anomalies - GetByDateRange", ex);
            }
        }

        public void Add(EnergyConsumptionAnomaliesDto dto)
        {
            try
            {
                _dataSource.Add(new EnergyConsumptionAnomaliesDalModel(dto));
            }
            catch(Exception ex)
            {
                throw new DalException("Energy consumption anomalies - Add", ex);
            }

        }

        public void Remove(EnergyConsumptionAnomaliesDto dto)
        {
            try
            {
                _dataSource.Remove(new EnergyConsumptionAnomaliesDalModel(dto));
                return;
            }
            catch (Exception ex)
            {
                throw new DalException("Energy consumption anomalies - Remove", ex);
            }
        }

        public void Update(EnergyConsumptionAnomaliesDto dto)
        {
            try
            {
                _dataSource.Update(new EnergyConsumptionAnomaliesDalModel(dto));
            }
            catch(Exception ex)
            {
                throw new DalException("Energy consumption anomalies - Update");
            }
        }

        public IEnumerable<EnergyConsumptionAnomaliesDto> GetByDateTime(DateTime dateTime)
        {
            return _dataSource.Records
                .Where(r => r.Timestamp == dateTime)
                .Select(r => r.MapToDto()); ;
        }
    }
}
