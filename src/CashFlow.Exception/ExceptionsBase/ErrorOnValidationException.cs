using System.Net;

namespace CashFlow.Exception.ExceptionsBase;

public class ErrorOnValidationException(List<string> errors) : CashFlowException(string.Empty)
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    private List<string> Errors { get; } = errors;

    public override List<string> GetErrors()
    {
        return Errors;
    }
}