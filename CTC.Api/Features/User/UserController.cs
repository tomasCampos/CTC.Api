using CTC.Api.Features.User.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.User.UseCases.GetUser.UseCase.IO;
using CTC.Application.Features.User.UseCases.RegisterUser.UseCase.IO;
using CTC.Application.Shared.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Features.User
{
    [ApiController]
    [Route("[controller]")]
    public sealed class UserController : BaseController
    {
        private readonly IUseCase<RegisterUserInput, RegisterUserOutput> _registerUserUseCase;
        private readonly IUseCase<IGetUserInput, GetUserOutput> _getUserUseCase;

        public UserController(IUseCase<RegisterUserInput, RegisterUserOutput> registerUserUseCase, IUseCase<IGetUserInput, GetUserOutput> getUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _getUserUseCase = getUserUseCase;
        }

        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var input = new RegisterUserInput
            (
                request.UserFirstName,
                request.UserEmail,
                request.UserPhone,
                request.UserDocument,
                request.UserLastName,
                request.UserPermission,
                request.UserPassword,
                GetRequestUserPermissiomFromClaims()
            );

            var output = await _registerUserUseCase.Execute(input);
            return GetHttpResponse(output, "/user");
        }

        [Authorize]
        [HttpGet("{userEmail}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetUser([FromRoute] string userEmail)
        {
            var input = new GetUserByEmailInput(userEmail, GetRequestUserPermissiomFromClaims());
            var output = await _getUserUseCase.Execute(input);
            return GetHttpResponse(output);
        }
    }
}
