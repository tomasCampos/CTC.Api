﻿using CTC.Api.Features.User.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.User.UseCases.AuthorizeUser.UseCase;
using CTC.Application.Features.User.UseCases.GetUser.UseCase.IO;
using CTC.Application.Features.User.UseCases.RegisterUser.UseCase;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Features.User
{
    [ApiController]
    [Route("[controller]")]
    public sealed class UserController : BaseController
    {
        private readonly IUseCase<RegisterUserInput, Output> _registerUserUseCase;
        private readonly IUseCase<IGetUserInput, Output> _getUserUseCase;
        private readonly IUseCase<AuthorizeUserInput, Output> _authorizeUserUseCase;

        public UserController(IUseCase<RegisterUserInput, Output> registerUserUseCase, IUseCase<IGetUserInput, Output> getUserUseCase, IUseCase<AuthorizeUserInput, Output> authorizeUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _getUserUseCase = getUserUseCase;
            _authorizeUserUseCase = authorizeUserUseCase;
        }

        [Authorize]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
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
        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListUsers([FromQuery] QueryRequest request)
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("{userEmail}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetUser([FromRoute] string userEmail)
        {
            var input = new GetUserByEmailInput(userEmail, GetRequestUserPermissiomFromClaims());
            var output = await _getUserUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [HttpPost("Authorize")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AuthorizeUser([FromBody] AuthorizeUserRequest request)
        {
            var input = new AuthorizeUserInput(request.UserEmail, request.UserPassword);
            var output = await _authorizeUserUseCase.Execute(input);
            return GetHttpResponse(output);
        }
    }
}
