using Shopping.Api.Contracts.Models.Enums;

namespace Shopping.Api.Contracts.Domain;

public class OperationResult
{
    public OperationResult() { }


    public OperationResult(string code, string message)
    {
        Code = code;
        Message = message;
        Severity = GetSeverity(code);
    }


    /// <summary>
    /// HttpStatus response code or system error code.
    /// </summary>
    public string Code { get; set; }


    /// <summary>
    /// Generic response status message
    /// </summary>
    public string Message { get; set; }


    /// <summary>
    /// Severity level of the response status
    /// </summary>
    public Severity Severity { get; set; }


    private Severity GetSeverity(string code)
    {
        switch (code)
        {
            case "200":
            case "201":
            case "202":
            case "204":
                return Severity.Information;
            case "207":
                return Severity.Warning;
            case "400":
            case "401":
            case "403":
            case "404":
            case "412":
            case "422":
                return Severity.Error;
            case "500":
                return Severity.Critical;
            default:
                return Severity.Critical;
        }
    }
}
