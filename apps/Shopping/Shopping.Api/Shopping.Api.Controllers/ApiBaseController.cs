using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Contracts.Domain;
using Shopping.Api.Contracts.Models.Enums;
using System.Net;

namespace Shopping.Api.Controllers;

[ApiController]
public class ApiBaseController : ControllerBase
{
    protected ObjectResult InternalServerError(Exception exception, bool fullException = false)
    {
        if (exception is UnauthorizedAccessException)
            throw new UnauthorizedAccessException(exception.Message);


        string statusDescription = "Internal Server Error";
#if DEBUG
        statusDescription = exception.ToString();
#endif
        if (fullException) statusDescription = FullException(exception);


        return StatusCode(StatusCodes.Status500InternalServerError,
            new StatusResponse(HttpStatusCode.InternalServerError, statusDescription));
    }


    private static string FullException(Exception exception)
    {
        string statusDescription = exception.ToString();


        if (exception.InnerException != null)
        {
            statusDescription += "||" + exception.InnerException;
            if (exception.InnerException.InnerException != null)
            {
                statusDescription += "||" + exception.InnerException.InnerException;
            }
        }


        return statusDescription;
    }


    protected ObjectResult Result(HttpStatus httpStatus)
    {
        switch (httpStatus.StatusCode)
        {
            case HttpStatusCode.NotFound:
                return NotFoundError(httpStatus.Description);
            case HttpStatusCode.BadRequest:
                return BadRequestError(httpStatus.Description);
            case HttpStatusCode.Unauthorized:
                return UnauthorizedRequestError(httpStatus.Description);
            default:
                return StatusResponseError(httpStatus);
        }
    }

    protected ObjectResult BadRequestError(string message = null)
    {
        return StatusCode(StatusCodes.Status400BadRequest, new StatusResponse
        {
            OperationResults = new[]{
                    new OperationResult
                    {
                        Code = StatusCodes.Status400BadRequest.ToString(),
                        Message = string.IsNullOrWhiteSpace(message) ? "Bad Request" : message,
                        Severity = Severity.Error
                    }
                }
        });
    }

    protected ObjectResult NotFoundError(string message = null)
    {
        return StatusCode(StatusCodes.Status404NotFound, new StatusResponse
        {
            OperationResults = new[]{
                    new OperationResult
                    {
                        Code = StatusCodes.Status404NotFound.ToString(),
                        Message = string.IsNullOrWhiteSpace(message) ? "Not Found" : message,
                        Severity = Severity.Error
                    }
                }
        });
    }
    protected ObjectResult UnauthorizedRequestError(string message = null)
    {
        return StatusCode(StatusCodes.Status401Unauthorized, new StatusResponse
        {
            OperationResults = new[]{
                    new OperationResult
                    {
                        Code = StatusCodes.Status401Unauthorized.ToString(),
                        Message = string.IsNullOrWhiteSpace(message) ? "Unauthorized Request" : message,
                        Severity = Severity.Error
                    }
                }
        });
    }


    protected ObjectResult StatusResponseError(HttpStatus httpStatus)
    {
        if (!int.TryParse(((int)httpStatus.StatusCode).ToString(), out int statusCode))
        {
            statusCode = 400;
        }


        return StatusCode(statusCode, new StatusResponse
        {
            OperationResults = new[]{
                    new OperationResult
                    {
                        Code = statusCode.ToString(),
                        Message = string.IsNullOrWhiteSpace(httpStatus.Description) ? "Error" : httpStatus.Description,
                        Severity = Severity.Error
                    }
                }
        });
    }
}





