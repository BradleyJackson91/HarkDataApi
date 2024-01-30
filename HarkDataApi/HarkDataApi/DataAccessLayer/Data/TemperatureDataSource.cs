using HarkDataApi.DataAccessLayer.Models;
using HarkDataApi.DataAccessLayer.Repositories;

namespace HarkDataApi.DataAccessLayer.Data
{
    public interface ITemperatureDataSource
    {
        public void RefreshData();
        public void Add(TemperatureDalModel model);
        public void Remove(TemperatureDalModel model);
        public void Update(TemperatureDalModel model);
        public List<TemperatureDalModel> Records { get; }
    }

    public class TemperatureDataSource : ITemperatureDataSource
    {
        private readonly string _filePath;
        public List<TemperatureDalModel> Records { get; }

        public TemperatureDataSource(string filePath)
        {
            _filePath = filePath;
            Records = new List<TemperatureDalModel>();
            PopulateFromCsv();
        }

        private void PopulateFromCsv()
        {
            Records.Clear();

            string[] lines = File.ReadAllLines(_filePath);

            foreach (string line in lines.Skip(1))
            {
                Records.Add(new TemperatureDalModel(line));
            }
        }

        public void RefreshData()
        {
            PopulateFromCsv();
        }

        public void Add(TemperatureDalModel model)
        {
            Records.Add(model);
            File.AppendAllText(_filePath, model.ToCsv());
        }

        public void Remove(TemperatureDalModel model)
        {
            Records.Remove(model);
            RewriteFile();
        }

        public void Update(TemperatureDalModel model)
        {
            TemperatureDalModel? matchedModel = Records.FirstOrDefault(r => r.Date.Equals(model.Date));
            if(matchedModel == null)
            {
                throw new KeyNotFoundException(model.Date.ToString());
            }
            Records.Remove(matchedModel);
            Records.Add(model);


        }   

        private void RewriteFile()
        {
            File.Delete(_filePath);
            File.Create(_filePath);

            StreamWriter sw = new StreamWriter(_filePath, true);
            Records.ForEach(r => sw.WriteLine(r.ToCsv()));
            sw.Close();
        }

    }
}
