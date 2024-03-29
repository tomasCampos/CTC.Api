﻿using CTC.Api.Controllers.User.Contracts;
using CTC.Api.Shared;
using CTC.Application.Features.User.UseCases.AuthorizeUser.UseCase;
using CTC.Application.Features.User.UseCases.DeleteUser.UseCase;
using CTC.Application.Features.User.UseCases.GetUser.UseCase.IO;
using CTC.Application.Features.User.UseCases.ListUsers.UseCase;
using CTC.Application.Features.User.UseCases.RegisterUser.UseCase;
using CTC.Application.Features.User.UseCases.UpdateUser.UseCase;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Controllers.User
{
    [ApiController]
    [Route("[controller]")]
    public sealed class UserController : BaseController
    {
        private readonly IUseCase<RegisterUserInput, Output> _registerUserUseCase;
        private readonly IUseCase<IGetUserInput, Output> _getUserUseCase;
        private readonly IUseCase<AuthorizeUserInput, Output> _authorizeUserUseCase;
        private readonly IUseCase<ListUsersInput, Output> _listUsersUseCase;
        private readonly IUseCase<UpdateUserInput, Output> _updateUserUseCase;
        private readonly IUseCase<DeleteUserInput, Output> _deleteUserUseCase;

        public UserController(IUseCase<RegisterUserInput, Output> registerUserUseCase,
                            IUseCase<IGetUserInput, Output> getUserUseCase,
                            IUseCase<AuthorizeUserInput, Output> authorizeUserUseCase,
                            IUseCase<ListUsersInput, Output> listUsersUseCase,
                            IUseCase<UpdateUserInput, Output> updateUserUseCase,
                            IUseCase<DeleteUserInput, Output> deleteUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _getUserUseCase = getUserUseCase;
            _authorizeUserUseCase = authorizeUserUseCase;
            _listUsersUseCase = listUsersUseCase;
            _updateUserUseCase = updateUserUseCase;
            _deleteUserUseCase = deleteUserUseCase;
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
            var input = new RegisterUserInput(request.FirstName, request.Email, request.Phone, request.Document, request.LastName, request.Permission, request.Password);
            var output = await _registerUserUseCase.Execute(input);
            return GetHttpResponse(output, "/user");
        }

        [Authorize]
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListUsers([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? queryParam)
        {
            var request = QueryRequest.Create(pageNumber, pageSize, queryParam);
            var input = new ListUsersInput(request);
            var output = await _listUsersUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpGet("{userEmail}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetUser([FromRoute] string userEmail)
        {
            var input = new GetUserByEmailInput(userEmail);
            var output = await _getUserUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpGet("Profile")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetUserProfile()
        {
            var input = new GetUserByBearerTokenInput();
            var output = await _getUserUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var input = new UpdateUserInput
            (
                request.Id,
                request.FirstName,
                request.Email,
                request.Phone,
                request.Document,
                request.LastName,
                request.Permission,
                request.Password
            );

            var output = await _updateUserUseCase.Execute(input);
            return GetHttpResponse(output);
        }

        [Authorize]
        [HttpPut("Profile")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileRequest request)
        {
            var input = new UpdateUserInput
            (
                null,
                request.FirstName,
                request.Email,
                request.Phone,
                request.Document,
                request.LastName,
                null,
                request.Password
            );

            var output = await _updateUserUseCase.Execute(input);
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

        [Authorize]
        [HttpDelete("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteUser([FromRoute] string? userId)
        {
            var input = new DeleteUserInput { UserId = userId };
            var output = await _deleteUserUseCase.Execute(input);
            return GetHttpResponse(output);
        }
    }
}
