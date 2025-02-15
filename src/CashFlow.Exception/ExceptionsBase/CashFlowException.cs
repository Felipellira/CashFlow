namespace CashFlow.Exception.ExceptionsBase;

public abstract class CashFlowException(string message) : System.Exception(message)
{
    public List<string> ErrorMessages { get; set; } = [];
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}