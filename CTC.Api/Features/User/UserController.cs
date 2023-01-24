using CTC.Api.Features.User.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.User.UseCases.RegisterUser.UseCase.IO;
using CTC.Application.Shared.UseCase;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Features.User
{
    [ApiController]
    [Route("[controller]")]
    public sealed class UserController : BaseController
    {
        private readonly IUseCase<RegisterUserInput, RegisterUserOutput> _registerUserUseCase;

        public UserController(IUseCase<RegisterUserInput, RegisterUserOutput> registerUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
        }

        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var input = new RegisterUserInput 
            {
                UserDocument= request.UserDocument,
                UserEmail= request.UserEmail,
                UserFirstName= request.UserFirstName,
                UserLastName= request.UserLastName,
                UserPassword= request.UserPassword,
                UserPermission= request.UserPermission,
                UserPhone= request.UserPhone
            };

            var output = await _registerUserUseCase.Execute(input);
            
            return GetHttpResponse(output, "/user");
        }
    }
}
