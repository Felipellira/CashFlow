namespace CashFlow.Exception.ExceptionsBase;

public class NotFoundException(string message) : CashFlowException(message);