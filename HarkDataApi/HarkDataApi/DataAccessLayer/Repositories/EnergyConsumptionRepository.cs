using HarkDataApi.DataAccessLayer.Data;
using HarkDataApi.DataAccessLayer.Models;
using HarkDataApi.DataTransferObjects.Models;
using System;

namespace HarkDataApi.DataAccessLayer.Repositories
{
    public interface IEnergyConsumptionRepository
    {
        public IEnumerable<EnergyConsumptionDto> GetAll();
        public IEnumerable<EnergyConsumptionDto> GetByDate(DateTime date);
        public IEnumerable<EnergyConsumptionDto> GetByDateTime(DateTime dateTime);
        public IEnumerable<EnergyConsumptionDto> GetByDateRange(DateTime startDate, DateTime endDate);
        public void Add(EnergyConsumptionDto dto);
        public void Remove(EnergyConsumptionDto dto);
        public void Update(EnergyConsumptionDto dto);
    }

    public class EnergyConsumptionRepository : IEnergyConsumptionRepository
    {
        private readonly IEnergyConsumptionDataSource _dataSource;

        public EnergyConsumptionRepository(IEnergyConsumptionDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<EnergyConsumptionDto> GetAll()
        {
            return _dataSource.Records.Select(r => r.MapToDto());

        }

        public IEnumerable<EnergyConsumptionDto> GetByDate(DateTime date)
        {
            return _dataSource.Records
                .Where(r => r.Timestamp.Date == date.Date)
                .Select(r => r.MapToDto());
        }

        public IEnumerable<EnergyConsumptionDto> GetByDateTime(DateTime dateTime)
        {
            return _dataSource.Records
                .Where(r => r.Timestamp == dateTime)
                .Select(r => r.MapToDto());
        }

        public IEnumerable<EnergyConsumptionDto> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dataSource.Records
                .Where(r => r.Timestamp >= startDate && r.Timestamp <= endDate)
                .Select(r => r.MapToDto());
        }

        public void Add(EnergyConsumptionDto dto)
        {
            _dataSource.Add(new EnergyConsumptionDalModel(dto));
        }

        public void Remove(EnergyConsumptionDto dto)
        {
            _dataSource.Remove(new EnergyConsumptionDalModel(dto));
        }

        public void Update(EnergyConsumptionDto dto)
        {
            _dataSource.Update(new EnergyConsumptionDalModel(dto));
        }

    }
}
