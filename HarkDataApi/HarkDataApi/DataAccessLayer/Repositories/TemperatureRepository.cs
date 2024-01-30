using HarkDataApi.DataAccessLayer.Data;
using HarkDataApi.DataAccessLayer.Models;
using HarkDataApi.DataTransferObjects.Models;
using System;

namespace HarkDataApi.DataAccessLayer.Repositories
{
    public interface ITemperatureRepository
    {
        public IEnumerable<TemperatureDto> GetAll();
        public IEnumerable<TemperatureDto> GetByDate(DateTime date);
        public IEnumerable<TemperatureDto> GetByDateTime(DateTime dateTime);
        public IEnumerable<TemperatureDto> GetByDateRange(DateTime startDate, DateTime endDate);
        public void Add(TemperatureDto dto);
        public void Update(TemperatureDto dto);
        public void Remove(TemperatureDto dto);

    }

    public class TemperatureRepository : ITemperatureRepository
    {
        private readonly ITemperatureDataSource _dataSource;

        public TemperatureRepository(ITemperatureDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<TemperatureDto> GetAll()
        {
            return _dataSource.Records.Select(r => r.MapToDto());
        }

        public IEnumerable<TemperatureDto> GetByDate(DateTime date)
        {
            return _dataSource.Records
                .Where(r => r.Date.Date == date.Date)
                .Select(r => r.MapToDto());
        }

        public IEnumerable<TemperatureDto> GetByDateTime(DateTime dateTime)
        {
            return _dataSource.Records
                .Where(r => r.Date == dateTime)
                .Select(r => r.MapToDto());
        }

        public IEnumerable<TemperatureDto> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dataSource.Records
                .Where(r => r.Date >= startDate && r.Date <= endDate)
                .Select(r => r.MapToDto());
        }

        public void Add(TemperatureDto dto)
        {
            _dataSource.Add(new TemperatureDalModel(dto));
        }

        public void Remove(TemperatureDto dto)
        {
            _dataSource.Remove(new TemperatureDalModel(dto));
        }

        public void Update(TemperatureDto dto)
        {
            _dataSource.Update(new TemperatureDalModel(dto));
        }

    }
}
