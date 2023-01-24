using CTC.Application.Shared.UseCase.IO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CTC.Api.Shared
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult GetHttpResponse(in IOutput output, in string? uri = null)
        {
            var httpResponse = new { output.StatusCode, output.ValidationErrorMessage, output.Body};

            if(output.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(httpResponse);
            if(output.StatusCode == HttpStatusCode.Conflict)
                return Conflict(httpResponse);
            if (output.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500);

            if (output.StatusCode == HttpStatusCode.Unauthorized)
                return Unauthorized();

            if(output.StatusCode == HttpStatusCode.OK)
                return Ok(httpResponse);
            if(output.StatusCode == HttpStatusCode.Created)
                return Created(uri!, httpResponse);

            return NoContent();
        }
    }
}
