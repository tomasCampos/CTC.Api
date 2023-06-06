using CTC.Integration.Test.Features.Supplier.Dtos;
using CTC.Integration.Test.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace CTC.Integration.Test.Features.Supplier
{
    [TestClass]
    public class SupplierTest : IntegrationTestBase
    {
        private const string REQUEST_URI = "/Supplier";

        [TestMethod]
        public async Task ShouldRegisterGetUpdateAndDeleteSupplier()
        {
            //Arrange
            var newSupplier = new
            {
                Name = "Test Supplier",
                Email = "test.supplier@gmail.com",
                Phone = "31912365544",
                Document = "11100092847"
            };

            //Act
            var getResultBeforeRegister = await MakeGetRequest<ListSupplierDto>(REQUEST_URI);
            var registerSupplierResult = await MakePostRequest(REQUEST_URI, newSupplier);
            var getResultAfterRegister = await MakeGetRequest<ListSupplierDto>(REQUEST_URI);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, getResultBeforeRegister.StatusCode);
            Assert.AreEqual(HttpStatusCode.Created, registerSupplierResult);
            Assert.AreEqual(HttpStatusCode.OK, getResultAfterRegister.StatusCode);
            Assert.IsTrue(getResultAfterRegister.Body!.TotalCount == (getResultBeforeRegister.Body!.TotalCount + 1));

            var registeredSupplier = getResultAfterRegister.Body.Results!.FirstOrDefault(c => c.Document == newSupplier.Document);
            Assert.IsNotNull(registeredSupplier);

            //Arrange
            var updateSupplier = new
            {
                Id = registeredSupplier.SupplierId,
                Name = "Test Supplier",
                Email = "test.supplier@gmail.com",
                Phone = "31912365544",
                Document = "66666666666"
            };

            //Act
            var updateSupplierResult = await MakePutRequest(REQUEST_URI, updateSupplier);
            var getResultAfterUpdate = await MakeGetRequest<GetSupplierDto>($"{REQUEST_URI}/{registeredSupplier.SupplierId}");

            //Assert
            Assert.IsNotNull(getResultAfterUpdate);
            Assert.AreEqual(HttpStatusCode.OK, getResultAfterUpdate.StatusCode);
            Assert.AreEqual(getResultAfterUpdate.Body!.Document, updateSupplier.Document);

            //Act
            var deletSupplierResult = await MakeDeleteRequest($"{REQUEST_URI}/{registeredSupplier.SupplierId}");
            var getResultAfterDelete = await MakeGetRequest<GetSupplierDto>($"{REQUEST_URI}/{registeredSupplier.SupplierId}");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, deletSupplierResult);
            Assert.AreEqual(HttpStatusCode.NotFound, getResultAfterDelete.StatusCode);
        }
    }
}
