using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FluentResults;
using MovieTicketBooking.Application.Common.Errors;
namespace MovieTicketBooking.Api.Controllers
{
    public class ApiController : ControllerBase
    {
        protected ActionResult Problem(List<IError> errors)
        {
            var firstError = errors.First();

            switch (firstError)
            {
                case NotFoundError:
                    return Problem(statusCode: StatusCodes.Status404NotFound,
                        detail: firstError.Message);
                case BadRequestError:
                    return Problem(statusCode: StatusCodes.Status400BadRequest,
                        detail: firstError.Message);
                case DuplicateError:
                    return Problem(statusCode: StatusCodes.Status409Conflict,
                        detail: firstError.Message);
                case UnauthorizedError:
                    return Problem(statusCode: StatusCodes.Status401Unauthorized,
                        detail: firstError.Message);
                case ValidationError:
                    ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

                    foreach (ValidationError error in errors.First().Reasons)
                    {
                        modelStateDictionary.AddModelError(error.PropertyName, error.Message);
                    }

                    return ValidationProblem(modelStateDictionary);
                default:
                    return Problem(statusCode: StatusCodes.Status500InternalServerError,
                        detail: firstError.Message);
            }
        }
    }
}
