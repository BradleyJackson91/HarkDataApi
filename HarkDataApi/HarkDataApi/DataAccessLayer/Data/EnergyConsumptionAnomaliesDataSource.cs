using HarkDataApi.DataAccessLayer.Models;
using HarkDataApi.DataAccessLayer.Repositories;

namespace HarkDataApi.DataAccessLayer.Data
{
    public interface IEnergyConsumptionAnomaliesDataSource
    {
        public void RefreshData();
        public void Add(EnergyConsumptionAnomaliesDalModel model);
        public void Remove(EnergyConsumptionAnomaliesDalModel model);
        public void Update(EnergyConsumptionAnomaliesDalModel model);
        public List<EnergyConsumptionAnomaliesDalModel> Records { get; }
    }

    public class EnergyConsumptionAnomaliesDataSource : IEnergyConsumptionAnomaliesDataSource
    {
        private readonly string _filePath;
        public List<EnergyConsumptionAnomaliesDalModel> Records { get; }

        public EnergyConsumptionAnomaliesDataSource(string filePath)
        {
            _filePath = filePath;
            Records = new List<EnergyConsumptionAnomaliesDalModel>();
            PopulateFromCsv();
        }

        private void PopulateFromCsv()
        {
            Records.Clear();

            string[] lines = File.ReadAllLines(_filePath);

            foreach (string line in lines.Skip(1))
            {
                Records.Add(new EnergyConsumptionAnomaliesDalModel(line));
            }
        }

        public void RefreshData()
        {
            PopulateFromCsv();
        }

        public void Add(EnergyConsumptionAnomaliesDalModel model)
        {
            Records.Add(model);
            File.AppendAllText(_filePath, model.ToCsv());
        }

        public void Remove(EnergyConsumptionAnomaliesDalModel model)
        {
            Records.Remove(model);
            RewriteFile();
        }

        public void Update(EnergyConsumptionAnomaliesDalModel model)
        {
            EnergyConsumptionAnomaliesDalModel? matchedModel = Records.FirstOrDefault(r => r.Timestamp.Equals(model.Timestamp));
            if(matchedModel == null)
            {
                throw new KeyNotFoundException(model.Timestamp.ToString());
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
