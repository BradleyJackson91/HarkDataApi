using HarkDataApi.DataAccessLayer.Data;

namespace HarkDataApiTests.DALTests.Data
{
    public class EnergyConsumptionDataAnomaliesTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void EnergyConsumptionAnomaliesDataSource_PopulateDuringConstruction_DataRecordsCountGreaterThatZero()
        {
            string rootFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            string fileName = "HalfHourlyEnergyDataAnomalies.csv";
            string filePath = Path.Combine(rootFolder, fileName);

            EnergyConsumptionAnomaliesDataSource data = new EnergyConsumptionAnomaliesDataSource(filePath);

            Assert.IsTrue(data.Records.Count > 0);
        }
    }
}