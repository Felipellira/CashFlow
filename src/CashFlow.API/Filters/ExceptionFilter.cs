using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CashFlowException)
        {
            HandleProjectException(context);
        }
        else
        {
            Console.WriteLine(context.Exception);
            HandleUnknownException(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = ((CashFlowException)context.Exception).StatusCode;
        context.Result = new ObjectResult(((CashFlowException)context.Exception).GetErrors());
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessage.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}