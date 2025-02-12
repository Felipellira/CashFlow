namespace CashFlow.Communication.Responses;

public class ResponseErrorJson(List<string> errorMessages)
{
    public List<string> ErrorMessages { get; set; } = errorMessages;

    public ResponseErrorJson(string errorMessage) : this([errorMessage])
    {
    }
}