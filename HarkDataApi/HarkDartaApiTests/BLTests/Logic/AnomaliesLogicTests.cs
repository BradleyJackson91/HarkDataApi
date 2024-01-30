using HarkDataApi.BusinessLayer.Logic;
using HarkDataApi.DataAccessLayer.Data;
using HarkDataApi.DataAccessLayer.Models;
using HarkDataApi.DataAccessLayer.Repositories;
using HarkDataApi.DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarkDataApiTests.BLTests.Logic
{
    public class AnomaliesLogicTests
    {
        IAnomaliesLogic logic;

        [SetUp]
        public void SetUp() 
        {
            string rootFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

            string fileName = "HalfHourlyEnergyData.csv";
            string filePath = Path.Combine(rootFolder, fileName);

            string anomaliesFileName = "HalfHourlyEnergyDataAnomalies.csv";
            string anomaliesFilePath = Path.Combine(rootFolder, anomaliesFileName);

            IEnergyConsumptionAnomaliesDataSource anomaliesDataSource = new EnergyConsumptionAnomaliesDataSource(anomaliesFilePath);
            IEnergyConsumptionAnomaliesRepository anomaliesRepo = new EnergyConsumptionAnomaliesRepository(anomaliesDataSource);

            IEnergyConsumptionDataSource dataSource = new EnergyConsumptionDataSource(filePath);
            IEnergyConsumptionRepository repo = new EnergyConsumptionRepository(dataSource);

            string temperatureFileName = "";
            string temperatureFilePath = Path.Combine(rootFolder, temperatureFileName);

            ITemperatureDataSource tempDataSource = new TemperatureDataSource(filePath);
            ITemperatureRepository tempRepo = new TemperatureRepository(tempDataSource);

            logic = new AnomaliesLogic(anomaliesRepo, repo, tempRepo);
        }

        [Test]
        public void CalculateConsumptionAnomaliesBasedOnProvidedPercentageVariation()
        {
            float percentage = 100;
            List<EnergyConsumptionAnomaliesDto> anomalies =
                logic.CalculateConsumptionAnomaliesBasedOnProvidedPercentageVariation(percentage);

            return;
        }

    }
}
