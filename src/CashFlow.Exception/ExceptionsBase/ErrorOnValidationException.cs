namespace CashFlow.Exception.ExceptionsBase;

public class ErrorOnValidationException(List<string> errors) : CashFlowException
{
    public List<string> Errors { get; set; } = errors;
}