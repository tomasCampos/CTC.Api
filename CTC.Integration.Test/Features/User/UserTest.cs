using CTC.Integration.Test.Features.User.Dtos;
using CTC.Integration.Test.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace CTC.Integration.Test.Features.User
{
    [TestClass]
    public class UserTest : IntegrationTestBase
    {
        private const string REQUEST_URI = "/User";

        [TestMethod]
        public async Task ShouldRegisterGetUpdateAndDeleteUser()
        {
            //Arrange
            var newUser = new
            {
                FirstName = "Test",
                Email = "test.user@gmail.com",
                Phone = "31912365544",
                Document = "11100092847",
                LastName = "User",
                Permission = 3,
                Password = "testUser1234",
            };

            //Act
            var getResultBeforeRegister = await MakeGetRequest<ListUserDto>(REQUEST_URI);
            var registerUserResult = await MakePostRequest(REQUEST_URI, newUser);
            var getResultAfterRegister = await MakeGetRequest<ListUserDto>(REQUEST_URI);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, getResultBeforeRegister.StatusCode);
            Assert.AreEqual(HttpStatusCode.Created, registerUserResult);
            Assert.AreEqual(HttpStatusCode.OK, getResultAfterRegister.StatusCode);
            Assert.IsTrue(getResultAfterRegister.Body!.TotalCount == (getResultBeforeRegister.Body!.TotalCount + 1));

            var registeredUser = getResultAfterRegister.Body.Results!.FirstOrDefault(c => c.Email == newUser.Email);
            Assert.IsNotNull(registeredUser);

            //Arrange
            var updateUser = new
            {
                Id = registeredUser.UserId,
                FirstName = "Test",
                Email = "test.user@gmail.com",
                Phone = "31912365544",
                Document = "00099900099",
                LastName = "User",
                Permission = 3,
                Password = "testUser1234",
            };

            //Act
            var updateUserResult = await MakePutRequest(REQUEST_URI, updateUser);
            var getResultAfterUpdate = await MakeGetRequest<GetUserDto>($"{REQUEST_URI}/{registeredUser.Email}");

            //Assert
            Assert.IsNotNull(getResultAfterUpdate);
            Assert.AreEqual(HttpStatusCode.OK, getResultAfterUpdate.StatusCode);
            Assert.AreEqual(getResultAfterUpdate.Body!.Document, updateUser.Document);

            //Act
            var deletUserResult = await MakeDeleteRequest($"{REQUEST_URI}/{registeredUser.UserId}");
            var getResultAfterDelete = await MakeGetRequest<GetUserDto>($"{REQUEST_URI}/{registeredUser.Email}");

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, deletUserResult);
            Assert.AreEqual(HttpStatusCode.NotFound, getResultAfterDelete.StatusCode);
        }
    }
}
