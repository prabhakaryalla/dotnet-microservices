using System.Net;

namespace Shopping.Api.Contracts.Domain;

public class HttpStatus
{
    public HttpStatus(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }


    public HttpStatus(HttpStatusCode statusCode, string? description)
    {
        StatusCode = statusCode;
        Description = description;
    }


    public HttpStatus() { }


    public HttpStatusCode StatusCode { get; set; }
    public string? Description { get; set; }
    public bool IsSuccessful => (int)StatusCode > 199 && (int)StatusCode < 300;
}


public class Status : HttpStatus
{


}