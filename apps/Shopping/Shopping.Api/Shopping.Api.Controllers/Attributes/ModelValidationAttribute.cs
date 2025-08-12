using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Contracts.Domain;
using System.Net;
using Shopping.Api.Contracts.Models.Enums;

namespace Shopping.Api.Controllers.Attributes;

public class ModelValidationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new StatusResult(context);
        }
    }


    public class StatusResult : ObjectResult
    {
        public StatusResult(ActionExecutingContext context)
            : base(new ValidationResultModel(context, HttpStatusCode.BadRequest))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }


    public class ValidationResultModel
    {
        public OperationResult[] OperationResults { get; set; }


        public ValidationResultModel(ActionExecutingContext context, HttpStatusCode errorStatusCode)
        {
            OperationResult operationResult = new OperationResult
            {
                Code = ((int)errorStatusCode).ToString(),
                Message = "Validation Failed. ",
                Severity = Severity.Error
            };


            ModelStateDictionary modelState = context.ModelState;


            IEnumerable<string> errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => GetErrorMessage(key, x.ErrorMessage)));


            var invalidTypes = errors.Where(x => x.Contains("Invalid Data Type"));
            if (invalidTypes.Any()) errors = invalidTypes;


            foreach (string error in errors) operationResult.Message += error;
            operationResult.Message = operationResult.Message.TrimEnd().TrimEnd(',');


            OperationResults = new[] { operationResult };
        }


        private string GetErrorMessage(string key, string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return string.Empty;


            string[] exceptions = { "Path", "line", "position" };
            if (key.StartsWith("$.")) key = key.Substring(2);
            if (ContainsAll(message, exceptions)) return $"{key}: Invalid Data Type, ";


            if (message.Contains("Error converting value"))
                return $"{key}: Invalid Data Type, ";


            //return !string.IsNullOrEmpty(key) ? $"{key}: {message}, " : $"{message}, ";
            return !string.IsNullOrEmpty(key) ? $"{key}: {message}, " : $"{message}, ";
            // return  $"{message}, ";
        }


        public bool ContainsAll(string source, IEnumerable<string> values, StringComparison comp = StringComparison.InvariantCultureIgnoreCase)
        {
            return values.All(value => source.Contains(value, comp));
        }
    }
}

