namespace Shopping.Api.Contracts.Domain;

public class Response<T>
{
    public Response(HttpStatus status, T data)
    {
        HttpStatus = status;
        Data = data;
    }


    public Response(HttpStatus status)
    {
        HttpStatus = status;
    }


    public Response() { }

    public HttpStatus? HttpStatus { get; set; }
    public T? Data { get; set; }
}
