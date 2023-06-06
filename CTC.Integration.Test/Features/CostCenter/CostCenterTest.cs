using CTC.Integration.Test.Features.Client.Dtos;
using CTC.Integration.Test.Features.CostCenter.Dtos;
using CTC.Integration.Test.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace CTC.Integration.Test.Features.CostCenter
{
    [TestClass]
    public class CostCenterTest : IntegrationTestBase
    {
        private const string REQUEST_URI = "/CostCenter";

        private GetClientDto _clientDto;

        [TestMethod]
        public async Task ShouldRegisterGetUpdateAndDeleteCostCenter()
        {
            //Arrange
            var newCostCenter = new
            {
                Name = "Obra Teste 4",
                Observations = "testando 4",
                StartingDate = "2023-01-01",
                ExpectedClosingDate = "2023-12-01",
                ClosingDate = "2023-02-01",
                ClientId = _clientDto.ClientId,
                Address = new {
                    PostalCode = "31089777",
                    StreetName = "Rua São Paulo",
                    Neighborhood = "Centro",
                    Number = 5,
                    Complement = "Apt. 2",
                    City = "Belo Horizonte",
                    State = "MG"
                }
            };

            //Act
            var getResultBeforeRegister = await MakeGetRequest<ListCostCenterDto>(REQUEST_URI);
            var registerCostCenterResult = await MakePostRequest(REQUEST_URI, newCostCenter);
            var getResultAfterRegister = await MakeGetRequest<ListCostCenterDto>(REQUEST_URI);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, getResultBeforeRegister.StatusCode);
            Assert.AreEqual(HttpStatusCode.Created, registerCostCenterResult);
            Assert.AreEqual(HttpStatusCode.OK, getResultAfterRegister.StatusCode);
            Assert.IsTrue(getResultAfterRegister.Body!.TotalCount == (getResultBeforeRegister.Body!.TotalCount + 1));

            var registeredCostCenter = getResultAfterRegister.Body.Results!.FirstOrDefault(c => c.Name == newCostCenter.Name);
            Assert.IsNotNull(registeredCostCenter);

            //Arrange
            var updateCostCenter = new
            {
                Id = registeredCostCenter.Id,
                Name = "Obra Teste 5",
                Observations = "testando 4",
                StartingDate = "2023-01-01",
                ExpectedClosingDate = "2023-12-01",
                ClosingDate = "2023-02-01",
                ClientId = _clientDto.ClientId,
                Address = new
                {
                    PostalCode = "31089777",
                    StreetName = "Rua São Paulo",
                    Neighborhood = "Centro",
                    Number = 5,
                    Complement = "Apt. 2",
                    City = "Belo Horizonte",
                    State = "MG"
                }
            };

            //Act
            var updateCostCenterResult = await MakePutRequest(REQUEST_URI, updateCostCenter);
            var getResultAfterUpdate = await MakeGetRequest<GetCostCenterDto>($"{REQUEST_URI}/{registeredCostCenter.Id}");

            //Assert
            Assert.IsNotNull(getResultAfterUpdate);
            Assert.AreEqual(HttpStatusCode.OK, getResultAfterUpdate.StatusCode);
            Assert.AreEqual(getResultAfterUpdate.Body!.Name, updateCostCenter.Name);

            //Act
            var deletCostCenterResult = await MakeDeleteRequest($"{REQUEST_URI}/{registeredCostCenter.Id}");
            var getResultAfterDelete = await MakeGetRequest<GetCostCenterDto>($"{REQUEST_URI}/{registeredCostCenter.Id}");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, deletCostCenterResult);
            Assert.AreEqual(HttpStatusCode.NotFound, getResultAfterDelete.StatusCode);
        }

        [TestInitialize]
        public async Task BeforeTest()
        {
            var newClient = new
            {
                Name = "Test Client",
                Email = "test.client@gmail.com",
                Phone = "31912365544",
                Document = "11100092847"
            };

            //Act
            _ = await MakePostRequest("/Client", newClient);
            var clients = await MakeGetRequest<ListClientDto>("/Client");
            _clientDto = clients.Body!.Results!.FirstOrDefault(c => c.Document == newClient.Document)!;
        }

        [TestCleanup]
        public async Task AfterTest()
        {
            _ = await MakeDeleteRequest($"/Client/{_clientDto.ClientId}");
        }
    }
}
