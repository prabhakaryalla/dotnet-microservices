using System.Net;
using System.Text.Json;

namespace Shopping.Api.Contracts.Domain;

public class StatusResponse
{
    public StatusResponse(HttpStatusCode code, string message)
    {
        OperationResults = new[]
        {
            new OperationResult(((int)code).ToString(), message)
        };
    }

    public StatusResponse() { }


    /// <summary>
    /// Collection of Operation result model
    /// </summary>
    public OperationResult[] OperationResults { get; set; } = { };


    /// <summary>
    /// Serialize object to string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}


