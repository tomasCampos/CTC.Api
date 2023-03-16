using System.Collections.Generic;
using System.Linq;

namespace CTC.Application.Shared.Request.Validator
{
    internal sealed class RequestValidationModel
    {
        public List<string> Errors { get; }
        public bool IsValid { get { return !Errors.Any(); } }
        public string ErrorMessage { get { return string.Join(", ", Errors); } }

        public RequestValidationModel(List<string> errors)
        {
            Errors = errors;
        }
    }
}
