using HarkDataApi.DataAccessLayer.Models;
using HarkDataApi.DataAccessLayer.Repositories;

namespace HarkDataApi.DataAccessLayer.Data
{
    public interface IEnergyConsumptionDataSource
    {
        public void RefreshData();
        public void Add(EnergyConsumptionDalModel model);
        public void Remove(EnergyConsumptionDalModel model);
        public void Update(EnergyConsumptionDalModel model);
        public List<EnergyConsumptionDalModel> Records { get; }
    }

    public class EnergyConsumptionDataSource : IEnergyConsumptionDataSource
    {
        private readonly string _filePath;
        public List<EnergyConsumptionDalModel> Records { get; }

        public EnergyConsumptionDataSource(string filePath)
        {
            _filePath = filePath;
            Records = new List<EnergyConsumptionDalModel>();
            PopulateFromCsv();
        }

        private void PopulateFromCsv()
        {
            Records.Clear();

            string[] lines = File.ReadAllLines(_filePath);

            foreach (string line in lines.Skip(1))
            {
                Records.Add(new EnergyConsumptionDalModel(line));
            }
        }

        public void RefreshData()
        {
            PopulateFromCsv();
        }

        public void Add(EnergyConsumptionDalModel model)
        {
            Records.Add(model);
            File.AppendAllText(_filePath, model.ToCsv());
        }

        public void Remove(EnergyConsumptionDalModel model)
        {
            Records.Remove(model);
            RewriteFile();
        }

        public void Update(EnergyConsumptionDalModel model)
        {
            EnergyConsumptionDalModel? matchedModel = Records.FirstOrDefault(r => r.Timestamp.Equals(model.Timestamp));
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
