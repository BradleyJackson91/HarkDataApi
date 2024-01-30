using HarkDataApi.DataAccessLayer.Data;

namespace HarkDataApiTests.DALTests.Data
{
    public class EnergyConsumptionDataTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EnergyConsumptionDataSource_PopulateDuringConstruction_DataRecordsCountGreaterThatZero()
        {
            string rootFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            string fileName = "HalfHourlyEnergyData.csv";
            string filePath = Path.Combine(rootFolder, fileName);

            EnergyConsumptionDataSource data = new EnergyConsumptionDataSource(filePath);

            Assert.IsTrue(data.Records.Count > 0);
        }
    }
}