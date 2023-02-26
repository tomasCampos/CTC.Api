using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase.IO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Shared
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult GetHttpResponse(in Output output, in string? uri = null)
        {
            var httpResponse = new { output.StatusCode, output.ValidationErrorMessage, output.Body};

            if(output.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(httpResponse);
            if(output.StatusCode == HttpStatusCode.Conflict)
                return Conflict(httpResponse);
            if (output.StatusCode == HttpStatusCode.Unauthorized)
                return Unauthorized();
            if (output.StatusCode == HttpStatusCode.Forbidden)
                return Forbid();
            if (output.StatusCode == HttpStatusCode.NotFound)
                return NotFound();

            if (output.StatusCode == HttpStatusCode.OK)
                return Ok(httpResponse);
            if(output.StatusCode == HttpStatusCode.Created)
                return Created(uri!, httpResponse);

            if (output.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500);

            return NoContent();
        }

        protected UserPermission GetRequestUserPermissiomFromClaims()
        {
            return (UserPermission)Convert.ToInt32(this.User.Claims.ToArray()[2].Value);
        }
    }
}
