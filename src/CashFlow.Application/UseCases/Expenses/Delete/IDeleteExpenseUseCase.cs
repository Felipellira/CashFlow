namespace CashFlow.API.Controllers;

public interface IDeleteExpenseUseCase
{
    public Task Execute(long id);
}