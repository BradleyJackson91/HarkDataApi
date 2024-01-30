using HarkDataApi.DataAccessLayer.Data;

namespace HarkDataApiTests.DALTests.Data
{
    public class TemperatureDataTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TemperatureataSource_PopulateDuringConstruction_DataRecordsCountGreaterThatZero()
        {
            string rootFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            string fileName = "Weather.csv";
            string filePath = Path.Combine(rootFolder, fileName);

            TemperatureDataSource data = new TemperatureDataSource(filePath);

            Assert.IsTrue(data.Records.Count > 0);
        }
    }
}