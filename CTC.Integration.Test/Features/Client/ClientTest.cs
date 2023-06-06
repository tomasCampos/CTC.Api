using CTC.Integration.Test.Features.Client.Dtos;
using CTC.Integration.Test.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace CTC.Integration.Test.Features.Client
{
    [TestClass]
    public class RegisterClientTest : IntegrationTestBase
    {
        private const string REQUEST_URI = "/Client";

        [TestMethod]
        public async Task ShouldRegisterGetUpdateAndDeleteClient()
        {
            //Arrange
            var newClient = new
            {
                Name = "Test Client",
                Email = "test.client@gmail.com",
                Phone = "31912365544",
                Document = "11100092847"
            };

            //Act
            var getResultBeforeRegister = await MakeGetRequest<ListClientDto>(REQUEST_URI);
            var registerClientResult = await MakePostRequest(REQUEST_URI, newClient);
            var getResultAfterRegister = await MakeGetRequest<ListClientDto>(REQUEST_URI);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, getResultBeforeRegister.StatusCode);
            Assert.AreEqual(HttpStatusCode.Created, registerClientResult);
            Assert.AreEqual(HttpStatusCode.OK, getResultAfterRegister.StatusCode);
            Assert.IsTrue(getResultAfterRegister.Body!.TotalCount == (getResultBeforeRegister.Body!.TotalCount + 1));

            var registeredClient = getResultAfterRegister.Body.Results!.FirstOrDefault(c => c.Document == newClient.Document);
            Assert.IsNotNull(registeredClient);

            //Arrange
            var updateClient = new
            {
                Id = registeredClient.ClientId,
                Name = "Test Client",
                Email = "test.client@gmail.com",
                Phone = "31912365544",
                Document = "66666666666"
            };

            //Act
            var updateClientResult = await MakePutRequest(REQUEST_URI, updateClient);
            var getResultAfterUpdate = await MakeGetRequest<GetClientDto>($"{REQUEST_URI}/{registeredClient.ClientId}");

            //Assert
            Assert.IsNotNull(getResultAfterUpdate);
            Assert.AreEqual(HttpStatusCode.OK, getResultAfterUpdate.StatusCode);
            Assert.AreEqual(getResultAfterUpdate.Body!.Document, updateClient.Document);

            //Act
            var deletClientResult = await MakeDeleteRequest($"{REQUEST_URI}/{registeredClient.ClientId}");
            var getResultAfterDelete = await MakeGetRequest<GetClientDto>($"{REQUEST_URI}/{registeredClient.ClientId}");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, deletClientResult);
            Assert.AreEqual(HttpStatusCode.NotFound, getResultAfterDelete.StatusCode);
        }
    }
}
